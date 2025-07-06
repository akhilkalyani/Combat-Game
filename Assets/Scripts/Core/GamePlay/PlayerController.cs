using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float hitPoint = 10;
    public float moveSpeed = 2;
    public float range = 20f;
    private float health = 100;
    private bool isEnemeyInRange = false;
    private Enemy detectedEnemy;
    public Camera cam;
    private MeshRenderer mesh;
    private Vector3 initialPosition;

    void Awake()
    {
        initialPosition = transform.position;
        mesh = GetComponent<MeshRenderer>();
    }
    void OnEnable()
    {
        InputManager.onMove += Move;
        InputManager.onShoot += Shoot;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isEnemeyInRange = true;
            detectedEnemy = other.gameObject.GetComponent<Enemy>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isEnemeyInRange = false;
            detectedEnemy = null;
        }
    }

    private void Shoot(string tag)
    {
        if (tag == "Player")
        {
            //shoot logic
            Vector3 shootOrigin = transform.position + Vector3.up;
            if (!detectedEnemy) return;
            Vector3 direction = detectedEnemy.transform.position - transform.position;

            if (Physics.Raycast(shootOrigin, direction, out RaycastHit hit, Mathf.Infinity))
            {
                Debug.DrawRay(shootOrigin, direction * range, Color.red, 0.5f);
                Debug.Log("Shooting.");
                if (hit.collider.CompareTag("Enemy"))
                {
                    if (detectedEnemy.TakeDamage(hitPoint))
                    {
                        health += 60;
                        health = health > 100 ? 100 : health;
                    }
                }
            }
        }
    }

    private void Move(string tag, Vector2 moveVector)
    {
        if (tag == "Player" && moveVector != Vector2.zero)
        {
            // Convert 2D input into 3D direction
            Vector3 direction = new Vector3(moveVector.x, 0, moveVector.y).normalized;

            // Rotate toward direction
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation.x = 0;  // Lock pitch
            targetRotation.z = 0;  // Lock roll
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

            // Move in the direction
            transform.position += -direction * moveSpeed * Time.deltaTime;
        }
    }
    void OnDisable()
    {
        InputManager.onMove -= Move;
        InputManager.onShoot -= Shoot;
    }

    internal bool TakeDamge(float hit)
    {
        if (health > 0)
        {
            health -= hit;
            UIController.Instance.UpdatePlayerHealth(health, 100);
        }

        if (health <= 0)
        {
            Die();
            return false;
        }

        return true;
    }

    private void Die()
    {
        mesh.enabled = false;
        InputManager.Instance.lockInputControll = true;
    }
    public void Spawn()
    {
        mesh.enabled = true;
        transform.position = initialPosition;
        health = 100;
    }
    public void RestPosition()
    {
        transform.position = initialPosition;
    }
}
