using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }


    //Look at the player.
    void Update()
    {
        transform.LookAt(player);
    }
}
