using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public FixedJoystick joystick;
    public Button shootBtn;
    public static event Action<string, Vector2> onMove;
    public static event Action<string> onShoot;
    private string playerTag = "Player";
    public float shootCooldown = 1f; // Cooldown in seconds
    private float shootTimer = 0f;   // Time since last shot
    private bool isCooldown = false;
    void Awake()
    {
        shootBtn.onClick.AddListener(OnShoot);
    }

    private void OnShoot()
    {
        if (!isCooldown)
        {
            // Shoot!
            onShoot?.Invoke(playerTag);
            isCooldown = true;
            shootTimer = 0f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Movement
        onMove?.Invoke(playerTag, new Vector2(joystick.Horizontal, joystick.Vertical));
        // Cooldown Timer Logic
        if (isCooldown)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootCooldown)
            {
                isCooldown = false;
            }
        }
    }
    void OnDestroy()
    {
        shootBtn.onClick.RemoveListener(OnShoot);
    }
}
