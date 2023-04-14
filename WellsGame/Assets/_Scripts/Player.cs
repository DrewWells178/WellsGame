using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public float staticDamageTime = 0;
    //public GameObject[] myWeapons = new GameObject{null, null, null};
    private Weapon weapon1 = null;
    private Weapon weapon2 = null;
    private Weapon weapon3 = null;

    public HealthBar healthBar;

    public static int option;

    // public GameObject deathEffect;

    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void Update()
    {
        ChangeWeapon();
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
        if(weapon1 == null)
        {
            weapon1 = weapon;
            Destroy(GameObject.FindGameObjectWithTag("Weapon"));
            Instantiate(weapon1, transform.GetChild(1).transform.position, transform.rotation, transform);
        }
        else if(weapon2 == null)
        {
            weapon2 = weapon;
            Destroy(GameObject.FindGameObjectWithTag("Weapon"));
            Instantiate(weapon2, transform.GetChild(1).transform.position, transform.rotation, transform);
        }
        else if(weapon3 == null)
        {
            weapon3 = weapon;
            Destroy(GameObject.FindGameObjectWithTag("Weapon"));
            Instantiate(weapon3, transform.GetChild(1).transform.position, transform.rotation, transform);
        }
        
        
    }

    private void ChangeWeapon()
    {
        if(Input.GetKeyDown("1"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Weapon"));
            Instantiate(weapon1, transform.GetChild(1).transform.position, transform.rotation, transform);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Destroy(GameObject.FindGameObjectWithTag("Weapon"));
            Instantiate(weapon2, transform.GetChild(1).transform.position, transform.rotation, transform);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Destroy(GameObject.FindGameObjectWithTag("Weapon"));
            Instantiate(weapon3, transform.GetChild(1).transform.position, transform.rotation, transform);
        }
    }

    public void WheelWeapon(int choice)
    {
        if(choice == 1)
        {
            Destroy(GameObject.FindGameObjectWithTag("Weapon"));
            Instantiate(weapon1, transform.GetChild(1).transform.position, transform.rotation, transform);
        }
        else if(choice == 2)
        {
            Destroy(GameObject.FindGameObjectWithTag("Weapon"));
            Instantiate(weapon2, transform.GetChild(1).transform.position, transform.rotation, transform);
        }
        else if(choice == 3)
        {
            Destroy(GameObject.FindGameObjectWithTag("Weapon"));
            Instantiate(weapon3, transform.GetChild(1).transform.position, transform.rotation, transform);
        }
    }

    // Check if the player is carrying other weapons
    private void CheckCarry()
    {

    }
}
