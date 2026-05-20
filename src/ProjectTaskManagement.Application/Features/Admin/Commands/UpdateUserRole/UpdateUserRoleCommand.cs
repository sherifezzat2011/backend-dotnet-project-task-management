using MediatR;
using ProjectTaskManagement.Application.DTOs.Admin;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.Features.Admin.Commands.UpdateUserRole;

public sealed record UpdateUserRoleCommand(Guid UserId, UserRole Role) : IRequest<UserResponse>;
