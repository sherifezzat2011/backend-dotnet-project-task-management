namespace ProjectTaskManagement.Application.DTOs.Projects;

public sealed record ProjectResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedAt);
