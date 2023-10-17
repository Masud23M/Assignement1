using System.Security.Claims;
using System.Text;
using Domain.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WebAPI.Controllers;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly JwtSettings _jwtSettings;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly IPasswordHasher<IdentityUser> _passwordHasher;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEmailSender _emailSender;

    public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration, IOptions<JwtSettings> jwtSettings, TokenValidationParameters tokenValidationParameters, IPasswordHasher<IdentityUser> passwordHasher, RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _configuration = configuration;
        _jwtSettings = jwtSettings.Value;
        _tokenValidationParameters = tokenValidationParameters;
        _passwordHasher = passwordHasher;
        _roleManager = roleManager;
        _emailSender = emailSender;
    }

    public async Task<AuthenticationResult> RegisterAsync(UserCreationDto userCreationDto)
    {
        var existingUser = await _userManager.FindByNameAsync(userCreationDto.UserName);
        if (existingUser != null)
        {
            var async = new AuthenticationResult();
            async.Errors = new[] { "User with this username already exists" };
            return async;
        }

        var newUser = new IdentityUser();
        newUser.UserName = userCreationDto.UserName;
        newUser.Email = userCreationDto.Email;

        var createdUser = await _userManager.CreateAsync(newUser, userCreationDto.Password);
        if (!createdUser.Succeeded)
        {
            var async = new AuthenticationResult();
            async.Errors = createdUser.Errors.Select(x => x.Description);
            return async;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor();
        tokenDescriptor.Subject = new ClaimsIdentity(new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
            new Claim("id", newUser.Id)
        });
        tokenDescriptor.Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime);
        tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var result = new AuthenticationResult();
        result.Success = true;
        result.Token = (string)tokenHandler.WriteToken(token);
        return result;
    }

    public Task<AuthenticationResult> LoginAsync(UserLoginDto userLoginDto)
    {
        throw new NotImplementedException();
    }

    public Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
    {
        throw new NotImplementedException();
    }
}

public class JwtRegisteredClaimNames
{
    public const string Sub = "sub";
    public const string Jti = "jti";
    public const string Email = "email";
}

public class AuthenticationResult
{
    public bool Success { get; set; }
    public string Token { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}

public interface IEmailSender
{
    
}

public class TokenValidationParameters
{
    
}

public class JwtSettings
{
    public string Secret { get; set; }
    public TimeSpan TokenLifetime { get; set; }
}