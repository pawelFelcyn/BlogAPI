using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly BlogDbContext _dbContext;

    public CommentRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Comment> GetAll(int postId)
    {
        return _dbContext.Comments.Where(c => c.PostId == postId);
    }

    public Comment GetById(int postId, int commentId)
    {
        var comment = _dbContext
                      .Comments
                      .Include(c => c.Post)
                      .FirstOrDefault(c => c.Id == commentId && c.PostId == postId);

        if (comment == null)
        {
            throw new CommentNotFoundException();
        }

        return comment;
    }

    public Comment Add(Comment comment)
    {
        _dbContext.Comments.Add(comment);
        _dbContext.SaveChanges();

        return comment;
    }

    public Comment Update(Comment comment, string content)
    {
        comment.Content = content;
        _dbContext.SaveChanges();

        return comment;
    }

    public void Remove(Comment comment)
    {
        _dbContext.Comments.Remove(comment);
        _dbContext.SaveChanges();
    }
}
