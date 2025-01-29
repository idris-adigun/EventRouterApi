using EventRouterApi.Models;

namespace EventRouterApi.Services;

public class EventRouterService
{
    private readonly List<Action<Event>> _listeners = new();

    public void RegisterListener(Action<Event> listener)
    {
        _listeners.Add(listener);
    }

    public void DispatchEvent(Event evnt)
    {
        foreach (var listener in _listeners)
        {
            listener(evnt);
        }
    }
}
