using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonUIScript : MonoBehaviour
{
    public Slider sizeSlider;
    public Slider speedSlider;
    public Slider eyesightSlider;
    public Slider foodSpawnRateSlider;
    public Slider numOfPredatorsSlider;
    public Slider numOfPreySlider;
    private string game = "Game";
    private string path;
    public void GoButton()
    {
        path = @"C:\projects\NEAProj\Assets\test.txt";
        File.WriteAllText(path, "");
        using (StreamWriter sw = File.AppendText(path))
        {
            sw.WriteLine(sizeSlider.value);
            sw.WriteLine(speedSlider.value);
            sw.WriteLine(eyesightSlider.value);
            sw.WriteLine(foodSpawnRateSlider.value);
            sw.WriteLine(numOfPredatorsSlider.value);
            sw.WriteLine(numOfPreySlider.value);
        }
        SceneManager.LoadScene(game);
    }
    
}
