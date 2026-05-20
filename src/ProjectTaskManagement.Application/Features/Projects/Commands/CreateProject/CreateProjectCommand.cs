using MediatR;
using ProjectTaskManagement.Application.DTOs.Projects;

namespace ProjectTaskManagement.Application.Features.Projects.Commands.CreateProject;

public sealed record CreateProjectCommand(string Name, string Description) : IRequest<ProjectResponse>;
