using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] GameObject GameOverImage;
    [SerializeField] Animator GameOverAnimation;
    [SerializeField] TMP_Text finalScore;

    ScoreManager score;
    void Start()
    {
        GameOverImage.SetActive(false);
        GameOverAnimation.enabled = false;
        score = GetComponent<ScoreManager>();
    }

    public void ShowGameOverImage()
    {
        GameOverImage.SetActive(true);
        finalScore.text = $"Score: {score.Score}";
        GameOverAnimation.speed = 0.10f;
        GameOverAnimation.enabled = true;
    }
}
