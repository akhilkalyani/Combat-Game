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
    private float health = 60;
    private Idle idleState;
    private Chase chaseState;
    private Attack attackState;
    private State currentState;
    private bool isStateChanging = false;
    public StateType stateType;
    private MeshRenderer mesh;
    public bool isAlive = false;
    void Awake()
    {
        mesh=GetComponent<MeshRenderer>();
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
        if (!isAlive) return;
        if (!isStateChanging && currentState != null)
            currentState.Update();
    }

    internal bool TakeDamage(float hitPoint)
    {
        if (health > 0)
        {
            health -= hitPoint;
        }

        if (health <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    private void Die()
    {
        isAlive = false;
        currentState.Exit();
        mesh.enabled = false;
        GameController.Instance.UpdateEnemyCount();
    }

    internal void Spawn()
    {
        health = 100;
        mesh.enabled = true;
        isAlive = true;
        isStateChanging = false;
        ChangeState(idleState);
    }
}
