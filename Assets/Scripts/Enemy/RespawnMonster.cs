using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnMonster : MonoBehaviour
{
    [Header("Setting's")]
    public GameObject objectToInstantiate;    
    public float scalingSpeed = 1f;

    static GameObject spawned;
    GameObject startPoint;

    
    private void Start()
    {
        startPoint = GameObject.Find("StartPoint");
    }

    
    void Update()
    {        
        //Spawn a monster, if the game iteration changes, and there is no monster. Also when the game is starting.
        if (startPoint.GetComponent<PlayerIsHereTrigger>().playerIsHere && spawned == null)
        {
            spawned = Instantiate(objectToInstantiate, transform.position, transform.rotation);
            spawned.transform.localScale = new Vector3(0, 0, 0);
            spawned.transform.parent = GameObject.Find("---Enemies---").transform;
        }

        //Scaling monster to scale 1, after spawning the monster.
        if (spawned != null)
        {
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
