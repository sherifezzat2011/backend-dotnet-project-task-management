using ProjectTaskManagement.Application.Abstractions.Auth;
using ProjectTaskManagement.Application.Abstractions.Persistence;
using ProjectTaskManagement.Application.DTOs.Tasks;
using ProjectTaskManagement.Application.Exceptions;
using ProjectTaskManagement.Application.Mapping;
using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Application.Services;

public sealed class TaskService(
    IProjectRepository projectRepository,
    ITaskRepository taskRepository,
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork)
{
    public async Task<ProjectTaskResponse> CreateAsync(Guid projectId, CreateTaskRequest request, CancellationToken cancellationToken = default)
    {
        RequestValidator.ValidateTaskRequest(request.Title, request.Description, request.DueDate, request.Priority);
        await EnsureProjectOwnershipAsync(projectId, cancellationToken);

        var task = new ProjectTask
        {
            Title = request.Title.Trim(),
            Description = request.Description.Trim(),
            DueDate = request.DueDate,
            Priority = request.Priority,
            ProjectId = projectId
        };

        await taskRepository.AddAsync(task, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return task.ToResponse();
    }

    public async Task<IReadOnlyList<ProjectTaskResponse>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        await EnsureProjectOwnershipAsync(projectId, cancellationToken);
        var tasks = await taskRepository.GetByProjectIdAsync(projectId, cancellationToken);
        return tasks.Select(task => task.ToResponse()).ToList();
    }

    public async Task<ProjectTaskResponse> UpdateStatusAsync(Guid taskId, UpdateTaskStatusRequest request, CancellationToken cancellationToken = default)
    {
        RequestValidator.ValidateTaskStatus(request.Status);

        var task = await taskRepository.GetByIdAsync(taskId, cancellationToken)
            ?? throw new NotFoundException("Task not found.");

        await EnsureProjectOwnershipAsync(task.ProjectId, cancellationToken);

        task.Status = request.Status;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return task.ToResponse();
    }

    public async Task DeleteAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        var task = await taskRepository.GetByIdAsync(taskId, cancellationToken)
            ?? throw new NotFoundException("Task not found.");

        await EnsureProjectOwnershipAsync(task.ProjectId, cancellationToken);

        taskRepository.Remove(task);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureProjectOwnershipAsync(Guid projectId, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(projectId, cancellationToken)
            ?? throw new NotFoundException("Project not found.");

        if (project.OwnerId != currentUserService.GetUserId())
        {
            throw new ForbiddenException("You do not have access to this project.");
        }
    }
}
