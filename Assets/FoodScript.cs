using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public GameObject food;
    public GameObject creature;
    public bool destroyed;
    public float destroyTimer = 5;
    // Start is called before the first frame update
    public void OnCollisionEnter(Collision collision)
    {
        destroyed = true;
        tag = "Destroyed";
    }
    void Start()
    {
        
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //Causes destruction of food if contact is made
    //    if (collision.collider.gameObject.name == "Creature")
    //        {
    //            Destroy(gameObject);
    //        }
    //}
    // Update is called once per frame
    void Update()
    {
        if (destroyed)
        {
            destroyTimer = destroyTimer - Time.deltaTime;
        }
    }
}
