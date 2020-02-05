using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoAI : MonoBehaviour
{
    [Header("Setting's")]
    public Transform[] ufoTriggers;
    public Transform[] ufoWaypoints;

    public float stepMove = 1f;
    public float stepRotate = 10f;
    public float rotationYSpeed = 100f;

    public bool[] activatedTriggers; //Stan wszystkich triggerów.
    

    private void Start()
    {
        activatedTriggers = new bool[ufoTriggers.Length];        
    }


    // Update is called once per frame
    void Update()
    {
        UfoBehavior();
        ActivateTrigger();
    }

    /// <summary>
    /// Ufo behavior.
    /// </summary>
    private void UfoBehavior()
    {
        //Stay in start position if everyone trigger are deactiveted
        if (EveryoneTriggerAreDeactivated(activatedTriggers))
        {
            RotateUfo(0);
        }

        int i = 0; //Iteration foreach

        //If one trigger are acive go to it and rotate to trigger.rotation.
        foreach (bool item in activatedTriggers)
        {
            if (item == true && transform.position == ufoWaypoints[i + 1].transform.position)
            {
                RotateUfo(i + 1);
            }
            else if (item == true && transform.position != ufoWaypoints[i + 1].transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, ufoWaypoints[i + 1].transform.position, stepMove * Time.deltaTime);
                RotateUfo(i + 1);
            }

            i++;
        }

        //If ufo are in endWaypoint.position, set ufo in startWayPoint.position
        if (transform.position == ufoWaypoints[ufoWaypoints.Length - 1].position)
        {
            DeactivateAllTriggers();            
            SetUfoInStartPosition();
        }

    }

    /// <summary>
    /// Check all trigger's in ufoTrigger and save state.
    /// </summary>
    void ActivateTrigger()
    {
        int i = 0;
        foreach (Transform item in ufoTriggers)
        {
            if (item.transform.GetComponent<PlayerIsHereTrigger>().playerIsHere == true)
            {
                activatedTriggers[i] = true;

                //Deactivate others trigger's.
                for (int j = 0; j < activatedTriggers.Length; j++)
                {
                    if (j != i)
                    {
                        activatedTriggers[j] = false;
                    }
                }
            }

            i++;
        }
    }

    /// <summary>
    /// Check do everyone triggers are deactivated.
    /// </summary>
    /// <param name="tab">Table of the triggers.</param>
    /// <returns></returns>
    bool EveryoneTriggerAreDeactivated(bool[] tab)
    {
        bool result = true;

        foreach (bool item in tab)
        {
            if (item == true)
            {
                result = false;
                break;
            }
        }

        return result;
    }

    /// <summary>
    /// Rotate ufo to current waypoint rotation.
    /// </summary>
    /// <param name="i">Number of the waypoint</param>
    void RotateUfo(int i)
    {
        Quaternion targetRotation = Quaternion.Euler(ufoWaypoints[i].transform.localEulerAngles.x, ufoWaypoints[i].transform.localEulerAngles.y, ufoWaypoints[i].transform.localEulerAngles.z);

        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, stepRotate * Time.deltaTime);
    }

    /// <summary>
    /// Deactivate all triggers.
    /// </summary>
    void DeactivateAllTriggers()
    {
        int i = 0;

        foreach (bool item in activatedTriggers)
        {
            activatedTriggers[i] = false;

            i++;
        }
    }

    /// <summary>
    /// Set ufo in start waypoint.
    /// </summary>
    void SetUfoInStartPosition()
    {
        transform.position = ufoWaypoints[0].position;
    }
}
