using MediatR;
using ProjectTaskManagement.Application.DTOs.Tasks;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.Application.Features.Tasks.Commands.UpdateTaskStatus;

public sealed class UpdateTaskStatusCommandHandler(TaskService taskService) : IRequestHandler<UpdateTaskStatusCommand, ProjectTaskResponse>
{
    public Task<ProjectTaskResponse> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        return taskService.UpdateStatusAsync(request.TaskId, new UpdateTaskStatusRequest(request.Status), cancellationToken);
    }
}
