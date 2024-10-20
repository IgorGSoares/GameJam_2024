using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private GameObject UIPanel;
    private GameObject pauseMenu;

    private bool isPaused = false;

    private void Start()
    {
        UIPanel = transform.Find("Panel").gameObject;
        pauseMenu = transform.Find("PauseMenu").gameObject;

        if (UIPanel != null)
        {
            Debug.Log("UIPanel encontrado");
        }
        if (pauseMenu != null)
        {
            Debug.Log("menuPanel encontrado");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            UIPanel.SetActive(false);
            pauseMenu.SetActive(true);
        }
        else if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
            pauseMenu.SetActive(false);
            UIPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
