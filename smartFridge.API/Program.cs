using SmartFridge.Core.Repositories;
using SmartFridge.Data.Repositories;
using SmartFridge.Service;
using SmartFridge.Core.Service;
using SmartFridge.Data;
using SmartFridge.Service.SmartFridge.Service;
using Microsoft.EntityFrameworkCore;
using SmartFridge.Core.Mapping;
using SmartFridge.API.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;





var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();



// Categories
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Fridges
builder.Services.AddScoped<IFridgeService, FridgeService>();
builder.Services.AddScoped<IFridgeRepository, FridgeRepository>();

// Products
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Recipes
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<RecipeService>();


builder.Services.AddSingleton<TranslationService>();

builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(@"Server=PROBERS-COMPUTE\SQLEXPRESS;Database=SmartFridge;Trusted_Connection=True;TrustServerCertificate=True"));

//options.UseSqlServer(@"Server=DESKTOP-NVEOGKP\MSSQLSERVER01;Database=SmartFridge;Trusted_Connection=True;TrustServerCertificate=True"));
//options.UseSqlServer(@"Server=R01;Database=SmartFridge;Trusted_Connection=True;TrustServerCertificate=True"));
builder.Services.AddAutoMapper(typeof(MappingProfile),typeof(PostModelsMappingProfile));


var policy = "policy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policy, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "font-src 'self' https://fonts.gstatic.com;");
    await next();
});

app.UseCors(policy);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();