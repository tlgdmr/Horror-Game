using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerRotation : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100;

    float yRotation = 0f;
    Transform Player;

    private void Update()
    {
        Player = transform.parent;
        CameraRotation();
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation -= mouseY;

        yRotation = Mathf.Clamp(yRotation, -20f, 20f);

        transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
        Player.transform.Rotate(Vector3.up * mouseX );
    }
}
