using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGunScript : Weapon
{
    [SerializeField] public Transform gunPoint;
    //[SerializeField] public GameObject bulletTrail;
    [SerializeField] public float weaponRange = 3300f;
    //[SerializeField] public Animator muzzleFlashAnimator;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);

    private LineRenderer laserLine;
    
    [SerializeField] private int damage = 35;
    private float timeBetweenShots = .5f;
    

    // Update is called once per frame
    void Update()
    {
        EquippedState();
        if(canRotate) Rotate();
        if(canShoot) ShootHitScan(timeBetweenShots, damage, weaponRange, gunPoint); 
    }

    private void Shooting()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //StartCoroutine (ShotEffect());
            //laserLine.SetPosition (0, gunPoint.position);
            var hit = Physics2D.Raycast(
                gunPoint.position,
                crosshair.position - transform.position,
                weaponRange
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

    private IEnumerator ShotEffect()
    {
        // Play the shooting sound effect
        //gunAudio.Play ();

        // Turn on our line renderer
        laserLine.enabled = true;

        //Wait for .07 seconds
        yield return shotDuration;

        // Deactivate our line renderer after waiting
        laserLine.enabled = false;
    }
    /*
    private void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //muzzleFlashAnimator.SetTrigger("Shoot");

            var hit = Physics2D.Raycast(
                gunpoint.position,
                transform.up,
                weaponRange
            );

            var trail = Instantiate(
                bulletTrail,
                gunPoint.Position,
                transform.rotation
            );

            var trailScript = trail.GetComponent<BulletTrail>();

            if(hit.collider != null)
            {
                trailScript.SetTargetPosition(hit.point);
                //var hittable = hit.collider.GetComponent<IHittable>();
                //hittable?.Hit();
            }
            else
            {
                var endPosition = gunPoint.position + transform.up * weaponRange;
                trailScript.SetTargetPosition(endPosition);
            }
        }
    }
    */
}
