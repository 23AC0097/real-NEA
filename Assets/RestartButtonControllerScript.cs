using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonControllerScript : MonoBehaviour
{
    private string mainmenu = "MainMenu";
    public void RestartButton()
    {
        SceneManager.LoadScene(mainmenu);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
