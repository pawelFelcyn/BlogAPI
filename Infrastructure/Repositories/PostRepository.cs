using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly BlogDbContext _dbContext;

    public PostRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Post> GetAll()
    {
        return _dbContext.Posts;
    }

    public Post GetById(int id)
    {
        var post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);

        if (post == null)
        {
            throw new PostNotFoundException();
        }

        return post;
    }

    public Post Add(Post post)
    {
        _dbContext.Posts.Add(post);
        _dbContext.SaveChanges();

        return post;
    }

    public Post Update(Post post, string contentToUpdate)
    {
        post.Content = contentToUpdate;
        _dbContext.SaveChanges();

        return post;
    }

    public void Remove(Post post)
    {
        _dbContext.Remove(post);
        _dbContext.SaveChanges();
    }
}
