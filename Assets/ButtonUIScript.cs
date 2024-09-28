using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUIScript : MonoBehaviour
{
    private string game = "Game";
    public void GoButton()
    {
        SceneManager.LoadScene(game);
    }
    
}
