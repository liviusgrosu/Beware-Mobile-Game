using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectilePrimitiveMovement : MonoBehaviour
{
    private Vector3 _currDirection { get; set; }
    private float _currMovementSpeed;

    [SerializeField]
    private float slowMovementSpeed = 0.05f;

    [SerializeField]
    private float regMovementSpeed = 0.1f;

    [SerializeField]
    private float fastMovementSpeed = 0.15f;

    public float currMovementSpeed {
        get { return _currMovementSpeed; }
        set { _currMovementSpeed = value; }
    }

    public Vector3 currDirection {
        get { return _currDirection; }
        set { _currDirection = value; }
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
    public BehaviourState behaviorState 
    {
        get { return _behaviorState; }
        set 
        {
            _behaviorState = value;
            if (value == BehaviourState.Attacking) behaviourTime = attackingTime;
            else if (value == BehaviourState.Moving) behaviourTime = movementTime;
        }
    }

    [SerializeField]
    private float movementTime, attackingTime; 
    private float behaviourTime;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        currMovementSpeed = regMovementSpeed;
        currMovementState = MovementState.Regular;

        behaviorState = BehaviourState.Moving;
        behaviourTime = movementTime;
        GenerateNewMovingDirection();

        StartCoroutine(BehaviourRoutine());
    }

    private void Update()
    {
        if (GetComponent<EnemySpawnTravel>() != null) return;
        switch (behaviorState)
        {
            case BehaviourState.Attacking:
                break;
            case BehaviourState.Moving:
                rb.MovePosition(transform.position + currDirection * currMovementSpeed);
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
                    GenerateNewMovingDirection();
                    break;
                case BehaviourState.Moving:
                    behaviorState = BehaviourState.Attacking;
                    break;
            }

        }
    }

    void GenerateNewMovingDirection()
    {
        int[] directions = new int[] { 1, -1 };
        currDirection = new Vector3(directions[Random.Range(0, 2)], 0, directions[Random.Range(0, 2)]);
    }
}
