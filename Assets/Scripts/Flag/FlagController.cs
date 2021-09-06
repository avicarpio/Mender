using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    private bool dragging = false;
    private float distance;
    private RaycastHit hit;
    private Ray ray;
    public GameObject flag;
    private Vector3 initPos;
    private GameObject player;

    void OnMouseDown()
    {
        player = GameObject.Find("Player_Cube(Clone)");
        player = player.gameObject.transform.Find("Player_Control").gameObject;

        player.gameObject.GetComponent<PlayerController>().allowMovement = false;

        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
        initPos = transform.position;
    }

    void OnMouseUp()
    {
        dragging = false;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 100.0f);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if(hit.collider.gameObject.tag == "Rock")
            {
                GameObject rock = hit.collider.gameObject;
                print(rock.gameObject.tag);

                if (rock != null)
                {
                    Vector3 flag_pos = new Vector3(rock.transform.position.x, rock.transform.position.y + 1, rock.transform.position.z - 0.1f);
                    var flagnew = Instantiate(flag, flag_pos, Quaternion.identity);
                    flagnew.transform.eulerAngles = new Vector3(90, 0, 0);
                    print("New flag");
                }
                else
                {
                    print("NO ROCK");
                }
            }
        }
        player.gameObject.GetComponent<PlayerController>().allowMovement = true;
        transform.position = initPos;

    }

    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        }
    }

}
