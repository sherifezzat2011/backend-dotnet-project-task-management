using MediatR;
using ProjectTaskManagement.Application.DTOs.Projects;

namespace ProjectTaskManagement.Application.Features.Projects.Queries.GetProjects;

public sealed record GetProjectsQuery : IRequest<IReadOnlyList<ProjectResponse>>;
