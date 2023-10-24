using Domain.Models;
using Domain.DTOs;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string username);
    public Task<IEnumerable<User>> GetAsync(SearchUserPatternDto searchParameters);
    Task<User?> GetByIdAsync(int dtoOwnerId);
}