using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public float staticDamageTime = 0;

    public HealthBar healthBar;

    public int option;

    // public GameObject deathEffect;

    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth); 
    }

    
    public void LateUpdate()
    {
        staticDamageTime -= Time.deltaTime;
        staticDamageTime = Helper.Clamp(staticDamageTime, -2f, 2f);
    }


    
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        Debug.Log(health);

        if(health <= 0)
        {
            Die();
        }
    }

    public void TakeStaticDamage(int damage)
    {
        if(staticDamageTime < 0)
        {
            health -= damage;
            healthBar.SetHealth(health);
            staticDamageTime = 1f;
            Debug.Log(health);
        }

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Instantiate() some gameObject death effect
        // GameOver
        Destroy(gameObject);
    }

    

    
}
