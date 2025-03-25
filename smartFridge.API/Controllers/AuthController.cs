using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartFridge.API.Models;
using SmartFridge.Core.Model;
using SmartFridge.Core.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IFridgeRepository _fridgeRepository;

    public AuthController(IConfiguration configuration, IFridgeRepository fridgeRepository)
    {
        _configuration = configuration;
        _fridgeRepository = fridgeRepository;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] FridgePostModel loginModel)
    {
        try
        {
            // 1️⃣ בדיקה אם הנתונים חסרים
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Name) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest("❌ נתונים חסרים. יש להזין שם משתמש וסיסמה.");
            }

            // 2️⃣ חיפוש המשתמש במערכת
            var fridge = _fridgeRepository.GetFridgeByName(loginModel.Name);
            if (fridge == null)
            {
                return Unauthorized("⚠️ שם המשתמש לא קיים במערכת.");
            }

            // 3️⃣ בדיקת סיסמה
            if (fridge.Password != HashPassword(loginModel.Password))
            {
                return Unauthorized("🔑 סיסמה שגויה.");
            }
            // 4️⃣ קביעת התפקיד - נשלוף את ה-Role מהמשתמש עצמו
            string userRole = fridge.Role ?? "User"; // אם אין תפקיד שמור, ברירת מחדל היא "User"

            // 5️⃣ יצירת תביעות (`Claims`) לפי תפקיד
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, fridge.Name),
            new Claim(ClaimTypes.Role, userRole), // התפקיד מגיע מהדאטהבייס
            new Claim("fridgeId", fridge.Id.ToString())
        };

            // 6️⃣ יצירת טוקן JWT
            var key = _configuration.GetValue<string>("JWT:Key");
            if (string.IsNullOrEmpty(key))
            {
                return StatusCode(500, "❌ בעיה בשרת - מפתח JWT לא מוגדר.");
            }
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("JWT:Issuer"),
                audience: _configuration.GetValue<string>("JWT:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60), // תוקף ארוך יותר
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            // 7️⃣ החזרת הטוקן ללקוח יחד עם תפקיד המשתמש
            return Ok(new { Token = tokenString, Role = userRole, fridgeId = fridge.Id });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"❌ שגיאה בשרת: {ex.Message}");
        }
    }


    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

}
