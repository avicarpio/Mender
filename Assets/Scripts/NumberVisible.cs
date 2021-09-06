using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberVisible : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject number = this.gameObject.transform.GetChild(1).gameObject;
            number.transform.position = number.transform.position + new Vector3(0, 2, 0);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject number = this.gameObject.transform.GetChild(1).gameObject;
            number.transform.position = number.transform.position + new Vector3(0, -2, 0);
        }
    }
}
