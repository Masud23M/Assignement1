namespace Domain.DTOs;

public class PostCreationDto
{
    public string Title { get; }
    public string Content { get; }
    public int AuthorId { get; set; }

    public PostCreationDto(string title, string content)
    {
        Title = title;
        Content = content;
    }
}