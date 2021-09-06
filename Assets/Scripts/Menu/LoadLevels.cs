using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class LoadLevels : MonoBehaviour
{
    public GameObject doneStar;
    public GameObject toDoStar;

    public class SaveData
    {
        public int curr_level;
        public float coins;
        public string lang;
        public int total_levels;
    }

    // Start is called before the first frame update
    void Start()
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
            //Li restem els 3 nivells de tutorial
            int current_level = data.curr_level - 3;
            if(current_level < 0)
            {
                current_level = 0;
            }

            for (int i = 0; i < data.total_levels; i++)
            {
                if(i < current_level && i<3)
                {
                    doneStar.GetComponentInChildren<TextMeshProUGUI>().text = "" + (i + 1);
                    GameObject star = Instantiate(doneStar, this.gameObject.transform);
                    star.transform.localPosition = new Vector3(130 * i - 120, 80);
                }
                else if (i < 3)
                {
                    toDoStar.GetComponentInChildren<TextMeshProUGUI>().text = "" + (i + 1);
                    GameObject star = Instantiate(toDoStar, this.gameObject.transform);
                    star.transform.localPosition = new Vector3(130 * i - 120, 80);
                }
                else if(i<current_level)
                {
                    int position = i - 3;
                    doneStar.GetComponentInChildren<TextMeshProUGUI>().text = "" + (i + 1);
                    GameObject star = Instantiate(doneStar, this.gameObject.transform);
                    star.transform.localPosition = new Vector3(130 * position - 120, -70);
                }
                else
                {
                    int position = i - 3;
                    toDoStar.GetComponentInChildren<TextMeshProUGUI>().text = "" + (i + 1);
                    GameObject star = Instantiate(toDoStar, this.gameObject.transform);
                    star.transform.localPosition = new Vector3(130 * position - 120, -70);
                }
            }

        }
        catch (Exception e)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
   
    }
}
