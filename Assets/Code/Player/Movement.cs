using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField]
    float xRotation;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float turnSmoothTime = 0.1f;
    [Header("Player Dash")]
    [SerializeField]
    float dashCoolDown = 5f;
    [SerializeField]
    float dashPower = 10;
    [SerializeField]
    float dashPowerMax = 30;
    [SerializeField]
    float dashPowerMin = 10;
    [SerializeField]
    float dashTime = 0.2f;
    [SerializeField]
    float chargeTime = 4f;
    [Header("Player Slash")]
    [SerializeField]
    float slashTime = 3f;
    [Header("Audio")]
    public AudioSource audioSource;
    [Header("Clips")]
    public AudioClip[] walkingClips;
    public AudioClip[] dashClips;
    public AudioClip[] chargeClips;
    public AudioClip[] attackMetalClips;
    public AudioClip[] attackWindClips;
    public AudioClip[] dieClips;
    public AudioClip[] stunClips;

    [Header("Attack")]
    public LayerMask whatIsEnemy;
    [SerializeField]
    Transform attackPoint;
    [SerializeField]
    float attackRange;
    [SerializeField]
    float attackCoolDown = 2.0f;
    [SerializeField]
    float damage;

    float lastPlayTime;

    float nextDashTime;
    float nextAttackTime;
    float turnSmoothVelocity;
    private float _timePressingDash;
    private float _chargeTimer;

    public CharacterController controller;
    private Vector3 playerVelocity;
    

    private Vector2 movementInput = Vector2.zero;

    private bool groundedPlayer;
    private bool dash = false;
    private bool _canAttack;
    private bool _isPressingDash;
    private bool attackinput;
    private bool _isWalking;

    //Anim
    public Animator animator;
    public Animation anim;
    int isWalkingHash;
    int isSlashingHash;
    int isChargingHash;
    int isDashingHash;
    int isDeadHash;
    int isStunnedHash;
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        nextDashTime = 0;
        lastPlayTime = 0;
        _canAttack = false;
        _isWalking = false;
        audioSource = GetComponent<AudioSource>();

        //Animation
        animator = GetComponent<Animator>();
        anim = GetComponent<Animation>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isSlashingHash = Animator.StringToHash("isSlashing");
        isChargingHash = Animator.StringToHash("isCharging");
        isDashingHash = Animator.StringToHash("isDashing");
        isDeadHash = Animator.StringToHash("isDead");
        isStunnedHash = Animator.StringToHash("isStunned");

    }


    void Update()
    {
        //animation
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isSlashing = animator.GetBool(isSlashingHash);
        bool isCharging = animator.GetBool(isChargingHash);
        bool isDashing = animator.GetBool(isDashingHash);
        bool isDead = animator.GetBool(isDeadHash);
        bool isStunned = animator.GetBool(isStunnedHash);

        //movement
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0 )
        {            
            playerVelocity.y = 0f;
        }

        //if (movementInput != null && Time.time - lastPlayTime > 0.5f)
        //{
        //    lastPlayTime = Time.time;
        //    PlayWalkingSFX();
        //}

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y).normalized;
        controller.Move(move * Time.deltaTime * playerSpeed);


        //rotation
        if (move != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(xRotation, angle, 0f);
        }
        
        //when pressing dash button
        if(_isPressingDash)
        {
            animator.SetBool(isChargingHash, true);
            _timePressingDash += Time.deltaTime;
            _chargeTimer = (Time.time / 0.1f);
            animator.SetLayerWeight(1,Mathf.Lerp(0,0.6f, _chargeTimer));
            
            dashPower = dashPower + 0.2f;
            if (_isPressingDash && Time.time - lastPlayTime > 4.0f)
            {
                lastPlayTime = Time.time;
                var chargeAudioClip = chargeClips[UnityEngine.Random.Range(0, chargeClips.Length)];
                audioSource.PlayOneShot(chargeAudioClip);
            }
            if (dashPower <= dashPowerMin)
                dashPower = dashPowerMin;
            if (dashPower >= dashPowerMax)
                dashPower = dashPowerMax;
            if (_timePressingDash > 4.0f)
            {
                _isPressingDash = false;
                animator.SetBool(isChargingHash, false);
                //animator.SetBool(isStunnedHash, true);
                //if(Time.time > 1f)
                //   animator.SetBool(isStunnedHash, false);
                //nextDashTime = Time.time;
                ExecuteBadDash();
            }
        }
        // if (attackinput)
        //{
        //    //animation
        //    animator.SetBool(isSlashingHash, true);
        //    StartCoroutine(Attack());
        //    //_canAttack = false;
        //}
        StopCoroutine(Attack());
        if (movementInput.magnitude >= 0.1)
            animator.SetBool(isWalkingHash, true);
        else
            animator.SetBool(isWalkingHash, false);
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    //When Move input
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();        
    }

    //When Attack Button press
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (_canAttack)
        {
            animator.SetBool(isSlashingHash, true);
            StartCoroutine(Attack());
        }

    }

    //When Dash Button Press
    public void OnDash(InputAction.CallbackContext context)
    {   
        //when holding dash button
        if(context.action.WasPressedThisFrame() && Time.time - nextDashTime > dashCoolDown)
        {
            _canAttack = false;
            _isPressingDash = true;
            _timePressingDash = 0f;
            dashPower = 0;
        }
        //when released button
        if (_isPressingDash && context.action.WasReleasedThisFrame())
        {
            audioSource.Stop();
            _chargeTimer = (Time.time / 0.1f);
            animator.SetBool(isChargingHash, false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0, _chargeTimer));
            nextDashTime = Time.time;
            _isPressingDash = false;
            _canAttack = true;
            ExecuteDash(Mathf.Clamp(_timePressingDash, 0.5f, 4f));
        }
        StopCoroutine(DashCoroutine());
        StopCoroutine(SlashCoroutine());
    }

    //Slash & Dash Coroutines
    private IEnumerator DashCoroutine()
    {
        float startTime = Time.time; // need to remember this to know how long to dash
        //Vector3 move = new Vector3(movementInput.x, 0, movementInput.y).normalized;
        while (Time.time < startTime + dashTime)
        {
            controller.Move(transform.forward * dashPower * Time.deltaTime);
            // or controller.Move(...), dunno about that script
            yield return null; // this will make Unity stop here and continue next frame
        }
    }
    private IEnumerator SlashCoroutine()
    {
        yield return new WaitForSeconds(dashTime + slashTime);
        _canAttack = false;
        Debug.Log("Time's UP");
    }

    //Dash
    private void ExecuteDash(float timePressingDash)
    {
        //Debug.Log("DashPower: " + dashPower);
        StartCoroutine(DashCoroutine());
        StartCoroutine(SlashCoroutine());
    }

    private void ExecuteBadDash()
    {
        var stunnedAudioClip = stunClips[UnityEngine.Random.Range(0, stunClips.Length)];
        audioSource.PlayOneShot(stunnedAudioClip);
        Debug.Log("ExecuteBadDash: ");
    }

    //public void PlayWalkingSFX()
    //{
    //    var walkAudioClip = walkingClips[UnityEngine.Random.Range(0, walkingClips.Length)];
    //    audioSource.PlayOneShot(walkAudioClip);
    //}

    //public void SoundsAttack()
    //{
    //    var windAudioClip = attackWindClips[UnityEngine.Random.Range(0, attackWindClips.Length)];
    //    var metalAudioClip = attackMetalClips[UnityEngine.Random.Range(0, attackMetalClips.Length)];
    //    audioSource.PlayOneShot(windAudioClip);
    //    audioSource.PlayOneShot(metalAudioClip);
    //}


    public IEnumerator Attack()
    {
        /*animator.SetTrigger(attackHash);*/
        _canAttack = false;
        Debug.Log(isSlashingHash);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool(isSlashingHash, false);

        Collider[] enemies = Physics.OverlapSphere(attackPoint.position, attackRange, whatIsEnemy);

        if (enemies.Length > 0)
        {
            foreach (Collider enemy in enemies)
            {
                DamageableController controller = enemy.GetComponent<DamageableController>();

                if (controller != null)
                {
                    controller.TakeDamage(damage);
                    animator.SetBool(isChargingHash, false);
                    animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0, _chargeTimer));
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}