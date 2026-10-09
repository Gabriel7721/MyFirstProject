using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
        {
            return;
        }

        currentHealth =
            Mathf.Max(currentHealth - damage, 0);

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died");

        gameObject.SetActive(false);
    }

}
