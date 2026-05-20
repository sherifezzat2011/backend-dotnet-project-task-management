using MediatR;
using ProjectTaskManagement.Application.DTOs.Admin;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.Application.Features.Admin.Queries.GetUsers;

public sealed class GetUsersQueryHandler(AdminService adminService) : IRequestHandler<GetUsersQuery, IReadOnlyList<UserResponse>>
{
    public Task<IReadOnlyList<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return adminService.GetUsersAsync(cancellationToken);
    }
}
