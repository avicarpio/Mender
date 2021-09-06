using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class changeLanguaje : MonoBehaviour
{
    public class Skins
    {
        public string name;
        public int price;
        public string image;
        public bool bought;
        public bool chosen;
    }

    public class SaveData
    {
        public int curr_level;
        public float coins;
        public string lang;
        public int total_levels;
        public List<Skins> skins;
    }

    private void Start()
    {
        
    }
    // Start is called before the first frame update
    public void change()
    {
        GameObject langGameObject = GameObject.Find("LanguajeText");
        try
        {
            string filePath = "";
#if UNITY_EDITOR
            filePath = "savegame.json";
#else
            filePath = Path.Combine(Application.persistentDataPath, "savegame.json");

#endif
            string jsonString = File.ReadAllText(filePath);
            SaveData data = JsonConvert.DeserializeObject<SaveData>(jsonString);
            String lang = data.lang;

            switch (lang)
            {
                case "es_ES":

                    langGameObject.GetComponent<TextMeshProUGUI>().text = "CAT";
                    data.lang = "cat_CAT";

                    break;

                case "cat_CAT":

                    langGameObject.GetComponent<TextMeshProUGUI>().text = "ENG";
                    data.lang = "en_US";
                    break;

                case "en_US":

                    langGameObject.GetComponent<TextMeshProUGUI>().text = "SPA";
                    data.lang = "es_ES";
                    break;

            }

            string jsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, jsonData);
           

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
