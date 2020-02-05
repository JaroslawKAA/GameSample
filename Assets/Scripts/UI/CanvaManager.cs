using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvaManager : MonoBehaviour
{
    [Header("Seting's")]
    public GameObject startTextP;
    public GameObject comunicatTextP;
    public GameObject deadTextP;
    public GameObject iterationTextP;
    public GameObject scoreTextP;

    static public GameObject startText;
    static public GameObject comunicatText;
    static public GameObject deadText;
    static public GameObject iterationText;
    static public GameObject scoreText;
    CharacterModeManager player;
    [HideInInspector]public static bool lifeBuffor;

    private void Awake()
    {
        //Inicjalization static var, because i would like easier to refer to them
        startText = startTextP;
        comunicatText = comunicatTextP;
        deadText = deadTextP;
        iterationText = iterationTextP;
        scoreText = scoreTextP;        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<CharacterModeManager>();
        lifeBuffor = true;        
    }

    // Update is called once per frame
    void Update()
    {        
        InstantiateDeadText();
        ControlStartText();
        
        //ComunicatText control by currentCharacteMode.

        PrintGameIteration();
    }

    /// <summary>
    /// Controle startText.
    /// </summary>
    private void ControlStartText()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startText.SetActive(false);
        }
    }

    /// <summary>
    /// Actualize GameIterationText.
    /// </summary>
    private void PrintGameIteration()
    {        
        iterationText.GetComponent<Text>().text = "Iteration: " + GameManager.gameIteration;
        scoreText.GetComponent<Text>().text = "Score: " + GameManager.score;

        if( player.currentCharacterMode  == player.idleMode && player.currentObstacle != CharacterModeManager.Obstacles.Enemy)
        {
            iterationText.SetActive(false);
            scoreText.SetActive(false);
        }
        else
        {
            iterationText.SetActive(true);
            scoreText.SetActive(true);
        }
    }

    /// <summary>
    /// Instantiate deadText in Canvas after dead.
    /// </summary>
    void InstantiateDeadText()
    {
        if (lifeBuffor != player.life && player.life == false)
        {
            GameObject obj = Instantiate(deadText, transform.GetComponent<RectTransform>().position,
                            Quaternion.Euler(transform.GetComponent<RectTransform>().localRotation.eulerAngles.x,
                            transform.GetComponent<RectTransform>().localRotation.eulerAngles.y, 33),
                            transform.GetComponent<RectTransform>());

            lifeBuffor = false;
        }
    }
}
