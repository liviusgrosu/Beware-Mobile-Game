using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    private MovementJoystickListener movementJoystickListener;

    private Vector3 inputDirection;
    private float zMove, xMove;
    public float rotationSpeed = 3f;

    private float _currMovementSpeed;


    //Regular movementspeed changes depending on the enemy varient

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

    public enum MovementState
    {
        Slow,
        Regular,
        Fast
    }

    private MovementState currMovementState, prevMovementState;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementJoystickListener = GameObject.Find("Virtual Joystick Background").GetComponent<MovementJoystickListener>();

        currMovementSpeed = regMovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        ReadPlayerInput();
    }

    private void ReadPlayerInput()
    {
        inputDirection = Vector3.zero;

        xMove = movementJoystickListener.Horizontal();
        zMove = movementJoystickListener.Vertical();

        inputDirection = new Vector3(xMove, 0, zMove);
        inputDirection = inputDirection.normalized * currMovementSpeed;

        if (inputDirection.magnitude != 0)
            rb.MovePosition(transform.position + inputDirection);
        else
            rb.velocity = Vector3.zero;
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
        prevMovementState = currMovementState;
        currMovementState = state;
    }

    public bool IsMoving()
    {
        return zMove != 0 || xMove != 0;
    }

    public Vector3 GetMovingDirection()
    {
        return inputDirection;
    }
}
