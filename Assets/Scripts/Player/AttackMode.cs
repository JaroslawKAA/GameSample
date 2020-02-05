using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackMode : ICharacterMode
{
    CharacterModeManager player;

    public bool isAttacking = false;
    Transform enemy;    

    public AttackMode(CharacterModeManager player)
    {
        this.player = player;
    }

    public void UpdateActions()
    {
        Attack();        
    }

    /// <summary>
    /// Attack enemy.
    /// </summary>
    void Attack()
    {
        RaycastHit hit;
        
        //Start blow animation.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAttacking = true;
        }        

        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") == true && player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)//Sprawdź czy na pewno animacja się zaczeła, a potem czy się skończyła
        {
            Physics.Raycast(player.transform.position + new Vector3(0, 0.5f, 0), player.transform.TransformDirection(Vector3.back), out hit, 0.5f, player.raycastMask);
            if (hit.transform != null && hit.transform.CompareTag("Enemy"))
            {
                player.GetComponent<AudioSource>().PlayOneShot(player.blowAudioClip);
                enemy = hit.transform;
                enemy.transform.SendMessage("KillMe", SendMessageOptions.DontRequireReceiver);
                enemy.GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Kill monster!");
                GameManager.score += 5;
            }
        }
    }
    public void ToMovementMode()
    {
        Debug.Log("Start MovementMode!");
        isAttacking = false;
        CanvaManager.comunicatText.GetComponent<Text>().text = "";        
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
        Debug.Log("I'm in AttackMode! Reference to the current mode.");
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
        
    }

    public void ToVerticalTubeMode()
    {
        
    }
}
