using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
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
    float dashPower;
    [SerializeField]
    float dashPowerMax = 30;
    [SerializeField]
    float dashPowerMin = 10;
    [SerializeField]
    float dashPowerDelta = 1.5f;
    [SerializeField]
    float dashTime = 0.2f;
    [SerializeField]
    float chargeTime = 4f;
    [SerializeField]
    GameObject dashIndicator; // An object in the ground that helps visualize how far the dash will go


    [Header("Player Slash")]
    [SerializeField]
    float slashTime = 3f;

    [Header("Player Bounce")]
    [SerializeField]
    float bounceTime = 5f;
    [SerializeField]
    float bounceMinForce = 15f;
    // TODO bounceMaxForce


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

    private Vector3 playerVelocity;    

    private Vector2 movementInput = Vector2.zero;

    private bool groundedPlayer;
    private bool dash = false;
    private bool _isDashing = false;
    private bool _isBouncing = false;
    private bool _canAttack;
    private bool _isPressingDash;
    private bool attackinput;
    private bool _isWalking;
    private bool collisionAnotherPlayer = false;
    private Rigidbody rb;
    private Coroutine dashCoroutine;
    private Coroutine slashCoroutine;
    private Coroutine bounceCoroutine;
    
    //Anim
    public Animator animator;
    public Animation anim;

    // References to each parameter in the Animator of this character
    int isWalkingHash; 
    int isSlashingHash;
    int isChargingHash;
    int isDashStartHash;
    int isDashingHash;
    int isDashEndHash;
    int isDeadHash;
    int isStunnedHash;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        isDashStartHash = Animator.StringToHash("isDashStart");
        isDashingHash = Animator.StringToHash("isDashing");
        isDashEndHash = Animator.StringToHash("isDashEnd");
        isDeadHash = Animator.StringToHash("isDead");
        isStunnedHash = Animator.StringToHash("isStunned");
    }


    void Update()
    {
        //animation
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isSlashing = animator.GetBool(isSlashingHash);
        bool isCharging = animator.GetBool(isChargingHash);
        bool isDashStart = animator.GetBool(isDashStartHash);
        bool isDashing = animator.GetBool(isDashingHash);
        bool isDashEnd = animator.GetBool(isDashEndHash);
        bool isDead = animator.GetBool(isDeadHash);
        bool isStunned = animator.GetBool(isStunnedHash);

        //movement
        groundedPlayer = IsGrounded();
        if (groundedPlayer && playerVelocity.y < 0 )
        {            
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y).normalized * playerSpeed;

        // If the player can move or if dash is charging, then change its direction
        if(CanMove() || _isPressingDash)
        {
            //Rotate to target direction
            if (move != Vector3.zero)
            {
                float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(xRotation, angle, 0f);
            }
        }

        // If the player can move, then change its velocity
        if (CanMove())
        {
            // Actually move, don't forget to add the gravity
            rb.velocity = move + Vector3.up * rb.velocity.y;
        }

        //when pressing dash button
        if (_isPressingDash)
        {
            dashIndicator.SetActive(true);

            //animator.SetBool(isChargingHash, true);
            _timePressingDash += Time.deltaTime;
            _chargeTimer = (Time.time / 0.1f);
            animator.SetLayerWeight(1,Mathf.Lerp(0,1, _chargeTimer));

            dashIndicator.transform.position = transform.position + transform.forward * _timePressingDash + Vector3.up * 0.1f;
            
            dashPower += dashPowerDelta * Time.deltaTime;
            if (_isPressingDash && Time.time - lastPlayTime > 4.0f)
            {
                lastPlayTime = Time.time;
                var chargeAudioClip = chargeClips[UnityEngine.Random.Range(0, chargeClips.Length)];
                audioSource.PlayOneShot(chargeAudioClip);
            }
            if (_timePressingDash > 4.0f)
            {
                _isPressingDash = false;
                animator.SetBool(isChargingHash, false);
                //animator.SetBool(isStunnedHash, true);
                //if(Time.time > 1f)
                //   animator.SetBool(isStunnedHash, false);
                //nextDashTime = Time.time;
                ExecuteBadDash();

                dashIndicator.SetActive(false);
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
        
        // Why move twice in the same update???
        //controller.Move(playerVelocity * Time.deltaTime);
    }

    /// <summary>
    /// Checks if the character is touching the ground
    /// </summary>
    /// <returns>true if the character is on the ground, false otherwise</returns>
    private bool IsGrounded()
    {
        RaycastHit hit;
        if(Physics.SphereCast(transform.position + Vector3.up, 0.5f, Vector3.down, out hit, 1.05f))
        {
            // Should it check if what's below is actually ground?
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks if this character can move on its own
    /// If no cooldowns or special conditions apply, the character can move
    /// </summary>
    /// <returns></returns>
    private bool CanMove()
    {
        bool canMove = true;

        canMove = canMove && !_isPressingDash;
        canMove = canMove && !_isDashing;
        canMove = canMove && !_isBouncing;

        return canMove;
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
            dashIndicator.SetActive(false);

            if (dashPower <= dashPowerMin)
                dashPower = dashPowerMin;
            if (dashPower >= dashPowerMax)
                dashPower = dashPowerMax;
            audioSource.Stop();
            _chargeTimer = (Time.time / 0.1f);
            //animator.SetBool(isChargingHash, true);
            animator.SetBool(isDashingHash, true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0, _chargeTimer));
            nextDashTime = Time.time;
            _isPressingDash = false;
            //_canAttack = true;
            ExecuteDash(Mathf.Clamp(_timePressingDash, 0.5f, 4f));
        }
    }
    
    /// <summary>
    /// Make this character bounce in the given direction
    /// </summary>
    /// <param name="direction">The direction of the bounce</param>
    public void Bounce(Vector3 direction)
    {
        // Start coroutine only if this character is not bouncing already
        if(!_isBouncing)
        {
            rb.velocity = direction * bounceMinForce;
            bounceCoroutine = StartCoroutine(BounceCoroutine());
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (_isDashing) // Make the other player bounce!
        {
            if (collision.collider.tag == "Player")
            {
                // Stop dashing
                StopCoroutine(dashCoroutine);
                _isDashing = false;
                rb.velocity = -rb.velocity;//Vector3.zero;

                // Make the other guy bounce in the direction of the hit
                Vector3 bounceDir = (collision.collider.transform.position - transform.position).normalized;
                collision.collider.GetComponent<Movement>().Bounce(bounceDir);

                VFXFactory.CreateVFX(VFX.HIT2, collision.collider.transform.position);
            }
        }
        else if (_isBouncing)
        {
            // If this character bounces into another caracter, make them bounce as well
            if (collision.collider.tag == "Player")
            {
                Vector3 bounceDir = (collision.collider.transform.position - transform.position).normalized;
                collision.collider.GetComponent<Movement>().Bounce(bounceDir);

                VFXFactory.CreateVFX(VFX.HIT2, collision.collider.transform.position);
            }

                // Reflect the vector
            rb.velocity = Vector3.Reflect(rb.velocity, collision.GetContact(0).normal);

            VFXFactory.CreateVFX(VFX.HIT1, collision.GetContact(0).point);
        }
    }



    //Slash & Dash Coroutines
    private IEnumerator DashCoroutine()
    {
        //const float clipDuration = 0.5f;
        //yield return new WaitForSeconds(clipDuration);
        _isDashing = true;
        float startTime = Time.time; // need to remember this to know how long to dash
        // ANIMATOR
        animator.SetBool(isDashingHash, true);
        Vector3 move = transform.forward; //new Vector3(movementInput.x, 0, movementInput.y).normalized;
        while (Time.time < startTime + dashTime)
        {
            if (collisionAnotherPlayer)
            {
                rb.velocity = Vector3.zero;
                break;
            }
            else
            {
                // TODO change Dash to work with Rigidbody
                //controller.Move(move * dashPower * Time.deltaTime);
                rb.velocity = transform.forward * dashPower;
            }
            // or controller.Move(...), dunno about that script
            yield return null; // this will make Unity stop here and continue next frame
        }
        _isDashing = false;
    }
    private IEnumerator SlashCoroutine()
    {
        yield return new WaitForSeconds(dashTime + slashTime);
        _canAttack = false;
        Debug.Log("Time's UP");
    }

    private IEnumerator BounceCoroutine()
    {
        print("START BOUNCING!!");

        _isBouncing = true;

        float startTime = Time.time;
        while (Time.time < startTime + bounceTime)
        {
            // TODO allow bounceForce to grow if the player is hit more than once
            // TODO validate if hitting a bouncing player is possible and fun

            // If the player is stuck in a wall, induce a new random velocity
            if(rb.velocity.magnitude < 1)
            {
                Vector3 newVelocity = UnityEngine.Random.onUnitSphere;
                newVelocity.y = 0;
                newVelocity = newVelocity.normalized * bounceMinForce;
                rb.velocity = newVelocity;
            }

            // While the player is bouncing, maintain the bounceSpeed, negate the Y factor to prevent the character shooting upwards
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized * bounceMinForce;
            yield return null;
        }

        _isBouncing = false;

        print("STOP BOUNCING!!");
    }

    //Dash
    private void ExecuteDash(float timePressingDash)
    {
        //Debug.Log("DashPower: " + dashPower);
        dashCoroutine = StartCoroutine(DashCoroutine());
        slashCoroutine = StartCoroutine(SlashCoroutine());
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
                DamageableController damageable = enemy.GetComponent<DamageableController>();

                if (damageable != null)
                {
                    damageable.TakeDamage(damage);
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