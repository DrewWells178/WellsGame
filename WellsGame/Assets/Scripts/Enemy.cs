using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;

    public HealthBar healthBar;

    // For Loot/Scrap Dropping after death of enemy.
    public GameObject scrapDrop;

    void Start()
    {
        health = maxHealth;
        //healthBar.SetMaxHealth(health);
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if(health <= 0)
        {
            Die();
            //Loot/Scrap drop
            Instantiate(scrapDrop, transform.position, Quaternion.identity);
        }
    }

    void Die()
    {
        //Instantiate() some gameObject death effect
        Destroy(gameObject);
    }
}
