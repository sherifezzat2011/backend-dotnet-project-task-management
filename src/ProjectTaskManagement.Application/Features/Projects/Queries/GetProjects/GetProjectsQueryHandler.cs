using MediatR;
using ProjectTaskManagement.Application.DTOs.Projects;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.Application.Features.Projects.Queries.GetProjects;

public sealed class GetProjectsQueryHandler(ProjectService projectService) : IRequestHandler<GetProjectsQuery, IReadOnlyList<ProjectResponse>>
{
    public Task<IReadOnlyList<ProjectResponse>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        return projectService.GetAllAsync(cancellationToken);
    }
}
