using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BafaradaAnim : MonoBehaviour
{
    public Animator animator;
    bool click = false;
    private GameObject player;
    private bool canMove;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player_Cube(Clone)");
        print(player.ToString());
        player = player.gameObject.transform.Find("Player_Control").gameObject;
        canMove = false;
        player.gameObject.GetComponent<PlayerController>().allowMovement = canMove;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            player.gameObject.GetComponent<PlayerController>().allowMovement = false;
        }
        animator.SetBool("onClick", click);
    }

    void OnMouseUp()
    {
        click = true;
        player = GameObject.Find("Player_Cube(Clone)");
        player = player.gameObject.transform.Find("Player_Control").gameObject;
        player.gameObject.GetComponent<PlayerController>().allowMovement = true;
        canMove = true;
    }
}
