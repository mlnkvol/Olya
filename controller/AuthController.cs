using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Olya.dto;
using Olya.model;

namespace Olya.controller;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;
    
    public AuthController(UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest model)
    {
        var user = new User { UserName = model.Email,
            Name = model.Name,
            Surname = model.Surname,
            Email = model.Email };

        var isUserExist = await _userManager.FindByEmailAsync(model.Email);

        if (isUserExist != null)
        {
            return Conflict("User with this email is exist");
        }
        
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            return Ok(new { message = "Registration successful" });
        }
        return BadRequest(new { message = "Registration failed", errors = result.Errors });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, 
            model.Password,
            false, 
            false);
        
        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var tokenString = GenerateJwtToken(user);
            return Ok(new { token = tokenString });
        }
        return Unauthorized();
    }
    
    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secret = _configuration["Jwt:Secret"] ?? String.Empty;
        var key = Encoding.ASCII.GetBytes(secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Email, user.Email ?? String.Empty)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}