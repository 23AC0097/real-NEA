using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static CreatureDataHandlerScript;
using Newtonsoft.Json;

public class CreatureDataHandlerScript : MonoBehaviour
{
    public string SessionNum;
    public List<SaveCreature> CreaturesToSave = new List<SaveCreature>();
    public GameTimerScript gameTimerScript;

    public class SaveCreature
    {
        public float SaveSize;
        public float SaveSpeed;
        public float SaveEyesight;
        public float SavePredTend;
        public float SaveTimeSpawned;
    }
    public class ListOfCreaturesToSave
    {
        public List<SaveCreature> list;
        public float gameTime;
        public string ghuh = "jckdrbvkj"; //Checks the thingy outputs everthing.
    }
    public void Save(float Size, float Speed, float Eyesight, float PredTend, float TimeSpawned)
    {
        SessionNum = System.DateTime.Now.ToString("yyyy'-'MM'-'dd'-'HH'-'mm'-'ssffff");
        SaveCreature saveCreature = new SaveCreature
        {
            SaveSize = Size,
            SaveSpeed = Speed,
            SaveEyesight = Eyesight,
            SavePredTend = PredTend,
            SaveTimeSpawned = TimeSpawned
        };
        CreaturesToSave.Add(saveCreature);
    }
    public void finalSave()
    {
        ListOfCreaturesToSave myList = new ListOfCreaturesToSave();
        myList.list = CreaturesToSave;
        string json = JsonConvert.SerializeObject(myList);
        File.WriteAllText(@"C:\projects\NEAProj\Assets" + @"\CreatureData\" + SessionNum + ".txt", json);
        
    }
    public List<SaveCreature> Load()
    {
        string creatureLines;
        ListOfCreaturesToSave myList = null;
        if (File.Exists(@"C:\projects\NEAProj\Assets" + @"\CreatureData\" + SessionNum + ".txt"))
        {
            using (StreamReader sr = new StreamReader(@"C:\projects\NEAProj\Assets" + @"\CreatureData\" + SessionNum + ".txt"))
            {
                creatureLines = sr.ReadToEnd();
            }
            myList = JsonConvert.DeserializeObject<ListOfCreaturesToSave>(creatureLines);
        }


        return myList.list;
    }
}
