using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.DTOs.Tasks;

public sealed record UpdateTaskStatusRequest(ProjectTaskStatus Status);
