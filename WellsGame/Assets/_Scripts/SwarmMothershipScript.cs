using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SVS.AIPlayerDetector;

public class SwarmMothershipScript : Enemy
{
    private Rigidbody2D rb;
    private BoxCollider2D bc2;

    [SerializeField]
    public Transform player;


    // Player Detection Variables.
    [SerializeField] AIPlayerDetector detector;
    bool isPursuing;

    /*
    //Patrolling Variables
    float waitTime;
    public float startWaitTime;
    public Transform[] patrolPoints;
    int currentPointIndex;
    */

    // Summoning Variables
    public float timeBetweenSummons;
    private float summonTime;
    public Enemy enemyToSummon;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            SummonerSpawn();
        }
    }
    /*
    //Patrolling Function
    private void Patrolling()
    {
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
    }
    */
    public void SummonerSpawn()
    {
        if (Time.time >= summonTime)
        {
            summonTime = Time.time + timeBetweenSummons;
            // Animation: anim.SetTrigger("summon");
            Summon();
        }
    }

    //Summoning script
    public void Summon()
    {
        if (player != null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }
}
