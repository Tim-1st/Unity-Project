using UnityEngine;

public class EnnemyPatrol : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private int moveSpeed = 7;

    [SerializeField]
    private LayerMask listObstacles;


    [SerializeField]
    private float obstacleLengthDetection = 0.15f;

    [SerializeField]
    private BoxCollider2D bc;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private bool HasCollisionWithObstacle()
    {
        Vector2 startCast = new Vector2(
            bc.bounds.center.x + (Mathf.Sign(transform.localScale.x) * bc.bounds.extents.x),
            bc.bounds.center.y
        );
        Vector2 endCast = new Vector2(
            startCast.x + (Mathf.Sign(transform.localScale.x) *
            obstacleLengthDetection),
            bc.bounds.center.y
        );
        RaycastHit2D hitObstacle = Physics2D.Linecast(
    startCast,
    endCast,
    listObstacles
);

        return hitObstacle.collider != null;

    }
    void Move()
    {
        rb.linearVelocity = new Vector2(
            moveSpeed * Mathf.Sign(transform.localScale.x),
            rb.linearVelocityY
        );
    }

}