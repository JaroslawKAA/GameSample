using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("Setting's")]
    public float rotationSpeed = 100f;
    public bool rotX = false;
    public bool rotY = false;
    public bool rotZ = false;
    public bool rotMinusX = false;
    public bool rotMinusY = false;
    public bool rotMinusZ = false;

    // Update is called once per frame
    void Update()
    {
        if (rotX == true)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
        }
        if (rotY == true)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        if (rotZ == true)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        if (rotMinusX == true)
        {
            transform.Rotate(-rotationSpeed * Time.deltaTime, 0, 0);
        }
        if (rotMinusY == true)
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
        if (rotMinusZ == true)
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }
}
