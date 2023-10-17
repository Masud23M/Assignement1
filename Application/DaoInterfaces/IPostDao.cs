using Domain.Models;

namespace Application.DaoInterfaces;

public interface IPostDao
{
    Task<List<Post>> GetPostsAsync();
    Task<Post> GetPostAsync(int id);
    Task<Post> AddPostAsync(Post post);
    Task<Post> UpdatePostAsync(Post post);
    Task<Post> DeletePostAsync(int id);
}