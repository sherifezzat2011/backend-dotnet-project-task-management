using MediatR;
using ProjectTaskManagement.Application.DTOs.Tasks;

namespace ProjectTaskManagement.Application.Features.Tasks.Queries.GetTasksByProject;

public sealed record GetTasksByProjectQuery(Guid ProjectId) : IRequest<IReadOnlyList<ProjectTaskResponse>>;
