using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UIElements;

public class CreatureScript : MonoBehaviour
{
    
    public float creatureSpeed = 5;
    public Rigidbody2D myRigidBody;
    //public bool FoodInRange = false;
    public GameObject food;
    public float randomNum1;
    public float randomNum2;
    public float timer = 0;
    public GameObject walls;
    public float score = 0;
    public List<GameObject> ObjectsInTrigger = new List<GameObject>();
    public float step;
    // Start is called before the first frame update
    void Start()
    {
        //Move();
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
            step = 5 * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, FindClosestGameObject().transform.position, step);
        }
        else
        {
            Move();
        }
        //Debug.Log("Moving");
        //Move();
        
    }
    private void Move() //Random movement
    {
        //transform.position = Vector3.zero;
        
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
