using MediatR;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.Application.Features.Tasks.Commands.DeleteTask;

public sealed class DeleteTaskCommandHandler(TaskService taskService) : IRequestHandler<DeleteTaskCommand>
{
    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        await taskService.DeleteAsync(request.TaskId, cancellationToken);
        return Unit.Value;
    }
}
