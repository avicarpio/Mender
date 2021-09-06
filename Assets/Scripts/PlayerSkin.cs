using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class PlayerSkin : MonoBehaviour
{
    private string selectedSkin;
    private GameObject pickaxe;

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

        selectedSkin = "Mender";

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
                if (info.chosen == true)
                {
                    selectedSkin = info.image;
                }

            }


        }
        catch (Exception e)
        {
            print("error " + e.Message);
        }

        pickaxe = GameObject.FindGameObjectWithTag("PickAxe");

        GetComponent<Renderer>().material = Resources.Load<Material>(selectedSkin);
        pickaxe.GetComponent<Renderer>().material = Resources.Load<Material>(selectedSkin + "_PickAxe");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
