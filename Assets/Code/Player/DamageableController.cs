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
            Destroy(gameObject);
            GameController.Instance.CharacterKilled();
        }
        else
        {
            /*animator.SetTrigger(takeDamageHash);*/
        }

       /* animator.ResetTrigger(takeDamageHash);*/
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 4) //check the int value in layer manager(User Defined starts at 8) 
        {
            TakeDamage(2);
        }
    }

    /*public void OnDie()
    {
        
        animator.ResetTrigger(deathHash);
        enabled = false;
        gameObject.SetActive(false);     
    }*/
}
