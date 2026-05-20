namespace ProjectTaskManagement.Application.DTOs.Admin;

public sealed record UserResponse(
    Guid Id,
    string FullName,
    string Email,
    string Role);
