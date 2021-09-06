using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public AudioClip explosionSound;
    public AudioClip gameOverSound;
    private AudioSource adSource;
    private AudioSource gameOverSource;
    private AudioSource backMusic;
    public GameObject explosion;
    private GameObject player;
    private GameObject child;
    private Animator playerAnim;
    private bool dead;
    private float restart_Counter;

    private void Start()
    {
        restart_Counter = 0;
        dead = false;
        adSource = GameObject.FindGameObjectWithTag("ExplosionSound").GetComponent<AudioSource>();
        backMusic = GameObject.FindGameObjectWithTag("BackMusic").GetComponent<AudioSource>();
        gameOverSource = GameObject.FindGameObjectWithTag("GameOverSound").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (dead)
        {
            restart_Counter += Time.deltaTime;
            if(restart_Counter > 4)
            {
                reloadLevel();
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            //StartCoroutine(GameObject.Find("Main Camera").GetComponent<CameraController>().Shake());

            Debug.Log("Game Over");
            Instantiate(explosion, new Vector3(transform.position.x,
                                               transform.position.y + 1, 
                                               transform.position.z),
                                               explosion.transform.rotation);

            StartCoroutine(playAudio());

            player = collision.gameObject;
            player.gameObject.GetComponent<PlayerController>().allowMovement = false;

            child = player.gameObject.transform.GetChild(0).gameObject;

            child.transform.localRotation = Quaternion.Euler(0, child.transform.localEulerAngles.y + 180, 0);

            playerAnim = player.GetComponentInChildren<Animator>();

            playerAnim.SetBool("Dead", true);

            dead = true;
        }
    }

    void reloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
        SceneManager.LoadScene("Robot", parameters);
    }

    IEnumerator playAudio()
    {
        backMusic.volume = 0;
        gameOverSource.clip = gameOverSound;
        gameOverSource.volume = 0.5f;
        gameOverSource.Play();
        adSource.clip = explosionSound;
        adSource.Play();
        yield return null;

    }


}
