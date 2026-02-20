using System.ComponentModel;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isGamePaused = false;

    [SerializeField] 
    private GameObject pauseMenuUI;

    void Awake ()
    {
        pauseMenuUI.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
 
            if (isGamePaused)
            {
                Pause();
            } else
            {
                Resume();
            }
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
