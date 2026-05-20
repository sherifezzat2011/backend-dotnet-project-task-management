using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Application.Abstractions.Persistence;

public interface IProjectRepository
{
    Task AddAsync(Project project, CancellationToken cancellationToken = default);
    Task<Project?> GetByIdAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Project>> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default);
    void Remove(Project project);
}
