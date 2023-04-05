using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SVS.AIPlayerDetector;

public class MeleeEnemyScript : Enemy
{

    private Rigidbody2D rb;
    private BoxCollider2D bc2;
    [SerializeField] LayerMask lm;

    public float stopDistance;
    public float attackTime;    
    public float attackSpeed;
    private float timeBetweenAttacks;

    //Patrolling Variables
    float waitTime;
    public float startWaitTime;
    public Transform[] patrolPoints;
    int currentPointIndex;

    //Detection Variables
    [SerializeField] AIPlayerDetector detector;
    bool isPursuing;


    


    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;        
        rb = GetComponent<Rigidbody2D>();
        bc2 = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

                
        if (detector.PlayerDetected)
        {
            isPursuing = true;
        }
        if (!isPursuing)
        {
            Patrolling();
        }
        else
        {
            PursueAttack();
        }
        
    }


    //Patrolling Function
    private void Patrolling()
    {
        /*
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
        if (transform.position == patrolPoints[currentPointIndex].position)
        {
            if (waitTime <= 0)
            {
                if (currentPointIndex + 1 < patrolPoints.Length)
                {
                    currentPointIndex++;
                }
                else
                {
                    currentPointIndex = 0;
                }
                waitTime = 0;
            }
            else { waitTime -= Time.deltaTime; }


        }
        */
    }
    private void PursueAttack()
    {
        /*
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            //transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else { if(Time.time >= attackTime)
            {
                StartCoroutine(Attack());
                attackTime = Time.time + timeBetweenAttacks;
            } }
        */
    }
    IEnumerator Attack() 
    {
        //player.GetComponent<PlayerScript>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        //Vector2 targetPosition = player.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            //transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
   
    }
}
