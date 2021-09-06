using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class BuySkin : MonoBehaviour
{

    public GameObject skinText;

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

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void buySkin()
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
            SaveData data = JsonConvert.DeserializeObject<SaveData>(jsonString);

            foreach (Skins info in data.skins)
            {
                if (skinText.GetComponent<TextMeshProUGUI>().text == info.name)
                {
                    if (data.coins >= info.price)
                    {
                        data.coins = data.coins - info.price;
                        info.bought = true;
                    }
                }
            }

            string jsonData = JsonConvert.SerializeObject(data);
            filePath = "";
#if UNITY_EDITOR
            filePath = "savegame.json";
#else
            filePath = Path.Combine(Application.persistentDataPath, "savegame.json");

#endif
            File.WriteAllText(filePath, jsonData);

            //Reload Scene y colocamos camera en la posicion
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);

            GameObject aux = GameObject.Find("MainCamera");
            aux.GetComponent<TemporalCamara>().shop = true;

        }
        catch (Exception e)
        {
            print("error " + e.Message);
        }

    }
}
