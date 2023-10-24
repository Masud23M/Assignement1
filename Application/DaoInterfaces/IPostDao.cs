using Domain.Models;
using Domain.DTOs;

namespace Application.DaoInterfaces;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
    Task<Post> AddPostAsync(Post post);
    Task<Post> UpdatePostAsync(Post post);
    Task<IEnumerable<Post>> GetPostAsync(SearchPostParametersDto searchParameters);
    Task<Post> GetPostsByAuthorAsync(int authorId);
    Task<Post> GetByIdAsync(int id);
    Task<Post> DeletePostAsync(int id);
}