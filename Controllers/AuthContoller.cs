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

    [HttpGet("login")]
    public async Task<IActionResult> Login(string login, string password)
    {
        var result = await _mediator.Send(new Login.LoginQuery(login, password));
        return new ObjectResult(ApiResponse.Success(200, result));
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        var result = await _mediator.Send(new RefreshToken.RefreshTokenQuery(Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value)));
        return new ObjectResult(ApiResponse.Success(200, result));
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(Register.Command command)
    {
        var result = await _mediator.Send(command);
        return new ObjectResult(ApiResponse.Success(200, result));
    }

    [Authorize]
    [HttpPut("user")]
    public async Task<IActionResult> UpdateUser(UpdateUser.UpdateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return new ObjectResult(ApiResponse.Success(200, result));
    }   
    

    [Authorize]
    [HttpDelete("user")]
    public async Task<IActionResult> RemoveUser()
    {
        var result =
            await _mediator.Send(
                new RemoveUser.RemoveUserCommand(Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value)));
        return new ObjectResult(ApiResponse.Success(200, result));
    }
    
    [Authorize]
    [HttpGet("user")]
    public async Task<IActionResult> GetUser()
    {
        var result = await _mediator.Send(new GetUser.GetUserQuery(Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value)));
        return new ObjectResult(ApiResponse.Success(200, result));
    }
    [Authorize]
    [HttpPut("changePhoto")]
    public async Task<IActionResult> Feature(ChangePhoto.ChangePhotoCommand command)
    {
        var result = await _mediator.Send(command);
        return new ObjectResult(ApiResponse.Success(200, result));
    }

}    