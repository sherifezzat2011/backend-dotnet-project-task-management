namespace ProjectTaskManagement.Application.Common;

public sealed class ApiResponse<T>
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public T? Data { get; init; }

    public static ApiResponse<T> Succeeded(T data, string message = "Request completed successfully.")
        => new() { Success = true, Message = message, Data = data };

    public static ApiResponse<T> Succeeded(string message = "Request completed successfully.")
        => new() { Success = true, Message = message };
}
