using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMode : ICharacterMode
{
    CharacterModeManager player;

    float targetRotation = 90f;

    public RotationMode(CharacterModeManager player)
    {
        this.player = player;
    }

    public void UpdateActions()
    {
        Rotate();
        SaveSpaceDown();
    }

    /// <summary>
    /// Rotate player 90 degrees left
    /// </summary>
    void Rotate()
    {
        float angle = player.rotationSpeed * Time.deltaTime;
        targetRotation -= angle;

        //Jeżeli przekroczy obrót 90 stopni, odejmij wartość przekroczoną, aby mieć równe 90 stopni
        if (targetRotation < 0)
        {
            angle += targetRotation;
            player.transform.Rotate(0, -angle, 0);

            targetRotation = 90f;
            ToMovementMode();
        }
        else if (targetRotation == 0)
        {

            targetRotation = 90f;
            ToMovementMode();
        }
        else
        {
            player.transform.Rotate(0, -angle, 0);
        }
    }

    /// <summary>
    /// Save if 'space' press down to fastest react befor attackMode, trampolineMode or jumpMode
    /// </summary>
    void SaveSpaceDown()
    {
        RaycastHit hit;

        if (Physics.Raycast(player.transform.position + new Vector3(0, 0.5f, 0), player.transform.TransformDirection(Vector3.back), out hit, 2, player.raycastMask))
        {
            if (hit.transform.CompareTag("Enemy") && Input.GetKeyDown(KeyCode.Space))
            {
                player.attackMode.isAttacking = true;
                Debug.Log(player.attackMode.isAttacking);
            }
            else if ((hit.transform.CompareTag("CanJump") || hit.transform.CompareTag("Trampoline")) && Input.GetKeyDown(KeyCode.Space))
            {
                player.jumpMode.isJumping = true;
            }

            
        }
    }
    public void ToMovementMode()
    {
        Debug.Log("Start MovementMode!");
        player.currentCharacterMode = player.movementMode;
    }
    public void ToIdleMode()
    {

    }
    public void ToJumpMode()
    {

    }
    public void ToAttackMode()
    {

    }
    public void ToDeadMode()
    {

    }
    public void ToRotationMode()
    {
        Debug.Log("I'm in RotationMode! Reference to the current mode.");
    }

    public void ToTrampolineMode()
    {
        
    }
    public void ToVerticalTubeMode()
    {

    }
}
