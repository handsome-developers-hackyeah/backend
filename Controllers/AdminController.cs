using Comptee.Actions.Admin.Command;
using Comptee.Actions.Admin.Query;
using Comptee.Jwt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Comptee.Controllers;

[Authorize(Policy = JwtPolicies.Admin)]
[ApiController]
[Route("admin")]
[Produces("application/json")]
public class AdminController : ControllerBase
{
    private IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("getReportPosts")]
    public async Task<IActionResult> GetReportPosts(int pageNumber)
    {
        var result = await _mediator.Send(new GetReportedPosts.Query(pageNumber));
        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpPost("addRank")]
    public async Task<IActionResult> AddRank(AddRank.AddRanksCommand addRanksCommand)
    {
        var result = await _mediator.Send(addRanksCommand);
        return Ok(result);
    }   
    
    [AllowAnonymous]
    [HttpGet("getStats")]
    public async Task<IActionResult> GetStats()
    {
        var result = await _mediator.Send(new GetStats.Query());
        return Ok(result);
    }    
    
    [HttpDelete("banPost")]
    public async Task<IActionResult> BanPost(BanPost.BanPostCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }    
    [HttpDelete("acceptPost")]
    public async Task<IActionResult> AcceptPost(AcceptPost.AcceptPostCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

}
