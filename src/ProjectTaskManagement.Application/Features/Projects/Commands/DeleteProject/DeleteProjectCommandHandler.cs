using MediatR;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.Application.Features.Projects.Commands.DeleteProject;

public sealed class DeleteProjectCommandHandler(ProjectService projectService) : IRequestHandler<DeleteProjectCommand>
{
    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        await projectService.DeleteAsync(request.ProjectId, cancellationToken);
        return Unit.Value;
    }
}
