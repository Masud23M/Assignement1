using Application.DaoInterfaces;
using Domain.Models;
using Domain.DTOs;
//using Application.DaoInterfaces;
//using Application.LogicInterfaces;
namespace BlazorApp.Auth;

public class CustomAuthProvider : IAuthProvider
{
    private readonly IUserDao _userDao;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;

    public CustomAuthProvider(IUserDao userDao, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
    {
        _userDao = userDao;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<LoginResult> LoginAsync(LoginDto loginDto)
    {
        var user = await _userDao.GetByUsernameAsync(loginDto.Username);
        if (user == null)
        {
            return new LoginResult
            {
                Successful = false,
                Error = "User not found."
            };
        }

        var passwordHash = _passwordHasher.Hash(loginDto.Password, user.Salt);
        if (passwordHash != user.PasswordHash)
        {
            return new LoginResult
            {
                Successful = false,
                Error = "Incorrect password."
            };
        }

        var token = _tokenGenerator.GenerateToken(user);
        return new LoginResult
        {
            Successful = true,
            Token = token
        };
    }

    public async Task<RegistrationResult> RegisterAsync(RegistrationDto registrationDto)
    {
        var existingUser = await _userDao.GetByUsernameAsync(registrationDto.Username);
        if (existingUser != null)
        {
            return new RegistrationResult
            {
                Successful = false,
                Error = "User with this username already exists."
            };
        }

        var salt = _passwordHasher.GenerateSalt();
        var passwordHash = _passwordHasher.Hash(registrationDto.Password, salt);
        var userToCreate = new User
        {
            Username = registrationDto.Username,
            PasswordHash = passwordHash,
            Salt = salt
        };
        var createdUser = await _userDao.CreateAsync(userToCreate);
        var token = _tokenGenerator.GenerateToken(createdUser);
        return new RegistrationResult
        {
            Successful = true,
            Token = token
        };
    }
}

public interface ITokenGenerator
{
    string GenerateToken(User user);
}

public interface IPasswordHasher
{
    string Hash(string password, string salt);
    string GenerateSalt();
}

public interface IAuthProvider
{
    Task<LoginResult> LoginAsync(LoginDto loginDto);
    Task<RegistrationResult> RegisterAsync(RegistrationDto registrationDto);
}

public class RegistrationDto
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}

public class RegistrationResult
{
    public bool Successful { get; set; }
    public string? Error { get; set; }
    public string? Token { get; set; }
}

public class LoginDto
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}

public class LoginResult
{
    public bool Successful { get; set; }
    public string? Error { get; set; }
    public string? Token { get; set; }
}