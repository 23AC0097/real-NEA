using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsButtonControllerScript : MonoBehaviour
{
    public GameObject graphCanvas;
    public void StatsButton()
    {
        graphCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
