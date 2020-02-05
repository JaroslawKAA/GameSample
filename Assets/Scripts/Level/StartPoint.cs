using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPoint : MonoBehaviour
{
    [Header("Seting's")]
    public float scalingSpeed = 1f;
    public AudioClip newIterationAudioClip;

    CharacterModeManager player;
    Transform weapon;


    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<CharacterModeManager>();
        CanvaManager.comunicatText.GetComponent<Text>().text = "";
        GameManager.score -= 10; //Because contact with this trigger add 10 to score, i have corect it after start game
    }

    private void OnTriggerEnter(Collider other)
    {
        //Reset hero param's in evryone game iteration
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterModeManager>().currentWeapon = null;
            if (other.GetComponent<CharacterModeManager>().rightHand.childCount > 1)
            {
                weapon = other.GetComponent<CharacterModeManager>().rightHand.GetChild(1);
            }
            if (player.bufforCurrentCharacterMode != player.deadMode)
            {
                NewIteration();
            }
        }

        //Activate startText if player is in idleMode in start point.
        if (player.currentCharacterMode == player.idleMode)
            CanvaManager.startText.SetActive(true);
        else
        {
            transform.GetComponent<AudioSource>().PlayOneShot(newIterationAudioClip);
        }
    }

    private void Update()
    {
        //If i have weapon, Destroy weapon obj.
        if (weapon != null)
        {
            StartCoroutine(IE_ScalingAndDestroingCurrentWeapon());
        }
    }

    /// <summary>
    /// Scale weapon to 0 and destroy object
    /// </summary>
    /// <returns></returns>
    IEnumerator IE_ScalingAndDestroingCurrentWeapon()
    {


        if (weapon.transform.localScale.x > 0 || weapon.transform.localScale.y > 0 || weapon.transform.localScale.x > 0)
        {
            Vector3 scale = weapon.transform.localScale;
            scale.x -= scalingSpeed * Time.deltaTime;
            scale.x = Mathf.Clamp(scale.x, 0, 100);
            scale.y -= scalingSpeed * Time.deltaTime;
            scale.y = Mathf.Clamp(scale.y, 0, 100);
            scale.z -= scalingSpeed * Time.deltaTime;
            scale.z = Mathf.Clamp(scale.z, 0, 100);

            weapon.transform.localScale = scale;
        }
        else if (weapon.transform.localScale.x == 0 || weapon.transform.localScale.y == 0 || weapon.transform.localScale.x == 0)
        {
            GameObject.Destroy(weapon.gameObject);
            GameObject.FindWithTag("Player").GetComponent<CharacterModeManager>().iHaveWeapon = false;
        }

        yield return new WaitForSeconds(0.1f);
    }

    /// <summary>
    /// Increment gameIteration param and add 10 to score
    /// </summary>
    void NewIteration()
    {
        GameManager.gameIteration++;
        GameManager.score += 10;
    }
}
