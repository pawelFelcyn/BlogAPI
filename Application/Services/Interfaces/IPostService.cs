using Application.Dtos;

namespace Application.Services.Interfaces;

public interface IPostService
{
    IEnumerable<PostDto> GetAll();
    PostDetailsDto GetById(int id);
    PostDetailsDto Create(CreatePostDto dto);
    PostDetailsDto Update(int id, UpdatePostDto dto);
    void Delete(int id);
}
