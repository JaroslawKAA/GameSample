using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadMode : ICharacterMode
{
    CharacterModeManager player;
    bool startParticles = false;
    static Transform weapon;    


    public DeadMode(CharacterModeManager player)
    {
        this.player = player;
    }

    public void UpdateActions()
    {
        Dead();
    }

    /// <summary>
    /// Dead of the hero.
    /// </summary>
    void Dead()
    {
        player.life = false;

        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("Dead") && player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && startParticles == false)
        {
            GameObject.Instantiate(player.sparkParticle, GameObject.Find("Body").transform.position, Quaternion.identity);
            startParticles = true;
        }

        player.timeToRespawn -= Time.deltaTime;

        if (player.timeToRespawn <= 0)
        {
            Respawn();
        }
    }

    /// <summary>
    /// Respawn hero.
    /// </summary>
    private void Respawn()
    {
        player.isRespawning = true;
        player.life = true;
        player.iHaveWeapon = false;
        player.currentWeapon = null;
        player.transform.position = player.startPoint.position;
        player.transform.rotation = player.startPoint.rotation;
        if (player.rightHand.childCount > 1)
        {
            weapon = player.rightHand.GetChild(1);
            GameObject.Destroy(weapon.gameObject);
        }

        player.isRespawning = false;
        player.timeToRespawn = player.bufforTimeTORespawn;
        ToIdleMode();
    }

    public void ToMovementMode()
    {
        
    }
    public void ToIdleMode()
    {
        Debug.Log("Start IdleMode!");
        startParticles = false;
        CanvaManager.lifeBuffor = true;
        GameManager.score -= 10;
        if(GameManager.score < 0)
        {
            GameManager.score = 0;
        }
        player.bufforCurrentCharacterMode = player.deadMode;
        player.currentCharacterMode = player.idleMode;
    }
    public void ToJumpMode()
    {

    }
    public void ToAttackMode()
    {

    }
    public void ToDeadMode()
    {
        Debug.Log("I'm in DeadMode! Reference to the current mode.");
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
