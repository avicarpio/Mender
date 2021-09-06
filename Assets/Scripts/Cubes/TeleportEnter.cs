using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEnter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<PlayerController>().teleporting)
            {
                collision.gameObject.GetComponent<PlayerController>().StopCoroutine(collision.gameObject.GetComponent<PlayerController>().moveCoro);
                collision.gameObject.transform.position = new Vector3(12, 1.61f, 0);
                collision.gameObject.GetComponent<PlayerController>().teleporting = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.GetComponent<PlayerController>().teleporting = false;
    }
}
