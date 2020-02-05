using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalTubeMode : ICharacterMode
{
    CharacterModeManager player;

    Transform target;//Position to move player
    Transform targetOfScaling; //Positon under which player return to scale 1
    bool isScalling = false;
    RaycastHit hit;
    Vector3 currentPlayerScale;
    Vector3 targetScale;
    Vector3 lerpedScale;


    public VerticalTubeMode(CharacterModeManager player)
    {
        this.player = player;
    }

    public void UpdateActions()
    {
        VerticalTube();
        ScalingPlayer();
    }

    /// <summary>
    /// Vertical tube behavior.
    /// </summary>
    void VerticalTube()
    {
        //if target of vertical tube are null, find target
        if (target == null)
        {
            if (Physics.Raycast(player.transform.position + new Vector3(0, 0.2f, 0), player.transform.TransformDirection(Vector3.down), out hit, 0.5f, player.raycastMask))
            {
                target = hit.transform.FindChild("TubeTarget");
            }
        }

        //Move player to target.position
        float step = player.jumpSpeed * Time.deltaTime;
        player.transform.position = Vector3.MoveTowards(player.transform.position, target.position, step);

        if (player.transform.position == target.position)
        {
            if (player.transform.position == target.position)
            {
                player.transform.localScale = new Vector3(1, 1, 1);
                isScalling = false;
            }
            Debug.Log("Start MovementMode!");
            target = null;
            ToMovementMode();
        }
    }

    /// <summary>
    /// Scale player in tube.
    /// </summary>
    void ScalingPlayer()
    {

        if (isScalling == false)
        {
            Vector3 playerTargetScale = player.playerTargetScale;
            targetOfScaling = hit.transform.FindChild("TubeTargetOfScalling");
            isScalling = true;
        }
        else if (isScalling == true)
        {

            if (player.transform.position.y > targetOfScaling.position.y)
            {
                currentPlayerScale = player.transform.localScale;
                targetScale = player.playerTargetScale;
                lerpedScale = Vector3.Lerp(currentPlayerScale, targetScale, player.scalingLerp);
            }
            else
            {
                currentPlayerScale = player.transform.localScale;
                lerpedScale = Vector3.Lerp(currentPlayerScale, new Vector3(1, 1, 1), player.scalingLerp);
            }

            player.transform.localScale = lerpedScale;

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

    }

    public void ToVerticalTubeMode()
    {
        Debug.Log("I'm in VerticalTubeMode! Reference to the current mode.");
    }
}
