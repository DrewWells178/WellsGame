using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGunScript : Weapon
{
    //public Transform shotPoint;
    public float timeBetweenShots;

    // Update is called once per frame
    void Update()
    {
        EquippedState();
        if(canRotate) Rotate();
        if(canShoot) ShootProj(timeBetweenShots);
    }
}
