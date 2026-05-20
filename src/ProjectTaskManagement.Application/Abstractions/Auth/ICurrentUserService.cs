namespace ProjectTaskManagement.Application.Abstractions.Auth;

public interface ICurrentUserService
{
    Guid GetUserId();
    bool IsInRole(string role);
}
