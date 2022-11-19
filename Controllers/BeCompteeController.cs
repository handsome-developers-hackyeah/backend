using Comptee.Actions.Auth.Command;
using Comptee.Actions.Auth.Query;
using Comptee.Actions.BeComptee.Command;
using Comptee.Actions.BeComptee.Query;
using Comptee.DataAccess.Entities;
using Comptee.Middlewears.Models;
using Handsomedevelopers.Actions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Respond = Comptee.Actions.BeComptee.Command.Respond;

namespace Comptee.Controllers;

[Authorize]
[ApiController]
[Route("beComptee")]
[Produces("application/json")]
public class BeCompteeController : ControllerBase
{
    private IMediator _mediator;

    public BeCompteeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("addPost")]
    public async Task<IActionResult> AddPost(AddBeComptee.AddPostCommand request)
    {
        var result = await _mediator.Send(request);
        return new ObjectResult(ApiResponse.Success(200, result));
    }    
    
    [HttpPost("addComment")]
    public async Task<IActionResult> AddComment(AddComment.AddCommentCommand request)
    {
        var result = await _mediator.Send(request);
        return new ObjectResult(ApiResponse.Success(200, result));
    }  
    
    [HttpPost("respond")]
    public async Task<IActionResult> Respond(Respond.RespondCommand  request)
    {
        var result = await _mediator.Send(request);
        return new ObjectResult(ApiResponse.Success(200, result));
    }    
    
    [HttpDelete("unrespond")]
    public async Task<IActionResult> Respond(Unrespond.UnrespondCommand  request)
    {
        var result = await _mediator.Send(request);
        return new ObjectResult(ApiResponse.Success(200, result));
    }    
    
    [HttpDelete("removeComment")]
    public async Task<IActionResult> RemoveComment(RemoveComment.RemoveCommentCommand request)
    {
        var result = await _mediator.Send(request);
        return new ObjectResult(ApiResponse.Success(200, result));
    }    
    
    [HttpDelete("report")]
    public async Task<IActionResult> Report(Report.ReportCommand request)
    {
        var result = await _mediator.Send(request);
        return new ObjectResult(ApiResponse.Success(200, result));
    }

    [HttpGet("getPost")]
    public async Task<IActionResult> GetPost(int page)
    {
        Guid userId = Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value);
        var result = await _mediator.Send(new GetPost.GetPostQuery(userId, page));
        return new ObjectResult(ApiResponse.Success(200, result));
    }
    
}