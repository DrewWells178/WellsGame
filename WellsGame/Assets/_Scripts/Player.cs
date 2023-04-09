using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public float staticDamageTime = 0;
    //private GameObject[] myWeapons = new GameObject{null, null, null};
    //private Weapon weapon1 = null;
    //private Weapon weapon2 = null;
    //private Weapon weapon3 = null;

    public HealthBar healthBar;

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

    

    public void PickUpWeapon(Weapon weapon)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weapon, transform.GetChild(1).transform.position, transform.rotation, transform);

    }

    private void ChangeWeapon()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            
        }
        else if(Input.GetKeyDown(KeyCode.Keypad2))
        {

        }
        else if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            
        }
    }

    // Check if the player is carrying other weapons
    private void CheckCarry()
    {

    }
}
