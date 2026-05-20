using ProjectTaskManagement.Application.Common;

namespace ProjectTaskManagement.Application.Exceptions;

public sealed class ValidationException(string message, IReadOnlyList<ValidationError> errors) : AppException(message)
{
    public IReadOnlyList<ValidationError> Errors { get; } = errors;
    public override int StatusCode => 400;
}
