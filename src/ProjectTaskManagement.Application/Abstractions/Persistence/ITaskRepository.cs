using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Application.Abstractions.Persistence;

public interface ITaskRepository
{
    Task AddAsync(ProjectTask task, CancellationToken cancellationToken = default);
    Task<ProjectTask?> GetByIdAsync(Guid taskId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProjectTask>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
    void Remove(ProjectTask task);
}
