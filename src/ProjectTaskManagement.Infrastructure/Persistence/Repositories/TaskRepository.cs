using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Application.Abstractions.Persistence;
using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Infrastructure.Persistence.Repositories;

public sealed class TaskRepository(ApplicationDbContext dbContext) : ITaskRepository
{
    public async Task AddAsync(ProjectTask task, CancellationToken cancellationToken = default)
    {
        await dbContext.Tasks.AddAsync(task, cancellationToken);
    }

    public Task<ProjectTask?> GetByIdAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        return dbContext.Tasks.FirstOrDefaultAsync(task => task.Id == taskId, cancellationToken);
    }

    public async Task<IReadOnlyList<ProjectTask>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Tasks
            .Where(task => task.ProjectId == projectId)
            .OrderBy(task => task.DueDate)
            .ThenByDescending(task => task.Priority)
            .ToListAsync(cancellationToken);
    }

    public void Remove(ProjectTask task)
    {
        dbContext.Tasks.Remove(task);
    }
}
