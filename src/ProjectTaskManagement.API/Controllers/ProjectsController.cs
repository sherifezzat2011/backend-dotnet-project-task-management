using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Application.Common;
using ProjectTaskManagement.Application.DTOs.Projects;
using ProjectTaskManagement.Application.DTOs.Tasks;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public sealed class ProjectsController(ProjectService projectService, TaskService taskService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await projectService.GetAllAsync(cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<ProjectResponse>>.Succeeded(result));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await projectService.GetByIdAsync(id, cancellationToken);
        return Ok(ApiResponse<ProjectResponse>.Succeeded(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var result = await projectService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, ApiResponse<ProjectResponse>.Succeeded(result, "Project created successfully."));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        var result = await projectService.UpdateAsync(id, request, cancellationToken);
        return Ok(ApiResponse<ProjectResponse>.Succeeded(result, "Project updated successfully."));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await projectService.DeleteAsync(id, cancellationToken);
        return Ok(ApiResponse<string>.Succeeded("Project deleted successfully."));
    }

    [HttpGet("{projectId:guid}/tasks")]
    public async Task<IActionResult> GetTasksByProject(Guid projectId, CancellationToken cancellationToken)
    {
        var result = await taskService.GetByProjectAsync(projectId, cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<ProjectTaskResponse>>.Succeeded(result));
    }

    [HttpPost("{projectId:guid}/tasks")]
    public async Task<IActionResult> CreateTask(Guid projectId, CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var result = await taskService.CreateAsync(projectId, request, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, ApiResponse<ProjectTaskResponse>.Succeeded(result, "Task created successfully."));
    }
}
