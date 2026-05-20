using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ProjectTaskManagement.Application.Abstractions.Auth;
using ProjectTaskManagement.Application.Exceptions;

namespace ProjectTaskManagement.Infrastructure.Authentication;

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid GetUserId()
    {
        var userIdValue = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdValue, out var userId))
        {
            throw new UnauthorizedException("Authenticated user could not be resolved.");
        }

        return userId;
    }
}
