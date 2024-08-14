using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UIElements;

public class CreatureScript : MonoBehaviour
{
    public GameObject creature;
    public float creatureSpeed = 5;
    public Rigidbody2D myRigidBody;
    public bool FoodInRange = false;
    public GameObject food;
    public float randomNum1;
    public float randomNum2;
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
        //Causes destruction of food if contact is made
        if (collision.collider.gameObject.name == "FoodClone")
        {
                Destroy(collision.gameObject);
                score++;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Walls")
        {
            Move();
            Debug.Log("Move triggered");
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "FoodClone")
        {
            FoodInRange = true;
        }
        else
        {
            FoodInRange = false;
        }
    }
    

    // Update is called once per frame
    void Update()
    {

        Move();
        
    }
    private void Move() //Random movement
    {
        if (FoodInRange == false)
        {
            if (timer > 2.5f)
            {
                randomNum1 = Random.Range(-100, 101);
                randomNum2 = Random.Range(-100, 101);
                Vector3 vector = new Vector3(randomNum1, randomNum2, 0);
                myRigidBody.velocity = vector * creatureSpeed * Time.deltaTime;
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        if (FoodInRange == true)
        {
            //Debug.Log("FoodInRange");
            transform.position = Vector3.MoveTowards(transform.position, food.transform.position, creatureSpeed * Time.deltaTime) * -1;
            //myRigidBody.velocity = vector * creatureSpeed * Time.deltaTime;
            FoodInRange = false;
        }
    }
    
}
