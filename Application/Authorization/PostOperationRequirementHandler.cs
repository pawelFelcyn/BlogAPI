using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Application.Authorization;

public class PostOperationRequirementHandler : AuthorizationHandler<PostOperationRequirement, Post>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PostOperationRequirement requirement, Post post)
    {
        if (context.User == null)
        {
            return Task.CompletedTask;
        }

        var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

        if (userId == post.CreatedById)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
