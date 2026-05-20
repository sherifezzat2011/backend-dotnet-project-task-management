using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Application.Abstractions.Persistence;
using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    public async Task AddAsync(AppUser user, CancellationToken cancellationToken = default)
    {
        await dbContext.Users.AddAsync(user, cancellationToken);
    }

    public async Task<IReadOnlyList<AppUser>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Users
            .OrderBy(user => user.FullName)
            .ToListAsync(cancellationToken);
    }

    public Task<AppUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return dbContext.Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }

    public Task<AppUser?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
    }
}
