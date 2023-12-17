using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //using SceneManager for the methods
    public void PlayGame()
    {
        SceneManager.LoadScene("Arena");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Successfully Quit");
        
    }

}
