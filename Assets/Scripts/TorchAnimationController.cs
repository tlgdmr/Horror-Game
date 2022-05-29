using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchAnimationController : MonoBehaviour
{
    Animator TorchAnimation;
    PlayerMovement playerMovement;
    
    void Start()
    {
        TorchAnimation = GetComponent<Animator>();
        TorchAnimation.enabled = false;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    
    void Update()
    {
        if (playerMovement.VerticalMovement != 0)
        {
            if (!TorchAnimation.isActiveAndEnabled)
            {
                TorchAnimation.enabled = true;
            }
        }
        else
        {
            TorchAnimation.enabled = false;
        }
    }
}
