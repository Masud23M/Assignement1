using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic
{
    private readonly IPostDao postDao;

    public PostLogic(IPostDao postDao)
    {
        this.postDao = postDao;
    }
    
    public async Task<Post> CreateAsync(PostCreationDto postToCreate)
    {
        Post toCreate = new Post()
        {
            Title = postToCreate.Title,
            Content = postToCreate.Content,
            AuthorId = postToCreate.AuthorId
        };

        Post created = await postDao.AddPostAsync(toCreate);

        return created;
    }
    
    public async Task<Post> UpdateAsync(PostUpdateDto postToUpdate)
    {
        Post toUpdate = new Post()
        {
            Id = postToUpdate.Id,
            Title = postToUpdate.Title,
            Content = postToUpdate.Content,
            AuthorId = postToUpdate.AuthorId
        };

        Post updated = await postDao.UpdatePostAsync(toUpdate);

        return updated;
    }
    
    public async Task<Post> DeleteAsync(int id)
    {
        Post deleted = await postDao.DeletePostAsync(id);

        return deleted;
    }
    
    public async Task<Post> GetAsync(int id)
    {
        Post? post = await postDao.GetPostAsync(id);
        if (post == null)
        {
            throw new Exception("Post not found!");
        }

        return post;
    }
    
    public async Task<List<Post>> GetPostsAsync()
    {
        List<Post> posts = await postDao.GetPostsAsync();

        return posts;
    }
    
    public async Task<List<Post>> GetPostsByAuthorAsync(int authorId)
    {
        List<Post> posts = await postDao.GetPostsAsync();
        List<Post> postsByAuthor = posts.Where(p => p.AuthorId == (object)authorId).ToList();

        return postsByAuthor;
    }
    
    public async Task<List<Post>> GetPostsByTitleAsync(string title)
    {
        List<Post> posts = await postDao.GetPostsAsync();
        List<Post> postsByTitle = posts.Where(p => p.Title.Contains(title)).ToList();

        return postsByTitle;
    }
    
    public async Task<List<Post>> GetPostsByContentAsync(string content)
    {
        List<Post> posts = await postDao.GetPostsAsync();
        List<Post> postsByContent = posts.Where(p => p.Content.Contains(content)).ToList();

        return postsByContent;
    }
    
    public Task<List<Post>> GetPostsByAuthorAndTitleAsync(int authorId, string title)
    {
        throw new NotImplementedException();
    }
}

public class PostUpdateDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int AuthorId { get; set; }
}