using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsHereTrigger : MonoBehaviour
{
    [Header("Setting's")]
    public bool playerIsHere = false;

    //If player is in trigger return true.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsHere = true;
        }
    }

    //If player is not in trigger return false.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsHere = false;
        }
    }
}
