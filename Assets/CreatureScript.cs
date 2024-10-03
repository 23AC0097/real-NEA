using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UIElements;

public class CreatureScript : MonoBehaviour
{
    public bool dead;
    public Vector2 vector;
    public float lowestPoint;
    public float highestPoint;
    public float leftestPoint;
    public float rightestPoint;
    public SpriteRenderer spriteRenderer;
    public CircleCollider2D eyesight;
    public GameObject Creature;
    public float reproTimer;
    public Vector2 randomVector;
    public float variation = 5;
    public float moveTowardSpeed;
    public Rigidbody2D myRigidBody;
    //public bool FoodInRange = false;
    public GameObject food;
    public float randomNum1;
    public float randomNum2;
    public float timer = 2.5f;
    public float score = 5;
    public List<GameObject> ObjectsInTrigger = new List<GameObject>();
    public List<GameObject> ObjectsInCollider = new List<GameObject>();
    public float step;
    public float creatureSize;
    bool predator;
    public float predatorTendency;
    public float deathCountdown = 20;
    public float actualSize;
    public float closestDistance;
    public GameObject closest;
    // Start is called before the first frame update
    void Start()
    {
        predatorTendency = 0;
        highestPoint = variation;
        lowestPoint = -variation;
        leftestPoint = +(variation * 2);
        rightestPoint = -(variation * 2);
        Move();
        actualSize = 0.25f;
        creatureSize = actualSize * 8;
        transform.localScale = new Vector2(actualSize,actualSize);
        eyesight.radius = 10 + (creatureSize/2);
        moveTowardSpeed = 20;
        predator = false;
        if (predatorTendency > 0.5)
        {
            predator = true;
        }
        if (predator)
        {
            this.tag = "Predator";
        }
        else
        {
            this.tag = "Prey";
        }
        if (ObjectsInTrigger.Count == 0)
        {
            closestDistance = 10000000000;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (predator)
        {
            if (collision.collider.gameObject.name == "CreatureClone" && tag == "Prey")
            {
                score++;
                if (score >= 20)
                {
                    ObjectsInTrigger.Remove(collision.collider.gameObject);
                }
            }
        }
        if (collision.collider.tag == "Predator")
        {
            dead = true;
            creatureSize--;
        }
        //Causes destruction of food if contact is made
        if (!predator)
        {
            if (collision.collider.gameObject.name == "FoodClone")
            {
                if (collision.gameObject == closest)
                {
                    if (ObjectsInTrigger.Count != 0)
                    {
                        closest = ObjectsInTrigger[0];
                    }
                    else
                    {
                        closest = null;
                    }
                }
                Destroy(collision.gameObject);
                score++;
            }
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
        if (predator == false)
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
        else
        {
            if (collision.gameObject.name == "CreatureClone")
            {
                ObjectsInTrigger.Remove(collision.gameObject);
                if (collision.gameObject == closest)
                {
                    closest = ObjectsInTrigger[0];
                }
            }
            if (collision.gameObject.name == "FoodClone")
            {
                ObjectsInCollider.Remove(collision.gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (predator == false)
        {
            if (collision.gameObject.name == "FoodClone")
            {
                ObjectsInTrigger.Add(collision.gameObject);
                if (Vector2.Distance(collision.gameObject.transform.position, transform.position) < closestDistance)
                {
                    closestDistance = Vector2.Distance(collision.gameObject.transform.position, transform.position);
                    closest = collision.gameObject;
                }
            }
            if (collision.gameObject.name == "CreatureClone")
            {
                ObjectsInCollider.Add(collision.gameObject);
            }
        }
        else
        {
            if (collision.gameObject.name == "CreatureClone")
            {
                ObjectsInTrigger.Add(collision.gameObject);
                if (Vector2.Distance(collision.gameObject.transform.position, transform.position) < closestDistance)
                {
                    closestDistance = Vector2.Distance(collision.gameObject.transform.position, transform.position);
                    closest = collision.gameObject;
                }
            }
            if (collision.gameObject.name == "FoodClone")
            {
                ObjectsInCollider.Add(collision.gameObject);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (ObjectsInTrigger.Count == 0)
        {
            closestDistance = 10000000000;
        }
        if (dead)
        {
            deathCountdown = deathCountdown - Time.deltaTime;
        }
        if (deathCountdown <= 0)
        {
            Destroy(gameObject);
        }
        if (predatorTendency > 0.5)
        {
            predator = true;
            ObjectsInTrigger.Clear();
        }
        if (predator)
        {
            this.tag = "Predator";
        }
        
        if (creatureSize <= 0)
        {
            Destroy(gameObject);
        }
        if (!dead)
        {
            if (ObjectsInTrigger.Count > 0)
            {
                step = moveTowardSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, closest.transform.position, step);

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
            score -= 0.0025f;
            if (score < 0)
            {
                Destroy(gameObject);
            }
            if (reproTimer > 15 && score > 10)
            {
                reproTimer = 0;
                highestPoint = transform.position.y + variation;
                lowestPoint = transform.position.y - variation;
                leftestPoint = transform.position.x + (variation * 2);
                float rightestPoint = transform.position.x - (variation * 2);
                GameObject CreatureClone = Instantiate(Creature, new Vector3(UnityEngine.Random.Range(rightestPoint, leftestPoint), UnityEngine.Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
                CreatureClone.name = "CreatureClone";
                CreatureClone.GetComponent<CreatureScript>().predatorTendency = 1;
                score -= 5;
            }
            if (score < 2.5f)
            {
                spriteRenderer.color = new Color32(88, 54, 54, 255);
            }
            else if (score >= 10)
            {
                spriteRenderer.color = new Color32(255, 227, 227, 255);
            }
            else if (score < 10 && score >= 2.5f)
            {
                spriteRenderer.color = new Color32(180, 109, 109, 255);
            }
            reproTimer += Time.deltaTime;
        }
    }
    private Vector2 FindRandomPoint()
    {
        vector = new Vector2(Random.Range(leftestPoint, rightestPoint), Random.Range(lowestPoint, highestPoint));
        while (Vector2.Distance(vector, transform.position) < 0.125f)
        {
            vector = new Vector2(Random.Range(leftestPoint, rightestPoint), Random.Range(lowestPoint, highestPoint));
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

    //private GameObject FindClosestGameObject()
    //{
    //    closest = ObjectsInTrigger[0];
    //    float stoDistance = 0;
    //    closestDistance = 0;
    //    float length = ObjectsInTrigger.Count;
    //    for (int i = 0; i < length; i++)
    //    {
    //        stoDistance = Vector3.Distance(transform.position, ObjectsInTrigger[i].transform.position);
    //        closestDistance = Vector3.Distance(transform.position, closest.transform.position);
    //        if(stoDistance < closestDistance)
    //        {
    //            closest = ObjectsInTrigger[i];
    //        }
    //        length = ObjectsInTrigger.Count;
    //    }
    //    return closest;
    //}
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
