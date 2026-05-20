using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task AddAsync(AppUser user, CancellationToken cancellationToken = default);
    Task<AppUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
