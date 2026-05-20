using MediatR;
using ProjectTaskManagement.Application.DTOs.Auth;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.Application.Features.Auth.Commands.Register;

public sealed class RegisterCommandHandler(AuthService authService) : IRequestHandler<RegisterCommand, AuthResponse>
{
    public Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return authService.RegisterAsync(new RegisterRequest(request.FullName, request.Email, request.Password), cancellationToken);
    }
}
