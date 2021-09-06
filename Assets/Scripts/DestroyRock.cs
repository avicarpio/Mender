using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DestroyRock : MonoBehaviour  
{

    private GameObject image;
    private AudioSource adSource;
    private AudioClip[] adClips;

    void Start()
    {
        adClips = Resources.LoadAll<AudioClip>("Sounds/PickSounds");
        adSource = GameObject.FindGameObjectWithTag("RockSound").GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            MeshRenderer meshRock = gameObject.GetComponent<MeshRenderer>();
            if (meshRock.enabled)
            {
                StartCoroutine(playAudio());
                meshRock.enabled = false;
            }

            string[] number = gameObject.name.Split('_');

            image = GameObject.Find("Danger");


            float alpha = 0;

            if (number[0] == "0" || number[0] == "1" || number[0] == "2" || number[0] == "3" || number[0] == "4")
            {
                alpha = ((float)int.Parse(number[0]) / 4);
            }

            image.GetComponent<Image>().color = new Color(image.GetComponent<Image>().color.r, image.GetComponent<Image>().color.g, image.GetComponent<Image>().color.b, alpha);

        }
    }
    
    IEnumerator playAudio()
    {

        int sel_audio = Random.Range(0, adClips.Length);

        adSource.clip = adClips[sel_audio];
        adSource.Play();
        yield return null;

    }
    
}
