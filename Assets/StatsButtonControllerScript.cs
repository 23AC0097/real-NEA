using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsButtonControllerScript : MonoBehaviour
{
    public GameObject seeStatsCanvas;
    public void StatsButton()
    {
        seeStatsCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
