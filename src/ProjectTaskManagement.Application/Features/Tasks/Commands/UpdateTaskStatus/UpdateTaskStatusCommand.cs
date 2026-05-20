using MediatR;
using ProjectTaskManagement.Application.DTOs.Tasks;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.Features.Tasks.Commands.UpdateTaskStatus;

public sealed record UpdateTaskStatusCommand(Guid TaskId, ProjectTaskStatus Status) : IRequest<ProjectTaskResponse>;
