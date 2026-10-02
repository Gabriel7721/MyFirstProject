using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("You hit: " + collision.gameObject.name + " !");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Debug.Log("You received a coin !");
            Destroy(collision.gameObject);
        }
    }
}
