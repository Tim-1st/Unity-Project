using UnityEngine;

public class PlayerMovement : MonoBehaviour // Correction de l'orthographe (Mouvement -> Movement)
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 10f; // Utilisation d'un float pour la précision
    
    private float moveDirectionX;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundCheckRadius;

    [SerializeField]
    private LayerMask listGroundLayers;

    private bool isGrounded;

    [SerializeField]
    private int jumpForce = 10;

    private bool jumpRequested = false;

    private bool jumpReleased = false;

    private bool isFacingRight = true;

    private bool IsTouchingGround()
{
    return Physics2D.OverlapCircle(
        groundCheck.position,
        groundCheckRadius,
        listGroundLayers
    );
}
    void Update()
    {
        // On récupère l'input dans le Update pour plus de réactivité
        moveDirectionX = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }
        
        if(Input.GetButtonUp("Jump"))
        {
            jumpReleased = true;
        }
        flip();
    }

    private void FixedUpdate()
    {
        // Les calculs physiques se font toujours dans FixedUpdate
        Move();
        isGrounded = IsTouchingGround();
        

        if (jumpRequested && isGrounded)
        {
            Jump();
        }

        if (jumpReleased && rb.linearVelocityY > 0)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocityX,
                rb.linearVelocityY * 0.5f
            );
        }

        jumpRequested = false;
        jumpReleased = false;
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(
            rb.linearVelocityX,
            jumpForce
        );
    }

    private void flip()
    {
        if (moveDirectionX > 0 && !isFacingRight || moveDirectionX < 0 && isFacingRight){
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *=-1;
            transform.localScale = localScale;
        }
    }
    private void Move()
    {
        // Correction de la faute de frappe "lenearVelocity" -> "linearVelocity"
        rb.linearVelocity = new Vector2(moveDirectionX * moveSpeed, rb.linearVelocity.y);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.DrawWireSphere(
                groundCheck.position,
                groundCheckRadius
            );
        }
    }
}

