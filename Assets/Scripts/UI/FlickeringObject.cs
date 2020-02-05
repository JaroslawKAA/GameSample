using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickeringObject : MonoBehaviour
{
    [Header("Setting's")]
    public float timerMin = 0.5f;
    public float timerMax = 1f;

    float timer;
    Color color;

    // Start is called before the first frame update
    void Start()
    {        
        color = transform.GetComponent<Text>().color;
        timer = UnityEngine.Random.Range(timerMin, timerMax);
    }

    // Update is called once per frame
    void Update()
    {
        FlickerFunction();          
    }

    /// <summary>
    /// Flicker text
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

            gameObject.GetComponent<Text>().color = color;
            

            timer = UnityEngine.Random.Range(timerMin, timerMax);
        }
    }
}
