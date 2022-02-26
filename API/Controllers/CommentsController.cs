using Application.Dtos;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/Posts/{postId}/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _service;

    public CommentsController(ICommentService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommentDto>> GetAll([FromRoute] int postId)
    {
        var comments = _service.GetAll(postId);

        return Ok(comments);
    }

    [HttpGet("{commentId}")]
    public ActionResult<CommentDto> GetById([FromRoute]int postId, [FromRoute]int commentId)
    {
        var comment = _service.GetById(postId, commentId);

        return Ok(comment);
    }

    [HttpPost]
    [Authorize]
    public ActionResult<CommentDto> Create([FromRoute]int postId, [FromBody]CreateCommentDto dto)
    {
        var com
    }
}
