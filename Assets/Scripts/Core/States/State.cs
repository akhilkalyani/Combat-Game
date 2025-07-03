using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum StateType{ Idle,Chase,Attack};
public abstract class State
{
    protected Transform player;
    protected NavMeshAgent agent;
    protected Enemy instance;
    public StateType type;
    public State(NavMeshAgent navMeshAgent,Transform target)
    {
        player = target;
        agent = navMeshAgent;
        instance = agent.GetComponent<Enemy>();
    }
    protected bool isExit = false;
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
