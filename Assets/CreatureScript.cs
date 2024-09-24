using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UIElements;

public class CreatureScript : MonoBehaviour
{
    public GameObject Creature;
    public float reproTimer;
    public Vector2 randomVector;
    public float variation = 5;
    public float opposite = 0;
    public float adjacent = 0;
    public float creatureSpeed = 20;
    public float moveTowardSpeed = 5;
    public Rigidbody2D myRigidBody;
    //public bool FoodInRange = false;
    public GameObject food;
    public float randomNum1;
    public float randomNum2;
    public float timer = 2.5f;
    public GameObject walls;
    public float score = 5;
    public List<GameObject> ObjectsInTrigger = new List<GameObject>();
    public List<GameObject> ObjectsInCollider = new List<GameObject>();
    public float step;
    // Start is called before the first frame update
    void Start()
    {
        Move();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Causes destruction of food if contact is made
        if (collision.collider.gameObject.name == "FoodClone")
        {
            Destroy(collision.gameObject);
            score++;
            ObjectsInTrigger.Remove(collision.collider.gameObject);
        }
        
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "CreatureClone")
        {
            timer = 2.5f;
            Move();
            //Debug.Log("Move triggered");
        }

    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name == "FoodClone")
    //    {
    //        FoodInRange = true;
    //    }
    //    else
    //    {
    //        FoodInRange = false;
    //    }
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "FoodClone")
        {
            ObjectsInTrigger.Remove(collision.gameObject);
        }
        if (collision.gameObject.name == "CreatureClone")
        {
            ObjectsInCollider.Remove(collision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "FoodClone")
        {
           ObjectsInTrigger.Add(collision.gameObject);
        }

        if (collision.gameObject.name == "CreatureClone")
        {
            ObjectsInCollider.Add(collision.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
        if (ObjectsInTrigger.Count > 0)
        {
            step = moveTowardSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, FindClosestGameObject().transform.position, step);

        }
        //else if (ObjectsInCollider.Count > 0)
        //{
        //    step = moveTowardSpeed * Time.deltaTime;
        //    transform.position = Vector2.MoveTowards(transform.position, FindClosestCreature().transform.position, step * -1);
        //}
        else
        {
            Move();
            
        }
        score -= 0.00125f;
        if (score < 0)
        {
            Destroy(gameObject);
        }
        if (reproTimer > 15 && score > 10)
        {
            reproTimer = 0;
            float highestPoint = transform.position.y + variation;
            float lowestPoint = transform.position.y - variation;
            float leftestPoint = transform.position.x + (variation * 2);
            float rightestPoint = transform.position.x - (variation * 2);
            GameObject CreatureClone = Instantiate(Creature, new Vector3(UnityEngine.Random.Range(rightestPoint, leftestPoint), UnityEngine.Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
            CreatureClone.name = "CreatureClone";
            score -= 5;
        }
        reproTimer += Time.deltaTime;

    }
    private Vector2 FindRandomPoint()
    {
        float highestPoint = 0;
        float lowestPoint = 0;
        float leftestPoint = 0;
        float rightestPoint = 0;
        Vector2 vector = new Vector2(Random.Range(leftestPoint, rightestPoint), Random.Range(lowestPoint, highestPoint));
        while (Vector2.Distance(vector, transform.position) < 2)
        {
            highestPoint = variation;
            lowestPoint = -variation;
            leftestPoint = +(variation * 2);
            rightestPoint = -(variation * 2);
            float randomX = Random.Range(leftestPoint, rightestPoint);
            float randomY = Random.Range(lowestPoint, highestPoint);
            vector = new Vector2(randomX, randomY);
        }
        return vector;
    } 
    private void Move() //Random movement
    {
        step = moveTowardSpeed * Time.deltaTime;
        if (timer > 5f || (transform.position.x == randomVector.x && transform.position.y == randomVector.y))
        {
            randomVector = FindRandomPoint();
            //Debug.Log(randomVector);
            timer = 0;
        }
        else if (Vector2.Distance(transform.position, randomVector) < 0.01f)
        {
            randomVector = FindRandomPoint();
            //Debug.Log(randomVector);
            timer = 0;
        }
        //Debug.Log(randomVector);
        transform.position = Vector2.MoveTowards(transform.position, randomVector, step);
        timer += Time.deltaTime;
        
        
        

    }

    private GameObject FindClosestGameObject()
    {
        GameObject closest = ObjectsInTrigger[0];
        float stoDistance = 0;
        float closestDistance = 0;
        foreach (GameObject go in ObjectsInTrigger)
        {
            stoDistance = Vector3.Distance(transform.position, go.transform.position);
            closestDistance = Vector3.Distance(transform.position, closest.transform.position);
            if(stoDistance < closestDistance)
            {
                closest = go;
            }
        }
        return closest;
    }
    //private GameObject FindClosestCreature()
    //{
    //    GameObject closest = ObjectsInCollider[0];
    //    float stoDistance = 0;
    //    float closestDistance = 0;
    //    foreach (GameObject go in ObjectsInCollider)
    //    {
    //        stoDistance = Vector3.Distance(transform.position, go.transform.position);
    //        closestDistance = Vector3.Distance(transform.position, closest.transform.position);
    //        if (stoDistance < closestDistance)
    //        {
    //            closest = go;
    //        }
    //    }
    //    return closest;
    //}
    
}
