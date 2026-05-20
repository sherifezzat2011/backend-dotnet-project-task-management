using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjectTaskManagement.Application.Abstractions.Auth;
using ProjectTaskManagement.Domain.Entities;
using ProjectTaskManagement.Domain.Enums;
using ProjectTaskManagement.Infrastructure.Authentication;

namespace ProjectTaskManagement.Infrastructure.Persistence;

public sealed class DbInitializer(
    ApplicationDbContext dbContext,
    IPasswordHasher passwordHasher,
    IOptions<AdminSeedOptions> adminSeedOptions)
{
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.Database.MigrateAsync(cancellationToken);

        var adminOptions = adminSeedOptions.Value;
        var normalizedEmail = adminOptions.Email.Trim().ToLowerInvariant();

        var existingAdmin = await dbContext.Users.FirstOrDefaultAsync(
            user => user.Email == normalizedEmail,
            cancellationToken);

        if (existingAdmin is not null)
        {
            if (existingAdmin.Role != UserRole.Admin)
            {
                existingAdmin.Role = UserRole.Admin;
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            return;
        }

        var adminUser = new AppUser
        {
            FullName = adminOptions.FullName.Trim(),
            Email = normalizedEmail,
            PasswordHash = passwordHasher.Hash(adminOptions.Password),
            Role = UserRole.Admin
        };

        await dbContext.Users.AddAsync(adminUser, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
