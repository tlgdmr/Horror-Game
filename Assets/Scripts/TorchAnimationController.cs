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
        TorchAnimation.enabled = false;
        PlayerMovement = FindObjectOfType<PlayerMovement>();
    }

    
    void Update()
    {
        WalkingTorchAnimation();
    }
    void WalkingTorchAnimation()
    {
        if (PlayerMovement.VerticalMovement != 0 || PlayerMovement.HorizontalMovement != 0)
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
