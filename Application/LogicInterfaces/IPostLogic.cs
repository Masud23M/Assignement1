using Application.Logic;
using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto dto);
    Task<Post> UpdateAsync(PostUpdateDto post);
    Task<Post> DeleteAsync(int id);
    Task<IEnumerable<Post>> GetPostAsync(SearchPostParametersDto searchParameters);
}