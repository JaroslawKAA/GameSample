using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMode : ICharacterMode
{
    CharacterModeManager player;

    public IdleMode(CharacterModeManager player)
    {
        this.player = player;
    }

    public void UpdateActions()
    {
        //Press 'SPACE' to start
        if (Input.GetKeyDown(KeyCode.Space) && player.currentObstacle != CharacterModeManager.Obstacles.Enemy)
        {
            ToMovementMode();
        }
    }
    
    public void ToMovementMode()
    {
        Debug.Log("StartMovementMode!");
        player.currentCharacterMode = player.movementMode;
    }
    public void ToIdleMode()
    {
        Debug.Log("I'm in IdleMode!");
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

    }
    public void ToVerticalTubeMode()
    {

    }
}
