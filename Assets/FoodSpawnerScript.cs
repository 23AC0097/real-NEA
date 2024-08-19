using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawnerScript : MonoBehaviour
{
    public GameObject Food;
    public float spawnrate = 6;
    public float timer = 0;
    public float variation = 3;
    public float gameTimer = 60;
    public float secondTimer;
    // Start is called before the first frame update
    void Start()
    {
        SpawnFood();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameTimer > 0)
        {
            if (timer > spawnrate)
            {
                SpawnFood();
            }
            timer += Time.deltaTime;
            if (secondTimer > 1)
            {
                secondTimer = 0;
                gameTimer--;
            }
            else
            {
                secondTimer = secondTimer + Time.deltaTime;
            }
        }
        
    }
    private void SpawnFood()
    {
        float highestPoint = transform.position.y + variation;
        float lowestPoint = transform.position.y - variation;
        float leftestPoint = transform.position.x + (variation * 2);
        float rightestPoint = transform.position.x - (variation * 2);
        GameObject FoodClone = Instantiate(Food, new Vector3(UnityEngine.Random.Range(rightestPoint, leftestPoint), UnityEngine.Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        FoodClone.name = "FoodClone";
        timer = 0;
    }
}
