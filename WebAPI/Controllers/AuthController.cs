using System.Security.Claims;
using System.Text;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class AuthController: ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserCreationDto userCreationDto)
    {
        var user = new User
        {
            UserName = userCreationDto.UserName
        };

        var result = await _userManager.CreateAsync(user, userCreationDto.Password);

        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        var user = await _userManager.FindByNameAsync(userLoginDto.UserName);

        var actionResult = BadRequest("Invalid username or password");
        if (user == null)
        {
            return actionResult;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);

        if (result.Succeeded)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }

        return actionResult;
    }
}

public class JwtSecurityTokenHandler
{
    public JwtSecurityTokenHandler()
    {
    }

    internal object WriteToken(object token)
    {
        throw new NotImplementedException();
    }

    internal object CreateToken(SecurityTokenDescriptor tokenDescriptor)
    {
        throw new NotImplementedException();
    }
}

public class SecurityTokenDescriptor
{
    public SecurityTokenDescriptor()
    {
    }

    public object Subject { get; internal set; }
    public DateTime Expires { get; internal set; }
    public SigningCredentials SigningCredentials { get; internal set; }
}

public class SecurityAlgorithms
{
    public static object HmacSha512Signature { get; internal set; }
    public static object HmacSha256Signature { get; set; }
}

public class SigningCredentials
{
    public SigningCredentials(SymmetricSecurityKey key, object hmacSha512Signature)
    {
        throw new NotImplementedException();
    }
}

public class SymmetricSecurityKey
{
    public SymmetricSecurityKey(byte[] getBytes)
    {
        throw new NotImplementedException();
    }
}