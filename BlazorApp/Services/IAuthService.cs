using Domain.Models;
using Domain.DTOs;
using BlazorApp.Auth;
namespace BlazorApp.Services;

public interface IAuthService
{
    Task<User> LoginAsync(UserLoginDto userToLogin);
    Task<User> RegisterAsync(UserRegisterDto userToRegister);
    Task LogoutAsync();
}

public class UserRegisterDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}