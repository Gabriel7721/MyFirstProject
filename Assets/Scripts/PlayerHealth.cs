using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private HealthBarUI healthBarUI;
    [SerializeField] private GameManager gameManager;
    private int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    public void SetHealth(int health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
        SaveHealth();
        if (healthBarUI != null)
        {
            healthBarUI.SetHealth(currentHealth);
        }
        if (currentHealth == 0 && gameObject.activeSelf)
        {
            Die();
        }
    }
    private void Start()
    {
        if (GameSession.Instance != null &&
           GameSession.Instance.HasSavedHealth)
        {
            currentHealth = Mathf.Clamp(
                GameSession.Instance.CurrentHealth,
                0,
                maxHealth
            );
        }
        else
        {
            currentHealth = maxHealth;
            SaveHealth();
        }

        if (healthBarUI != null)
        {
            healthBarUI.SetMaxHealth(maxHealth);
            healthBarUI.SetHealth(currentHealth);
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || currentHealth == 0)
        {
            return;
        }

        currentHealth =
            Mathf.Min(currentHealth + amount, maxHealth);

        UpdateHealth();
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || currentHealth == 0) return;

        currentHealth =
            Mathf.Max(currentHealth - damage, 0);

        UpdateHealth();
        if (currentHealth == 0) Die();
    }

    private void Die()
    {
        Debug.Log("Player died");
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
        gameObject.SetActive(false);
    }
    private void UpdateHealth()
    {
        SaveHealth();

        if (healthBarUI != null)
        {
            healthBarUI.SetHealth(currentHealth);
        }
    }

    private void SaveHealth()
    {
        if (GameSession.Instance != null)
        {
            GameSession.Instance.SaveHealth(currentHealth);
        }
    }
}
