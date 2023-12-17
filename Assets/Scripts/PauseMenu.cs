using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    //checking if paused
    public static bool isPaused;

    //referencing UI elements
    public GameObject pauseUI;
    public GameObject HUD;


    private void Start()
    {
        //automatically having the game in a "resumed" state
        resumeGame();
    }


    void Update()
    {
        
        //if player presses pause key, check if paused, if not, pause the game
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

    
    //method for resuming the game
    private void resumeGame()
    {
        pauseUI.SetActive(false);
        HUD.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        isPaused = false;
    }

    //method for pausing the game
    private void pauseGame()
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