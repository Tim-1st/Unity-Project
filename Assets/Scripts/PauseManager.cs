using System.ComponentModel;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isGamePaused = false;
    
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
    }

    private void Resume()
    {
        Time.timeScale = 1f;
    }
}
