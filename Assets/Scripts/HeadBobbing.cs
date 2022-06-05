using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    [SerializeField] float runBobSpeed = 13;
    [SerializeField] float runBobAmount = 0.01f;
    [SerializeField] float walkBobSpeed = 13;
    [SerializeField] float walkBobAmount = 0.01f;

    float timer;

    PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        HeadBobController();
    }
    void HeadBobController()
    {
        if (Mathf.Abs(playerMovement.VerticalMovement) > 0 || Mathf.Abs(playerMovement.HorizontalMovement) > 0)
        {
            timer += Time.deltaTime * (playerMovement.IsRunning ? runBobSpeed : walkBobSpeed);

            transform.localPosition = new Vector3(transform.localPosition.x,
            transform.localPosition.y + Mathf.Sin(timer) * (playerMovement.IsRunning ? runBobAmount : walkBobAmount),
            transform.localPosition.z);
        }
    }
}
