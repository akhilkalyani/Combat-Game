using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject sensor;
    public Transform[] points;
    private NavMeshAgent agent;
    private float health = 50;
    private Idle idleState;
    private Attack attackState;
    private Chase chaseState;
    private State currentState;
    private bool isStateChanging = false;
    internal Vector3 shootPoint;

    void Awake()
    {
        shootPoint = transform.forward;
        agent = GetComponent<NavMeshAgent>();
        idleState = new Idle(agent, points);
        attackState=new Attack(agent);
        chaseState =new Chase(agent,attackState);
    }
    void Start()
    {
        ChangeState(idleState);
    }
    void OnEnable()
    {
        SensorEvents.OnPlayerEnteredArena += ChasePlayer;
        SensorEvents.OnPlayerExitedArena += StopPlayer;
    }

    private void StopPlayer(string sensorName)
    {
        if (sensor.name == sensorName)
        {
            ChangeState(idleState);
        }
    }

    private void ChasePlayer(string sensorName, Transform player)
    {
        if (sensorName == sensor.name)
        {
            chaseState.target = player;
            ChangeState(chaseState);
        }
    }
    public void ChangeState(State state)
    {
        isStateChanging = true;
        if (currentState != null)
            currentState.Exit();
        currentState = state;
        currentState.Enter();
        isStateChanging = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isStateChanging && currentState != null)
            currentState.Update();
    }
    void OnDisable()
    {
        SensorEvents.OnPlayerEnteredArena -= ChasePlayer;
    }
}
