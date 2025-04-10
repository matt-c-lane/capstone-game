using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("PlayerCreation"); // Replace with your actual game scene name
    }

    public void ContinueGame()
    {
        //Empty
    }

    public void OpenOptions()
    {
        //Empty
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}