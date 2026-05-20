namespace ProjectTaskManagement.Application.Exceptions;

public sealed class UnauthorizedException(string message) : AppException(message)
{
    public override int StatusCode => 401;
}
