using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int scoreReward = 100;

    [SerializeField]
    private GameScore gameScore;

    [SerializeField]
    private int maxHealth = 50;

    [SerializeField]
    private float knockbackForce = 5f;

    [SerializeField]
    private float knockbackDuration = 0.2f;

    private EnemyMovement enemyMovement;

    private int currentHealth;
    private bool isDead = false;
    private EnemySpawner enemySpawner;
    private Rigidbody2D rb;

    [System.Obsolete]
    private void Awake()
    {
        currentHealth = maxHealth;
        // enemySpawner = FindFirstObjectByType<EnemySpawner>();
        gameScore = FindFirstObjectByType<GameScore>();
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void SetSpawner(EnemySpawner spawner)
    {
        enemySpawner = spawner;
    }

    public void TakeDamage(int damage, Vector2 attackerPosition)
    {
        if (isDead || damage <= 0)
        {
            return;
        }

        currentHealth = Mathf.Max(currentHealth - damage, 0);

        Debug.Log("Enemy Health: " + currentHealth);

        if (currentHealth == 0)
        {
            Die();
            return;
        }
        ApplyKnockback(attackerPosition);
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;

        if (gameScore != null)
        {
            gameScore.AddScore(scoreReward);
        }

        if (enemySpawner != null)
        {
            enemySpawner.SpawnEnemy();
        }

        Destroy(gameObject);
    }

    private void ApplyKnockback(Vector2 attackerPosition)
    {
        if (rb == null)
        {
            return;
        }

        Vector2 direction =
            (rb.position - attackerPosition).normalized;

        if (enemyMovement != null)
        {
            enemyMovement.ApplyKnockbackDelay(knockbackDuration);
        }

        rb.linearVelocity = Vector2.zero;

        rb.AddForce(
            direction * knockbackForce,
            ForceMode2D.Impulse
        );
    }
}
