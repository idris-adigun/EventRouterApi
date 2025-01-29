using Microsoft.AspNetCore.Mvc;
using EventRouterApi.Models;
using EventRouterApi.Services;

namespace EventRouterApi.Controllers;

[ApiController]
[Route("api/events")]
public class EventController : ControllerBase
{
    private readonly EventRouterService _eventRouterService;

    public EventController(EventRouterService eventRouterService)
    {
        _eventRouterService = eventRouterService;
    }

    [HttpPost("publish")]
    public IActionResult PublishEvent([FromBody] Event evnt)
    {
        _eventRouterService.DispatchEvent(evnt);
        return Ok(new { message = "Event dispatched", evnt.Type });
    }
}
