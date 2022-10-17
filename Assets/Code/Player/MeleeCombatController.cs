using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombatController : MonoBehaviour
{
    public AudioSource punch1;
    //public AudioSource punch2;
    public LayerMask whatIsEnemy;
    [SerializeField]
    Transform attackPoint;
    [SerializeField]
    float attackRange;
    [SerializeField]
    float attackRate = 2.0f;
    [SerializeField]
    float damage;
    //[SerializeField]
    //float heavyDamage;

    Movement endAnim;

    Animator animator;
    int attackHash;
    int heavyAttackhash;

    //private StarterAssetsInputs _input;



    private void Awake()
    {
        animator = GetComponent<Animator>();
        //_input = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        //if (_input.attack)
        //{
        //    punch1.Play();
            
        //}
        //if (_input.heavyAttack)
        //{
        //    punch2.Play();
            
        //}
    }

    public void Attack()
    {
        /*animator.SetTrigger(attackHash);*/
        Collider[] enemies = Physics.OverlapSphere(attackPoint.position, attackRate, whatIsEnemy);

        if (enemies.Length > 0)
        {
            Debug.Log("mayor");
            foreach (Collider enemy in enemies)
            {
                DamageableController controller = enemy.GetComponent<DamageableController>();

                if (controller != null)
                {
                    controller.TakeDamage(damage);
                    //StartCoroutine(endAnim.EndAnimAttack());
                }
                //StopCoroutine(endAnim.EndAnimAttack());

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
