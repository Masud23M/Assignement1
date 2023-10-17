using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task<IEnumerable<Post>> GetAsync(string? titleContains = null);
    Task<Post> GetPostByIdAsync(int id);
    Task<Post> CreatePostAsync(PostCreationDto dto);


    Task GetPostsAsync();
    Task GetPostAsync(int id);
}