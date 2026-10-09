using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // [SerializeField]
    // private int attackDamage = 10;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Hit: " + collision.gameObject.name);
        }
        // else if (collision.gameObject.CompareTag("Enemy"))
        // {
        //     EnemyHealth enemyHealth = collision
        //         .gameObject
        //         .GetComponent<EnemyHealth>();
        //     if (enemyHealth != null)
        //     {
        //         enemyHealth.TakeDamage(attackDamage);
        //     }
        // }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            Debug.Log("You received a " + collision.gameObject.name + " !");
        }
    }
}
