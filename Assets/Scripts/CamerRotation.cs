using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerRotation : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100;

    float yRotationForCamera = 0f;
    float yRotationForTorch;

    float MinCameraRotationValue = -30f;
    float MaxCameraRotationValue = 20f;

    float mouseX;
    float mouseY;

    Transform Player;
    Transform Torch;
  
    private void Start()
    {
       Player = transform.parent;
       Torch = transform.parent.GetChild(1);
       yRotationForTorch = Torch.localRotation.eulerAngles.x;
    }

    private void Update()
    {
        GetMouseInput();
        CameraRotation();
        PlayerBodyRotation();
        TorchRotation();
    }

    void GetMouseInput()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
    }

    private void CameraRotation()
    {
        yRotationForCamera -= mouseY;
        yRotationForCamera = Mathf.Clamp(yRotationForCamera, MinCameraRotationValue, MaxCameraRotationValue);
        transform.localRotation = Quaternion.Euler(yRotationForCamera, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
    }

    void PlayerBodyRotation()
    {
        Player.transform.Rotate(Vector3.up * mouseX);
    }

    void TorchRotation()
    {
        float cameraXRotationValue;
        
        if (transform.localRotation.eulerAngles.x > 340)
        {
            cameraXRotationValue = transform.localEulerAngles.x - 360;
        }
        else
        {
            cameraXRotationValue = transform.localEulerAngles.x;
        }


        if (cameraXRotationValue >= -10 && cameraXRotationValue <= 10)
        {
            yRotationForTorch -= mouseY;
            yRotationForTorch = Mathf.Clamp(yRotationForTorch, 10, 25);
            Torch.localRotation = Quaternion.Euler(yRotationForTorch, Torch.localRotation.eulerAngles.y, Torch.localRotation.eulerAngles.z);
        }
    }
}
