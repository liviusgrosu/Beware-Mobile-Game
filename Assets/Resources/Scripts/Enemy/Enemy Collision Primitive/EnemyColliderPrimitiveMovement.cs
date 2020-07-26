using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class EnemyColliderPrimitiveMovement : MonoBehaviour, IEnemyMovement
{
    private Vector3 _currDirection { get; set; }
    private float _currMovementSpeed;

    //Regular movementspeed changes depending on the enemy varient

    [SerializeField]
    private float slowMovementSpeed = 0.05f;

    [SerializeField]
    private float regMovementSpeed = 0.1f;

    [SerializeField]
    private float fastMovementSpeed = 0.15f;

    public float currMovementSpeed 
    {
        get { return _currMovementSpeed; }
        set { _currMovementSpeed = value; }
    }

    public Vector3 currDirection 
    {
        get { return _currDirection; }
        set { _currDirection = value; }
    }

    public enum MovementState
    {
        Slow,
        Regular,
        Fast
    }

    private MovementState currMovementState, prevMovementState;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        currMovementSpeed = regMovementSpeed;
        currMovementState = MovementState.Regular;

        int[] directions = new int[] { 1, -1 };
        currDirection = new Vector3(directions[Random.Range(0, 2)], 0, directions[Random.Range(0, 2)]);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + currDirection * currMovementSpeed);
    }

    //Deal with slowing down
    public void ChangeMovementState(MovementState state)
    {
        if (state == currMovementState) return;

        switch(state)
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
        prevMovementState = currMovementState;
        currMovementState = state;
    }

    public void SetMovementSpeed()
    {
        
    }
}
