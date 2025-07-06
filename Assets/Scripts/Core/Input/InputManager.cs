using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : SingleTon<InputManager>
{
    public FixedJoystick joystick;
    public Button shootBtn;
    public static event Action<string, Vector2> onMove;
    public static event Action<string> onShoot;
    private string playerTag = "Player";
    public float shootCooldown = 1f; // Cooldown in seconds
    private float shootTimer = 0f;   // Time since last shot
    private bool isCooldown = false;
    private Vector2 moveVector;
    public bool lockInputControll=false;
    void Awake()
    {
        shootBtn.onClick.AddListener(OnShoot);
    }

    private void OnShoot()
    {
        if (lockInputControll) return;
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
        if (lockInputControll) return;
        //Movement
        moveVector = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (moveVector != Vector2.zero)
        {
            onMove?.Invoke(playerTag, moveVector);
        }
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
    protected override void OnDestroy()
    {
        shootBtn.onClick.RemoveListener(OnShoot);
        base.OnDestroy();
    }
}
