using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Weapon weaponToEquip;

    public static event Action<Weapon> OnCollideWithPickup;

    void Start()
    {
        //Put shit in the start function
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Input.GetKey("e"))
        {
            OnCollideWithPickup?.Invoke(weaponToEquip);
        }
    }

    public void PickedUpWeapon(int ID)
    {
        if(ID == weaponToEquip.pickUpID)
        {
            if(gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }

    public void DroppedWeapon(int ID, GameObject weaponToDrop)
    {
        if(ID == weaponToEquip.pickUpID)
        {
            Vector3 v = transform.position;
            v.x = v.x - 2f;
            Quaternion rot = transform.rotation;
            Instantiate(weaponToDrop, v, rot);
            if(gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
