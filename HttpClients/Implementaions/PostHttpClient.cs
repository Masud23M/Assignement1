using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementaions;

public class PostHttpClient: IPostService
{
    private readonly HttpClient _client;

    public PostHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<Post>> GetAsync(string? titleContains = null)
    {
        var url = "posts";
        if (!string.IsNullOrWhiteSpace(titleContains))
        {
            url += $"?titleContains={titleContains}";
        }

        var response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        var response = await _client.GetAsync($"posts/{id}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<Post> CreatePostAsync(PostCreationDto dto)
    {
        var json = JsonSerializer.Serialize(dto);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("posts", data);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public Task GetPostsAsync()
    {
        throw new NotImplementedException();
    }

    public Task GetPostAsync(int id)
    {
        throw new NotImplementedException();
    }
}