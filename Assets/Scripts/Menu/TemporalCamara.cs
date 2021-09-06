using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemporalCamara : MonoBehaviour
{
    public static TemporalCamara instance;
    public GameObject downBar;
    public GameObject mainCamera;
    public Sprite backSelected;
    public Sprite backNotSelected;
    private GameObject left,center,right;
    private bool start;
    public bool shop;
    public int position;

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    private Vector3 vectorInici;
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool Tap { get { return tap; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    private Animator animator;

    // Start is called before the first frame update
    void Start(){
        start = true;
        shop = false;
        position = 1;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //DownBar Backs
        GameObject left = downBar.transform.Find("LeftBack").gameObject;
        GameObject center = downBar.transform.Find("CenterBack").gameObject;
        GameObject right = downBar.transform.Find("RightBack").gameObject;
        //DownBar Texts
        GameObject leftTxt = downBar.transform.Find("TiendaText").gameObject;
        GameObject centerTxt = downBar.transform.Find("WorldsText").gameObject;
        GameObject rightTxt = downBar.transform.Find("PlayerText").gameObject;
        //Medias de Escalado
        Vector3 scale = new Vector3(0, 0.2f, 0);
        //Worlds, Tienda and Player Img
        GameObject tiendaImg = downBar.transform.Find("Shop").gameObject;
        GameObject worldsImg = downBar.transform.Find("Worlds").gameObject;
        GameObject playerImg = downBar.transform.Find("Player").gameObject;

        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile Input
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                tap = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }
        #endregion

        // Calculate the distance
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        //Did we cross the distance?
        if (swipeDelta.magnitude > 125)
        {
            //Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or right
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                // Up or down
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }
            Reset();
        }

        if (SwipeLeft)
        {

            if(position == 0)
            {
                position = 1;

            }else if(position == 1)
            {
                position = 2;
            }
        }

        if (SwipeRight)
        {

            if (position == 2)
            {
                position = 1;
            }
            else if (position == 1)
            {
                position = 0;
            }
        }


        if (position == 1)
        {
            //Worlds
            leftTxt.SetActive(false);
            centerTxt.SetActive(true);
            rightTxt.SetActive(false);

            left.GetComponent<Image>().sprite = backNotSelected;
            center.GetComponent<Image>().sprite = backSelected;
            right.GetComponent<Image>().sprite = backNotSelected;

            left.transform.localPosition = new Vector3(-143, -300f, 0f);
            center.transform.localPosition = new Vector3(48f, -320, 0f);
            right.transform.localPosition = new Vector3(143, -300f, 0f);

            tiendaImg.transform.localScale = new Vector3(1f, 0.7f, tiendaImg.transform.localScale.z);
            worldsImg.transform.localScale = new Vector3(1f, 0.7f, worldsImg.transform.localScale.z);
            playerImg.transform.localScale = new Vector3(1f, 0.7f, playerImg.transform.localScale.z);

            tiendaImg.transform.localPosition = new Vector3(-128, -275 , 0);
            worldsImg.transform.localPosition = new Vector3(0, -255 , 0);
            playerImg.transform.localPosition = new Vector3(128, -275 , 0);

            //mainCamera.transform.position = new Vector3(0, mainCamera.transform.position.y, mainCamera.transform.position.z);
            animator.SetInteger("screen", 0);
            start = false;
        }
        if (position == 0)
        {
            //Tienda
            leftTxt.SetActive(true);
            centerTxt.SetActive(false);
            rightTxt.SetActive(false);

            left.GetComponent<Image>().sprite = backSelected;
            center.GetComponent<Image>().sprite = backNotSelected;
            right.GetComponent<Image>().sprite = backNotSelected;

            left.transform.localPosition = new Vector3(-143, -320f, 0f);
            center.transform.localPosition = new Vector3(48f, -300, 0f);
            right.transform.localPosition = new Vector3(143, -300f, 0f);

            tiendaImg.transform.localScale = new Vector3(1.2f, 0.9f, tiendaImg.transform.localScale.z);
            worldsImg.transform.localScale = new Vector3(0.8f, 0.5f, worldsImg.transform.localScale.z);
            playerImg.transform.localScale = new Vector3(1f, 0.7f, playerImg.transform.localScale.z);

            tiendaImg.transform.localPosition = new Vector3(-130, -250, 0);
            worldsImg.transform.localPosition = new Vector3(0, -275, 0);
            playerImg.transform.localPosition = new Vector3(128, -275, 0);

            //mainCamera.transform.position = new Vector3(-5.5f, mainCamera.transform.position.y, mainCamera.transform.position.z);
            animator.SetInteger("screen", 1);
            shop = false;
        }
        if (position == 2)
        {
            //Player
            leftTxt.SetActive(false);
            centerTxt.SetActive(false);
            rightTxt.SetActive(true);

            left.GetComponent<Image>().sprite = backNotSelected;
            center.GetComponent<Image>().sprite = backNotSelected;
            right.GetComponent<Image>().sprite = backSelected;

            left.transform.localPosition = new Vector3(-143, -300f, 0f);
            center.transform.localPosition = new Vector3(48f, -300, 0f);
            right.transform.localPosition = new Vector3(143, -320f, 0f);

            tiendaImg.transform.localScale = new Vector3(1f, 0.7f, tiendaImg.transform.localScale.z);
            worldsImg.transform.localScale = new Vector3(0.8f, 0.5f, worldsImg.transform.localScale.z);
            playerImg.transform.localScale = new Vector3(1.2f, 0.9f, playerImg.transform.localScale.z);

            tiendaImg.transform.localPosition = new Vector3(-128, -275, 0);
            worldsImg.transform.localPosition = new Vector3(0, -275, 0);
            playerImg.transform.localPosition = new Vector3(132, -250, 0);

            //mainCamera.transform.position = new Vector3(5.5f, mainCamera.transform.position.y, mainCamera.transform.position.z);
            animator.SetInteger("screen", 2);
        }

    }

    void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}
