using UnityEngine;

public class ennemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        ContactPoint2D contact = collision.GetContact(0);

        if(contact.normal.y < -0.5f)
        {
            Vector2 bounce = Vector2.up * 10;
            collision.rigidbody.linearVelocityY = 0;
            collision.rigidbody.AddForce (bounce, ForceMode2D.Impulse);

            Die();
        }

        
}

void Die ()
    {
        Destroy(gameObject);
    }
}