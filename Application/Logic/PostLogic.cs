using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic: IPostLogic
{
    private readonly IPostDao postDao;

    public PostLogic(IPostDao postDao)
    {
        this.postDao = postDao;
    }
    
    public async Task<Post> CreateAsync(PostCreationDto postToCreate)
    {
        Post? existing = await postDao.GetPostsByAuthorAsync(postToCreate.AuthorId);
        if (existing != null)
        {
            throw new Exception("Username already taken!");
        }

        ValidateData(postToCreate);
        Post toCreate = new Post
        {
            AuthorId = postToCreate.AuthorId
        };

        Post created = await postDao.CreateAsync(toCreate);
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
    
    public Task<IEnumerable<Post>> GetPostAsync(SearchPostParametersDto searchParameters)
    {
        return postDao.GetPostAsync(searchParameters);
    }
    
    /*
     * public async Task<List<Post>> GetPostsAsync()
     * {
        List<Post> posts = await postDao.GetPostsAsync();

        return posts;
    }
     */
    
    
    public async Task<List<Post>> GetPostsByAuthorAndTitleAsync(int authorId)
    {
        throw new NotImplementedException();
    }
    /*
     * public async Task<List<Post>> GetPostsByAuthorAsync(int authorId)
     * {
        List<Post> posts = await postDao.GetPostsByAuthorAsync(authorId);
        List<Post> postsByAuthor = posts.Where(p => p.AuthorId == (object)authorId).ToList();

        return postsByAuthor;
    }
     */
    
   /*
    *  public async Task<List<Post>> GetPostsByTitleAsync(string title)
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
    */
   
    
    public Task<List<Post>> GetPostsByAuthorAndTitleAsync(int authorId, string title)
    {
        throw new NotImplementedException();
    }
    
    private static void ValidateData(PostCreationDto postToCreate)
    {
        string title = postToCreate.Title;
        string content = postToCreate.Content;

        if (title.Length is < 3 or > 15)
        {
            throw new Exception("Title must be between 3 and 15 characters");
        }

        if (content.Length is < 3 or > 15)
        {
            throw new Exception("Content must be between 3 and 15 characters");
        }
    }
}

public class PostUpdateDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int AuthorId { get; set; }
}

