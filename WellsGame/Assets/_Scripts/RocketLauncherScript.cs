using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherScript : Weapon
{
    public float timeBetweenShots;

    // Update is called once per frame
    void Update()
    {
        Rotate();
        ShootProj(timeBetweenShots);
    }
}
