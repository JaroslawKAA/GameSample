using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [Header("Setting's")]
    public Transform startWayPoint;
    public Transform endWayPoint;
    public float moveSteps = 1;
    public float delay = 1f;

    bool iSawPlayer = false;
    bool toForward = true;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = delay;
        delay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        //Draw a ray from the front.
        if (iSawPlayer == false)
            if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.TransformDirection(Vector3.forward), out hit, 5f))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    iSawPlayer = true;
                }
            }
        //Start tank movement after first contact with player
        if (iSawPlayer)
            TankMovement();
    }

    //Kill player if he touch me.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterModeManager>().currentCharacterMode.ToDeadMode();
            other.GetComponent<AudioSource>().PlayOneShot(other.GetComponent<CharacterModeManager>().robotDeadAudioClip);
        }
    }

    /// <summary>
    /// Behavior of the tank.
    /// </summary>
    void TankMovement()
    {
        float step = moveSteps * Time.deltaTime;

        //Wait in the start position until delay drops to zero.
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        //Go to endWayPoint.position.
        if (toForward == true && delay <= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, endWayPoint.position, step * 1.5f);
            if (transform.position == endWayPoint.position)
            {
                toForward = false;
            }
        }
        //Go to startWayPoint.position.
        else if (toForward == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, startWayPoint.position, step);
            if (transform.position == startWayPoint.position)
            {
                toForward = true;
                delay = timer;
            }
        }
    }

}
