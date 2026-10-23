using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private GameScore gameScore;
    private PlayerInventory playerInventory;

    private void Awake()
    {
        playerInventory = GetComponent<PlayerInventory>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Hit: " + collision.gameObject.name);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerInventory == null)
        {
            return;
        }

        // 1. Xử lý Coin
        if (collision.CompareTag("Coin"))
        {
            if (gameScore == null)
            {
                return;
            }

            string coinName = collision.gameObject.name.Trim();

            var points = coinName switch
            {
                "BronzeCoin" => 1,
                "SilverCoin" => 5,
                "GoldenCoin" => 10,
                _ => 1,
            };

            playerInventory.AddCoin(1);
            gameScore.AddScore(points);

            Destroy(collision.gameObject);
            return;
        }

        // 2. Xử lý Item
        ItemPickup itemPickup = collision.GetComponent<ItemPickup>();
        if (itemPickup != null)
        {
            Item item = itemPickup.CreateItem();
            playerInventory.AddItem(item);
            Debug.Log("Picked up: " + item.ItemName);
            Destroy(collision.gameObject);
        }
    }
}
