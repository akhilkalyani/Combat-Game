using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    private State nextState;
    private float stopDistance = 10f;
    private float attackCooldown = 1f;  // seconds between attacks
    private float lastAttackTime = 0f;
    private float raycastRange = 10f;
    public Attack(NavMeshAgent navMeshAgent, Transform player) : base(navMeshAgent, player)
    {
        type = StateType.Attack;
    }
    public void SetNextState(State state)
    {
        nextState = state;
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
        if (isExit || player == null) return;


        if (Vector3.Distance(agent.destination, player.position) > stopDistance+3)
        {
            instance.ChangeState(nextState);
        }

        if (!agent.pathPending && agent.remainingDistance <= stopDistance)
        {
            // // Face the target
            // Vector3 lookDir = (player.position - agent.transform.position).normalized;
            // lookDir.y = 0;
            // agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);

            // Perform raycast attack
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                ShootRay();
                lastAttackTime = Time.time;
            }
        }
    }
    private void ShootRay()
    {
        Vector3 shootPoint = player.position - agent.transform.position; // Enemy must assign this in inspector
        Vector3 direction = shootPoint.normalized;

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
