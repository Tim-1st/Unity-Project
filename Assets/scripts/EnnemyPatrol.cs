using UnityEngine;
 
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
 
    [SerializeField]
    private int moveSpeed = 7;
 
    [SerializeField]
    private float obstacleCheckRadius = 0.15f;
 
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
 
    void FixedUpdate()
    {
 
        if (HasCollisionWithObstacle() || HasNotTouchedGround())
        {
            Flip();
        }
        Move();
    }
 
    private bool HasNotTouchedGround()
    {
        Vector2 center = new Vector2(
            bc.bounds.center.x + bc.bounds.extents.x,
            bc.bounds.min.y
        );
 
        return !Physics2D.OverlapCircle(
            center,
            obstacleCheckRadius,
            listObstacles
        );
    }
 
    private bool HasCollisionWithObstacle()
    {
        Vector2 startCast = new Vector2(
            bc.bounds.center.x + 
                (Mathf.Sign(transform.localScale.x) 
                * bc.bounds.extents.x),
                bc.bounds.center.y
        );
 
        Vector2 endCast = new Vector2(
            startCast.x + 
                Mathf.Sign(transform.localScale.x) * 
                obstacleLengthDetection,
            bc.bounds.center.y
        );
 
        RaycastHit2D hitObstacle = Physics2D.Linecast(
            startCast, endCast, listObstacles);
 
        return hitObstacle.collider != null;
    }
 
    void Move()
    {
        rb.linearVelocity = new Vector2(
            moveSpeed * Mathf.Sign(transform.localScale.x),
            rb.linearVelocityY
        );
    }
 
    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
 
        transform.localScale = localScale;
    }
 
    void OnDrawGizmos()
    {
        if (bc == null) return;
 
        // On dessine un cercle qui représente la détection de vides, nous le allons situer :
        // x : A l'extrême du BoxCollider du GameObject dans la direction où il se déplace
        // y : A la base du BoxCollider
        //Gizmos.color = Color.aquamarine;
        Gizmos.DrawWireSphere(
            new Vector2(
                bc.bounds.center.x + (Mathf.Sign(transform.localScale.x) * bc.bounds.extents.x),
                bc.bounds.min.y
            ),
            obstacleCheckRadius
        );
 
        Gizmos.color = Color.red;
        // On dessine un trait qui représente la détection de murs, nous le allons situer :
        // x : A l'extrême du BoxCollider du GameObject dans la direction où il se déplace
        // y : Au milieu du BoxCollider
        Vector2 startCast = new Vector2(
            bc.bounds.center.x + (Mathf.Sign(transform.localScale.x) * bc.bounds.extents.x),
            bc.bounds.center.y
        );
        Gizmos.DrawLine(
            startCast,
            new Vector2(startCast.x + (Mathf.Sign(transform.localScale.x) * obstacleLengthDetection), startCast.y)
        );
 
    }
}