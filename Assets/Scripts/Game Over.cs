using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void resetScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void playButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void quitButton()
    {
        Application.Quit();
    }
}
