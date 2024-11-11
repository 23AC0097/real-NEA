using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimerScript : MonoBehaviour
{
    public GameObject UIEndGame;
    public CreatureDataHandlerScript CreatureDataHandlerScript;
    public List<GameObject> creatures;
    public List <GameObject> endCreatures;
    public List<GameObject> food;
    public bool gameOver = false;
    public float overallGameTimer = 60;
    public float saveGameTimer;
    public float secondTimer = 0;
    public bool gameOverHappened = false;
    // Start is called before the first frame update
    void Start()
    {
        saveGameTimer = overallGameTimer;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "CreatureClone")
        {
            if (collision.gameObject.GetComponent<CreatureScript>().detected == false)
            {
                creatures.Add(collision.gameObject);
                CreatureDataHandlerScript.Save(collision.gameObject.GetComponent<CreatureScript>().creatureSize, collision.gameObject.GetComponent<CreatureScript>().moveTowardSpeed, collision.gameObject.GetComponent<CreatureScript>().eyesight.radius, collision.gameObject.GetComponent<CreatureScript>().predatorTendency, collision.gameObject.GetComponent<CreatureScript>().TimeSpawned);
                Debug.Log("Creature Added To List");
                collision.gameObject.GetComponent<CreatureScript>().detected = true;
            }
            
        }
        if (collision.gameObject.name == "FoodClone")
        {
            food.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "CreatureClone")
        {
            creatures.Remove(collision.gameObject);
        }
        if (collision.gameObject.name == "FoodClone")
        {
            food.Remove(collision.gameObject);
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (gameOver)
        {
            if (collision.gameObject.name != "CreatureClone")
            {
                Destroy(collision.gameObject);
            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (creatures.Count == 0)
        {
            if (overallGameTimer < saveGameTimer - 2)
            {
                gameOver = true;
            }
            
            
            
        }
        if (overallGameTimer < 2)
        {
            gameOver = true;
        }
        if (!gameOver)
        {
            if (secondTimer > 1)
            {
                secondTimer = 0;
                overallGameTimer--;
            }
            else
            {
                secondTimer = secondTimer + Time.deltaTime;
            }
        }
        if (gameOver)
        {
            endCreatures = creatures;
            foreach (GameObject go in creatures)
            {
                go.GetComponent<CreatureScript>().active = false;
            }
            if (gameOverHappened == false)
            {
                CreatureDataHandlerScript.finalSave();
                gameOverHappened = true;
            }
            UIEndGame.SetActive(true);
        }
    }
}
