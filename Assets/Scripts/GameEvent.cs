using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> listeners = new List<GameEventListener>();

    // Raise event through different methods signatures

    public void Raise()
    {
        if (listeners.Count > 0)
        {
            foreach (GameEventListener listener in listeners)
            {
                listener.OnEventRaised();
                if (listeners.Count == 0) return;
            }
        }        
    }

    // Manage Listeners

    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
