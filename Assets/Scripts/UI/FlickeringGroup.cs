using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickeringGroup : MonoBehaviour
{
    [Header("Setting's")]
    public float timerMin = 0.5f;
    public float timerMax = 1f;

    float timer;
    Color color;    

    public Transform[] textObjects;

    // Start is called before the first frame update
    void Start()
    {
        textObjects = new Transform[transform.GetChildCount()];
        color = transform.GetChild(0).GetComponent<Text>().color;
        timer = UnityEngine.Random.Range(timerMin, timerMax);        

        int i = 0;
        foreach (Transform item in textObjects)
        {
            textObjects[i] = transform.GetChild(i);

            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FlickerFunction();                  
    }

    /// <summary>
    /// Flicker a lot of text Object's
    /// </summary>
    private void FlickerFunction()
    {       

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            float alpha = color.a;
            if (alpha == 1)
            {
                alpha = 0;
            }
            else
                alpha = 1;

            color = new Color(color.r, color.g, color.b, alpha);

            foreach (Transform item in textObjects)
            {
                item.GetComponent<Text>().color = color;
            }

            timer = UnityEngine.Random.Range(timerMin, timerMax);
        }
    }
}
