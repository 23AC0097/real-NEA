using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public GameObject slider;
    private void Awake()
    {
        DontDestroyOnLoad(slider);
    }
}
