using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float xAxis;
    float zAxis;

    public float VerticalMovement { get { return zAxis; } }
    public float HorizontalMovement { get { return xAxis; } }


    [SerializeField] float runningSpeed = 3f;
    [SerializeField] float walkingForward = 2f;
    [SerializeField] float walkingBackward = 1f;
    

    public CharacterController controller;

    float gravity = -9.81f;
    Vector3 velocity;

    bool isRunning;
    public bool dead = false;

    public bool IsRunning { get { return isRunning; } }
    
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        isRunning = false;
    }

    void Update()
    {
        if (!dead)
        {
            GetInput();
        }

        AddingGravity();
    }
   
    void GetInput()
    {
        xAxis = Input.GetAxis("Horizontal") * Time.deltaTime;
        zAxis = Input.GetAxis("Vertical") * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (zAxis > 0)
        {
            if (isRunning)
            {
                ApplyMovementSpeed(runningSpeed);
            }
            else
            {
                ApplyMovementSpeed(walkingForward);
            }
        }
        else if (zAxis < 0)
        {
            ApplyMovementSpeed(walkingBackward);
        }
        else
        {
            ApplyMovementSpeed(walkingBackward);
        }
    }
    void ApplyMovementSpeed(float movementSpeed)
    {
        zAxis *= movementSpeed * Time.deltaTime;
        xAxis *= movementSpeed * Time.deltaTime;

        Vector3 move = (transform.right * xAxis + transform.forward * zAxis).normalized;
        
        controller.Move(move * movementSpeed);
    }
    
    void AddingGravity()
    {
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
