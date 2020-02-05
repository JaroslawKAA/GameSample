using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpMode : ICharacterMode
{

    CharacterModeManager player;    

    Transform target;
    Vector3 targetPosition;
    public bool isJumping = false;

    public JumpMode(CharacterModeManager player)
    {
        this.player = player;
    }

    public void UpdateActions()
    {
        Jump();
    }

    /// <summary>
    /// Jump.
    /// </summary>
    void Jump()
    {
        //Wait for press space by player.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            player.bufforCurrentCharacterMode = player.jumpMode;
        }

        //Start Jumping.
        if (isJumping == true)
        {
            RaycastHit hit;
            float maxDistanceOfRay = 0.5f;
            if (target == null)
            {
                if (Physics.Raycast(player.transform.position + new Vector3(0, 0.5f, 0), player.transform.TransformDirection(Vector3.back), out hit, maxDistanceOfRay, player.raycastMask))
                {
                    //Set target to jump depending on the obstacle
                    if (hit.transform.CompareTag("Trampoline"))
                    {
                        target = hit.transform.FindChild("TrampolinaArmatura");
                        target = target.FindChild("Obijak");
                        targetPosition = target.position;
                    }
                    else if (hit.transform.CompareTag("CanJump"))
                    {
                        target = hit.transform;
                        targetPosition = target.position + new Vector3(0, 0.5f, 0); //Orgin boksów jest w środku obiektu, dlatego należy targetPsition podnieść o 0.5f
                    }
                }
            }

            if (player.transform.position.y < targetPosition.y) //zakrzywia tor lotu
            {
                player.transform.position += new Vector3(0, player.jumpSpeed * Time.deltaTime, 0);
            }

            //Move player to jumpTarget.
            float step = player.jumpSpeed * Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, step);

            if (player.transform.position == targetPosition)
            {

                if (target.CompareTag("CanJump"))
                {
                    target = null;
                    isJumping = false;
                    ToMovementMode();
                }
                else
                {
                    isJumping = false;
                    target = null;
                    ToTrampolineMode();
                }
            }
        }
    }

    public void ToMovementMode()
    {
        Debug.Log("Start MovementMode!");
        CanvaManager.comunicatText.GetComponent<Text>().text = "";
        player.currentCharacterMode = player.movementMode;
    }
    public void ToIdleMode()
    {

    }
    public void ToJumpMode()
    {
        Debug.Log("I'm in JumpMode!");
    }
    public void ToAttackMode()
    {

    }
    public void ToDeadMode()
    {
        Debug.Log("Start DeadMode!");
        CanvaManager.comunicatText.GetComponent<Text>().text = "";
        player.currentCharacterMode = player.deadMode;
    }
    public void ToRotationMode()
    {

    }
    public void ToTrampolineMode()
    {
        Debug.Log("Start TrampolineMode!");
        CanvaManager.comunicatText.GetComponent<Text>().text = "";
        player.currentCharacterMode = player.trampolineMode;
    }
    public void ToVerticalTubeMode()
    {

    }
}
