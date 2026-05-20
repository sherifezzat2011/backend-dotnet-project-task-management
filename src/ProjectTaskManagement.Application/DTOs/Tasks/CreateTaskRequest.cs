using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.DTOs.Tasks;

public sealed record CreateTaskRequest(
    string Title,
    string Description,
    DateTime? DueDate,
    TaskPriority Priority);
