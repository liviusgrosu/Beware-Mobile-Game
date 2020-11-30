using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCollisionAdvancedMovement : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private float _currMovementSpeed;

    [SerializeField]
    private float slowMovementSpeed = 0.05f;

    [SerializeField]
    private float regMovementSpeed = 0.1f;

    [SerializeField]
    private float fastMovementSpeed = 0.15f;

    public float currMovementSpeed {
        get { return _currMovementSpeed; }
        set 
        { 
            _currMovementSpeed = value;
            agent.speed = value;
        }
    }
    public enum MovementState
    {
        Slow,
        Regular,
        Fast
    }

    private MovementState currMovementState;

    //Awake is used for setting up current components
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        currMovementSpeed = regMovementSpeed;

        currMovementState = MovementState.Regular;
    }

    //Start is used for setting up with other components
    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (GetComponent<EnemySpawnTravel>() != null) return;
        if (agent == null) 
        {
            Debug.Log("agent doesnt exist");
        }
        agent.destination = player.position;
        agent.isStopped = (Vector3.Distance(transform.position, player.position) <= agent.stoppingDistance);
    }

    //Deal with slowing down
    public void ChangeMovementState(MovementState state)
    {
        if (state == currMovementState) return;

        switch (state)
        {
            case MovementState.Slow:
                currMovementSpeed = slowMovementSpeed;
                break;
            case MovementState.Regular:
                currMovementSpeed = regMovementSpeed;
                break;
            case MovementState.Fast:
                currMovementSpeed = fastMovementSpeed;
                break;
        }
        currMovementState = state;
    }
}
