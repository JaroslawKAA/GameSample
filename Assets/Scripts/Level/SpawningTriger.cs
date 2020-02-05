using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningTriger : MonoBehaviour
{
    [Header("Setting's")]
    public Transform monsterSpawnPosition;
    public GameObject monsterGameObject;
    public float scalingSpeed = 1f;

    static GameObject spawned;

    // Start is called before the first frame update
    void Start()
    {
        monsterSpawnPosition = transform.GetChild(0).transform;
    }

    // If player are in trigger spawn monster
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (spawned == null)
            {
                spawned = Instantiate(monsterGameObject, monsterSpawnPosition.position, monsterSpawnPosition.rotation);
                spawned.transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }

    //Scale object to 1 after spawn
    private void Update()
    {
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
