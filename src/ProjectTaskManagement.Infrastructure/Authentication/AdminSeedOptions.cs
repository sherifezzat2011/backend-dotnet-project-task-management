namespace ProjectTaskManagement.Infrastructure.Authentication;

public sealed class AdminSeedOptions
{
    public const string SectionName = "AdminSeed";

    public string FullName { get; set; } = "System Admin";
    public string Email { get; set; } = "admin@projecttask.local";
    public string Password { get; set; } = "Admin123!";
}
