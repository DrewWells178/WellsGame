using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech_Movement : MonoBehaviour
{
    Vector3 lJump = new Vector3(1f,1f,0f);
    
    // Player initialization variables
    private Rigidbody2D rb;
    private BoxCollider2D bc2;
    [SerializeField] LayerMask lm;
    public EnergyBar energyBar;

    // Foreign initialization variables
    public Transform crosshair;

    // Player input variables
    float inputHorizontal;
    float inputVertical;

    // Flight variables
    float flightForce = .8f;
    private int flightEnergy = 3;
    private float flightWait = .2f;

    // Jumping Variables
    public float jumpForce = 5f;

    // Wall Jumping Variables
    bool isWallJumping;
    float wallJumpingDirection;
    float wallJumpingTime = .2f;
    float wallJumpingCounter;
    float wallJumpingDuration = .3f;
    Vector2 wallJumpingPower = new Vector2(6f, 12f);

    // Dash Variables
    float startDashTime = .1f;
    private float dashTime;
    float extraSpeed = 20f;
    private bool isDashing;
    private int dashEnergy = 20;

    // Running Variables
    public float runSpeed = 5f;
    float maxSpeed = 5f;

    // Climbing Variables
    private float climbSpeed = 2.5f;
    private bool climbing;

    // 
    bool facingRight = true;
    int energy;
    int maxEnergy = 150;

    // Wall Variables
    bool isSliding;
    float slideSpeed = 0f;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc2 = transform.GetComponent<BoxCollider2D>();
        Cursor.visible = false;
        energy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
    }

    void Update()
    {
        // Get player inputs
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        
        rb.velocity = new Vector2(rb.velocity.x, Helper.Clamp(rb.velocity.y, -10f, 9f));

        Flip();
        Jump();
        Dash();
        WallSliding();
        WallJump();
        WallClimbing();

        flightWait -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        Run();
        if(energy < maxEnergy)
        {
            energy += 1;
            energyBar.SetEnergy(energy);
        }
        Flight();
        //Debug.Log(energy);
    }

    void Flight()
    {
        if((Input.GetKey("space") || inputVertical > 0) && !isGrounded() && !isClimbingLeft() && !isClimbingRight() && energy > flightEnergy && flightWait <= 0)
        {
            // Do Flight (Apply Force up)
            rb.AddForce(Vector2.up * flightForce, ForceMode2D.Impulse);
            energy -= flightEnergy;
            energyBar.SetEnergy(energy);
        }
    }

    void Run()
    {
        if(inputHorizontal != 0 && !isWallJumping)
        {
            rb.velocity = new Vector2(inputHorizontal * runSpeed, rb.velocity.y);
        }
        else if(inputHorizontal == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void WallJump()
    {
        if(isSliding || climbing)
        {
            isWallJumping = false;
            if(isClimbingLeft())
            {
                wallJumpingDirection = 1f;
            }
            else
            {
                wallJumpingDirection = -1f;
            }
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if(Input.GetKeyDown("space") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            flightWait = .2f;
            wallJumpingCounter = 0f;

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    void Dash()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && energy >= dashEnergy)
        {
            runSpeed += extraSpeed;
            Debug.Log(runSpeed);
            isDashing = true;
            dashTime = startDashTime;
            energy -= dashEnergy;
            energyBar.SetEnergy(energy);
        }

        if(dashTime <= 0 && isDashing == true)
        {
            isDashing = false;
            runSpeed -= extraSpeed;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown("space") && isGrounded())
        {
            flightWait = .2f;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void WallClimbing()
    {
        if(!isSliding && isClimbing() && !isGrounded())
        {
            climbing = true;
            rb.velocity = new Vector2(rb.velocity.x, climbSpeed);
        }
        else
        {
            climbing = false;
        }
    }

    void WallSliding()
    {
        if(!isGrounded() && isClimbing() && inputVertical <= 0)
        {
            isSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Helper.Clamp(rb.velocity.y, slideSpeed, maxSpeed));
        }
        else
        {
            isSliding = false;
        }
    }

    void Flip()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(v.x < transform.position.x && facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            //Vector3 currentScale = gameObject.transform.localScale;
            //currentScale.x *= -1;
            //gameObject.transform.localScale = currentScale;

            facingRight = !facingRight;
        }
        else if(v.x > transform.position.x && !facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            
            //Vector3 currentScale = gameObject.transform.localScale;
            //currentScale.x *= -1;
            //gameObject.transform.localScale = currentScale;

            facingRight = !facingRight;
        }
    }

    bool isGrounded()
    {
        RaycastHit2D rayCastGroundCheck = Physics2D.BoxCast(bc2.bounds.center, bc2.bounds.size, 0f, Vector2.down, 1f, lm);
        return rayCastGroundCheck.collider != null;
    }

    private bool isClimbingRight()
    {
        RaycastHit2D rayCastWallCheck = Physics2D.BoxCast(bc2.bounds.center, bc2.bounds.size * .7f, 0f, Vector2.right, .3f, lm);
        return rayCastWallCheck.collider != null;
    }

    private bool isClimbingLeft()
    {
        RaycastHit2D rayCastWallCheck = Physics2D.BoxCast(bc2.bounds.center, bc2.bounds.size * .7f, 0f, Vector2.left, .3f, lm);
        return rayCastWallCheck.collider != null;
    }

    private bool isClimbing()
    {
        if(isClimbingLeft() && inputHorizontal < 0)
        {
            return true;
        }
        else if(isClimbingRight() && inputHorizontal > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
