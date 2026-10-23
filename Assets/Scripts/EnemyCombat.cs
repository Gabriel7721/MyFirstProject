using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackCooldown = 1f;
    private float nextAttackTime;
    public void TryAttack(Transform target)
    {
        if (target == null) return;
        if (Time.time < nextAttackTime) return;
        if (!target.gameObject
            .TryGetComponent<PlayerHealth>(
                out var playerHealth)) return;
                
        playerHealth.TakeDamage(damage);
        nextAttackTime = Time.time + attackCooldown;
    }
}
