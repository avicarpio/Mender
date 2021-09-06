using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class InitializeSkins : MonoBehaviour
{

    public GameObject skinObj;

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
        initialize();
    }

    public void initialize()
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
            int aux = 0;

            foreach (Skins info in data.skins)
            {
                if (info.bought == false)
                {
                    GameObject skin = Instantiate(skinObj, this.gameObject.transform);
                    skin.transform.Find("SkinText").GetComponent<TextMeshProUGUI>().text = info.name;

                    //Material material = Resources.Load<Material>(info.image);

                    Sprite image = Resources.Load<Sprite>(info.image);
                    skin.transform.Find("SkinImage").GetComponent<UnityEngine.UI.Image>().sprite = image;
                    skin.transform.Find("SkinBuy").GetComponentInChildren<TextMeshProUGUI>().text = "" + info.price;
                    skin.transform.localPosition = new Vector3(skin.transform.localPosition.x, aux * -110 + skin.transform.localPosition.y, skin.transform.localPosition.z);
                    aux++;
                }

            }

        }
        catch (Exception e)
        {
            print("error " + e.Message);
        }
    }
}
