using System.Collections.Concurrent;
using System.Threading.Channels;
using EventRouterApi.Models;

namespace EventRouterApi.Services;

public class EventRouterService
{
    private readonly Channel<Event> _eventChannel;
    private readonly ConcurrentBag<Func<Event, Task>> _listeners = new();

    public EventRouterService()
    {
        _eventChannel = Channel.CreateUnbounded<Event>(); // Unbounded queue
        StartProcessing();
    }

    public void RegisterListener(Func<Event, Task> listener)
    {
        _listeners.Add(listener);
    }

    public async Task QueueEventAsync(Event evnt)
    {
        await _eventChannel.Writer.WriteAsync(evnt);
    }

    private void StartProcessing()
    {
        Task.Run(async () =>
        {
            await foreach (var evnt in _eventChannel.Reader.ReadAllAsync())
            {
                await ProcessEvent(evnt);
            }
        });
    }

    private async Task ProcessEvent(Event evnt)
    {
        var tasks = _listeners.Select(listener => listener(evnt));
        await Task.WhenAll(tasks);
    }
}
