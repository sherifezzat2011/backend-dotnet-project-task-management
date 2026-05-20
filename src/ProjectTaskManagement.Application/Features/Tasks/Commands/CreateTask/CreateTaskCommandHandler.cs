using MediatR;
using ProjectTaskManagement.Application.DTOs.Tasks;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.Application.Features.Tasks.Commands.CreateTask;

public sealed class CreateTaskCommandHandler(TaskService taskService) : IRequestHandler<CreateTaskCommand, ProjectTaskResponse>
{
    public Task<ProjectTaskResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var dto = new CreateTaskRequest(request.Title, request.Description, request.DueDate, request.Priority);
        return taskService.CreateAsync(request.ProjectId, dto, cancellationToken);
    }
}
