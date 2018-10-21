using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager
{
    public Dictionary<EventID, EventBase> Event;
    private ScoreChangeEvent ScoreChangeEvent;
    private ForceChangeEvent ForceChangeEvent;

    public EventManager()
    {
        Event = new Dictionary<EventID, EventBase>();
        ScoreChangeEvent = new ScoreChangeEvent(EventID.ScoreChangeEvent);
        Event.Add(ScoreChangeEvent.EventID, ScoreChangeEvent);
        ForceChangeEvent = new ForceChangeEvent(EventID.ForceChangeEvent);
        Event.Add(ForceChangeEvent.EventID, ForceChangeEvent);
    }

    public void FireEvent(EventID id,EventParam param)
    {
        EventBase m_event;
        if (Event.TryGetValue(id,out m_event))
        {
            m_event.Invoke(param);
        }
    }
}
