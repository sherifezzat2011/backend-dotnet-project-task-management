using ProjectTaskManagement.Application.Abstractions.Persistence;
using ProjectTaskManagement.Application.DTOs.Admin;
using ProjectTaskManagement.Application.Exceptions;
using ProjectTaskManagement.Application.Mapping;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.Services;

public sealed class AdminService(IUserRepository userRepository, IUnitOfWork unitOfWork)
{
    public async Task<IReadOnlyList<UserResponse>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        var users = await userRepository.GetAllAsync(cancellationToken);
        return users.Select(user => user.ToResponse()).ToList();
    }

    public async Task<UserResponse> UpdateUserRoleAsync(Guid userId, UpdateUserRoleRequest request, CancellationToken cancellationToken = default)
    {
        RequestValidator.ValidateUserRole(request.Role);

        var user = await userRepository.GetByIdAsync(userId, cancellationToken)
            ?? throw new NotFoundException("User not found.");

        user.Role = request.Role;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.ToResponse();
    }
}
