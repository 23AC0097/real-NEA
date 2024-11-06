using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphBuilderScript : MonoBehaviour
{
    public Sprite circleSprite;
    public RectTransform graphContainer;
    public CreatureDataHandlerScript creatureDataHandlerScript;
    public List<CreatureDataHandlerScript.SaveCreature> loadedCreatures;
    public List<GameObject> dots;

    private void Awake()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        loadedCreatures = creatureDataHandlerScript.Load();
    }
    public void SpeedGraph()
    {
        foreach (CreatureDataHandlerScript.SaveCreature saveObject in loadedCreatures)
        {
            CreateDot(new Vector2(saveObject.SaveTimeSpawned, saveObject.SaveSpeed * 20));
            
        }
    }
    public void SizeGraph()
    {
        foreach (CreatureDataHandlerScript.SaveCreature saveObject in loadedCreatures)
        {
            CreateDot(new Vector2(saveObject.SaveTimeSpawned, saveObject.SaveSize * 100));

        }
    }
    public void EyesightGraph()
    {
        foreach (CreatureDataHandlerScript.SaveCreature saveObject in loadedCreatures)
        {
            CreateDot(new Vector2(saveObject.SaveTimeSpawned, saveObject.SaveEyesight * 20));

        }
    }
    public void EmptyGraph()
    {
        foreach (GameObject dot in dots)
        {
            Destroy(dot, 0.5f);
        }
    }
    private void CreateDot(Vector2 anchorPos)
    {
        
        GameObject dot = new GameObject("dot", typeof(Image));
        dots.Add(dot);
        dot.transform.SetParent(graphContainer, false);
        dot.GetComponent<Image>().sprite = circleSprite;
        RectTransform dotRect = dot.GetComponent<RectTransform>();
        dotRect.anchoredPosition = anchorPos;
        dotRect.sizeDelta = new Vector2(20, 20);
        dotRect.anchorMin = new Vector2(0.05f, 0.05f);
        dotRect.anchorMax = new Vector2(0.05f, 0.05f);
    }

}
