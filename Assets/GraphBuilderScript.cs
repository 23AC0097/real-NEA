using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphBuilderScript : Graphic
{
    public Sprite circleSprite;
    public RectTransform graphContainer;
    public CreatureDataHandlerScript creatureDataHandlerScript;
    public GameTimerScript gameTimerScript;
    public List<CreatureDataHandlerScript.SaveCreature> loadedCreatures;
    public List<GameObject> dots;

    protected override void Awake()
    {
        loadedCreatures = creatureDataHandlerScript.Load();
    }
    public void SpeedGraph()
    {
        foreach (CreatureDataHandlerScript.SaveCreature saveObject in loadedCreatures)
        {
            CreateDot(new Vector2(0, saveObject.SaveSpeed * 20), saveObject);
            
        }
    }
    public void SizeGraph()
    {
        foreach (CreatureDataHandlerScript.SaveCreature saveObject in loadedCreatures)
        {
            CreateDot(new Vector2(0, saveObject.SaveSize * 20), saveObject);

        }
    }
    public void EyesightGraph()
    {
        foreach (CreatureDataHandlerScript.SaveCreature saveObject in loadedCreatures)
        {
            CreateDot(new Vector2(0, saveObject.SaveEyesight * 20), saveObject);

        }
    }
    public void LineOfBestFit()
    {
        float gradient = 0;
        float allXY = 0;
        float allXSquared = 0;
        float xmean = 0;
        float ymean = 0;
        foreach (GameObject dot in dots)
        {
            xmean += dot.transform.position.x;
            ymean += dot.transform.position.y;
        }
        xmean = xmean / dots.Count;
        ymean = ymean / dots.Count;
        foreach(GameObject dot in dots)
        {
            allXY += (dot.transform.position.x - xmean) * (dot.transform.position.y);
            allXSquared += (dot.transform.position.x - xmean) * (dot.transform.position.x - xmean);
        }
        gradient = allXY / allXSquared;
    }
    public void EmptyGraph()
    {
        foreach (GameObject dot in dots)
        {
            Destroy(dot, 0.1f);
        }
        dots.Clear();
    }
    private void CreateDot(Vector2 anchorPos, CreatureDataHandlerScript.SaveCreature saveObject)
    {
        width = graphContainer.rect.width;
        height = graphContainer.rect.height;
        float timePos = saveObject.SaveTimeSpawned / gameTimerScript.saveGameTimer;
        timePos = timePos * width;
        anchorPos.x = timePos;
        GameObject dot = new GameObject("dot", typeof(Image));
        dots.Add(dot);
        dot.transform.SetParent(graphContainer, false);
        dot.GetComponent<Image>().sprite = circleSprite;
        RectTransform dotRect = dot.GetComponent<RectTransform>();
        dotRect.anchoredPosition = anchorPos;
        dotRect.sizeDelta = new Vector2(20, 20);
        dotRect.anchorMin = new Vector2(0, 0);
        dotRect.anchorMax = new Vector2(0, 0);
    }
    public Vector2Int gridSize;

    public List<Vector2> points;

    public float width;
    public float height;
    public float unitWidth;
    public float unitHeight;

    public float thickness = 10f;
    

}
