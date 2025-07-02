using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Transform[] rewards;
    int currentTarget = 0;
    public Transform player;
    public float moveSpeed = 2;
    public Transform target;

    void OnEnable()
    {
        InputManager.onMove += Move;
    }

    private void Move(Vector2 moveVector)
    {
        if (moveVector != Vector2.zero)
        {
            // Convert 2D input into 3D direction
            Vector3 direction = new Vector3(moveVector.x, 0, moveVector.y).normalized;

            // Rotate toward direction
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation.x = 0;  // Lock pitch
            targetRotation.z = 0;  // Lock roll
            player.rotation = Quaternion.Slerp(player.rotation, targetRotation, Time.deltaTime * 10f);

            // Move in the direction
            player.position += -direction * moveSpeed * Time.deltaTime;
        }
    }
    void OnDisable()
    {
        InputManager.onMove -= Move;
    }
}
