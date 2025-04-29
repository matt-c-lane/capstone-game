using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuManager : MonoBehaviour
{
    public void DeathButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}