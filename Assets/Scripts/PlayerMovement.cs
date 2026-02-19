using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // =========================================================
    // UNITY CALLBACKS
    // =========================================================

    private void Update()
    {
        moveDirectionX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
            jumpRequested = true;

        if (Input.GetButtonUp("Jump"))
            jumpReleased = true;

        Flip();
    }

    private void FixedUpdate()
    {
        isGrounded = IsTouchingGround();

        if (isGrounded && !wasGrounded)
            nbJumps = 0;

        Move();

        if (jumpRequested && (isGrounded || nbJumps < nbMaxJumpsAllowed))
        {
            Jump();
            nbJumps++;
        }

        if (jumpReleased && rb.linearVelocityY > 0)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocityX,
                rb.linearVelocityY * 0.5f
            );
        }

        jumpRequested = false;
        jumpReleased  = false;

        wasGrounded = isGrounded;
    }


    // =========================================================
    // MOVE
    // =========================================================

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 10f;

    private float moveDirectionX;

    private void Move()
    {
        rb.linearVelocity = new Vector2(moveDirectionX * moveSpeed, rb.linearVelocity.y);
    }


    // =========================================================
    // FLIP
    // =========================================================

    private bool isFacingRight = true;

    private void Flip()
    {
        if ((moveDirectionX > 0 && !isFacingRight) ||
            (moveDirectionX < 0 &&  isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }


    // =========================================================
    // JUMP
    // =========================================================

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int nbMaxJumpsAllowed = 2;

    private int nbJumps;
    private bool jumpRequested;
    private bool jumpReleased;

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
    }


    // =========================================================
    // GROUND CHECK
    // =========================================================

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask listGroundLayers;

    private bool isGrounded;
    private bool wasGrounded;

    private bool IsTouchingGround()
    {
        return Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            listGroundLayers
        );
    }


    // =========================================================
    // DEBUG
    // =========================================================

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}