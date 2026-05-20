using ProjectTaskManagement.Application.Abstractions.Auth;
using ProjectTaskManagement.Application.Abstractions.Persistence;
using ProjectTaskManagement.Application.DTOs.Projects;
using ProjectTaskManagement.Application.Exceptions;
using ProjectTaskManagement.Application.Mapping;
using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Application.Services;

public sealed class ProjectService(
    IProjectRepository projectRepository,
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork)
{
    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken cancellationToken = default)
    {
        RequestValidator.ValidateProjectRequest(request.Name, request.Description);

        var project = new Project
        {
            Name = request.Name.Trim(),
            Description = request.Description.Trim(),
            CreatedAt = DateTime.UtcNow,
            OwnerId = currentUserService.GetUserId()
        };

        await projectRepository.AddAsync(project, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return project.ToResponse();
    }

    public async Task<IReadOnlyList<ProjectResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var projects = await projectRepository.GetByOwnerIdAsync(currentUserService.GetUserId(), cancellationToken);
        return projects.Select(project => project.ToResponse()).ToList();
    }

    public async Task<ProjectResponse> GetByIdAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return (await GetOwnedProjectAsync(projectId, cancellationToken)).ToResponse();
    }

    public async Task<ProjectResponse> UpdateAsync(Guid projectId, UpdateProjectRequest request, CancellationToken cancellationToken = default)
    {
        RequestValidator.ValidateProjectRequest(request.Name, request.Description);

        var project = await GetOwnedProjectAsync(projectId, cancellationToken);
        project.Name = request.Name.Trim();
        project.Description = request.Description.Trim();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return project.ToResponse();
    }

    public async Task DeleteAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        var project = await GetOwnedProjectAsync(projectId, cancellationToken);
        projectRepository.Remove(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task<Project> GetOwnedProjectAsync(Guid projectId, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(projectId, cancellationToken)
            ?? throw new NotFoundException("Project not found.");

        if (project.OwnerId != currentUserService.GetUserId())
        {
            throw new ForbiddenException("You do not have access to this project.");
        }

        return project;
    }
}
