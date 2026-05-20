using ProjectTaskManagement.Application.Common;
using ProjectTaskManagement.Application.DTOs.Auth;
using ProjectTaskManagement.Application.Exceptions;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.Services;

internal static class RequestValidator
{
    public static void ValidateRegisterRequest(RegisterRequest request)
    {
        var errors = new List<ValidationError>();

        if (string.IsNullOrWhiteSpace(request.FullName))
        {
            errors.Add(new ValidationError(nameof(request.FullName), "Full name is required."));
        }

        ValidateEmail(request.Email, errors);
        ValidatePassword(request.Password, errors);

        ThrowIfAny(errors);
    }

    public static void ValidateLoginRequest(LoginRequest request)
    {
        var errors = new List<ValidationError>();
        ValidateEmail(request.Email, errors);

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            errors.Add(new ValidationError(nameof(request.Password), "Password is required."));
        }

        ThrowIfAny(errors);
    }

    public static void ValidateProjectRequest(string name, string description)
    {
        var errors = new List<ValidationError>();

        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add(new ValidationError(nameof(name), "Project name is required."));
        }
        else if (name.Trim().Length > 150)
        {
            errors.Add(new ValidationError(nameof(name), "Project name must not exceed 150 characters."));
        }

        if ((description ?? string.Empty).Trim().Length > 1000)
        {
            errors.Add(new ValidationError(nameof(description), "Project description must not exceed 1000 characters."));
        }

        ThrowIfAny(errors);
    }

    public static void ValidateTaskRequest(string title, string description, DateTime? dueDate, TaskPriority priority)
    {
        var errors = new List<ValidationError>();

        if (string.IsNullOrWhiteSpace(title))
        {
            errors.Add(new ValidationError(nameof(title), "Task title is required."));
        }
        else if (title.Trim().Length > 200)
        {
            errors.Add(new ValidationError(nameof(title), "Task title must not exceed 200 characters."));
        }

        if ((description ?? string.Empty).Trim().Length > 2000)
        {
            errors.Add(new ValidationError(nameof(description), "Task description must not exceed 2000 characters."));
        }

        if (dueDate.HasValue && dueDate.Value.Date < DateTime.UtcNow.Date)
        {
            errors.Add(new ValidationError(nameof(dueDate), "Due date cannot be in the past."));
        }

        if (!Enum.IsDefined(priority))
        {
            errors.Add(new ValidationError(nameof(priority), "Invalid task priority."));
        }

        ThrowIfAny(errors);
    }

    public static void ValidateTaskStatus(ProjectTaskStatus status)
    {
        if (!Enum.IsDefined(status))
        {
            throw new ValidationException(
                "One or more validation errors occurred.",
                [new ValidationError(nameof(status), "Invalid task status.")]);
        }
    }

    private static void ValidateEmail(string email, List<ValidationError> errors)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            errors.Add(new ValidationError(nameof(email), "Email is required."));
            return;
        }

        var validator = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
        if (!validator.IsValid(email))
        {
            errors.Add(new ValidationError(nameof(email), "A valid email address is required."));
        }
    }

    private static void ValidatePassword(string password, List<ValidationError> errors)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            errors.Add(new ValidationError(nameof(password), "Password is required."));
        }
        else if (password.Length < 6)
        {
            errors.Add(new ValidationError(nameof(password), "Password must be at least 6 characters long."));
        }
    }

    private static void ThrowIfAny(List<ValidationError> errors)
    {
        if (errors.Count > 0)
        {
            throw new ValidationException("One or more validation errors occurred.", errors);
        }
    }
}
