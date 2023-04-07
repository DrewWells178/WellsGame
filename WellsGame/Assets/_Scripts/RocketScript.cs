using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RocketScript : MonoBehaviour
{
    public Transform crosshair;
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;

    public float speed = 3000f;
    public float rotateSpeed = 200f;

    public int damage = 30;

    private float splashRange = 1.5f;

    private Vector3 directions;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Creating things");
        crosshair = GameObject.FindGameObjectWithTag("Crosshair").transform;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        directions = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(directions.x, directions.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.right * force;
            //rotate and travel towards mouse position
        //Vector2 direction = (Vector2)crosshair.position - rb.position;
        //direction.Normalize();
            //Vector2 face = (Vector2)tip.position - (Vector2)transform.position;
        //float rotateAmount = Vector3.Cross(direction, transform.up).z;
        //rb.angularVelocity = rotateAmount * rotateSpeed;
        //rb.velocity = -transform.up * speed;
        RotateRocket();
    }

    private void RotateRocket()
    {
        var heading = crosshair.position - transform.position;
        var rotatedHeading = Quaternion.Euler(0,0,90) * heading;
        var rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedHeading);

        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D (Collider2D hitInfo)
    {
        //Debug.Log(hitInfo.name);
        

        if(hitInfo.name != "Player" && hitInfo.name != "Rocket(Clone)")
        {
            if(splashRange > 0)
            {
                var hitColliders = Physics2D.OverlapCircleAll(transform.position, splashRange);
                foreach(var hitCollider in hitColliders)
                {
                    Enemy enemy = hitCollider.GetComponent<Enemy>();
                    if(enemy != null)
                    {
                        //var closestPoint = hitCollider.ClosestPoint(transform.position);
                        //var distance = Vector3.Distance(closestPoint, transform.position);

                        //var damagePercent = Mathf.InversLerp(splashRange, 0, distance);
                        enemy.TakeDamage(damage);
                    }
                }
            }
            else
            {
                Enemy enemy = hitInfo.GetComponent<Enemy>();
                if(enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
            
            Destroy(gameObject);
        }
    }
}
