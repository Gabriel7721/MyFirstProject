using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private TMP_Text healthText;

    private int maxHealthValue;

    public void SetMaxHealth(int maxHealth)
    {
        maxHealthValue = maxHealth;
        
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        UpdateHealthText(maxHealth);
    }

    public void SetHealth(int currentHealth)
    {
        healthSlider.value = currentHealth;
        UpdateHealthText(currentHealth);
    }

    private void UpdateHealthText(int currentHealth)
    {
        healthText.SetText(
            "HP: {0:0} / {1:0}",
            (float)currentHealth,
            (float)maxHealthValue);
    }
}
