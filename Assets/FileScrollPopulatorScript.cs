using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class FileScrollPopulatorScript : MonoBehaviour
{
    public GameObject FilePanel;
    public RectTransform rectTransform;
    

    private void Awake()
    {
        string path = @"C:\projects\NEAProj\Assets\CreatureData";
        rectTransform = GetComponent<ScrollRect>().content;
        DirectoryInfo dirInfo = new DirectoryInfo(path);
        FileInfo[] fileInfo = dirInfo.GetFiles("?.txt");
        foreach (FileInfo file in fileInfo)
        {
            GameObject instantiatedPanel = Instantiate(FilePanel);
            instantiatedPanel.transform.parent = rectTransform;
        }
    }
}
