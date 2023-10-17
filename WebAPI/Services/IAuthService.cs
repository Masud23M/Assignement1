using Domain.DTOs;

namespace WebAPI.Services;

public interface IAuthService
{
    Task<AuthenticationResult> RegisterAsync(UserCreationDto userCreationDto);
    Task<AuthenticationResult> LoginAsync(UserLoginDto userLoginDto);
    Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
}