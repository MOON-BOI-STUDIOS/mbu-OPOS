using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveDirection;
    public bool isMoving;
    public float moveSpeed;
    public float runSpeed;
    private float currentSpeed;
    public bool isRunning;
    public PlayerInput inputs;
    public CustomJoystick joystick;
    private PlayerManager _manager;

    void Start()
    {
        _manager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;

        // Subscribe to the Move action events
        inputs.actions["Move"].performed += Move;
        inputs.actions["Move"].canceled += Move;
    }

    void FixedUpdate()
    {
        UpdateMoveDirection();
        HandleMovement();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
        Debug.Log(moveDirection);
    }

    private void UpdateMoveDirection()
    {
#if !UNITY_STANDALONE && !UNITY_WEBGL
        moveDirection = joystick.GetJoystickDirection();
#endif
    }

    private void HandleMovement()
    {
        Vector2 currentPosition = rb.position;
        Vector2 movement = moveDirection * currentSpeed;
        Vector2 newPos = currentPosition + movement * Time.fixedDeltaTime;
        rb.MovePosition(newPos);

        isMoving = moveDirection.magnitude > 0;
        UpdateRunningState();
    }

    private void UpdateRunningState()
    {
        if (moveDirection.magnitude >= 0.7f)
        {
#if UNITY_STANDALONE || UNITY_WEBGL
            if (Input.GetButton("Fire3"))
            {
                currentSpeed = runSpeed;
                isRunning = true;
                return;
            }
#endif
            currentSpeed = runSpeed;
            isRunning = true;
        }
        else
        {
            currentSpeed = moveSpeed;
            isRunning = false;
        }
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveDirection;
    public bool isMoving;
    public float moveSpeed;
    public float runSpeed;
    private float currentSpeed;
    public bool isRunning;
    private PlayerManager _manager;
    public PlayerInput inputs;
    public CustomJoystick joystick;
    
    void Start()
    {
        _manager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
        
    {
        //Gets current position and input from the RigidBody
        Vector2 currentPosition = rb.position;




          moveDirection = joystick.GetJoystickDirection();

        /*
#if UNITY_WEBGL || UNITY_STANDALONE
        moveDirection.Normalize();
#endif

/*-//
        //Uses the direction with movement speed, to actually move the Character
        Vector2 movement = moveDirection * currentSpeed;
        Vector2 newPos = currentPosition + movement * Time.fixedDeltaTime;
        rb.MovePosition(newPos);


        //Checks if moving
        if (moveDirection.magnitude > 0)
        { isMoving = true; }
        else
        { isMoving = false; }



        //Makes Dre run, if the joystick is further from it's centre (Phone Controls)
        if (moveDirection.magnitude >= 0.7f)
        {
            currentSpeed = runSpeed;
            isRunning = true;
        }
        else
        {
            currentSpeed = moveSpeed;
            isRunning = false;
        }
        
        /*


#if UNITY_WEBGL || UNITY_STANDALONE
        //Makes Dre run, if shift is pressed/right shoulder button on a controller is pressed (PC Controls)
        if (moveDirection.magnitude >= 0.7f && Input.GetButton("Fire3"))
        {
            currentSpeed = runSpeed;
            isRunning = true;
        }
        else
        {
            currentSpeed = moveSpeed;
            isRunning = false;
        }
#endif

    *-/
    }




    //PC Controls

    public void Move(InputAction.CallbackContext context)
    {
        moveDirection.x = inputs.actions["Move"].ReadValue<Vector2>().x;
        moveDirection.y = inputs.actions["Move"].ReadValue<Vector2>().y;
        Debug.Log(moveDirection);
    }

    



    public void stopMoving()
    {
        isMoving = false;
    }
}
*/
