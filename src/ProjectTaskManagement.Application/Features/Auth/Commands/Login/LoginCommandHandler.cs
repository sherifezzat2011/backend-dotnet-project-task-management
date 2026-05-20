using MediatR;
using ProjectTaskManagement.Application.DTOs.Auth;
using ProjectTaskManagement.Application.Services;

namespace ProjectTaskManagement.Application.Features.Auth.Commands.Login;

public sealed class LoginCommandHandler(AuthService authService) : IRequestHandler<LoginCommand, AuthResponse>
{
    public Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return authService.LoginAsync(new LoginRequest(request.Email, request.Password), cancellationToken);
    }
}
