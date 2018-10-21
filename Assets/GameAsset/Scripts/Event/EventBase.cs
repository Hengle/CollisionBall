using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public abstract class EventBase : UnityEvent<EventParam>
{
    public EventID EventID;
    public EventBase(EventID id)
    {
        EventID = id;
    }
}
