using MediatR;
using ProjectTaskManagement.Application.DTOs.Auth;

namespace ProjectTaskManagement.Application.Features.Auth.Commands.Register;

public sealed record RegisterCommand(string FullName, string Email, string Password) : IRequest<AuthResponse>;
