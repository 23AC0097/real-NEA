using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CreatureScript : MonoBehaviour
{
    public GameObject creature;
    public float creatureSpeed = 5;
    public Rigidbody2D myRigidBody;
    public bool FoodInRange = false;
    public GameObject food;
    public Transform foodPos;
    public Vector3 foodPosition;
    public float randomNum;
    public float timer = 0;
    public GameObject walls;
    public float score = 0;
    // Start is called before the first frame update
    void Start()
    {
        Move();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        //Causes destruction of food if contact is made
        if (collision.collider.gameObject.name == "FoodClone")
        {
            score++;
        }
        if (collision.collider.gameObject.name == "Walls")
        {
            Move();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "FoodClone")
        {
            FoodInRange = true;
            //Vector3.MoveTowards(creature.transform.position, foodPos.position, creatureSpeed * Time.deltaTime);
        }
        else
        {
            FoodInRange = false;
        }
    }
    //private void OnTriggerEnter2D(Collider2D thing)
    //{
    //    if (thing.gameObject == food)
    //    {
    //        FoodInRange = true;
    //        foodPosition = Vector3.MoveTowards(creature.transform.position, food.transform.position, 3);
    //        transform.Translate(foodPosition * creatureSpeed * Time.deltaTime);

    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        //if (Vector3.Distance(creature.transform.position, foodPos.position) > 3) //Checks if food is in range
        //{
        //    FoodInRange = false;
        //}
        if (FoodInRange == false) //Moves randomly if food is in range
        {
            if (timer > 1.5)
            {
                Move();
            }
            timer += Time.deltaTime;
        }
        if (FoodInRange == true)
        {
            foodPosition = Vector3.MoveTowards(creature.transform.position, foodPos.position, creatureSpeed * Time.deltaTime);
            transform.Translate(foodPosition * creatureSpeed * Time.deltaTime * -1);
        }
        //if (Vector3.Distance(creature.transform.position, foodPos.position) <= 6) //Finds food and moves towards it
        //{
        //    FoodInRange = true;
        //    foodPosition = Vector3.MoveTowards(creature.transform.position, foodPos.position, creatureSpeed * Time.deltaTime);
        //    //transform.Translate(foodPosition * creatureSpeed * Time.deltaTime);
        //    //transform.position = foodPosition;
        //}
        

        //if (Input.GetKey(KeyCode.W))
        //{
        //    //myRigidBody.velocity = Vector3.up * creatureSpeed;
        //    transform.Translate(Vector3.up * creatureSpeed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    //myRigidBody.velocity = Vector3.down * creatureSpeed;
        //    transform.Translate(Vector3.down * creatureSpeed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    //myRigidBody.velocity = Vector3.right * creatureSpeed;
        //    transform.Translate(Vector3.right * creatureSpeed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    //myRigidBody.velocity = Vector3.left * creatureSpeed;
        //    transform.Translate(Vector3.left * creatureSpeed * Time.deltaTime);
        //}
    }
    private void Move() //Random movement
    {
        randomNum = Random.Range(1, 5);
        if (randomNum == 1)
        {
            myRigidBody.velocity = Vector3.up * creatureSpeed;
        }
        else if (randomNum == 2)
        {
            myRigidBody.velocity = Vector3.down * creatureSpeed;
        }
        else if (randomNum == 3)
        {
            myRigidBody.velocity = Vector3.right * creatureSpeed;
        }
        else if (randomNum == 4)
        {
            myRigidBody.velocity = Vector3.left * creatureSpeed;
        }
        timer = 0;
    }
}
