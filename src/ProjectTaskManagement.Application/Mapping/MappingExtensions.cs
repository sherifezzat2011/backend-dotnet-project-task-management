using ProjectTaskManagement.Application.DTOs.Projects;
using ProjectTaskManagement.Application.DTOs.Tasks;
using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Application.Mapping;

public static class MappingExtensions
{
    public static ProjectResponse ToResponse(this Project project)
        => new(project.Id, project.Name, project.Description, project.CreatedAt);

    public static ProjectTaskResponse ToResponse(this ProjectTask task)
        => new(task.Id, task.Title, task.Description, task.Status, task.DueDate, task.Priority, task.ProjectId);
}
