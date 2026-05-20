using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Application.Common;
using ProjectTaskManagement.Application.DTOs.Tasks;
using ProjectTaskManagement.Application.Features.Tasks.Commands.DeleteTask;
using ProjectTaskManagement.Application.Features.Tasks.Commands.UpdateTaskStatus;

namespace ProjectTaskManagement.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public sealed class TasksController(ISender sender) : ControllerBase
{
    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, UpdateTaskStatusRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new UpdateTaskStatusCommand(id, request.Status), cancellationToken);
        return Ok(ApiResponse<ProjectTaskResponse>.Succeeded(result, "Task status updated successfully."));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteTaskCommand(id), cancellationToken);
        return Ok(ApiResponse<string>.Succeeded("Task deleted successfully."));
    }
}
