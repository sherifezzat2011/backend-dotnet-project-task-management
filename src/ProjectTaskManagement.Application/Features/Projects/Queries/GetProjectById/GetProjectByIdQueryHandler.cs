using MediatR;
using ProjectTaskManagement.Application.DTOs.Projects;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.Application.Features.Projects.Queries.GetProjectById;

public sealed class GetProjectByIdQueryHandler(ProjectService projectService) : IRequestHandler<GetProjectByIdQuery, ProjectResponse>
{
    public Task<ProjectResponse> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        return projectService.GetByIdAsync(request.ProjectId, cancellationToken);
    }
}
