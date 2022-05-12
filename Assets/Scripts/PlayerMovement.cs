using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10;
    float xAxis;
    float zAxis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        zAxis = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        transform.Translate(xAxis, 0, zAxis);
        
    }
}
