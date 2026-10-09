using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Transform playerTransform;
    private float knockbackEndTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
    }

    private void FixedUpdate()
    {
        if (playerTransform == null)
        {
            return;

        }

        if (Time.time < knockbackEndTime)
        {
            return;
        }

        Vector2 direction = ((Vector2)playerTransform.position - rb.position).normalized;

        Vector2 targetPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(targetPosition);
    }

    public void ApplyKnockbackDelay(float duration)
    {
        knockbackEndTime = Time.time + duration;
    }
}
