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
    private float raycastRange = 20f;
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


        if (Vector3.Distance(agent.destination, player.position) > stopDistance + 3)
        {
            instance.ChangeState(nextState);
        }

        if (!agent.pathPending && agent.remainingDistance <= stopDistance)
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                ShootRay();
                lastAttackTime = Time.time;
            }
        }
    }
    private void ShootRay()
    {
        Vector3 shootOrigin = agent.transform.position + Vector3.up; // Raise it a bit to shoot from chest height
        Vector3 direction = (player.position - shootOrigin).normalized;

        if (Physics.Raycast(shootOrigin, direction, out RaycastHit hit, raycastRange,LayerMask.GetMask("Player")))
        {
            Debug.DrawRay(shootOrigin, direction * raycastRange, Color.red, 0.5f);

            if (hit.collider.tag=="Player")
            {
                Debug.Log("Enemy hit the player!");
            }
        }
    }
}
