using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterScript : Enemy
{
    private Rigidbody2D rb;
    private BoxCollider2D bc2;
    [SerializeField] LayerMask lm;

    // *** MY ADDITIONS***

  

    // Roaming Variables
    public float moveSpeed;
    private Vector3 startingPosition;
    public float stopDistance;


    // Shooting Variables
    public float rangeAttackTime;
    public Transform shotPoint;
    public GameObject bullet;
    public float timeBetweenFiring;


    [SerializeField]
    public Transform player;



    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position; // What does this portion do?
        rb = GetComponent<Rigidbody2D>();
        bc2 = transform.GetComponent<BoxCollider2D>();
        

 
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= rangeAttackTime)
        {
            rangeAttackTime = Time.time + timeBetweenFiring;
            RangedAttack();
        }
    }
    public void RangedAttack()
    {
        Vector2 direction = player.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoint.rotation = rotation;

        Instantiate(bullet, shotPoint.position, shotPoint.rotation);

    }

}
