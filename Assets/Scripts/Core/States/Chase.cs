using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : State
{
    private float stopDistance = 10f;
    private float arrivalThreshold = 0.3f;
    private State nextState=null;
    public Chase(NavMeshAgent navMeshAgent,Transform player,State nextState) : base(navMeshAgent,player)
    {
        type = StateType.Chase;
        this.nextState = nextState;
    }

    public override void Enter()
    {
        Debug.Log("chase state enter");
        if(player==null){
        Debug.Log("target is null");

        }
        isExit = false;
    }

    public override void Exit()
    {
        isExit = true;
        agent.isStopped = true;
        Debug.Log("chase state exit");
    }

    public override void Update()
    {
        if (isExit || player == null)
            return;

        Vector3 toTarget = player.position - agent.transform.position;
        Vector3 direction = toTarget.normalized;
        Vector3 chasePosition = player.position - direction * stopDistance;

        // Set destination only if different
        if (Vector3.Distance(agent.destination, chasePosition) > 0.5f)
        {
            agent.SetDestination(chasePosition);
        }
        if (!agent.pathPending && agent.remainingDistance <= stopDistance + arrivalThreshold)
        {
            Debug.Log("Reached chase target with offset. Exiting chase state.");
            instance.ChangeState(nextState);
        }
    }
}
