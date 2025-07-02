using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State
{
    protected NavMeshAgent agent;
    protected Enemy instance;
    public State(NavMeshAgent navMeshAgent)
    {
        agent = navMeshAgent;
        instance=agent.GetComponent<Enemy>();
    }
    protected bool isExit = false;
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
