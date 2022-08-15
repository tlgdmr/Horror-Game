using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMenuManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;

    private void Start()
    {
        PauseMenu.SetActive(false);
    }

    void Update()
    {
        StopTheGame();
    }
    public void StopTheGame()
    {
        if (Time.timeScale == 1 && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }
    public void NewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
