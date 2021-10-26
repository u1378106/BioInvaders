using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartManager : MonoBehaviour
{
    [SerializeField]
    GameObject player, introText, startButton, StartUI, background, title, virus, score, countdown, playerBlink;

    [SerializeField]
    Animator newBg;

    private Animator playerAnim, introTextAnim, startButtonAnim, bgAnim;

    CameraMovement camMovement;
    PlayerMovement playerMovement;
    SpawnManager spawnManager;
    ScoreManager scoreManager;
    AudioManager audioManager;

    bool isGameReady = false;

    private void Awake()
    {
        camMovement = GameObject.FindObjectOfType<CameraMovement>();
        camMovement.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {     
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        playerMovement.enabled = false;

        spawnManager = GameObject.FindObjectOfType<SpawnManager>();
        spawnManager.enabled = false;

        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        scoreManager.enabled = false;

        audioManager = GameObject.FindObjectOfType<AudioManager>();

        Camera.main.GetComponent<ColorCycler>().enabled = false;
        startButton.GetComponent<Button>().interactable = false;

        playerAnim = player.GetComponent<Animator>();
        introTextAnim = introText.GetComponent<Animator>();
        startButtonAnim = startButton.GetComponent<Animator>();
        bgAnim = background.GetComponent<Animator>();

        playerAnim.enabled = false;
        introTextAnim.enabled = false;
        startButtonAnim.enabled = false;
        bgAnim.enabled = false;

        newBg.enabled = false;

        score.SetActive(false);
        countdown.SetActive(false);
        playerBlink.GetComponent<Animator>().enabled = false;

        StartCoroutine(EnableStartButton());
    }

    public void StartGame()
    {
        audioManager.startAudio.GetComponent<AudioSource>().Play();

        playerAnim.enabled = true;
        introTextAnim.enabled = true;
        startButtonAnim.enabled = true;
        bgAnim.enabled = true;
        startButton.GetComponent<Button>().interactable = false;

        //title.SetActive(false);
        StartCoroutine(LogoFade());

        newBg.enabled = true;

        Camera.main.GetComponent<ColorCycler>().enabled = true;
        Camera.main.GetComponent<CameraMovement>().cameraSpeed = 0.08f;

        StartCoroutine(StartBgAudio());

    }

    IEnumerator LogoFade()
    {
        title.GetComponent<Animator>().speed = 0f;
        virus.GetComponent<Animator>().speed = 0f;

        yield return new WaitForSeconds(1.5f);

        title.GetComponent<Animator>().SetTrigger("bio_fade");
        title.GetComponent<Animator>().speed = 1;

        virus.GetComponent<Animator>().SetTrigger("virus_fade");
        virus.GetComponent<Animator>().speed = 1;
    }

    private void Update()
    {    
        if (!isGameReady)
        {
            if (Camera.main.transform.position.y >= 0)
            {
                isGameReady = true;

                Camera.main.GetComponent<CameraMovement>().cameraSpeed = 0f;

                countdown.SetActive(true);
                playerBlink.GetComponent<Animator>().enabled = true;

                StartCoroutine(DelayCameraMovement());

                StartUI.SetActive(false);              
            }
        }     
    }

    IEnumerator DelayCameraMovement()
    {         

        yield return new WaitForSeconds(7f);

        playerMovement.enabled = true;
        
        scoreManager.enabled = true;
        score.SetActive(true);
        Destroy(countdown);
        Destroy(newBg.gameObject);
        Camera.main.GetComponent<CameraMovement>().cameraSpeed = 0.04f;

        yield return new WaitForSeconds(2f);
        
        spawnManager.enabled = true;
    }

    IEnumerator StartBgAudio()
    {
        audioManager.startScreenAudio.GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(1f);
        audioManager.bgAudios[Random.Range(0,3)].GetComponent<AudioSource>().Play();
        
        camMovement.enabled = true;
    }

    IEnumerator EnableStartButton()
    {
        yield return new WaitForSeconds(1.5f);
        startButton.GetComponent<Button>().interactable = true;
    }
}
