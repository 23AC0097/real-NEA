using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawnerScript : MonoBehaviour
{
    public GameObject Creature;
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
        timer = 0;
    }
}
