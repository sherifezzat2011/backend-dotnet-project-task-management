using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Domain.Entities;

public sealed class AppUser
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;

    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
