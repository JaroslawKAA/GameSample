using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMonster : MonoBehaviour
{
    [Header("Setting's")]
    public bool life = true;
    public ParticleSystem dust;
    public bool attack = false;
    public float timeToAttack = 2;
    public AudioClip monsterAudioClip;
    public LayerMask mask; // Mask for raycast.

    Animator animator;
    bool startDust = true;
    CharacterModeManager player;
    float timeToAttackBuffor;

    // Assign variables.
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterModeManager>();
        timeToAttackBuffor = timeToAttack;
    }

    // Update is called once per frame
    void Update()
    {
        Animations();
        StoneMonsterAI();
    }

    //If monster destroyed, change the player mode to MovementMode.
    private void OnDestroy()
    {
        player.currentCharacterMode.ToMovementMode();
    }

    /// <summary>
    /// Behavior of stone monster.
    /// </summary>
    void StoneMonsterAI()
    {
        RaycastHit hit;

        //Draw a ray from the front.
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.TransformDirection(Vector3.forward), out hit, 1f, mask))
        {
            //If hit is player start attack.
            if (hit.transform.CompareTag("Player") && player.currentCharacterMode != player.deadMode)
            {
                //Attack dellay.
                timeToAttack -= Time.deltaTime;

                //Start attack.
                if (timeToAttack <= 0)
                {
                    attack = true;
                }

                //If animation of attack is in half lenght kill player, and play sounds
                if (attack && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.2f && player.attackMode.isAttacking == false)
                {
                    transform.GetComponent<AudioSource>().PlayOneShot(monsterAudioClip);
                    player.GetComponent<AudioSource>().PlayOneShot(player.robotDeadAudioClip);

                    player.GetComponent<CharacterModeManager>().currentCharacterMode.ToDeadMode();

                    animator.SetBool("IsAttacking", false);
                    attack = false;
                    timeToAttack = timeToAttackBuffor;
                }
            }
        }
        if (life == false)
        {
            DeathOfMonster();
        }
    }

    /// <summary>
    /// Death of the monster.
    /// </summary>
    void DeathOfMonster()
    {
        //Start particles and destroy monster GameObject after 1 second.
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dead") == true && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
        {
            if (startDust == true)
            {
                Instantiate(dust, transform.position, Quaternion.identity);
                startDust = false;
            }

            Destroy(gameObject, 1);
        }
    }

    /// <summary>
    /// Kill me. Player can use it to kill Monster.
    /// </summary>
    void KillMe()
    {
        life = false;
    }

    /// <summary>
    /// Animation's controller.
    /// </summary>
    void Animations()
    {
        if (life == false)
        {
            animator.SetBool("Life", false);
        }

        if (attack == true)
        {
            animator.SetBool("IsAttacking", true);
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }
    }
}
