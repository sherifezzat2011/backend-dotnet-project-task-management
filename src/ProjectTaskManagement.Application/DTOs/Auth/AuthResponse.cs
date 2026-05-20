namespace ProjectTaskManagement.Application.DTOs.Auth;

public sealed record AuthResponse(
    Guid UserId,
    string FullName,
    string Email,
    string Role,
    string Token);
