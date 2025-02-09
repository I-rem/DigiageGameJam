using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private int jumpCount = 0;
    private int maxJump = 2;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask waterLayer;
    
    public float stepRate = 0.5f;
    public float stepCoolDown;

    public Animator[] animator;

    public ParticleSystem dust;

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        {
            if ((IsGrounded() && !IsSwimming()) || (!IsSwimming() && IsWalled()))
            {
                FindObjectOfType<AudioManager>().OneShot("Jump");
                dust.Play();
            }
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpCount++;
            
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f && jumpCount < maxJump)
        {
            if ((IsGrounded() && !IsSwimming()) || (!IsSwimming() && IsWalled()))
            {
                FindObjectOfType<AudioManager>().OneShot("Jump");
                dust.Play();
            }
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            jumpCount++;
            
        }

        if (IsGrounded())
            jumpCount = 0;

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            stepCoolDown -= Time.deltaTime;
            if (horizontal != 0 && IsGrounded() && !IsWalled() && !IsSwimming() && stepCoolDown < 0f)
            {
                FindObjectOfType<AudioManager>().OneShot("Footstep");
                stepCoolDown = stepRate;
                for (int i=0;i<3;i++)
                    animator[i].SetFloat("Speed", 1);
            }
            else
                for (int i=0;i<3;i++)
                    animator[i].SetFloat("Speed", -1);
        }
        //else
         //   for (int i=0;i<3;i++)
          //          animator[i].SetFloat("Speed", -1);
    }

    private bool IsSwimming()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, waterLayer);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}