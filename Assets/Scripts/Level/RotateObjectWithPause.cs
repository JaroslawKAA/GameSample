using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectWithPause : MonoBehaviour
{
    [Header("Setting's")]
    public float rotationSpeed = 100f;
    public bool rotX = false;
    public bool rotY = false;
    public bool rotZ = false;
    public float rotationDelay = 1;
    public float angleToRotate = 360f;

    float bufforRotationDelay;
    float bufforAngleToRotate;

    private void Start()
    {
        bufforRotationDelay = rotationDelay;
        bufforAngleToRotate = angleToRotate;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (rotationDelay > 0)
        {
            rotationDelay -= Time.deltaTime;
        }
        else if(rotationDelay < 0 && angleToRotate >= 0)
        {
            float step = rotationSpeed * Time.deltaTime;
            angleToRotate -= step;

            //Subtract rotation if it exceeds zero 360 degrees
            if (angleToRotate <= 0)
            {
                step += angleToRotate;

                rotationDelay = bufforRotationDelay;
                angleToRotate = bufforAngleToRotate;
            }

            if (rotX == true)
            {
                transform.Rotate(step, 0, 0);
            }
            if (rotY == true)
            {
                transform.Rotate(0, step, 0);
            }
            if (rotZ == true)
            {
                transform.Rotate(0, 0, step);
            }
        }
    }
}
