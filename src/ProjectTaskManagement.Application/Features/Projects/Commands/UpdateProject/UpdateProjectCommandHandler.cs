using MediatR;
using ProjectTaskManagement.Application.DTOs.Projects;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.Application.Features.Projects.Commands.UpdateProject;

public sealed class UpdateProjectCommandHandler(ProjectService projectService) : IRequestHandler<UpdateProjectCommand, ProjectResponse>
{
    public Task<ProjectResponse> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        return projectService.UpdateAsync(request.ProjectId, new UpdateProjectRequest(request.Name, request.Description), cancellationToken);
    }
}
