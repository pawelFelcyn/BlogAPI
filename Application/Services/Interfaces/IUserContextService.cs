using System.Security.Claims;

namespace Application.Services;

public interface IUserContextService
{
    public ClaimsPrincipal User { get; }
    public int GetId { get; }
}
