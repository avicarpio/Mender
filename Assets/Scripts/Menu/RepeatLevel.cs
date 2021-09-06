using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RepeatLevel : MonoBehaviour
{
    public class SaveData
    {
        public int curr_level;
        public float coins;
        public string lang;
        public int total_levels;
    }

    public void repeat()
    {
        try
        {
            String level = this.GetComponentInChildren<TextMeshProUGUI>().text;

            int repeat_level = Int32.Parse(level) + 3;

            SceneManager.LoadScene(repeat_level);
            var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
            SceneManager.LoadScene("Robot", parameters);

        }
        catch (Exception e)
        {
        }
    }

}
