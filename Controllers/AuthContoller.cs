using Comptee.Actions.Auth.Command;
using Comptee.Actions.Auth.Query;
using Comptee.Middlewears.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Comptee.Controllers;

[ApiController]
[Route("auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Login")]
    public async Task<IActionResult> Login(string login, string password)
    {
        var result = await _mediator.Send(new Login.Query(login, password));
        return new ObjectResult(ApiResponse.Success(200, result));
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(Register.Command command)
    {
        var result = await _mediator.Send(command);
        return new ObjectResult(ApiResponse.Success(200, result));
    }

    [Authorize]
    [HttpPut("User")]
    public async Task<IActionResult> UpdateUser(UpdateUser.UpdateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return new ObjectResult(ApiResponse.Success(200, result));
    }

    [Authorize]
    [HttpDelete("User")]
    public async Task<IActionResult> RemoveUser()
    {
        var result =
            await _mediator.Send(
                new RemoveUser.RemoveUserCommand(Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value)));
        return new ObjectResult(ApiResponse.Success(200, result));
    }

    [Authorize]
    [HttpPut("ChangePhoto")]
    public async Task<IActionResult> Feature(ChangePhoto.ChangePhotoCommand command)
    {
        var result = await _mediator.Send(command);
        return new ObjectResult(ApiResponse.Success(200, result));
    }
}    