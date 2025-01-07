using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimerScript : MonoBehaviour
{
    public bool done = false;
    public GameObject UIEndGame;
    public CreatureDataHandlerScript CreatureDataHandlerScript;
    public GameObject quitButton;
    public List<GameObject> creatures;
    public List <GameObject> endCreatures;
    public List<GameObject> food;
    public bool gameOver = false;
    public float overallGameTimer;
    public float saveGameTimer;
    public float secondTimer = 0;
    public bool gameOverHappened = false;
    // Start is called before the first frame update
    void Start()
    {
        
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "CreatureClone")
        {
            if (collision.gameObject.GetComponent<CreatureScript>().detected == false)
            {
                creatures.Add(collision.gameObject);
                CreatureDataHandlerScript.Save(collision.gameObject.GetComponent<CreatureScript>().creatureSize, collision.gameObject.GetComponent<CreatureScript>().moveTowardSpeed, collision.gameObject.GetComponent<CreatureScript>().eyesight.radius, collision.gameObject.GetComponent<CreatureScript>().predatorTendency, saveGameTimer - (overallGameTimer + secondTimer));
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
        if (done == false)
        {
            if (Time.timeSinceLevelLoad > 0.1)
            {
                saveGameTimer = Mathf.RoundToInt(overallGameTimer + Time.timeSinceLevelLoad);
                done = true;
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
            quitButton.SetActive(false);
        }
    }
}
