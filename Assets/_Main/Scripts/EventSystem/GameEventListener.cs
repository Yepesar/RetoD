using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntEvent: UnityEvent<int>
{ }

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;
    public IntEvent intResponse;

    private void OnEnable() => Event.RegisterListener(this);

    private void OnDisable() => Event.UnregisterListener(this);

    public void OnEventRaised() => Response.Invoke();
    public void OnEventRaised(int value) => intResponse.Invoke(value);

}
