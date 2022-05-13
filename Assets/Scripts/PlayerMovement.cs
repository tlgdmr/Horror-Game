using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10;
    [SerializeField] float mouseSensitivity = 100;

    float yRotation = 0f;

    Transform Player;

    private void Start()
    {
        Player = gameObject.transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        float zAxis = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        Player.transform.Translate(xAxis, 0, zAxis);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation -= mouseY;

        yRotation = Mathf.Clamp(yRotation, -20f, 20f);

        transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
        Player.transform.Rotate(Vector3.up * mouseX);

       
    }
}
