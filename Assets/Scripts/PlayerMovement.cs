using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float xAxis;
    float zAxis;

    public float VerticalMovement { get { return zAxis; } }


    float walkingSpeed = 8;
    float movementSpeed;

    public CharacterController controller;

    float gravity = -9.81f;
    Vector3 velocity;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        movementSpeed = walkingSpeed;
    }

    void Update()
    {
        Movement();
    }
   
    void Movement()
    {
       
        xAxis = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        zAxis = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        Vector3 move = transform.right * xAxis + transform.forward * zAxis;

        controller.Move(move);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //transform.Translate(xAxis, 0, zAxis);
    }
}
