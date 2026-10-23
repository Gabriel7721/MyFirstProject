using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField]
    private int itemId;

    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity = 1;

    public Item CreateItem()
    {
        return new Item(
        itemId,
        itemName,
        quantity
    );
    }
}
