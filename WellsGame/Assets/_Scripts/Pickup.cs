using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Weapon weaponToEquip;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Input.GetKey("e"))
        {
            collision.GetComponent<Player>().PickUpWeapon(weaponToEquip);
            Destroy(gameObject);
        }
    }
}
