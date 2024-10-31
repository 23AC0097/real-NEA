using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGridScript : MonoBehaviour
{
    public GameTimerScript GameTimerScript;
    public GameObject statsButton;
    public RectTransform content;
    public void PopulateStatsGrid()
    {
        foreach (GameObject go in GameTimerScript.creatures)
        {
            GameObject newGridButton = Instantiate(statsButton, Vector2.zero, Quaternion.identity);
            if (go.GetComponent<CreatureScript>().dead)
            {
                newGridButton.GetComponent<Text>().text = "Status: Dead \n Time Alive: " + go.GetComponent<CreatureScript>().timeAlive.ToString();
            }
            else
            {
                newGridButton.GetComponent<Text>().text = "Status: Alive \n Time live: " + go.GetComponent<CreatureScript>().timeAlive.ToString();
            }
            newGridButton.transform.SetParent(content);
        }
    }
}
