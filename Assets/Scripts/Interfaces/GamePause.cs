using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public GameObject pauseButton;
    public GameObject inventoryPanel;
    public bool usingPausePanel;
    void Start()
    {
        isPaused = false;
        usingPausePanel = false;
    }

    // Update is called once per frame
    public void OnPause()
    {
        isPaused = !isPaused;
        Pause();

    }
    private void Pause()
    {
            if (isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
                pauseButton.SetActive(false);
                pausePanel.SetActive(true);
                usingPausePanel = true;
        }
            else
            {
                inventoryPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
                pauseButton.SetActive(true);
                pausePanel.SetActive(false);
        }
    }
    public void QuitMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
        isPaused = false;
        pausePanel.SetActive(false);
    }
    public void SwitchPanels()
    {
        usingPausePanel = !usingPausePanel;
        if (usingPausePanel)
        {
            pausePanel.SetActive(true);
            inventoryPanel.SetActive(false);
        }
        else
        {
            pausePanel.SetActive(false);
            inventoryPanel.SetActive(true);
        }
    }
  
}
