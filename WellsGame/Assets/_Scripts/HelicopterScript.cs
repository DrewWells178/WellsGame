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



    // Pursuing Variables
    // *** MY ADDITIONS ***



    // Flipping Variables
 





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
        
    }
   
}
