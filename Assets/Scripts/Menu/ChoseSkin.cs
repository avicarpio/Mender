using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Versioning;

public class ChoseSkin : MonoBehaviour
{
    public GameObject mender;
    public GameObject pickAxe;
    private int actual_skin;
    private int total_skins;
    private List<String> bought_skins = new List<string>();

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
        //Guardar les skins que tens comprades
        try
        {
            total_skins = 0;

#if UNITY_EDITOR
            string filePath = "savegame.json";
#else
            string filePath = Path.Combine(Application.persistentDataPath, "savegame.json");

#endif
            string jsonString = File.ReadAllText(filePath);
            SaveData data = JsonConvert.DeserializeObject<SaveData>(jsonString);

            foreach (Skins info in data.skins)
            {
                if (info.bought == true)
                {
                    bought_skins.Add(info.image);

                    if(info.chosen == true)
                    {
                        actual_skin = total_skins;
                    }
                    else
                    {
                        actual_skin = 0;
                    }

                    total_skins++;
                }

            }


        }
        catch (Exception e)
        {
            //Json no trobat

#if UNITY_EDITOR
            string jsonString = File.ReadAllText("Assets/Resources/savegame_bak.json");
            string filePath = "savegame.json";
#else
            TextAsset file = Resources.Load("savegame_bak") as TextAsset;
            string jsonString = file.ToString ();
            string filePath = Path.Combine(Application.persistentDataPath, "savegame.json");
            print("Root: " + Application.persistentDataPath);
            print(filePath);
#endif

            SaveData data = JsonConvert.DeserializeObject<SaveData>(jsonString);
            string jsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, jsonData);
            print("error " + e.Message);
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
       //Skin que es mostra
       mender.GetComponent<Renderer>().material = Resources.Load<Material>(bought_skins[actual_skin]);
       pickAxe.GetComponent<Renderer>().material = Resources.Load<Material>(bought_skins[actual_skin] + "_PickAxe");

    }

    public void modifyActualSkin()
    {

        saveJson(actual_skin, false);

        actual_skin++;
        if(actual_skin > (total_skins - 1))
        {
            actual_skin = 0;
        }

        saveJson(actual_skin, true);


    }

    public void saveJson(int aux, bool chose)
    {
        //Modificar el chosen al Json
        try
        {

            #if UNITY_EDITOR
                string jsonString = File.ReadAllText("savegame.json");
                SaveData data = JsonConvert.DeserializeObject<SaveData>(jsonString);
#else
                string filePath = Path.Combine(Application.persistentDataPath, "savegame.json");
                string jsonString = File.ReadAllText(filePath);
                SaveData data = JsonConvert.DeserializeObject<SaveData>(jsonString);
#endif

            foreach (Skins skin_info in data.skins)
            {

                if (bought_skins[aux] == skin_info.image)
                {
                    skin_info.chosen = chose;
                }

            }

#if UNITY_EDITOR
            string filePath = "savegame.json";
#else
            filePath = Path.Combine(Application.persistentDataPath, "savegame.json");

#endif

            string jsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, jsonData);



        }
        catch (Exception e)
        {
            print("error " + e.Message);
        }
    }
}
