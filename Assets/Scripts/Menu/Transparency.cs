using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparency : MonoBehaviour
{
    public float transparency;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, transparency);

    }
}
