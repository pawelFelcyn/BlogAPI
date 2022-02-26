using Domain.Entities;

namespace Domain.Interfaces;

public interface ICommentRepository
{
    IEnumerable<Comment> GetAll(int postId);
    Comment GetById(int postId, int commentId);
    Comment Add(Comment post);
    Comment Update(Comment post, string content);
    void Remove(Comment post);
}
