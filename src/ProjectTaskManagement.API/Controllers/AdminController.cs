using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Application.Common;
using ProjectTaskManagement.Application.DTOs.Admin;
using ProjectTaskManagement.Application.Features.Admin.Commands.UpdateUserRole;
using ProjectTaskManagement.Application.Features.Admin.Queries.GetUsers;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.API.Controllers;

[ApiController]
[Authorize(Roles = nameof(UserRole.Admin))]
[Route("api/[controller]")]
public sealed class AdminController(ISender sender) : ControllerBase
{
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetUsersQuery(), cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<UserResponse>>.Succeeded(result));
    }

    [HttpPatch("users/{id:guid}/role")]
    public async Task<IActionResult> UpdateUserRole(Guid id, UpdateUserRoleRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new UpdateUserRoleCommand(id, request.Role), cancellationToken);
        return Ok(ApiResponse<UserResponse>.Succeeded(result, "User role updated successfully."));
    }
}
