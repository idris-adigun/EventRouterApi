using System.Collections.Concurrent;
using EventRouterApi.Models;

namespace EventRouterApi.Services;

public class EventRouterService
{
    private readonly ConcurrentBag<Action<Event>> _listeners = new();

    public void RegisterListener(Action<Event> listener)
    {
        _listeners.Add(listener);
    }

    public void DispatchEvent(Event evnt)
    {
        Parallel.ForEach(_listeners, listener =>
        {
            listener(evnt);
        });
    }
}
