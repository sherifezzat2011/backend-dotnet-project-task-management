using ProjectTaskManagement.Application.Abstractions.Auth;
using ProjectTaskManagement.Application.Abstractions.Persistence;
using ProjectTaskManagement.Application.DTOs.Auth;
using ProjectTaskManagement.Application.Exceptions;
using ProjectTaskManagement.Domain.Entities;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.Services;

public sealed class AuthService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator,
    IUnitOfWork unitOfWork)
{
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        RequestValidator.ValidateRegisterRequest(request);

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var existingUser = await userRepository.GetByEmailAsync(normalizedEmail, cancellationToken);
        if (existingUser is not null)
        {
            throw new ConflictException("A user with this email already exists.");
        }

        var user = new AppUser
        {
            FullName = request.FullName.Trim(),
            Email = normalizedEmail,
            PasswordHash = passwordHasher.Hash(request.Password),
            Role = UserRole.User
        };

        await userRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponse(user.Id, user.FullName, user.Email, user.Role.ToString(), jwtTokenGenerator.GenerateToken(user));
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        RequestValidator.ValidateLoginRequest(request);

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var user = await userRepository.GetByEmailAsync(normalizedEmail, cancellationToken);

        if (user is null || !passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedException("Invalid email or password.");
        }

        return new AuthResponse(user.Id, user.FullName, user.Email, user.Role.ToString(), jwtTokenGenerator.GenerateToken(user));
    }
}
