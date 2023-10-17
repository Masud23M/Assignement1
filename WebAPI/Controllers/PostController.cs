using Application.Logic;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class PostController: ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync()
    {
        var posts = await _postService.GetAsync();

        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPostByIdAsync(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);

        return Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreatePostAsync(PostCreationDto dto)
    {
        var post = await _postService.CreatePostAsync(dto);

        return Ok(post);
    }
}