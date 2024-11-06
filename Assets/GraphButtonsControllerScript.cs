using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphButtonsControllerScript : MonoBehaviour
{
    public GraphBuilderScript graphBuilderScript;
    public void SpeedPressed()
    {
        graphBuilderScript.EmptyGraph();
        graphBuilderScript.SpeedGraph();
    }
    public void SizePressed()
    {
        graphBuilderScript.EmptyGraph();
        graphBuilderScript.SizeGraph();
    }
    public void EyesightPressed()
    {
        graphBuilderScript.EmptyGraph();
        graphBuilderScript.EyesightGraph();
    }
}
