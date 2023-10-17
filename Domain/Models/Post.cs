namespace Domain.Models;

public class Post
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Content { get; init; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public int UserId { get; }
    public User User { get; }
    public object AuthorId { get; set; }

    public Post(string title, string content, DateTime createdAt, DateTime updatedAt, int userId, User user)
    {
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        UserId = userId;
        User = user;
    }

    public Post()
    {
        throw new NotImplementedException();
    }
}