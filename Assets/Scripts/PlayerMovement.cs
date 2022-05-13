using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.FreezePositionY;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Movement();
    }
   
    void Movement()
    {
        rb.freezeRotation = true;
        float xAxis = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        float zAxis = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        transform.Translate(xAxis, 0, zAxis);
        rb.freezeRotation = false;
        
    }
}
