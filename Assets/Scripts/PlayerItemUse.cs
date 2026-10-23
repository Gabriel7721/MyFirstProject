using UnityEngine;

public class PlayerItemUse : MonoBehaviour
{
    private PlayerInventory inventory;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (inventory != null && playerHealth != null)
            {
                inventory.UseHealthPotion(playerHealth);
            }
        }
    }
}
