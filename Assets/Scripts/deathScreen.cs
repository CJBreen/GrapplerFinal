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
        deathUI.SetActive(false);
        
    }
    

    public void notDead()
    {
        deathUI.SetActive(false);
        HUD.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        isDead = false;
    }

    public void playerDeath()
    {
        deathUI.SetActive(true);
        HUD.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        isDead = true;
    }
    
    
    public void returntoMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void retryGame()
    {
        SceneManager.LoadScene("Arena");
    }
    
    
}
