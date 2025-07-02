using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    public Transform target;
    private float attackCooldown = 1f;  // seconds between attacks
    private float lastAttackTime = 0f;
    private float raycastRange = 10f;
    public Attack(NavMeshAgent navMeshAgent) : base(navMeshAgent)
    {
    }

    public override void Enter()
    {
        Debug.Log("Attack state enter");
        isExit = false;
        agent.isStopped = true;
    }

    public override void Exit()
    {
        isExit = true;
        agent.isStopped = false;
        Debug.Log("Attack state exit");
    }

    public override void Update()
    {
        if (isExit || target == null) return;

        // Face the target
        Vector3 lookDir = (target.position - agent.transform.position).normalized;
        lookDir.y = 0;
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);

        // Perform raycast attack
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            ShootRay();
            lastAttackTime = Time.time;
        }
    }
    private void ShootRay()
    {
        Vector3 shootPoint = instance.shootPoint; // Enemy must assign this in inspector
        Vector3 direction = (target.position - shootPoint).normalized;

        if (Physics.Raycast(shootPoint, direction, out RaycastHit hit, raycastRange))
        {
            Debug.DrawRay(shootPoint, direction * raycastRange, Color.red, 0.5f);

            // Check if hit the actual player
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Enemy hit the player!");

                // // Apply damage (assuming player has a TakeDamage() method)
                // var player = hit.collider.GetComponent<PlayerHealth>();
                // if (player != null)
                // {
                //     player.TakeDamage(instance.damage);
                // }
            }
        }
    }
}
