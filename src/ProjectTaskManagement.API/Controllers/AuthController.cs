using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Application.Common;
using ProjectTaskManagement.Application.DTOs.Auth;
using ProjectTaskManagement.Application.Features.Auth.Commands.Login;
using ProjectTaskManagement.Application.Features.Auth.Commands.Register;

namespace ProjectTaskManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResponse<AuthResponse>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterCommand(request.FullName, request.Email, request.Password);
        var result = await sender.Send(command, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, ApiResponse<AuthResponse>.Succeeded(result, "User registered successfully."));
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<AuthResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email, request.Password);
        var result = await sender.Send(command, cancellationToken);
        return Ok(ApiResponse<AuthResponse>.Succeeded(result, "Login successful."));
    }
}
