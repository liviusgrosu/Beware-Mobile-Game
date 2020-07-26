using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyProjectileAdvancedMovement : MonoBehaviour, IEnemyMovement
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
        set {
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

    [HideInInspector]
    public enum BehaviourState
    {
        Attacking,
        Moving
    }

    private BehaviourState _behaviorState;
    public BehaviourState behaviorState {
        get { return _behaviorState; }
        set {
            _behaviorState = value;
            if (value == BehaviourState.Attacking) behaviourTime = attackingTime;
            else if (value == BehaviourState.Moving) behaviourTime = movementTime;
        }
    }

    [SerializeField]
    private float movementTime, attackingTime;
    private float behaviourTime;


    //Awake is used for setting up current components
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        currMovementSpeed = regMovementSpeed;
        currMovementState = MovementState.Regular;

        behaviorState = BehaviourState.Moving;
        behaviourTime = movementTime;

        StartCoroutine(BehaviourRoutine());
    }

    //Start is used for setting up with other components
    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {


        switch (behaviorState)
        {
            case BehaviourState.Attacking:
                //Start attacking
                agent.isStopped = true;
                break;
            case BehaviourState.Moving:
                agent.destination = player.position;
                agent.isStopped = (Vector3.Distance(transform.position, player.position) <= 1.5f);
                break;
        }
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
    IEnumerator BehaviourRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(behaviourTime);
            switch (behaviorState)
            {
                case BehaviourState.Attacking:
                    behaviorState = BehaviourState.Moving;
                    break;
                case BehaviourState.Moving:
                    behaviorState = BehaviourState.Attacking;
                    break;
            }

        }
    }

    public void SetMovementSpeed()
    {
        print("Accessing this fine....");
    }
}
