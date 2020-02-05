using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModeManager : MonoBehaviour
{
    //Instantions of the game modes
    [HideInInspector] public MovementMode movementMode;
    [HideInInspector] public IdleMode idleMode;
    [HideInInspector] public JumpMode jumpMode;
    [HideInInspector] public AttackMode attackMode;
    [HideInInspector] public DeadMode deadMode;
    [HideInInspector] public RotationMode rotationMode;
    [HideInInspector] public TrampolineMode trampolineMode;
    [HideInInspector] public VerticalTubeMode verticalTubeMode;
    [HideInInspector] public ICharacterMode currentCharacterMode;
    [HideInInspector] public ICharacterMode bufforCurrentCharacterMode; //this help me to define is player in start point after dead or after end of gameIteration

    [Header("Setting's")]
    public bool life = true;
    public float characterSpeed = 2f;
    public float verticalMovement = 1f;
    public LayerMask raycastMask; //Mask for objects in front of the hero

    [Header("Rotation Mode")]
    public float rotationSpeed = 1f;

    [Header("Jump Mode")]
    public float jumpSpeed = 5f;

    [Header("Trampoline Mode")]
    public AudioClip trampolineAudioClip;

    [Header("Vertical Tube")]
    public Vector3 playerTargetScale = new Vector3(0.6f, 0, 0.6f); //Scale of player in tube.
    public float scalingLerp = 10f;

    [Header("Dead")]    
    public ParticleSystem sparkParticle;
    public float timeToRespawn = 3f;
    public Transform startPoint;
    public AudioClip robotDeadAudioClip;
    [HideInInspector] public float bufforTimeTORespawn;
    [HideInInspector]public bool isRespawning = false;    

    [Header("Weapon")]
    public Transform rightHand; //Animating bone.
    private GameObject previousWeapon;
    public AudioClip pickUpClip;
    public AudioClip blowAudioClip;
    [HideInInspector]public GameObject currentWeapon;
    [HideInInspector]public GameObject rightHandObject;
    [HideInInspector]public bool iHaveWeapon = false;

    [Header("Raycast")]
    public Obstacles currentObstacle = new Obstacles();
    public enum Obstacles { Null, Obstacle, CanJump, Trampoline, Enemy };
    [HideInInspector]public float maxDistanceOfRay = 0.5f;
    public RaycastHit hit;

    //Variables    
    [HideInInspector]public Animator animator;
    [HideInInspector]public CharacterController cc;    

    private void Awake()
    {
        //Constructors of the game mode's.
        movementMode = new MovementMode(this);
        idleMode = new IdleMode(this);
        jumpMode = new JumpMode(this);
        attackMode = new AttackMode(this);
        deadMode = new DeadMode(this);
        rotationMode = new RotationMode(this);
        trampolineMode = new TrampolineMode(this);
        verticalTubeMode = new VerticalTubeMode(this);


        animator = GameObject.Find("Robot").GetComponent<Animator>();
        cc = GetComponent<CharacterController>();

    }
    void Start()
    {
        currentCharacterMode = idleMode;
        bufforCurrentCharacterMode = idleMode;

        bufforTimeTORespawn = timeToRespawn;
        startPoint = GameObject.Find("StartPoint").transform;        
    }

    // Update is called once per frame
    void Update()
    {
        currentCharacterMode.UpdateActions();

        Animations();
        Raycast();
        ReturnColliderInPreviousWeapon();
    }

    //Pick up weapon if hero OnTriggerEnter with weapon GameObject.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon") && iHaveWeapon == false)
        {            
            SetWeaponInInventory(other);

        }
        else if (other.CompareTag("Weapon") && iHaveWeapon == true)
        {
            RemoveWeaponFromInventory();
            SetWeaponInInventory(other);
        }
    }    

    /// <summary>
    /// When I change weapon, the previous weapon appears on the map, but i don't activate the trigger of previous weapon until the player has move away.
    /// </summary>
    void ReturnColliderInPreviousWeapon()
    {
        if (previousWeapon != null && previousWeapon.GetComponent<SphereCollider>().enabled == false)
        {
            float distance = Vector3.Distance(previousWeapon.transform.position, transform.position);
            if (distance > 2)
            {
                previousWeapon.GetComponent<SphereCollider>().enabled = true;
            }
        }
    }
    void RemoveWeaponFromInventory()
    {
        currentWeapon.transform.parent = null;
        currentWeapon.transform.position = transform.position + new Vector3(0, 0.25f, 0);
        currentWeapon.transform.rotation = Quaternion.Euler(-90, 0, 90);        
        previousWeapon = currentWeapon;
        currentWeapon = null;        
    }

    /// <summary>
    /// Set obiect in right hand.
    /// </summary>
    /// <param name="obj">Colider of the weapon.</param>
    void SetWeaponInInventory(Collider obj)
    {
        obj.GetComponent<SphereCollider>().enabled = false;        
        obj.transform.parent = rightHand;
        obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.transform.localRotation = Quaternion.Euler(30, 0, 0);
        iHaveWeapon = true;
        currentWeapon = obj.gameObject;
        GameManager.score += 1;
        GameManager.mainSource.PlayOneShot(pickUpClip);
    }

    /// <summary>
    /// Draw a ray forward from hero. Save to enum current obstacle.
    /// </summary>
    void Raycast()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.TransformDirection(Vector3.back) * maxDistanceOfRay, Color.red);

        Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.TransformDirection(Vector3.back), out hit, maxDistanceOfRay, raycastMask);

        if (hit.transform == null)
        {
            currentObstacle = Obstacles.Null;
        }else if (hit.transform.CompareTag("Obstacle"))
        {
            currentObstacle = Obstacles.Obstacle;
        }else if (hit.transform.CompareTag("CanJump"))
        {
            currentObstacle = Obstacles.CanJump;
        }else if (hit.transform.CompareTag("Trampoline"))
        {
            currentObstacle = Obstacles.Trampoline;
        }else if (hit.transform.CompareTag("Enemy"))
        {
            currentObstacle = Obstacles.Enemy;
        }
    }

    /// <summary>
    /// Controling hero animations.
    /// </summary>
    void Animations()
    {
        if (currentCharacterMode == movementMode)
        {
            animator.SetBool("Movement", true);
        }
        else
        {
            animator.SetBool("Movement", false);
        }

        if (jumpMode.isJumping == true && currentCharacterMode == jumpMode)
        {
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }

        if (attackMode.isAttacking == true && currentCharacterMode == attackMode)
        {
            animator.SetBool("IsAttacking", true);
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }

        if(life == false)
        {
            animator.SetBool("Life", false);
        }else
        {
            animator.SetBool("Life", true);
        }

        if(trampolineMode.isJumping == true)
        {
            animator.SetBool("IsJumping", true);
        }else if (trampolineMode.isJumping == false)
        {
            animator.SetBool("IsJumping", false);
        }
    }
   
}
