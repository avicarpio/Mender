using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class ChooseLevel : MonoBehaviour
{
    public class SaveData
    {
        public int curr_level;
        public float coins;
        public string lang;
        public int total_levels;
    }

    //Control de quin nivell ha de mostrar al text
    public void Start()
    {

        try
        {
            string filePath = "";
#if UNITY_EDITOR
            filePath = "savegame.json";
#else
            filePath = Path.Combine(Application.persistentDataPath, "savegame.json");

#endif
            string jsonString = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(jsonString);
            int curr_level = data.curr_level;

            if(curr_level <= 3)
            {
                this.GetComponentInChildren<TextMeshProUGUI>().text = "Tutorial";
            }
            else
            {
                curr_level = curr_level - 3;
                this.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + curr_level;

            }
        }
        catch (Exception e)
        {
        }

    }
}
