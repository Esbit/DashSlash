using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableController : MonoBehaviour
{
    public float maximumHealth = 1f;

    public float health;

    //float health;

    /*Animator animator;
    int deathHash;
    int takeDamageHash;*/

    void Awake()
    {
        health = maximumHealth;
        /*animator = GetComponent<Animator>();
        deathHash = Animator.StringToHash("Die");
        takeDamageHash = Animator.StringToHash("TakeDamage");*/
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0.0F)
        {
            /*animator.SetTrigger(deathHash);*/
            gameObject.SetActive(false);        
        }
        else
        {
            /*animator.SetTrigger(takeDamageHash);*/
        }

       /* animator.ResetTrigger(takeDamageHash);*/
    }

    /*public void OnDie()
    {
        
        animator.ResetTrigger(deathHash);
        enabled = false;
        gameObject.SetActive(false);     
    }*/
}
