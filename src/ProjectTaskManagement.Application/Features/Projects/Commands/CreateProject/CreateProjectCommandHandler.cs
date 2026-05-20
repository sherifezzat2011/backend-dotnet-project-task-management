using MediatR;
using ProjectTaskManagement.Application.DTOs.Projects;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.Application.Features.Projects.Commands.CreateProject;

public sealed class CreateProjectCommandHandler(ProjectService projectService) : IRequestHandler<CreateProjectCommand, ProjectResponse>
{
    public Task<ProjectResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        return projectService.CreateAsync(new CreateProjectRequest(request.Name, request.Description), cancellationToken);
    }
}
