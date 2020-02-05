using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementMode : ICharacterMode
{
    CharacterModeManager player;    

    public MovementMode(CharacterModeManager player)
    {
        this.player = player;
    }

    public void UpdateActions()
    {
        React();
        Movement();
        SaveSpaceDown();
    }

    /// <summary>
    /// Player is moving forward.
    /// </summary>
    void Movement()
    {
        Vector3 movement = new Vector3(0, -player.verticalMovement, -player.characterSpeed);

        player.bufforCurrentCharacterMode = player.movementMode;

        player.cc.Move(player.transform.rotation * movement * Time.deltaTime);
    }

    /// <summary>
    /// React to obstacle's.
    /// </summary>
    void React()
    {
        if (player.currentObstacle != CharacterModeManager.Obstacles.Null)
        {
            if (player.currentObstacle == CharacterModeManager.Obstacles.Obstacle)
            {
                ToRotationMode();
            }
            else if (player.currentObstacle == CharacterModeManager.Obstacles.CanJump || player.currentObstacle == CharacterModeManager.Obstacles.Trampoline)
            {
                ToJumpMode();
            }
            else if (player.currentObstacle == CharacterModeManager.Obstacles.Enemy)
            {
                if (player.iHaveWeapon == true)
                {
                    ToAttackMode();
                }
                if (player.iHaveWeapon == false)
                {
                    ToIdleMode();
                }
            }
            
            //HERE I CAN DEFINE MORE REACTION
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
            }else if ((hit.transform.CompareTag("CanJump") || hit.transform.CompareTag("Trampoline")) && Input.GetKeyDown(KeyCode.Space))
            {
                player.jumpMode.isJumping = true;
            }            
        }        
    }

    public void ToMovementMode()
    {
        Debug.Log("I'm in MovementMode!");
    }
    public void ToIdleMode()
    {
        Debug.Log("Start IdleMode!");        
        player.currentCharacterMode = player.idleMode;
    }
    public void ToJumpMode()
    {
        Debug.Log("Start JumpMode!");
        CanvaManager.comunicatText.GetComponent<Text>().text = "JUMP!\n(SPACE)";
        GameManager.score += 1;
        player.currentCharacterMode = player.jumpMode;
    }
    public void ToAttackMode()
    {
        Debug.Log("Start AttackMode!");
        CanvaManager.comunicatText.GetComponent<Text>().text = "ATTACK!\n(SPACE)";
        player.currentCharacterMode = player.attackMode;
    }
    public void ToDeadMode()
    {
        Debug.Log("Start DeadMode!");      
        player.currentCharacterMode = player.deadMode;
    }
    public void ToRotationMode()
    {
        Debug.Log("Start RotationMode!");
        player.currentCharacterMode = player.rotationMode;
    }

    public void ToTrampolineMode()
    {
        //First i have to jump, in jumpMode i can use it function.
    }
    public void ToVerticalTubeMode()
    {
        //First i have use trampoline.
    }
}
