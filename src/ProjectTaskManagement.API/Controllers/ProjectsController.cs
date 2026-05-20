using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Application.Common;
using ProjectTaskManagement.Application.DTOs.Projects;
using ProjectTaskManagement.Application.DTOs.Tasks;
using ProjectTaskManagement.Application.Features.Projects.Commands.CreateProject;
using ProjectTaskManagement.Application.Features.Projects.Commands.DeleteProject;
using ProjectTaskManagement.Application.Features.Projects.Commands.UpdateProject;
using ProjectTaskManagement.Application.Features.Projects.Queries.GetProjectById;
using ProjectTaskManagement.Application.Features.Projects.Queries.GetProjects;
using ProjectTaskManagement.Application.Features.Tasks.Commands.CreateTask;
using ProjectTaskManagement.Application.Features.Tasks.Queries.GetTasksByProject;

namespace ProjectTaskManagement.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public sealed class ProjectsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetProjectsQuery(), cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<ProjectResponse>>.Succeeded(result));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetProjectByIdQuery(id), cancellationToken);
        return Ok(ApiResponse<ProjectResponse>.Succeeded(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateProjectCommand(request.Name, request.Description);
        var result = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, ApiResponse<ProjectResponse>.Succeeded(result, "Project created successfully."));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateProjectCommand(id, request.Name, request.Description);
        var result = await sender.Send(command, cancellationToken);
        return Ok(ApiResponse<ProjectResponse>.Succeeded(result, "Project updated successfully."));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteProjectCommand(id), cancellationToken);
        return Ok(ApiResponse<string>.Succeeded("Project deleted successfully."));
    }

    [HttpGet("{projectId:guid}/tasks")]
    public async Task<IActionResult> GetTasksByProject(Guid projectId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetTasksByProjectQuery(projectId), cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<ProjectTaskResponse>>.Succeeded(result));
    }

    [HttpPost("{projectId:guid}/tasks")]
    public async Task<IActionResult> CreateTask(Guid projectId, CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateTaskCommand(projectId, request.Title, request.Description, request.DueDate, request.Priority);
        var result = await sender.Send(command, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, ApiResponse<ProjectTaskResponse>.Succeeded(result, "Task created successfully."));
    }
}
