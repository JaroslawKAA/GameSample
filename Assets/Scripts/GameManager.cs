using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Setting's")]
    static public int gameIteration = 0;
    static public int score = 0;
    public GameObject player;
    Transform startPoint;

    [Header("Music and Sounds")]
    public AudioClip mainMusic;
    [HideInInspector] static public AudioSource mainSource;


    // Instantiate player in startPoint.
    private void Awake()
    {
        startPoint = GameObject.Find("StartPoint").transform;

        Instantiate(player, startPoint.position, startPoint.rotation);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Play main music.
    void Start()
    {

        mainSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        mainSource.clip = mainMusic;
        mainSource.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
