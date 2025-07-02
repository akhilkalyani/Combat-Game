using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public FixedJoystick joystick;
    public static event Action<Vector2> onMove;
    // Update is called once per frame
    void Update()
    {
        onMove?.Invoke(new Vector2(joystick.Horizontal, joystick.Vertical));
    }
}
