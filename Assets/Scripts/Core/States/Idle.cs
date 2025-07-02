using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    public Transform[] points;         // Assign 4 (or more) patrol point transforms
    private int destPoint = 0;
    private bool circular = true;    // true = loop; false = back-and-forth
    private bool goingForward = true;
    public Idle(NavMeshAgent entity, Transform[] points) : base(entity)
    {
        this.points = points;
        agent.autoBraking = false;
    }
    public override void Enter()
    {
        Debug.Log("Idle state enter");
        destPoint = 0;
        GotoNextPoint();
        isExit = false;
    }
    void GotoNextPoint()
    {
        if (points.Length == 0) return;

        agent.destination = points[destPoint].position;

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
        if (!isExit && !agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    public override void Exit()
    {
        Debug.Log("Idle state exit");
        isExit = true;
    }
}
