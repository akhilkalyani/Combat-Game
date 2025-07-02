using System;
using UnityEngine;

public static class SensorEvents
{
    public static event Action<string, Transform> OnPlayerEnteredArena;
    public static event Action<string> OnPlayerExitedArena;

    public static void PlayerEntered(string sensorID, Transform player)
    {
        OnPlayerEnteredArena?.Invoke(sensorID, player);
    }
    public static void PlayerExit(string sensorID)
    {
        OnPlayerExitedArena?.Invoke(sensorID);
    }
}
