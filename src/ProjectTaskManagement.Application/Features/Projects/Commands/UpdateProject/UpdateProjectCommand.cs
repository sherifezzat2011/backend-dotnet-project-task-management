using MediatR;
using ProjectTaskManagement.Application.DTOs.Projects;

namespace ProjectTaskManagement.Application.Features.Projects.Commands.UpdateProject;

public sealed record UpdateProjectCommand(Guid ProjectId, string Name, string Description) : IRequest<ProjectResponse>;
