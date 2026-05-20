using MediatR;
using ProjectTaskManagement.Application.DTOs.Admin;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.Application.Features.Admin.Commands.UpdateUserRole;

public sealed class UpdateUserRoleCommandHandler(AdminService adminService) : IRequestHandler<UpdateUserRoleCommand, UserResponse>
{
    public Task<UserResponse> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        return adminService.UpdateUserRoleAsync(request.UserId, new UpdateUserRoleRequest(request.Role), cancellationToken);
    }
}
