
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private PathWalkability pathWalkability;
    private Vector2 actualMovement;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Vector2 lastDirection = Vector2.down;
    private void UpdateAnimation()
    {
        if (animator == null) return;

        if (direction.sqrMagnitude > 0.01f)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                lastDirection = direction.x > 0
                    ? Vector2.right
                    : Vector2.left;
            }
            else
            {
                lastDirection = direction.y > 0
                    ? Vector2.up
                    : Vector2.down;
            }
        }

        animator.SetFloat("DirectionX", lastDirection.x);
        animator.SetFloat("DirectionY", lastDirection.y);
        animator.SetFloat("Speed", direction.magnitude);
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
        UpdateAnimation();
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }
    private void FixedUpdate()
    {
        actualMovement = Vector2.zero;
        if (pathWalkability == null) return;

        Vector2 targetPosition =
            rb.position + (moveSpeed * Time.fixedDeltaTime * direction);

        // rb.MovePosition(targetPosition);
        if (pathWalkability.CanMoveTo(targetPosition))
        {
            rb.MovePosition(targetPosition);
            actualMovement = direction;
        }
    }
}
