using MediatR;
using ProjectTaskManagement.Application.DTOs.Admin;

namespace ProjectTaskManagement.Application.Features.Admin.Queries.GetUsers;

public sealed record GetUsersQuery : IRequest<IReadOnlyList<UserResponse>>;
