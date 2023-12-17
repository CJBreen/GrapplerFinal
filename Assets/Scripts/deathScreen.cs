using UnityEngine;
using UnityEngine.SceneManagement;

public class deathScreen : MonoBehaviour
{

    //same as the pause UI, i just need to make some methods to call for in a separate script
    public GameObject deathUI;
    public GameObject HUD;
    public static bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        
        //automatically hiding death screen
        deathUI.SetActive(false);
        
    }
    


    
    //when the player isn't dead
    public void notDead()
    {
        deathUI.SetActive(false);
        HUD.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        isDead = false;
    }

    
    //when player dies
    public void playerDeath()
    {
        deathUI.SetActive(true);
        HUD.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        isDead = true;
    }
    
    //functions to go to main menu and to restart
    public void returntoMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void retryGame()
    {
        SceneManager.LoadScene("Arena");
    }
    
    
}
