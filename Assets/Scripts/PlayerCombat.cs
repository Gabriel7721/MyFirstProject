using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private Transform attackPoint;

    [SerializeField]
    private float attackRange = 1f;

    [SerializeField]
    private int attackDamage = 20;

    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private float attackCooldown = 0.5f;
    private float nextAttackTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&
            Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void Attack()
    {
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                attackPoint.position,
                attackRange,
                enemyLayer
            );

        foreach (Collider2D hit in hits)
        {
            EnemyHealth enemyHealth =
                hit.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            attackPoint.position,
            attackRange
        );
    }

}

