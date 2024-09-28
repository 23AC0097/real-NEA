using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public PlayButtonScript playButtonScript;
    public bool GameStart = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playButtonScript.clicked == true)
        {
            GameStart = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
