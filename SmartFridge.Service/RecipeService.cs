
namespace SmartFridge.Service
{
    using AutoMapper.Execution;
    using global::SmartFridge.Core.Model;
    using global::SmartFridge.Core.Repositories;
    using global::SmartFridge.Core.Service;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Net;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    namespace SmartFridge.Service
    {
        public class RecipeService : IRecipeService
        {
            private readonly IRecipeRepository _recipeRepository;
            private readonly IProductRepository _productRepository;

            private readonly HttpClient _httpClient;
            private readonly TranslationService _translationService;
            private readonly string ApiKey = "";
            private const string BaseUrl = "https://api.spoonacular.com/recipes";


            public RecipeService(IRecipeRepository recipeRepository, HttpClient httpClient, TranslationService translationService, IProductRepository productRepository)
            {
                _recipeRepository = recipeRepository;
                _productRepository = productRepository;
                _translationService = translationService;
                _httpClient = httpClient;
            }
       
            public List<Recipe> GetAll()
            {
                return _recipeRepository.GetAll();
            }
            public async Task<List<Recipe>> GetRecipesByIngredientsAsync(string ingredients, int number = 2)
            {
                if (string.IsNullOrEmpty(ingredients))
                {
                    throw new ArgumentException("Ingredients cannot be null or empty", nameof(ingredients));
                }

                // תרגום רשימת הרכיבים לעברית -> אנגלית
                string translatedIngredients = await _translationService.TranslateTextAsync(ingredients, "en");
                Console.WriteLine($"Translated ingredients: {translatedIngredients}");

                // מחליף רווחים בפסיקים כדי להתאים למבנה שה-API מצפה לו
                translatedIngredients = translatedIngredients.Replace(" ", ",");

                // בונה את כתובת ה-API
                string url = $"{BaseUrl}/findByIngredients?ingredients={translatedIngredients}&number={number}&ranking=2&ignorePantry=true&apiKey={ApiKey}";

                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();
                    var recipes = JsonSerializer.Deserialize<List<Recipe>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (recipes != null)
                    {
                        foreach (var recipe in recipes)
                        {
                            // תרגום שם המתכון
                            recipe.Title = await _translationService.TranslateTextAsync(recipe.Title, "he");

                            // השגת הוראות הבישול ותרגומן
                            var instructions = await GetRecipeInstructionsAsync(recipe.Id);
                            recipe.Instructions = !string.IsNullOrEmpty(instructions)
                                ? await _translationService.TranslateTextAsync(instructions, "he")
                                : "ההוראות לא זמינות.";

                            // תרגום רשימת המצרכים
                            if (recipe.UsedIngredients != null)
                            {
                                for (int i = 0; i < recipe.UsedIngredients.Count; i++)
                                {
                                    // תרגום שם המוצר בלבד
                                    recipe.UsedIngredients[i].Name = await _translationService.TranslateTextAsync(recipe.UsedIngredients[i].Name, "he");
                                }
                            }

                            if (recipe.MissedIngredients != null)
                            {
                                for (int i = 0; i < recipe.MissedIngredients.Count; i++)
                                {
                                    // תרגום שם המוצר בלבד
                                    recipe.MissedIngredients[i].Name = await _translationService.TranslateTextAsync(recipe.MissedIngredients[i].Name, "he");
                                }
                            }
                        }
                    }

                    return recipes ?? new List<Recipe>(); // אם לא התקבלו מתכונים, מחזיר רשימה ריקה
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching recipes: {ex.Message}");
                    return new List<Recipe>();
                }
            }

            public async Task<string> GetRecipeInstructionsAsync(int id)
            {
                try
                {
                    string url = $"{BaseUrl}/{id}/analyzedInstructions?apiKey={ApiKey}";

                    HttpResponseMessage response = await _httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();

                    // שולפים רק את השלבים עצמם כמחרוזת ישירה
                    using var doc = JsonDocument.Parse(json);
                    var steps = doc.RootElement.EnumerateArray()
                        .SelectMany(instruction => instruction.GetProperty("steps").EnumerateArray()
                        .Select(step => step.GetProperty("step").GetString()));

                    return steps.Any() ? string.Join(" ", steps) : "No instructions found.";
                }
                catch (Exception ex)
                {
                    return $"Error retrieving instructions: {ex.Message}";
                }
            }

            public async Task<List<Recipe>> GetRecipesByProduct(string ingredients, int fridgeId)
            {
                var allFridgeProducts = _productRepository.GetProductsByFridgeId(fridgeId);
                var allRecipes = _recipeRepository.GetAll();

                if (string.IsNullOrWhiteSpace(ingredients))
                {
                    return new List<Recipe>();
                }

                var ingredientList = ingredients.Split(',')
                                                  .Select(ing => ing.Trim())
                                                  .ToList();
                var criticalProducts = allFridgeProducts
                                .Where(product => (product.ExpiryDate - DateTime.Now).TotalDays <= 3)
                                .Select(product => product.Name)
                                .ToList();




                var rankedRecipes = allRecipes
                    .Select(recipe =>
                    {
                        var requiredIngredients = recipe.Products.Split(',')
                                                    .Select(p => p.Trim())
                                                    .Where(p => !string.IsNullOrWhiteSpace(p))
                                                    .ToList();

                        var usedIngredients = requiredIngredients
                                .Where(req => ingredientList.Any(ing => ing.Contains(req, StringComparison.OrdinalIgnoreCase) || req.Contains(ing, StringComparison.OrdinalIgnoreCase)))
                                .ToList();

                        var missedIngredients = requiredIngredients
                                                .Where(req => !ingredientList.Any(ing => ing.Contains(req, StringComparison.OrdinalIgnoreCase) || req.Contains(ing, StringComparison.OrdinalIgnoreCase)))
                                                .ToList();

                        int criticalCount = usedIngredients.Count(req => criticalProducts.Any(cp => cp.Contains(req, StringComparison.OrdinalIgnoreCase) || req.Contains(cp, StringComparison.OrdinalIgnoreCase)));
                        int baseScore = usedIngredients.Count;


                        recipe.UsedIngredientCount = baseScore;
                        recipe.MissedIngredientCount = missedIngredients.Count;
                        recipe.UsedPro = string.Join(", ", usedIngredients);
                        recipe.MissedPro = string.Join(", ", missedIngredients);

                        recipe.Score = baseScore + (criticalCount * 2);

                        return new
                        {
                            Recipe = recipe,
                            Scores = recipe.Score
                        };
                    })
                    .Where(x => x.Scores > 0)
                    .OrderByDescending(x => x.Scores)
                    .Take(3)
                    .Select(x => x.Recipe)
                    .ToList();

                return rankedRecipes;
            }

            public void AddRecipe(Recipe model)
            {
                var newRecipe = new Recipe
                {
                    Title = model.Title,
                    Products = model.Products,
                    Instructions = model.Instructions
                };

                _recipeRepository.AddRecipe(newRecipe);
            }

           
        }


    }
}




