using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] Slider slider;
    [SerializeField] Image fill;
    [SerializeField] Gradient gradient;

    Animator PlayerDeadAnim;

    int playerHealth = 100;
    int damageToPlayer = 25;

    GameOverHandler gameOverHandler;
    PlayerMovement checkDead;

    void Start()
    {
        PlayerDeadAnim = GetComponent<Animator>();
        gameOverHandler = FindObjectOfType<GameOverHandler>();
        checkDead = GetComponent<PlayerMovement>();
        slider.maxValue = playerHealth;
        PlayerDeadAnim.enabled = false;
    }
    
    public void DecreasePlayerHealth()
    {
        slider.value -= damageToPlayer;
        fill.color = gradient.Evaluate(slider.normalizedValue);

        if (slider.value <= 0)
        {
           checkDead.dead = true;
           StartCoroutine(ActivePlayerDeadAnimation());
        }
    }

    IEnumerator ActivePlayerDeadAnimation()
    {
        PlayerDeadAnim.speed = 0.15f;
        PlayerDeadAnim.enabled = true;
        yield return new WaitForSeconds(3);
        gameOverHandler.ShowGameOverImage();
    }
}
