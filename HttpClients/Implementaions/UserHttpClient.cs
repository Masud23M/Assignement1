using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementaions;

public class UserHttpClient: IUserService
{

    public Task<User> Create(UserCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetUsers(string? usernameContains = null)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<User>> IUserService.GetUsers(string? usernameContains)
    {
        return GetUsers(usernameContains);
    }
}