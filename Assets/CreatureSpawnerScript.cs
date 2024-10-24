using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreatureSpawnerScript : MonoBehaviour
{
    public SizeSliderScript sizeSliderScript;
    public GameObject Creature;
    public bool firstGameStart;
    public float spawnrate = 6;
    public float timer = 0;
    public float variation = 3;
    public float gameTimer = 60;
    public float secondTimer;
    // Start is called before the first frame update
    void Start()
    {
        SpawnCreature();
        SpawnCreature();
        SpawnCreature();
        SpawnCreature();
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
        CreatureClone.GetComponent<CreatureScript>().actualSize = 0.25f;
        CreatureClone.GetComponent<CreatureScript>().predatorTendency = 0;
        CreatureClone.GetComponent<CreatureScript>().actualEyesight = 10;
        CreatureClone.GetComponent<CreatureScript>().moveTowardSpeed = 20;
        CreatureClone.GetComponent<CreatureScript>().score = 5;
        timer = 0;
    }
    public void SpawnRandomCreature()
    {
        float highestPoint = transform.position.y + variation;
        float lowestPoint = transform.position.y - variation;
        float leftestPoint = transform.position.x + (variation * 2);
        float rightestPoint = transform.position.x - (variation * 2);
        GameObject CreatureClone = Instantiate(Creature, new Vector3(UnityEngine.Random.Range(rightestPoint, leftestPoint), UnityEngine.Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        CreatureClone.name = "CreatureClone";
        timer = 0;
    }
}
