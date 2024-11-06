using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManagerScript : MonoBehaviour
{
    public float numToAdd;
    // Start is called before the first frame update
    void Start()
    {
        string path = @"C:\projects\NEAProj\Assets\DataManagerThing.txt";
        
        string nums;
        using (StreamReader sr = new StreamReader(path))
        {
            nums = sr.ReadToEnd();
            numToAdd = nums.Length;
        }
        using (StreamWriter sw = new StreamWriter(path, true))
        {
            sw.WriteLine(numToAdd);
        }
    }

    
}
