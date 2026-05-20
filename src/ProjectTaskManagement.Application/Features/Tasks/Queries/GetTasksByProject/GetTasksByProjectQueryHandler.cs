using MediatR;
using ProjectTaskManagement.Application.DTOs.Tasks;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.Application.Features.Tasks.Queries.GetTasksByProject;

public sealed class GetTasksByProjectQueryHandler(TaskService taskService) : IRequestHandler<GetTasksByProjectQuery, IReadOnlyList<ProjectTaskResponse>>
{
    public Task<IReadOnlyList<ProjectTaskResponse>> Handle(GetTasksByProjectQuery request, CancellationToken cancellationToken)
    {
        return taskService.GetByProjectAsync(request.ProjectId, cancellationToken);
    }
}
