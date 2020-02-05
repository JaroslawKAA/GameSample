using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Setting's")]
    public float offset;
    public Transform controllerHelper;
    public float lerpStrenght = 0.1f;

    Quaternion currentRotation;
    Quaternion targetRotation;
    Quaternion lerpedRotation;
    

    // Copy Y rotation from controllerHelper and Lerp() it. controllerHelper is looing at the player.
    void Update()
    {
        currentRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0, controllerHelper.rotation.eulerAngles.y + offset, 0);
        lerpedRotation = Quaternion.Lerp(currentRotation, targetRotation, lerpStrenght * Time.deltaTime);

        transform.rotation = lerpedRotation;
    }

}
