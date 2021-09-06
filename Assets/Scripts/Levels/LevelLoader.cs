using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections.Specialized;

public class LevelLoader : MonoBehaviour
{
    public TextMeshProUGUI bombasText;
    public GameObject[] cubes_objects;
    private string level;
    private float x;
    private float z;
    private float start_x;
    private float start_z;
    private float max_x;
    private float max_z;
    private char[] arrayCubes;
    private int n_Bombas = 0;
    private List<string> levelArray = new List<string>();
    public int what2load;
    public Vector3 player_pos;
    private int n_missils = 0;

    // Start is called before the first frame update
    void Start()
    {
        levelArray.Add("1/x/2/1/0/0/0/0/e-1/o/o/x/2/1/1/0/0-1/1/1/1/1/x/1/0/0-x/1/0/0/o/o/o/o/o-1/1/1/1/1/0/1/x/1-x/1/1/x/2/0/1/1/1-o/o/o/x/2/1/1/2/x-p/0/1/1/1/1/x/1/1");
        levelArray.Add("1/1/1/0/0/0/0/0/e-1/x/1/0/0/0/1/1/1-1/1/1/1/1/1/1/x/1-0/0/0/2/x/2/1/1/1-0/0/0/2/x/2/0/0/0-1/1/1/1/1/1/1/1/1-1/x/1/0/0/0/1/x/1-p/1/1/0/0/0/1/1/1");
        levelArray.Add("1/1/1/0/0/0/0/0/c-1/x/1/1/1/1/1/1/1-1/1/m/1/x/1/1/x/1-0/0/0/1/1/1/1/1/1-1/1/1/1/1/1/1/m/0-1/x/2/x/1/1/x/1/0-1/1/m/1/1/1/1/1/0-p/0/1/x/1/0/0/0/0");
        levelArray.Add("0/0/0/0/0/0/0/0/e-0/0/0/0/0/0/0/0/0-0/0/1/1/1/0/0/0/0-0/0/1/x/1/0/0/0/0-0/0/1/1/1/1/1/1/0-0/0/0/0/0/1/x/1/0-1/1/1/0/0/1/1/1/0-p/x/1/0/0/0/0/0/0");
        levelArray.Add("0/0/0/0/0/0/0/0/e-0/0/0/0/0/0/0/0/0-0/0/0/0/0/0/0/0/0-0/0/0/0/0/0/0/0/0-0/0/0/0/0/0/0/0/0-0/0/0/0/0/0/0/0/0-0/0/0/0/0/0/0/0/0-p/0/0/0/0/0/0/0/0");
        levelArray.Add("0/0/0/0/0/0/0/0/e-0/0/0/0/0/0/0/0/0-0/0/0/0/0/0/0/0/0-0/0/0/0/0/0/0/0/0-0/0/0/0/0/0/0/0/0-0/0/0/0/0/0/0/0/0-e/x/0/0/0/0/0/0/0-p/x/0/0/0/0/0/0/0");
        levelArray.Add("o/0/1/1/1/1/x/1/e-0/o/1/x/1/1/1/1/0-1/1/o/1/2/1/1/1/1-1/x/1/o/1/x/1/1/x-1/1/t/0/o/1/1/1/1-1/1/0/0/1/o/0/1/1-x/1/0/0/1/x/o/z/x-1/1/1/1/2/1/1/o/1-p/0/1/x/1/0/0/0/o");

        level = levelArray[what2load];

        arrayCubes = level.ToCharArray(0, level.Length);

        foreach (char c in arrayCubes) {

            int rndRot = Random.Range(0, 3);
            Quaternion myRot = Quaternion.identity;
           // myRot.eulerAngles = new Vector3(0, 90 * rndRot, 0);

            switch (c)
            {
                case '0':
                    

                    if (calculaDistancia(x, z))
                    {
                        var newRock = Instantiate(cubes_objects[0], new Vector3(x, 0, z), myRot);
                        newRock.GetComponent<MeshRenderer>().enabled = false;
                    }
                    else
                    {
                        Instantiate(cubes_objects[0], new Vector3(x, 0, z), myRot);
                    }
                    break;
                case '1':
                    if (calculaDistancia(x, z))
                    {
                        var newRock = Instantiate(cubes_objects[1], new Vector3(x, 0, z), myRot);
                        newRock.GetComponent<MeshRenderer>().enabled = false;

                        var number = newRock.transform.GetChild(1);

                        //myRot.eulerAngles = new Vector3(0, -90 * rndRot, 0);

                        //number.transform.rotation = Quaternion.Euler(myRot);
                    }
                    else
                    {
                        var newRock = Instantiate(cubes_objects[1], new Vector3(x, 0, z), myRot);

                        var number = newRock.transform.GetChild(1);

                        //myRot.eulerAngles = new Vector3(0, -90 * rndRot, 0);

                        //number.transform.rotation = Quaternion.Euler(myRot);
                    }

                    break;
                case '2':
                    if (calculaDistancia(x, z))
                    {
                        var newRock = Instantiate(cubes_objects[2], new Vector3(x, 0, z), myRot);
                        newRock.GetComponent<MeshRenderer>().enabled = false;
                    }
                    else
                    {
                        var newRock = Instantiate(cubes_objects[2], new Vector3(x, 0, z), myRot);
                    }
                    break;
                case '3':
                    if (calculaDistancia(x, z))
                    {
                        var newRock = Instantiate(cubes_objects[3], new Vector3(x, 0, z), myRot);
                        newRock.GetComponent<MeshRenderer>().enabled = false;
                    }
                    else
                    {
                        var newRock = Instantiate(cubes_objects[3], new Vector3(x, 0, z), myRot);
                    }
                    break;
                case '4':
                    if (calculaDistancia(x, z))
                    {
                        var newRock = Instantiate(cubes_objects[4], new Vector3(x, 0, z), myRot);
                        newRock.GetComponent<MeshRenderer>().enabled = false;
                    }
                    else
                    {
                        var newRock = Instantiate(cubes_objects[4], new Vector3(x, 0, z), myRot);
                    }
                    break;
                case 'x':
                    if (calculaDistancia(x, z))
                    {
                        var newRock = Instantiate(cubes_objects[5], new Vector3(x, 0, z), myRot);
                        newRock.GetComponent<MeshRenderer>().enabled = false;
                    }
                    else
                    {
                        Instantiate(cubes_objects[5], new Vector3(x, 0, z), myRot);
                    }
                    n_Bombas++;
                    break;
                case 'm':
                    if (calculaDistancia(x, z))
                    {
                        var newRock = Instantiate(cubes_objects[9], new Vector3(x, 0, z), myRot);
                        newRock.GetComponent<MeshRenderer>().enabled = false;
                    }
                    else
                    {
                        Instantiate(cubes_objects[9], new Vector3(x, 0, z), myRot);
                    }
                    n_missils++;
                    break;
                case 'c':
                    if (calculaDistancia(x, z))
                    {
                        var newRock = Instantiate(cubes_objects[9], new Vector3(x, 0, z), myRot);
                        newRock.GetComponent<MeshRenderer>().enabled = false;
                    }
                    else
                    {
                        Instantiate(cubes_objects[10], new Vector3(x, 0, z), myRot);
                    }
                    break;
                case 'p':
                    Instantiate(cubes_objects[6], new Vector3(x, 0, z), Quaternion.identity);
                    break;
                case 't':
                    Instantiate(cubes_objects[11], new Vector3(x, 0, z), Quaternion.identity);
                    break;
                case 'z':
                    Instantiate(cubes_objects[12], new Vector3(x, 0, z), Quaternion.identity);
                    break;
                case 'e':
                    Instantiate(cubes_objects[7], new Vector3(x, 0, z), Quaternion.identity);
                    break;
                case 'o':
                    Instantiate(cubes_objects[8], new Vector3(x, 0, z), Quaternion.identity);
                    break;
                case '/':
                    x++;
                    break;
                case '-':
                    z--;
                    x = 0;
                    break;

            }
        }

        max_x = x;
        max_z = z;

        for (float i = max_z; i <= 0; i++)
        {
            Instantiate(cubes_objects[8], new Vector3(-1, 0, i), Quaternion.identity);
        }

        for (float i = max_x; i >= 0; i--)
        {
            Instantiate(cubes_objects[8], new Vector3(i, 0, 1), Quaternion.identity);
        }

        for (float i = max_x; i >= 0; i--)
        {
            Instantiate(cubes_objects[8], new Vector3(i, 0, max_z - 1), Quaternion.identity);
        }

        for (float i = max_z; i <= 0; i++)
        {
            Instantiate(cubes_objects[8], new Vector3(max_x + 1, 0, i), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool calculaDistancia(float x, float z)
    {
        Vector3 posRock = new Vector3(x, 0, z);
        Vector3 dist = new Vector3(posRock.x - player_pos.x, posRock.y - player_pos.y, posRock.z - player_pos.z);
        float result = Mathf.Abs(dist.magnitude);

        if(result > 1.5f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
