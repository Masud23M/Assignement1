namespace Domain.DTOs;

public class SearchUserPatternDto
{
    public string? Username { get; }
    public string? Email { get; }
    public string? Name { get; }
    public string? Surname { get; }

    public SearchUserPatternDto(string? username, string? email, string? name, string? surname)
    {
        Username = username;
        Email = email;
        Name = name;
        Surname = surname;
    }
}