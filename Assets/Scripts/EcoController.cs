using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class EcoController : MonoBehaviour
{

    private TMP_Text coins;
    float n_coins;

    public class SaveData
    {
        public int curr_level;
        public float coins;
    }

    // Start is called before the first frame update
    void Start()
    {

        GameObject[] coinsGameObject = GameObject.FindGameObjectsWithTag("Coins");

        //print(GetComponent<TMP_Text>());

        if(coinsGameObject != null)
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
                SaveData dataLoad = JsonUtility.FromJson<SaveData>(jsonString);
                n_coins = dataLoad.coins;

            }
            catch (Exception e)
            {
                n_coins = 0;
            }

            coins = GetComponent<TMP_Text>();

            coins.text = n_coins.ToString();

        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
