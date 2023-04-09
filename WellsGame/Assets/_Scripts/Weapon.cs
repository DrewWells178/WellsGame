using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    private float shotTime;
    [SerializeField] public Transform crosshair;
    [SerializeField] public LayerMask lm;
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
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
