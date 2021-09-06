using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MisilDetector : MonoBehaviour
{

    private GameObject isMisil;
    public Material grey;
    private bool usedMisil = false;

    private void Start()
    {
        GameObject[] imagesMisil = GameObject.FindGameObjectsWithTag("MisilImage");

        isMisil = imagesMisil[0];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MeshRenderer meshRock = gameObject.GetComponent<MeshRenderer>();
            meshRock.enabled = false;
            if (!isMisil.GetComponent<Image>().enabled && !usedMisil)
            {
                isMisil.GetComponent<Image>().enabled = true;
                usedMisil = false;

                GameObject misil = this.gameObject.transform.GetChild(0).gameObject;

                misil.GetComponent<Renderer>().material = grey;
            }
            
        }
    }
}
