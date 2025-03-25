using Google.Cloud.Translation.V2;
using System;
using System.Threading.Tasks;

public class TranslationService
{
    private readonly TranslationClient _translationClient;
    public TranslationService()
    {

        string credentialsPath = "";
        Console.WriteLine($"🔍 Trying to set Google credentials from: {credentialsPath}");

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);

        try
        {
            _translationClient = TranslationClient.Create();
            Console.WriteLine("✅ TranslationClient created successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error creating TranslationClient: {ex.Message}");
        }
    }
    public async Task<string> TranslateTextAsync(string text, string targetLanguage = "he")
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        var response = await _translationClient.TranslateTextAsync(text, targetLanguage);
        return response.TranslatedText;
    }
}
