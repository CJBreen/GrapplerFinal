using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused;

    public GameObject pauseUI;
    public GameObject HUD;


    private void Start()
    {
        resumeGame();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void resumeGame()
    {
        pauseUI.SetActive(false);
        HUD.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void pauseGame()
    {
        pauseUI.SetActive(true);
        HUD.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        isPaused = true;

    }


public void openGit()
    {
        Application.OpenURL("https://github.com/CJBreen/GrapplerFinal");
    }

    public void returntoMain()
    {
        SceneManager.LoadScene("MainMenu");
    }














}