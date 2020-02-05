using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGameObject : MonoBehaviour
{
    [Header("Setting's")]
    public GameObject objectToInstantiate;
    public float scalingSpeed = 1.5f;    
    public GameObject spawned;

    CharacterModeManager player;
    Transform startPoint;


    
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<CharacterModeManager>();
        startPoint = GameObject.Find("StartPoint").transform;
    }

    //Spawn obj when player is in startPoint
    void Update()
    {

        if (startPoint.GetComponent<PlayerIsHereTrigger>().playerIsHere && spawned == null)        {
            
            spawned = Instantiate(objectToInstantiate, transform.position, transform.rotation);
            spawned.transform.localScale = new Vector3(0, 0, 0);
            spawned.transform.parent = GameObject.Find("---LEVEL---").transform;
        }
        if (spawned != null && player.iHaveWeapon == false)
        {

            //Scale obj to 1.
            if (spawned.transform.localScale.x < 1 || spawned.transform.localScale.x < 1 || spawned.transform.localScale.x < 1)
            {
                Vector3 scale = spawned.transform.localScale;
                scale.x += scalingSpeed * Time.deltaTime;
                scale.x = Mathf.Clamp(scale.x, 0, 1);
                scale.y += scalingSpeed * Time.deltaTime;
                scale.y = Mathf.Clamp(scale.y, 0, 1);
                scale.z += scalingSpeed * Time.deltaTime;
                scale.z = Mathf.Clamp(scale.z, 0, 1);

                spawned.transform.localScale = scale;
            }
        }

    }
}
