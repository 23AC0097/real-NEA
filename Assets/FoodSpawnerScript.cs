using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawnerScript : MonoBehaviour
{
    public GameTimerScript gameTimer;
    public UIScript uiScript;
    public bool firstGameStart;
    public GameObject Food;
    public float spawnrate = 6;
    public float timer = 0;
    public float variation = 3;
    public float secondTimer;
    // Start is called before the first frame update
    void Start()
    {
        SpawnFood();
        SpawnFood();
        SpawnFood();
        SpawnFood();
        SpawnFood();
    }

    // Update is called once per frame
    void Update()
    {
        
            
            //gameTimer = GetComponent<GameTimerScript>();
            if (!gameTimer.gameOver)
            {
                if (timer > spawnrate)
                {
                    SpawnFood();
                    timer = 0;
                }
                else
                {
                    timer += Time.deltaTime;
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
