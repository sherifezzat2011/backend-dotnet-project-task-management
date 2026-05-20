using ProjectTaskManagement.Application.Abstractions.Auth;
using ProjectTaskManagement.Application.Abstractions.Persistence;
using ProjectTaskManagement.Application.DTOs.Projects;
using ProjectTaskManagement.Application.Exceptions;
using ProjectTaskManagement.Application.Services;
using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Tests;

public sealed class ProjectServiceTests
{
    [Fact]
    public async Task GetByIdAsync_ShouldThrowForbidden_WhenProjectBelongsToAnotherUser()
    {
        var currentUserId = Guid.NewGuid();
        var otherUserProject = new Project
        {
            Name = "Private Project",
            Description = "Owned by another user",
            OwnerId = Guid.NewGuid()
        };

        var service = new ProjectService(
            new FakeProjectRepository(otherUserProject),
            new FakeCurrentUserService(currentUserId),
            new FakeUnitOfWork());

        await Assert.ThrowsAsync<ForbiddenException>(() => service.GetByIdAsync(otherUserProject.Id));
    }

    [Fact]
    public async Task CreateAsync_ShouldAssignCurrentUserAsOwner()
    {
        var currentUserId = Guid.NewGuid();
        var repository = new FakeProjectRepository();
        var service = new ProjectService(repository, new FakeCurrentUserService(currentUserId), new FakeUnitOfWork());

        var response = await service.CreateAsync(new CreateProjectRequest("Demo Project", "Description"));

        var storedProject = await repository.GetByIdAsync(response.Id);
        Assert.NotNull(storedProject);
        Assert.Equal(currentUserId, storedProject!.OwnerId);
    }

    private sealed class FakeProjectRepository(params Project[] projects) : IProjectRepository
    {
        private readonly List<Project> _projects = projects.ToList();

        public Task AddAsync(Project project, CancellationToken cancellationToken = default)
        {
            _projects.Add(project);
            return Task.CompletedTask;
        }

        public Task<Project?> GetByIdAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_projects.FirstOrDefault(project => project.Id == projectId));
        }

        public Task<IReadOnlyList<Project>> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
        {
            IReadOnlyList<Project> projects = _projects.Where(project => project.OwnerId == ownerId).ToList();
            return Task.FromResult(projects);
        }

        public void Remove(Project project)
        {
            _projects.Remove(project);
        }
    }

    private sealed class FakeCurrentUserService(Guid userId) : ICurrentUserService
    {
        public Guid GetUserId() => userId;

        public bool IsInRole(string role) => false;
    }

    private sealed class FakeUnitOfWork : IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(1);
    }
}
