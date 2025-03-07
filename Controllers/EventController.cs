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

        // Register an async event listener
        _eventRouterService.RegisterListener(async evnt =>
        {
            await Task.Delay(500); // Simulate async work
            Console.WriteLine($"[ASYNC] Processed event: {evnt.Type} - {evnt.Payload}");
        });
    }

    [HttpPost("publish")]
    public async Task<IActionResult> PublishEvent([FromBody] Event evnt)
    {
        await _eventRouterService.QueueEventAsync(evnt);
        return Accepted(new { message = "Event queued for processing", evnt.Type });
    }
}
