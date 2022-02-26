using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Application.Authorization;

public class CommentOperationRequirementHandler : AuthorizationHandler<CommentOperationRequirement, Comment>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CommentOperationRequirement requirement, Comment comment)
    {
        throw new NotImplementedException();
    }
}
