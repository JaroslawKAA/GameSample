using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrampolineMode : ICharacterMode
{
    CharacterModeManager player;

    public Transform target;
    public Transform platform;
    Animator animator;
    public bool isJumping = false;

    public TrampolineMode(CharacterModeManager player)
    {
        this.player = player;
    }

    public void UpdateActions()
    {
        Debug.DrawRay(player.transform.position + new Vector3(0, 0.1f, 0), player.transform.TransformDirection(Vector3.down) * 0.5f, Color.red);
        Trampoline();
        TrampolineAnimation();
    }

    /// <summary>
    /// Trampoline behavior.
    /// </summary>
    void Trampoline()
    {
        RaycastHit hit;
        float maxDistanceOfRay = 0.5f;

        //Find platfor and jump target. Set parent of player to platform. 
        if (target == null)
        {
            if (Physics.Raycast(player.transform.position + new Vector3(0, 0.1f, 0), player.transform.TransformDirection(Vector3.down), out hit, maxDistanceOfRay, player.raycastMask))
            {
                if (hit.transform.CompareTag("Trampoline"))
                {
                    target = hit.transform.FindChild("TrampolineTarget");
                    animator = hit.transform.GetComponent<Animator>();
                    platform = hit.transform.FindChild("TrampolinaArmatura").transform.FindChild("Obijak");
                    player.transform.SetParent(platform);
                    platform.GetComponent<AudioSource>().PlayOneShot(player.trampolineAudioClip);
                }
            }
        }



        //Check if animator finish trampoline animation and then jump to trampoline target.
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            isJumping = true;
        }
        if (isJumping == true)
        {
            player.transform.parent = null;
            if (player.transform.position.y < target.position.y) //zakrzywia tor lotu
            {
                player.transform.position += new Vector3(0, player.jumpSpeed * Time.deltaTime, 0);
            }

            float step = player.jumpSpeed * Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(player.transform.position, target.position, step);

            //If player is in target.position, Check where are player and go to another mode.
            if (player.transform.position == target.position)
            {

                target = null;
                isJumping = false;

                RaycastHit hit2;

                if (Physics.Raycast(player.transform.position + new Vector3(0, 0.1f, 0), player.transform.TransformDirection(Vector3.down), out hit2, maxDistanceOfRay, player.raycastMask))
                {
                    if (hit2.transform.CompareTag("Obstacle") || hit2.transform.CompareTag("CanJump"))
                    {
                        Debug.Log("Start MovementMode!");
                        ToMovementMode();
                    }
                    else if (hit2.transform.CompareTag("VerticalTube"))
                    {
                        Debug.Log("Start VerticalTubeMode!");
                        ToVerticalTubeMode();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Animate trampoline.
    /// </summary>
    void TrampolineAnimation()
    {
        if (player.currentCharacterMode == player.trampolineMode)
        {
            animator.SetBool("IsJumping", true);
        }
        else if (isJumping == false)
        {
            animator.SetBool("IsJumping", false);
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
        Debug.Log("Start DeadMode!");
        player.currentCharacterMode = player.deadMode;
    }
    public void ToRotationMode()
    {

    }

    public void ToTrampolineMode()
    {
        Debug.Log("I'm in TrampolineMode! Reference to the current mode.");
    }

    public void ToVerticalTubeMode()
    {
        player.currentCharacterMode = player.verticalTubeMode;
    }
}
