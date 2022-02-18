using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public ClaimsPrincipal User => _httpContextAccessor.HttpContext.User;

    public int GetId => int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
}
