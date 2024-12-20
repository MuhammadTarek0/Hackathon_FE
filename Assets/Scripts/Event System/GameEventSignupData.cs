using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event Signup Data", menuName = "Game Specific Events/Game Signup Data")]
public class GameEventSignupData : ScriptableObject
{
    private List<GameEventListenerSignupData> listeners = new List<GameEventListenerSignupData>();


    // Register a listener
    public void RegisterListener(GameEventListenerSignupData listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    // Unregister a listener
    public void UnregisterListener(GameEventListenerSignupData listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }

    // Raise the event
    public void Raise(SignupData value)
    {
        Debug.Log($"{this.name}: Raised");
        foreach (var listener in listeners)
        {
            listener.OnGameEventRaised(value);
        }
    }

    [ContextMenu("Test Raise Event")] // Parameterless method for testing in the Unity Editor
    private void TestRaise()
    {
        Debug.Log("Testing Raise Event with dummy SignupData...");
        var testSignupData = new SignupData
        {
            username = "testuser",
            firstName = "Test",
            lastName = "User",
            email = "test@example.com",
            password = "password123"
        };
        Raise(testSignupData);
    }
}
