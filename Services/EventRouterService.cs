using System.Collections.Concurrent;
using EventRouterApi.Models;

namespace EventRouterApi.Services;

public class EventRouterService
{
    private readonly ConcurrentBag<Func<Event, Task>> _listeners = new();

    public void RegisterListener(Func<Event, Task> listener)
    {
        _listeners.Add(listener);
    }

    public async Task DispatchEventAsync(Event evnt)
    {
        var tasks = _listeners.Select(listener => Task.Run(() => listener(evnt)));
        await Task.WhenAll(tasks);
    }
}
