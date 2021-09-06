using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagDestroyer : MonoBehaviour
{

    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    private GameObject[] flags;

    private void TaskOnClick()
    {

        flags = GameObject.FindGameObjectsWithTag("FlagCreated");

        foreach (GameObject flag in flags)
        {
            Destroy(flag);
        }
    }
}
