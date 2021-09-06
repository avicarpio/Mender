using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.InteropServices;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;


public class DialogueSystem : MonoBehaviour
{

    private TextMeshProUGUI text;

    public class DialogueLevel
    {
        public string level;
        public string[] say;
    }

    public class DialogueSys
    {
        public string lang;
        public List<DialogueLevel> dial = new List<DialogueLevel>();
    }

    public class LangData
    {
        public string lang;
    }

    private string sel_lang;
    private List<DialogueSys> dialogues = new List<DialogueSys>();

    // Start is called before the first frame update
    void Start()
    {

        sel_lang = "es_ES";

        try
        {
            string filePath = "";
#if UNITY_EDITOR
            filePath = "savegame.json";
#else
            filePath = Path.Combine(Application.persistentDataPath, "savegame.json");

#endif
            string jsonString = File.ReadAllText(filePath);
            LangData data = JsonConvert.DeserializeObject<LangData>(jsonString);
            sel_lang = data.lang;
        }
        catch (Exception e)
        {
            print("error " + e.Message);
        }

        //CSV Dialogue Parser 

        text = this.gameObject.GetComponent<TMPro.TextMeshProUGUI>();

        //string path = "Assets/Dialogue/DialogueDict.csv";
        TextAsset file = Resources.Load("DialogueDict") as TextAsset;
        string csvFile = file.ToString();

        //StreamReader reader = new StreamReader(path, Encoding.GetEncoding("iso-8859-1"));

        //string csvFile = reader.ReadToEnd();

        //reader.Close();

        string[] lines = csvFile.Split('\n');

        for(int i = 0; i < lines.Length; i++)
        {
            string[] data = lines[i].Split(';');

            if(i == 0)
            {
                for(int j = 1; j < data.Length; j++)
                {
                    DialogueSys ds = new DialogueSys();
                    ds.lang = data[j];
                    ds.lang = ds.lang.Replace("\r", "").Replace("\n", "");
                    dialogues.Add(ds);
                }
            }
            else
            {
                for (int j = 1; j < data.Length; j++)
                {

                    DialogueLevel dl = new DialogueLevel();
                    dl.level = data[0];
                    dl.say = data[j].Split('/');

                    dialogues[j - 1].dial.Add(dl);
                    
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        Scene scene = SceneManager.GetActiveScene();

        string lev_curr = scene.name;

        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);

            if(loadedScenes[i].name.Contains("Level") || loadedScenes[i].name.Contains("Tuto"))
            {
                lev_curr = loadedScenes[i].name;
            }
        }

        int myDial = 0;

        lev_curr = lev_curr.Split('_')[0];

        string[] what2say;


        for (int i = 0; i < dialogues.Count; i++)
        {

            if (dialogues[i].lang == sel_lang)
            {
                
                myDial = i;

                break; //don't need to check the remaining ones now that we found one
            }
        }

        for (int i = 0; i < dialogues[myDial].dial.Count; i++)
        {
            if (dialogues[myDial].dial[i].level == lev_curr)
            {

                what2say = dialogues[myDial].dial[i].say;
                text.text = "";
                foreach (string say in what2say)
                {
                    text.text = text.text + say;
                }
                

                break; //don't need to check the remaining ones now that we found one
            }
        }

        

    }
}
