using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public Transform[] points;
    private NavMeshAgent agent;
    private float health = 50;
    private Idle idleState;
    private Chase chaseState;
    private Attack attackState;
    private State currentState;
    private bool isStateChanging = false;
    public StateType stateType;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        idleState = new Idle(agent, player, points);
        attackState = new Attack(agent, player);
        chaseState = new Chase(agent, player, attackState);
        attackState.SetNextState(idleState);
        idleState.SetNextState(chaseState);
    }
    void Start()
    {
        ChangeState(idleState);
    }
    public void ChangeState(State state)
    {
        isStateChanging = true;
        if (currentState != null)
            currentState.Exit();
        currentState = state;
        currentState.Enter();
        isStateChanging = false;
        stateType = currentState.type;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isStateChanging && currentState != null)
            currentState.Update();
    }
}
