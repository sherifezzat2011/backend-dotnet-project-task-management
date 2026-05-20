using MediatR;
using ProjectTaskManagement.Application.DTOs.Projects;

namespace ProjectTaskManagement.Application.Features.Projects.Queries.GetProjectById;

public sealed record GetProjectByIdQuery(Guid ProjectId) : IRequest<ProjectResponse>;
