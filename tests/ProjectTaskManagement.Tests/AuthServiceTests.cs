using ProjectTaskManagement.Application.Abstractions.Auth;
using ProjectTaskManagement.Application.Abstractions.Persistence;
using ProjectTaskManagement.Application.DTOs.Auth;
using ProjectTaskManagement.Application.Exceptions;
using ProjectTaskManagement.Application.Services;
using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Tests;

public sealed class AuthServiceTests
{
    [Fact]
    public async Task RegisterAsync_ShouldThrowConflict_WhenEmailAlreadyExists()
    {
        var userRepository = new FakeUserRepository(new AppUser
        {
            FullName = "Existing User",
            Email = "existing@example.com",
            PasswordHash = "hashed"
        });

        var service = new AuthService(
            userRepository,
            new FakePasswordHasher(),
            new FakeJwtTokenGenerator(),
            new FakeUnitOfWork());

        await Assert.ThrowsAsync<ConflictException>(() =>
            service.RegisterAsync(new RegisterRequest("New User", "existing@example.com", "password123")));
    }

    [Fact]
    public async Task LoginAsync_ShouldThrowUnauthorized_WhenPasswordIsInvalid()
    {
        var userRepository = new FakeUserRepository(new AppUser
        {
            FullName = "Existing User",
            Email = "existing@example.com",
            PasswordHash = "hashed-password123"
        });

        var service = new AuthService(
            userRepository,
            new FakePasswordHasher(),
            new FakeJwtTokenGenerator(),
            new FakeUnitOfWork());

        await Assert.ThrowsAsync<UnauthorizedException>(() =>
            service.LoginAsync(new LoginRequest("existing@example.com", "wrong-password")));
    }

    private sealed class FakeUserRepository(params AppUser[] users) : IUserRepository
    {
        private readonly List<AppUser> _users = users.ToList();

        public Task AddAsync(AppUser user, CancellationToken cancellationToken = default)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task<AppUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_users.FirstOrDefault(user => user.Email == email));
        }
    }

    private sealed class FakePasswordHasher : IPasswordHasher
    {
        public string Hash(string password) => $"hashed-{password}";

        public bool Verify(string password, string hashedPassword) => Hash(password) == hashedPassword;
    }

    private sealed class FakeJwtTokenGenerator : IJwtTokenGenerator
    {
        public string GenerateToken(AppUser user) => $"token-{user.Id}";
    }

    private sealed class FakeUnitOfWork : IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(1);
    }
}
