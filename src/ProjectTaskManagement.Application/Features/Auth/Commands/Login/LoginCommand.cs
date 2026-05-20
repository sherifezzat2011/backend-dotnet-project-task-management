using MediatR;
using ProjectTaskManagement.Application.DTOs.Auth;

namespace ProjectTaskManagement.Application.Features.Auth.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<AuthResponse>;
