using System.Collections;
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


        
         //Phone Controls
          //moveDirection = joystick.GetJoystickDirection();
        

        // PC Controls
        moveDirection.Normalize();
       


        //Uses the direction with movement speed, to actually move the Character
        Vector2 movement = moveDirection * currentSpeed;
        Vector2 newPos = currentPosition + movement * Time.fixedDeltaTime;
        rb.MovePosition(newPos);


        //Checks if moving
        if (moveDirection.magnitude > 0)
        { isMoving = true; }
        else
        { isMoving = false; }

        
        /*
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
        */
        
        

        
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
        
        
    }


    
    
    //PC Controls
    public void Move(InputAction.CallbackContext context)
    {
        moveDirection.x = inputs.actions["Move"].ReadValue<Vector2>().x;
        moveDirection.y = inputs.actions["Move"].ReadValue<Vector2>().y;
    }

    
    
    

    public void stopMoving()
    {
        isMoving = false;
    }
}
