namespace ProjectTaskManagement.Application.Abstractions.Auth;

public interface ICurrentUserService
{
    Guid GetUserId();
}
