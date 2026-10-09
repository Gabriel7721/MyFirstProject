
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new(horizontal, vertical);

        if (direction.sqrMagnitude > 1f)
        {
            direction.Normalize();
        }
    }

    void FixedUpdate()
    {
        Vector2 targetPosition =
            rb.position + (moveSpeed * Time.fixedDeltaTime * direction);

        rb.MovePosition(targetPosition);
    }


}
