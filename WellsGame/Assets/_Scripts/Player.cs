using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 100;
    public float staticDamageTime = 0;

    // public GameObject deathEffect;

    
    public void LateUpdate()
    {
        staticDamageTime -= Time.deltaTime;
        staticDamageTime = Helper.Clamp(staticDamageTime, -2f, 2f);
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);

        if(health <= 0)
        {
            Die();
        }
    }

    public void TakeStaticDamage(float damage)
    {
        if(staticDamageTime < 0)
        {
            health -= damage;
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
