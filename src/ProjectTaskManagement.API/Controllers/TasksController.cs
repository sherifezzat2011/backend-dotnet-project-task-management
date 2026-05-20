using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Application.Common;
using ProjectTaskManagement.Application.DTOs.Tasks;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public sealed class TasksController(TaskService taskService) : ControllerBase
{
    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, UpdateTaskStatusRequest request, CancellationToken cancellationToken)
    {
        var result = await taskService.UpdateStatusAsync(id, request, cancellationToken);
        return Ok(ApiResponse<ProjectTaskResponse>.Succeeded(result, "Task status updated successfully."));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await taskService.DeleteAsync(id, cancellationToken);
        return Ok(ApiResponse<string>.Succeeded("Task deleted successfully."));
    }
}
