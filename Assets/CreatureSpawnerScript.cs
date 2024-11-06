using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class CreatureSpawnerScript : MonoBehaviour
{
    public SizeSliderScript sizeSliderScript;
    public FoodSpawnerScript foodSpawner;
    public GameObject Creature;
    public bool firstGameStart;
    public float spawnrate = 6;
    public float timer = 0;
    public float variation = 3;
    public float gameTimer = 60;
    public float secondTimer;
    private string path = @"C:\projects\NEAProj\Assets\test.txt";
    private float[] stats = new float[6];
    public float size = 0;
    public float speed = 0;
    public float eyesight = 0;
    public float foodSpawn = 0;
    public float numPred = 0;
    public float numPrey = 0;
    // Start is called before the first frame update
    void Start()
    {
        stats[0] = size;
        stats[1] = eyesight;
        stats[2] = speed;
        stats[3] = foodSpawn;
        stats[4] = numPred;
        stats[5] = numPrey;
        string[] statsStrings;
        using (StreamReader sr = new StreamReader(path))
        {
            statsStrings = sr.ReadToEnd().Split('\n');
            for (int i = 0; i < stats.Length; i++)
            {
                stats[i] = float.Parse(statsStrings[i]);
                Debug.Log(stats[i]);
            }
        }
        for (int i = 1;i <= stats[4]; i++)
        {
            SpawnPredator();
        }
        for (int i = 1; i <= stats[5]; i++)
        {
            SpawnCreature();
        }
        foodSpawner.spawnrate = stats[3];   
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnCreature()
    {
        float highestPoint = transform.position.y + variation;
        float lowestPoint = transform.position.y - variation;
        float leftestPoint = transform.position.x + (variation * 2);
        float rightestPoint = transform.position.x - (variation * 2);
        GameObject CreatureClone = Instantiate(Creature, new Vector3(UnityEngine.Random.Range(rightestPoint, leftestPoint), UnityEngine.Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        CreatureClone.name = "CreatureClone";
        CreatureClone.GetComponent<CreatureScript>().actualSize = stats[0];
        CreatureClone.GetComponent<CreatureScript>().predatorTendency = 0;
        CreatureClone.GetComponent<CreatureScript>().actualEyesight = stats[2];
        CreatureClone.GetComponent<CreatureScript>().moveTowardSpeed = stats[1];
        CreatureClone.GetComponent<CreatureScript>().score = 5;
        timer = 0;
    }
    public void SpawnPredator()
    {
        Debug.Log("Creature Spawned");
        float highestPoint = transform.position.y + variation;
        float lowestPoint = transform.position.y - variation;
        float leftestPoint = transform.position.x + (variation * 2);
        float rightestPoint = transform.position.x - (variation * 2);
        GameObject CreatureClone = Instantiate(Creature, new Vector3(UnityEngine.Random.Range(rightestPoint, leftestPoint), UnityEngine.Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        CreatureClone.name = "CreatureClone"; 
        CreatureClone.GetComponent<CreatureScript>().actualSize = stats[0];
        CreatureClone.GetComponent<CreatureScript>().predatorTendency = 1;
        CreatureClone.GetComponent<CreatureScript>().actualEyesight = stats[2];
        CreatureClone.GetComponent<CreatureScript>().moveTowardSpeed = stats[1];
        CreatureClone.GetComponent<CreatureScript>().score = 5;
        timer = 0;
    }
}
