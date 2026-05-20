using MediatR;

namespace ProjectTaskManagement.Application.Features.Tasks.Commands.DeleteTask;

public sealed record DeleteTaskCommand(Guid TaskId) : IRequest;
