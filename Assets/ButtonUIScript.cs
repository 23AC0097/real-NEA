using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ButtonUIScript : MonoBehaviour
{
    private string game = "Game";
    public Slider sizeSlider;
    private string path;
    public void GoButton()
    {
        path = @"""C:\projects\NEAProj\Assets\test.txt";
        StreamWriter streamWriter = new StreamWriter(path, true);
        streamWriter.Write("Test");
        SceneManager.LoadScene(game);
    }
    
}
