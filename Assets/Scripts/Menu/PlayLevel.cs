using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayLevel : MonoBehaviour
{
    public class SaveData
    {
        public int curr_level;
        public float coins;
        public string lang;
        public int total_levels;
    }

    //Load escena del boto de Play, iniciara sempre el Current Level
    public void loadScene()
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

            //Controlar que fagi tot el tutorial sencer i despres ja pugui jugar
            if(curr_level <= 3)
            {
                curr_level = 1;
            }

            SceneManager.LoadScene(curr_level);
            var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
            SceneManager.LoadScene("Robot", parameters);

        }
        catch (Exception e)
        {
        }
      
    }
}
