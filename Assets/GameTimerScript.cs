using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimerScript : MonoBehaviour
{
    public List<GameObject> creatures;
    public List<GameObject> food;
    public bool gameOver = false;
    public float overallGameTimer = 60;
    public float secondTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "CreatureClone")
        {
            creatures.Add(collision.gameObject);
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
            Destroy(collision.gameObject);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (creatures.Count == 0)
        {
            if (overallGameTimer < 58)
            {
                gameOver = true;
            }
            
            
        }
        if (overallGameTimer < 0)
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
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            
        }
    }
}
