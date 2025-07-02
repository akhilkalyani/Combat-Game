using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : State
{
    public Transform target;
    private float stopDistance = 2f;
    private float arrivalThreshold = 0.3f;
    private State nextState=null;
    public Chase(NavMeshAgent navMeshAgent, State nextState) : base(navMeshAgent)
    {
        this.nextState = nextState;
    }

    public override void Enter()
    {
        Debug.Log("chase state enter");
        if(target==null){
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
        if (isExit || target == null)
            return;

        Vector3 toTarget = target.position - agent.transform.position;
        Vector3 direction = toTarget.normalized;
        Vector3 chasePosition = target.position - direction * stopDistance;

        // Set destination only if different
        if (Vector3.Distance(agent.destination, chasePosition) > 0.5f)
        {
            agent.SetDestination(chasePosition);
        }

        // Check if reached the offset position
        if (!agent.pathPending && agent.remainingDistance <= stopDistance + arrivalThreshold)
        {
            Debug.Log("Reached chase target with offset. Exiting chase state.");
            var st = (Attack)nextState;
            st.target = target;
            instance.ChangeState(st);
        }
    }
}
