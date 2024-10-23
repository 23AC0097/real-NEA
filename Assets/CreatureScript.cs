using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UIElements;

public class CreatureScript : MonoBehaviour
{
    public SizeSliderScript sliderScript;
    public GameTimerScript gameTimerScript;
    public bool active;
    public float timeAlive;
    public float sizeRandom;
    public float eyeRandom;
    public float predRandom;
    public float speedRandom;
    public bool isRunning;
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
    public float timer = 2.5f;
    public float score;
    public List<GameObject> ObjectsInTrigger = new List<GameObject>();
    public float step;
    public float creatureSize;
    bool predator;
    public float predatorTendency;
    public float deathCountdown = 20;
    public float actualSize;
    public GameObject closest;
    public float actualEyesight;
    // Start is called before the first frame update
    void Start()
    {
        active = true;
        highestPoint = variation;
        lowestPoint = -variation;
        leftestPoint = +(variation * 2);
        rightestPoint = -(variation * 2);
        Move();
        creatureSize = actualSize * 8;
        transform.localScale = new Vector2(actualSize,actualSize);
        eyesight.radius = actualEyesight + (creatureSize/2);
        predator = false;
        if (predatorTendency > 5)
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
        closest = null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            
            if (predator)
            {
                if (collision.collider.gameObject.name == "CreatureClone" && tag == "Prey")
                {
                    score++;
                if (isRunning == false)
                {
                    isRunning = true;
                    if (score >= 20)
                    {
                        ObjectsInTrigger.Remove(collision.collider.gameObject);
                    }
                    isRunning = false;
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
        if (isRunning == false)
        {
            isRunning = true;
            if (predator == false)
            {
                if (collision.gameObject.name == "FoodClone" && collision.tag != "Destroyed")
                {
                    ObjectsInTrigger.Remove(collision.gameObject);
                }
            }
            else
            {
                if (collision.gameObject.name == "CreatureClone")
                {
                    ObjectsInTrigger.Remove(collision.gameObject);
                }
            }
            isRunning = false;
        }
        isRunning = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isRunning == false)
        {
            isRunning = true;
            if (predator == false)
            {
                if (collision.gameObject.name == "FoodClone" && collision.tag != "Destroyed")
                {
                    ObjectsInTrigger.Add(collision.gameObject);

                }
            }
            else
            {
                if (collision.gameObject.name == "CreatureClone")
                {
                    ObjectsInTrigger.Add(collision.gameObject);

                }
            }
            isRunning = false;
        }
        isRunning = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (gameTimerScript.gameOver)
        {
            active = false;
        }
        if (dead)
        {
            deathCountdown = deathCountdown - Time.deltaTime;
        }
        if (deathCountdown <= 0)
        {
            Destroy(gameObject);
        }
        if (predatorTendency > 5)
        {
            predator = true;
            this.tag = "Predator";
        }
        
        if (creatureSize <= 0)
        {
            Destroy(gameObject);
        }
        if (!dead && active)
        {
            timeAlive += Time.deltaTime;
            if (ObjectsInTrigger.Count > 0)
            {
                GameObject closestGameObject = null;
                step = moveTowardSpeed * Time.deltaTime;
                if (isRunning == false)
                {
                    closestGameObject = FindClosestGameObject();
                }
                if (closestGameObject != null)
                {
                    transform.position = Vector2.MoveTowards(transform.position, closestGameObject.transform.position, step);
                }

            }
            //else if (ObjectsInCollider.Count > 0)
            //{
            //    step = moveTowardSpeed * Time.deltaTime;
            //    transform.position = Vector2.MoveTowards(transform.position, FindClosestCreature().transform.position, step * -1);
            //}
            else
            {
                Move();
                closest = null;
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
                sizeRandom = GetRandomMutation() / 8;
                if ((actualSize + sizeRandom) * 8 < 2 && (actualSize + sizeRandom) * 8 > 0.1)
                {
                    CreatureClone.GetComponent<CreatureScript>().actualSize = actualSize + sizeRandom;
                }
                predRandom = GetRandomMutation() * 2;
                if (predatorTendency + predRandom >= 0 && predatorTendency + predRandom <= 10)
                {
                    CreatureClone.GetComponent<CreatureScript>().predatorTendency = predatorTendency + predRandom;
                }
                eyeRandom = GetRandomMutation() * 4;
                if (actualEyesight + eyeRandom >= 0 && actualEyesight + eyeRandom <= 20)
                {
                    CreatureClone.GetComponent<CreatureScript>().actualEyesight = actualEyesight + eyeRandom;
                }
                speedRandom = GetRandomMutation() * 4;
                if (moveTowardSpeed + speedRandom >= 0 && predatorTendency + speedRandom <= 40)
                {
                    CreatureClone.GetComponent<CreatureScript>().moveTowardSpeed = moveTowardSpeed + speedRandom;
                    if (CreatureClone.GetComponent<CreatureScript>().creatureSize > creatureSize)
                    {
                        CreatureClone.GetComponent<CreatureScript>().moveTowardSpeed -= 2;
                    }
                    
                }
                CreatureClone.GetComponent<CreatureScript>().score = 5;
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

    private GameObject FindClosestGameObject()
    {
        if (isRunning == false)
        {
            isRunning = true;
            if (ObjectsInTrigger[0] != null)
            {
                closest = ObjectsInTrigger[0];
                float stoDistance = 0;
                float closestDistance = 0;
                foreach (GameObject go in ObjectsInTrigger)
                {
                    if (go != null)
                    {
                        stoDistance = Vector3.Distance(transform.position, go.transform.position);
                        closestDistance = Vector3.Distance(transform.position, closest.transform.position);
                        if (stoDistance < closestDistance)
                        {
                            closest = go;
                        }
                    }
                }

                isRunning = false;

                return closest;
            }
            else
            {
                ObjectsInTrigger.RemoveAll(GameObject => GameObject == null);
                return null;
            }
        }
        else
        {
            return null;
        }
    }
    
    private float GetRandomMutation()
    {
        float Ran1 = Random.value;
        if (Ran1 >= 0.5)
        {
            return 0;
        }
        else
        {
            float Ran2 = Random.value;
            if (Ran2 >= 0.5)
            {
                return Random.Range(0.01f, 0.49f);
            }
            else
            {
                return Random.Range(0.01f, 0.49f) * -1;
            }
        }
    }

}
