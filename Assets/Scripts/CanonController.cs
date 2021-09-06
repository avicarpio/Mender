using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanonController : MonoBehaviour
{
    public GameObject isMisil;
    private int hp = 3;

    private void Start()
    {
        GameObject[] imagesMisil = GameObject.FindGameObjectsWithTag("MisilImage");

        isMisil = imagesMisil[0];
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Boss Health: " + hp);

        if (collision.gameObject.tag == "Player")
        {
            MeshRenderer meshRock = gameObject.GetComponent<MeshRenderer>();
            meshRock.enabled = false;
            if (isMisil.GetComponent<Image>().enabled)
            {
                hp--;
                print("Boss Health: " + hp);
                isMisil.GetComponent<Image>().enabled = false;
            }

            if(hp == 0)
            {
                print("Boss Health: " + hp);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

        }
    }

}
