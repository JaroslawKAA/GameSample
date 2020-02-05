using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadText : MonoBehaviour
{
    [Header("Setting's")]
    public int scalingSpeed = 1;
    public float alphaStep = 1f;
    public int fontSize = 40;

    CharacterModeManager player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<CharacterModeManager>();
        transform.GetComponent<Text>().color = new Color(transform.GetComponent<Text>().color.r, transform.GetComponent<Text>().color.g, transform.GetComponent<Text>().color.b, 0);
        
    }

    // Scales text to fontSize.
    void Update()
    {
        if (transform.GetComponent<Text>().fontSize >= fontSize)
        {
            transform.GetComponent<Text>().fontSize -= scalingSpeed;
            transform.GetComponent<Text>().fontSize = Mathf.Clamp(transform.GetComponent<Text>().fontSize, fontSize, 300);
        }

        if (transform.GetComponent<Text>().color.a < 1)
        {
            float alpha = transform.GetComponent<Text>().color.a + (alphaStep * Time.deltaTime);
            alpha = Mathf.Clamp(alpha, 0, 1);
            transform.GetComponent<Text>().color = new Color(transform.GetComponent<Text>().color.r, transform.GetComponent<Text>().color.g, transform.GetComponent<Text>().color.b, alpha);

        }

        if (player.currentCharacterMode != player.deadMode)
        {
            Destroy(gameObject);
        }
    }
}
