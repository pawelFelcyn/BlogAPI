using Application.Dtos;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostService _service;

    public PostsController(IPostService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PostDto>> GetAll()
    {
        var posts = _service.GetAll();

        return Ok(posts);
    }

    [HttpGet("{id}")]
    public ActionResult<PostDetailsDto> GetByid([FromRoute]int id)
    {
        var post = _service.GetById(id);

        return Ok(post);
    }

    [HttpPost]
    [Authorize(Roles ="Admin")]
    public ActionResult<PostDetailsDto> Craete([FromBody]CreatePostDto dto)
    {
        var post = _service.Create(dto);

        return Created($"api/Posts/{post.Id}", post);
    }

    [HttpPut("{id}")]
    public ActionResult<PostDetailsDto> Update([FromRoute]int id, [FromBody]UpdatePostDto dto)
    {
        var post = _service.Update(id, dto);

        return Ok(post);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute]int id)
    {
        _service.Delete(id);

        return NoContent();
    }
}
