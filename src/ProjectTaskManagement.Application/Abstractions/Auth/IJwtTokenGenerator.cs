using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Application.Abstractions.Auth;

public interface IJwtTokenGenerator
{
    string GenerateToken(AppUser user);
}
