using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    
    public Animator animator;
    int isWalkingHash;
    int isSlashingHash;
    int isChargingHash;
    int isDashingHash;
    int isDeadHash;
    int isStunnedHash;




    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isSlashingHash = Animator.StringToHash("isSlashing");
        isChargingHash = Animator.StringToHash("isCharging");
        isDashingHash = Animator.StringToHash("isDashing");
        isDeadHash = Animator.StringToHash("isDead");
        isStunnedHash = Animator.StringToHash("isStunned");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isSlashing = animator.GetBool(isSlashingHash);
        bool isCharging = animator.GetBool(isChargingHash);
        bool isDashing = animator.GetBool(isDashingHash);
        bool isDead = animator.GetBool(isDeadHash);
        bool isStunned = animator.GetBool(isStunnedHash);


        bool forwardPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("s");
        bool runPressed = Input.GetKey("left shift");
        bool attackPressed = Input.GetKey("e");
        bool heavyAttackPressed = Input.GetKey("q");
        bool isThrowingPressed = Input.GetKey(KeyCode.Mouse0)||Input.GetKey("f");
        bool diePressed = Input.GetKey("p");
       
        /*bool dodgeLPressed1 = Input.GetKey("a");
        bool dodgeRPressed1 = Input.GetKey("d");
        bool dyingexample = Input.GetKey("p");
        bool throwingPressed = Input.GetKey("Fire1");*/

        //Animation Walking
        //if (forwardPressed && !isWalking)
        //{
        //    animator.SetBool(isWalkingHash, true);
        //}
        //if (!forwardPressed && isWalking)
        //{
        //    animator.SetBool(isWalkingHash, false);
        //}
        //Animation Running
        //if (!isrunning && (forwardPressed && runPressed))
        //{
        //    animator.SetBool(isRunningHash, true);
        //}
        //if (isrunning && (!forwardPressed || !runPressed))
        //{
        //    animator.SetBool(isRunningHash, false);
        //}
        //Animation LightAttack
        //if (attackPressed && !isAttacking)
        //{
        //    animator.SetBool(attackHash, true);
        //}
        //if (!attackPressed && isAttacking)
        //{
        //    animator.SetBool(attackHash, false);
        //}
        //Animation HeavyAttack
        //if (heavyAttackPressed && !isHeavyPunch)
        //{
        //    animator.SetBool(heavyAttackHash, true);
        //}
        //if (!heavyAttackPressed && isHeavyPunch)
        //{
        //    animator.SetBool(heavyAttackHash, false);
        //}
        //Animation Throw Batarang
        //if (Time.time >= _CD.nextAttackTime) {
        //    if (isThrowingPressed && !isthrowing)
        //    {
        //        animator.SetBool(isthrowingHash, true);
        //    }
        //}
        //if (!isThrowingPressed && isthrowing)
        //{
        //    animator.SetBool(isthrowingHash, false);
        //}
        ////Animation die
        //if (diePressed)
        //{
        //    animator.SetTrigger(dieHash);
        //}

        /*if (dodgeLPressed1 && runPressed && !isdodginL)
        {
            animator.SetBool(isdodginLHash, true);
        }
        if (!dodgeLPressed1 && isdodginL)
        {
            animator.SetBool(isdodginLHash, false);
        }

        if (dodgeRPressed1 && runPressed && !isdodginR)
        {
            animator.SetBool(isdodginRHash, true);
        }
        if (!dodgeRPressed1 && isdodginR)
        {
            animator.SetBool(isdodginRHash, false);
        }

        if (dyingexample && !isdying)
        {
            animator.SetBool(isdyingHash, true);
        }

        if (throwingPressed && !isthrowing)
        {
            animator.SetBool(isthrowingHash, true);

        }*/

    }


}
