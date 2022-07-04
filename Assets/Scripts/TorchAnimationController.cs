using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchAnimationController : MonoBehaviour
{
    Animator TorchAnimation;
    public Animator _TorchAnimation { get { return TorchAnimation; } }
    PlayerMovement PlayerMovement;
    
    void Start()
    {
        TorchAnimation = GetComponent<Animator>();
        PlayerMovement = FindObjectOfType<PlayerMovement>();
    }

    
    void Update()
    {
        WalkingTorchAnimation();
        FiringTorchAnimation();
    }

    public void FiringTorchAnimation()
    {
        if (Input.GetMouseButton(0))
        {
            TorchAnimation.SetBool("Shooting", true);
        }
        else
        {
            TorchAnimation.SetBool("Shooting", false);
        }
        
    }
    
    void WalkingTorchAnimation()
    {
        if (PlayerMovement.VerticalMovement != 0 || PlayerMovement.HorizontalMovement != 0)
        {
            TorchAnimation.SetBool("Walking", true);
        }
        else
        {
            TorchAnimation.SetBool("Walking", false);
        }
    }
}
