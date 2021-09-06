using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class SceneLoader : MonoBehaviour
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


    private IEnumerator loadingIE;
    private Scene newScene;
    private AsyncOperation asyncLoadLevel;

    public void loadScene(int scene)
    {
        SceneManager.LoadScene(scene);
        var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
        SceneManager.LoadScene("Robot", parameters);
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        //loadingIE = LoadLevel(scene);
        //StartCoroutine(loadingIE);
    }
    /*
    IEnumerator LoadLevel(int scene)
    {
        asyncLoadLevel = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        while (!asyncLoadLevel.isDone)
        {
            print("Loading the Scene");
            yield return null;
        }


        print("Scene Loaded");

        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
            if (loadedScenes[i].name != "Robot")
            {
                newScene = loadedScenes[i];
            }
        }

        SceneManager.SetActiveScene(newScene);
        SceneManager.UnloadSceneAsync("LoadingScene");
        
    }*/

    public void nextScene()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            int n_scene = SceneManager.GetActiveScene().buildIndex + 1;
            loadScene(n_scene);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            savingGame(SceneManager.GetActiveScene().buildIndex + 1);

            nextScene();

        }
    }

    private void savingGame(int lev)
    {
        SaveData data = new SaveData();

        try
        {
            string pathExtra = "";
#if UNITY_EDITOR
            pathExtra = "savegame.json";
#else
            pathExtra = Path.Combine(Application.persistentDataPath, "savegame.json");

#endif
            string jsonString = File.ReadAllText(pathExtra);
            SaveData dataLoad = JsonConvert.DeserializeObject<SaveData>(jsonString);
            data.coins = dataLoad.coins + 10;

            if(lev > dataLoad.curr_level)
            {
                data.curr_level = lev;
            }
            data.lang = dataLoad.lang;
            data.total_levels = dataLoad.total_levels;
            data.skins = dataLoad.skins;

        }
        catch (Exception e)
        {
            data.coins = 10;
            lev = 4;
            data.curr_level = 4;
        }

        print(data.curr_level);

        string jsonData = JsonConvert.SerializeObject(data);

        string filePath = "";
#if UNITY_EDITOR
        filePath = "savegame.json";
#else
            filePath = Path.Combine(Application.persistentDataPath, "savegame.json");

#endif
        File.WriteAllText(filePath, jsonData);

    }

    public void loadMenu()
    {
        SceneManager.LoadScene(0);
    }


}
