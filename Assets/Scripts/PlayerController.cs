using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private AudioSource adSource;
    public AudioClip walkSound;
    private float anim_pick;
    private GameObject child;
    public bool teleporting;

    private float movement;
    private float rotation;
    private float speed = 0.25f;
    public GameObject flag;
    public IEnumerator moveCoro;
    private float counter;
    private float aux;
    private bool pressed;

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;
    public bool allowMovement;

    private Vector3 vectorInici;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool Tap { get { return tap; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    public void Start()
    {
        child = this.gameObject.transform.GetChild(0).gameObject;
        pressed = false;
        allowMovement = true;
        animator = GetComponentInChildren<Animator>();
        adSource = GetComponent<AudioSource>();
        teleporting = false;
    }

    private void Update()
    {
        if(anim_pick >= 1)
        {
            anim_pick += Time.deltaTime;
            if(anim_pick >= 1.5f)
            {
                print("Finish");
                animator.SetBool("Picar", false);
                anim_pick = 0;
            }
        }
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

        if (SwipeRight && allowMovement)
        {

            movement = 0;
            child.transform.localRotation = Quaternion.Euler(0, 90, 0);
            vectorInici = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            moveCoro = Move(new Vector3(1, 0, 0));
            StartCoroutine(moveCoro);
            playPick();
            StartCoroutine(playAudio());

        }

        if (SwipeLeft && allowMovement)
        {

            movement = 0;
            child.transform.localRotation = Quaternion.Euler(0, -90, 0);
            vectorInici = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            moveCoro = Move(new Vector3(-1, 0, 0));
            StartCoroutine(moveCoro);
            playPick();
            StartCoroutine(playAudio());

        }

        if (SwipeUp && allowMovement)
        {

            movement = 0;
            child.transform.localRotation = Quaternion.Euler(0, 0, 0);
            vectorInici = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            moveCoro = Move(new Vector3(0, 0, 1));
            StartCoroutine(moveCoro);
            playPick();
            StartCoroutine(playAudio());

        }

        if (SwipeDown && allowMovement)
        {

            movement = 0;
            child.transform.localRotation = Quaternion.Euler(0, 180, 0);
            vectorInici = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            moveCoro = Move(new Vector3(0, 0, -1));
            StartCoroutine(moveCoro);
            playPick();
            StartCoroutine(playAudio());

        }
    }

    void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NoPass" && !teleporting)
        {
            StopCoroutine(moveCoro);
            transform.position = vectorInici;
        }
    }

    private IEnumerator Move(Vector3 vectorDirect)
    {

        while (movement < 1)
        {
            transform.position = transform.position + new Vector3(vectorDirect.x * speed, 0, vectorDirect.z * speed);
            movement += speed;
            yield return 0;
        }
    }


    private void playPick()
    {
        anim_pick = 1;
        animator.SetBool("Picar", true);
    }

    IEnumerator playAudio()
    {

        adSource.clip = walkSound;
        adSource.Play();
        yield return null;

    }

}
