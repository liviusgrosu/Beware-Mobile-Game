using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    private DynamicJoystickHandler movementJoystickListener;

    private Vector3 inputDirection;
    private float zMove, xMove;
    public float rotationSpeed = 3f;

    private float _currMovementSpeed;

    private Vector3 prevPos, curPos;

    //Regular movementspeed changes depending on the enemy varient

    [SerializeField]
    private float slowMovementSpeed = 0.05f;

    [SerializeField]
    private float regMovementSpeed = 0.1f;

    [SerializeField]
    private float fastMovementSpeed = 0.15f;

    [SerializeField]
    private Animator modelAnimator; 

    public float currMovementSpeed {
        get { return _currMovementSpeed; }
        set { _currMovementSpeed = value; }
    }

    private EnumDefinitions.MovementState currMovementState, prevMovementState;

    private void Awake()
    {
        currMovementState = EnumDefinitions.MovementState.Regular;

        prevPos = curPos = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementJoystickListener = GameObject.Find("Virtual Joystick Handler").GetComponent<DynamicJoystickHandler>();

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
        inputDirection *= currMovementSpeed;

        if (inputDirection.magnitude != 0)
        {
            rb.MovePosition(transform.position + inputDirection);
            if (!modelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Running Cycle"))
            {
                modelAnimator.SetTrigger("Start Running");
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            if (!modelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle Cycle") &&
                !modelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shooting Cycle"))
            {
                modelAnimator.SetTrigger("Start Idling");
            }
        }
    }

    //Deal with slowing down
    public void ChangeMovementState(EnumDefinitions.MovementState state)
    {
        if (state == currMovementState) return;

        switch (state)
        {
            case EnumDefinitions.MovementState.Slow:
                currMovementSpeed = slowMovementSpeed;
                break;
            case EnumDefinitions.MovementState.Regular:
                currMovementSpeed = regMovementSpeed;
                break;
            case EnumDefinitions.MovementState.Fast:
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

    public float GetMovingSpeed()
    {
        return inputDirection.magnitude;
    }
}
