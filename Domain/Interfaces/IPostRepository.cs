using Domain.Entities;

namespace Domain.Interfaces;

public interface IPostRepository
{
    public IEnumerable<Post> GetAll();
    Post GetById(int id);
    Post Add(Post post);
    Post Update(Post post, string contentToUpdate);
    void Remove(Post post);
}
