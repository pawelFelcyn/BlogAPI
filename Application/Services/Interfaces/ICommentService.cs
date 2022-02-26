using Application.Dtos;

namespace Application.Services;

public interface ICommentService
{
    IEnumerable<CommentDto> GetAll(int postId);
    CommentDto GetById(int postId, int commentId);
    CommentDto Create(int postId, CreateCommentDto dto);
    CommentDto Update(int postId, int commentId, UpdateCommentDto dto);
    void Delete(int postId, int commentId);
}
