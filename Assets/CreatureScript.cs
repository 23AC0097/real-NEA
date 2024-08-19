using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UIElements;

public class CreatureScript : MonoBehaviour
{
    Vector2 randomVector;
    public float variation = 3;
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
        if (collision.collider.gameObject.name == "Walls")
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "FoodClone")
        {
           ObjectsInTrigger.Add(collision.gameObject);
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
        else
        {
            Move();
            
        }
        score -= 0.00125f;
        if (score < 0)
        {
            Destroy(gameObject);
        }
        //Debug.Log("Moving");
        //Move();
        
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
        if (timer > 2.5f)
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
        
        
        //transform.position = Vector3.zero;

        //if (timer > 2.5f)
        //{
        //    Vector2 vector;
        //    randomNum1 = Random.Range(0, 360);
        //    if (randomNum1 < 90)
        //    {
        //        opposite = Mathf.Sin(randomNum1) * creatureSpeed;
        //        adjacent = Mathf.Cos(randomNum1) * creatureSpeed;
        //        vector = new Vector2(adjacent, opposite);
        //        myRigidBody.velocity = vector * Time.deltaTime * creatureSpeed;
        //    }
        //    if (randomNum1 > 90 && randomNum1 < 180)
        //    {
        //        randomNum1 -= 90;
        //        opposite = Mathf.Sin(randomNum1) * creatureSpeed;
        //        adjacent = Mathf.Cos(randomNum1) * creatureSpeed * -1;
        //        vector = new Vector2(adjacent, opposite);
        //        myRigidBody.velocity = vector * Time.deltaTime * creatureSpeed;
        //    }
        //    if (randomNum1 > 180 && randomNum1 < 270)
        //    {

        //        randomNum1 -= 180;
        //        opposite = Mathf.Sin(randomNum1) * creatureSpeed * -1;
        //        adjacent = Mathf.Cos(randomNum1) * creatureSpeed * -1;
        //        vector = new Vector2(adjacent, opposite);
        //        myRigidBody.velocity = vector * Time.deltaTime * creatureSpeed;
        //    }
        //    if (randomNum1 > 270)
        //    {

        //        randomNum1 -= 270;
        //        opposite = Mathf.Sin(randomNum1) * creatureSpeed * -1;
        //        adjacent = Mathf.Cos(randomNum1) * creatureSpeed;
        //        vector = new Vector2(adjacent, opposite);
        //        myRigidBody.velocity = vector * Time.deltaTime * creatureSpeed;
        //    }
        //    if (randomNum1 == 0)
        //    {
        //        myRigidBody.velocity = Vector2.up;
        //    }
        //    if (randomNum1 == 90)
        //    {
        //        myRigidBody.velocity = Vector2.left;
        //    }
        //    if (randomNum1 == 180)
        //    {
        //        myRigidBody.velocity = Vector2.down;
        //    }
        //    if (randomNum1 == 270)
        //    {
        //        myRigidBody.velocity = Vector2.right;
        //    }
        //randomNum1 = Random.Range(-100, 101);
        //randomNum2 = Random.Range(-100, 101);
        //timer = 0;
        //}

        //else
        //{

        //    timer += Time.deltaTime;
        //}

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
    
}
