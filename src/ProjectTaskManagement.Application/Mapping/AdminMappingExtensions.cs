using ProjectTaskManagement.Application.DTOs.Admin;
using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Application.Mapping;

public static class AdminMappingExtensions
{
    public static UserResponse ToResponse(this AppUser user)
        => new(user.Id, user.FullName, user.Email, user.Role.ToString());
}
