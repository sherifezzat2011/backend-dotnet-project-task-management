using MediatR;

namespace ProjectTaskManagement.Application.Features.Projects.Commands.DeleteProject;

public sealed record DeleteProjectCommand(Guid ProjectId) : IRequest;
