using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    private float shotTime;
    [SerializeField] public Transform crosshair;
    [SerializeField] public LayerMask lm;
    [SerializeField] public GameObject weaponPickUp;
    
    [SerializeField] public Sprite weaponSprite;
    [SerializeField] public int pickUpID;

    [SerializeField] public bool isEquipped = false;
    [SerializeField] public bool isPickedUp = false;
    [SerializeField] public bool canRotate;
    [SerializeField] public bool canShoot;
    
    public static event Action<Weapon> OnCollideWithWeapon;

    public void EquippedState()
    {
        if(isEquipped)
        {
            ActivateWeapon();
        }
        else if(!isPickedUp)
        {
            gameObject.SetActive(true);
            DeactivateWeapon();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void EquipFromGround(Transform player)
    {
        this.transform.parent = player;
        this.transform.position = player.GetChild(1).transform.position;
        this.isEquipped = true;
        this.isPickedUp = true;
    }

    public void Drop(Vector3 dropLocation)
    {
        this.transform.parent = null;
        this.transform.position = dropLocation;
        this.isPickedUp = false;
        this.isEquipped = false;
        gameObject.SetActive(true);
    }

    public void ActivateWeapon()
    {
        canRotate = true;
        canShoot = true;
        gameObject.SetActive(true);
    }

    public void DeactivateWeapon()
    {
        canRotate = false;
        canShoot = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Input.GetKey("e") && !isPickedUp)
        {
            OnCollideWithWeapon?.Invoke(this);
        }
    }

    public void Rotate()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }

    public void ShootProj(float timeBetweenShots)
    {
        if(Input.GetMouseButton(0))
        {
            if(Time.time >= shotTime)
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                shotTime = Time.time + timeBetweenShots;
            }
        }
    }

    public void ShootHitScan(float timeBetweenShots, int damage, float weaponRange, Transform gunPoint)
    {
        if(Input.GetMouseButtonDown(0))
        {
            //StartCoroutine (ShotEffect());
            //laserLine.SetPosition (0, gunPoint.position);
            var hit = Physics2D.Raycast(
                gunPoint.position,
                Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position,
                weaponRange,
                lm
            );

            Debug.Log(hit.collider);
            if(hit.collider != null)
            {
                //laserLine.SetPosition (1, hit.point);
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if(enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}
