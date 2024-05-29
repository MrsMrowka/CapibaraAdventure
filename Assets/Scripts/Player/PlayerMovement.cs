using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float climbSpeed;
    [SerializeField] float swimSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float smallJumpMultiplier;
    [SerializeField] float jumpHeightOnJumpPad;
    [SerializeField] float crouchColliderMultiplier;
    [SerializeField] KeyCode jumpKeyCode;
    [SerializeField] KeyCode crouchKeyCode;
    [SerializeField] KeyCode crouchKeyCode2;
    [SerializeField] BoxCollider2D playerCollider;
    [SerializeField] GameObject feet;
    [SerializeField] float footCollisionRange;
    [SerializeField] float wallCollisionRange;
    [SerializeField] float gravityScale;
    [SerializeField] float waterGravityScale;
    [SerializeField] float waterTimeBetweenDmgTicks;
    [SerializeField] Vector2 playerColliderDefaultSize;

    [HideInInspector] public Vector2 lookingSide;
    [HideInInspector] public bool disableMovement = false;

    private float verticalInput;
    private float horizontalInput;
    private float moveSpeedDivider = 1f;
    private float defaultJumpHeight;

    private bool isSwimming = false;
    private bool canJump = false;
    private bool canSecondJump = false;
    private bool canCheckGround = true;
    private bool canWallCheck = false;
    private bool canWallJump = false;
    private bool didWallJump = false;
    private bool isClimbing = false;
    private bool canGoVertical = false;
    private bool isInTheAir = false;
    private Rigidbody2D rb;
    private Death deathClass;
    private Animator animator;
    private PlayerStats playerStats;

    private bool recordKeys = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        deathClass = GameObject.FindObjectOfType<Death>();
        animator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
        defaultJumpHeight = jumpHeight;
        Invoke(nameof(StopRecordingKeys), 30);
    }

    void Update()
    {
        if (recordKeys)
        {
            RecordKeys();
        }

        if (canCheckGround)
        {
            CheckIfGrounded();
        }

        if (canWallCheck)
        {
            CheckIfWallCollision();
        }

        CalculateInput();
    }

    void RecordKeys()
    {
        foreach (char c in Input.inputString)
        {
            GlobalVariables.KeysPressed += c + ";";
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GlobalVariables.KeysPressed += "UpArrow;";
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            GlobalVariables.KeysPressed += "DownArrow;";
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GlobalVariables.KeysPressed += "LeftArrow;";
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            GlobalVariables.KeysPressed += "RightArrow;";
        }
    }

    private void CheckIfGrounded()
    {
        Vector2 originPoint = new Vector2(feet.transform.position.x, feet.transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(originPoint, Vector2.down, footCollisionRange, LayerMask.GetMask("Floor"));

        if (hit.collider != null)
        {
            canJump = true;
            canSecondJump = true;
            canWallCheck = false;
            didWallJump = false;
            isInTheAir = false;
            animator.SetBool("InTheAir", false);
            animator.SetBool("Jump", false);
            animator.SetBool("SlidingOnWall", false);
        } else
        {
            if (!isSwimming && !isClimbing)
            {
                isInTheAir = true;
                canWallCheck = true;
                animator.SetBool("InTheAir", true);
            }
        }
    }

    private void CheckIfWallCollision()
    {
        Vector2 originPoint = new Vector2(transform.position.x, transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(originPoint, lookingSide, wallCollisionRange, LayerMask.GetMask("Wall"));
        
        if (hit.collider != null)
        {
            GlobalVariables.UsedWallSliding += 1;
            animator.SetBool("InTheAir", false);
            animator.SetBool("Movement", false);
            animator.SetBool("Jump", false);
            animator.SetBool("SlidingOnWall", true);
            if (!didWallJump)
            {
                canWallJump = true;
            }
        } else
        {
            animator.SetBool("InTheAir", true);
            animator.SetBool("Movement", false);
            animator.SetBool("SlidingOnWall", false);
            canWallJump = false;
        }
    }
    private void FixedUpdate()
    {
        if (!disableMovement)
        {
            MoveCharacter();
        }
        CheckLookingSide();
    }

    void CalculateInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (canGoVertical)
        {
            verticalInput = Input.GetAxis("Vertical");
            if (verticalInput != 0 && !isSwimming)
            {
                isClimbing = true;
            }
        }

        if ((Input.GetKeyDown(crouchKeyCode) || Input.GetKeyDown(crouchKeyCode2)) && !isSwimming && !isClimbing && !isInTheAir)
        {
            if (!disableMovement)
            {
                Crouch();
            }
        }
        if (Input.GetKeyUp(crouchKeyCode) || Input.GetKeyUp(crouchKeyCode2))
        {
            if (disableMovement)
            {
                StopCrouch();
            }
        }

        if (Input.GetKeyDown(jumpKeyCode) && !isSwimming && !isClimbing && (canJump || canSecondJump))
        {
            Jump();
        }
        else if(Input.GetKeyDown(jumpKeyCode) && !isSwimming && !isClimbing && canWallJump && !canJump)
        {
            WallJump();
        }

        if (Input.GetKeyUp(jumpKeyCode) && !isSwimming && !isClimbing && rb.velocity.y > 0f)
        {
            SmallJump();
        }
    }

    private void StopCrouch()
    {
        disableMovement = false;
        playerCollider.offset = new Vector2(playerCollider.offset.x, 0);
        playerCollider.size = new Vector2(playerColliderDefaultSize.x, playerColliderDefaultSize.y);
        animator.SetBool("Crouch", false);
    }

    void Crouch()
    {
        GlobalVariables.UsedCrouch += 1;
        disableMovement = true;
        rb.velocity = new Vector2(0f, rb.velocity.y);
        float offsetY = playerCollider.size.y * crouchColliderMultiplier / 4;
        playerCollider.offset = new Vector2(playerCollider.offset.x, (offsetY * -1));
        playerCollider.size = new Vector2(playerCollider.size.x, playerCollider.size.y * playerCollider.size.y * crouchColliderMultiplier);
        animator.SetBool("Movement", false);
        animator.SetBool("Crouch", true);
    }

    void CheckLookingSide()
    {
        if (rb.velocity.x > 0)
        {
            lookingSide = Vector2.right;
            transform.localScale = new Vector3(lookingSide.x, transform.localScale.y, transform.localScale.z);
        } else if (rb.velocity.x < 0)
        {
            lookingSide = Vector2.left;
            transform.localScale = new Vector3(lookingSide.x, transform.localScale.y, transform.localScale.z);
        }
    }

    void MoveCharacter()
    {
        if (isSwimming)
        {
            animator.SetBool("InTheAir", false);
            animator.SetBool("Jump", false);
            animator.SetBool("Movement", false);
            animator.SetBool("Swimming", true);
            canWallCheck = false;
            rb.gravityScale = waterGravityScale;
            rb.velocity = new Vector2(horizontalInput * moveSpeed / moveSpeedDivider, verticalInput * climbSpeed);
        }
        else if (isClimbing)
        {
            animator.SetBool("InTheAir", false);
            animator.SetBool("Jump", false);
            animator.SetBool("Movement", false);
            animator.SetBool("Climbing", true);
            canWallCheck = false;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(horizontalInput * moveSpeed / moveSpeedDivider, verticalInput * climbSpeed);
        }
        else
        {
            if (horizontalInput != 0 && !isInTheAir)
            {
                animator.SetBool("Movement", true);
            }
            else
            {
                animator.SetBool("Movement", false);
            }
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }
    }

    void Jump()
    {
        StopClimbing();
        canWallCheck = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        isInTheAir = true;
        animator.SetBool("Movement", false);
        animator.SetBool("Jump", true);

        if (canJump)
        {
            if (jumpHeight == jumpHeightOnJumpPad)
            {
                GlobalVariables.UsedBigJumpPad += 1;
            }
            else
            {
                GlobalVariables.UsedBigJump += 1;
            }
            canCheckGround = false;
            canJump = false;
            Invoke(nameof(AllowCheckGround), 0.1f);
        }
        else
        {
            GlobalVariables.UsedDoubleJump += 1;
            canSecondJump = false;
        }
    }

    void SmallJump()
    {
        if (jumpHeight == jumpHeightOnJumpPad)
        {
            GlobalVariables.UsedSmallJumpPad += 1;
        }
        else
        {
            GlobalVariables.UsedSmallJump += 1;
        }
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * smallJumpMultiplier);
    }

    void WallJump()
    {
        GlobalVariables.UsedWallJump += 1;
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        animator.SetBool("Movement", false);
        animator.SetBool("Jump", true);
        canWallJump = false;
        didWallJump = true;
    }

    void AllowCheckGround()
    {
        canCheckGround = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
        {
            GlobalVariables.UsedLadder += 1;
            canCheckGround = false;
            canGoVertical = true;
        }
        if (collision.tag == "WaterToSwim")
        {
            GlobalVariables.HitWater += 1;
            isSwimming = true;
            isInTheAir = false;
            canWallCheck = false;
            canGoVertical = true;
            canCheckGround = false;
        }
        if (collision.tag == "WaterToReallyDie")
        {
            GlobalVariables.HitWater += 1;
            deathClass.PlayerDeath(DeathTypes.Obstacle);
        }
        if (collision.tag == "WaterToDie")
        {
            GlobalVariables.HitWater += 1;
            isSwimming = true;
            isInTheAir = false;
            canWallCheck = false;
            canGoVertical = true;
            canCheckGround = false;
            InvokeRepeating(nameof(DmgWhenSwimming), 0, waterTimeBetweenDmgTicks);
        }
        if (collision.tag == "JumpPad")
        {
            jumpHeight = jumpHeightOnJumpPad;
            collision.GetComponent<Animator>().SetBool("Push", true);
        }
        if (collision.tag == "Enemy")
        {
            GlobalVariables.KilledEnemies += 1;
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
        {
            canGoVertical = false;
            StopClimbing();
        }
        if (collision.tag == "WaterToSwim")
        {
            isSwimming = false;
            canGoVertical = false;
            StopSwimming();
        }
        if (collision.tag == "WaterToDie")
        {
            isSwimming = false;
            canGoVertical = false;
            StopSwimming();
            CancelInvoke(nameof(DmgWhenSwimming));
        }
        if (collision.tag == "JumpPad")
        {
            jumpHeight = defaultJumpHeight;
            collision.GetComponent<Animator>().SetBool("Push", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BoxToPush")
        {
            GlobalVariables.UsedBox += 1;
            BoxToPush box = collision.gameObject.GetComponent<BoxToPush>();
            moveSpeedDivider = box.boxWeigth;
            box.ApplyForce(new Vector2(horizontalInput * moveSpeed, box.gameObject.GetComponent<Rigidbody2D>().velocity.y));
        } else
        {
            moveSpeedDivider = 1f;
        }
    }

    void StopClimbing()
    {
        animator.SetBool("Climbing", false);
        isClimbing = false;
        rb.gravityScale = gravityScale;
        canCheckGround = true;
    }

    void StopSwimming()
    {
        animator.SetBool("Swimming", false);
        rb.gravityScale = gravityScale;
        canCheckGround = true;
    }

    public void InvokeActivateMovement(float disableMovementFor)
    {
        Invoke(nameof(ActivateMovement), disableMovementFor);
    }

    void ActivateMovement()
    {
        disableMovement = false;
    }

    void DmgWhenSwimming()
    {
        playerStats.SubstractHealth(1, DeathTypes.Obstacle);
    }

    void StopRecordingKeys()
    {
        recordKeys = false;
    }

    public void TakesDmg()
    {
        animator.SetBool("TakingDmg", true);
    }

    void StopTakesDmg()
    {
        animator.SetBool("TakingDmg", false);
    }
}
