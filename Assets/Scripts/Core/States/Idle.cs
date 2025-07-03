using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    public Transform[] points;         // Assign 4 (or more) patrol point transforms
    public int destPoint = 0;
    private float thresholdDistance = 40f;
    private bool circular = true;    // true = loop; false = back-and-forth
    private bool goingForward = true;
    private State nextState;
    private bool isHome = true;
    public Idle(NavMeshAgent entity, Transform player, Transform[] points) : base(entity, player)
    {
        type = StateType.Idle;
        this.points = points;
        agent.autoBraking = false;
    }
    public void SetNextState(State state) {
        nextState = state;
    }
    public override void Enter()
    {
        Debug.Log("Idle state enter");
        agent.ResetPath();
        agent.SetDestination(points[0].position);
        isExit = false;
    }
    void GotoNextPoint()
    {
        if (points.Length == 0) return;

        agent.SetDestination(points[destPoint].position);

        if (circular)
            destPoint = (destPoint + 1) % points.Length;
        else
        {
            if (goingForward)
            {
                destPoint++;
                if (destPoint >= points.Length)
                {
                    destPoint = points.Length - 2;
                    goingForward = false;
                }
            }
            else
            {
                destPoint--;
                if (destPoint < 0)
                {
                    destPoint = 1;
                    goingForward = true;
                }
            }
        }
    }
    public override void Update()
    {
        // Once close enough, head to the next waypoint
        if (!isExit)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
                isHome = true;
            }

            if (isHome && Vector3.Distance(instance.transform.position, player.position) <= thresholdDistance)
            {
                isHome = false;
                //chnage state to chase
                instance.ChangeState(nextState);
            }
        }
    }
    public override void Exit()
    {
        Debug.Log("Idle state exit");
        isExit = true;
    }
}
