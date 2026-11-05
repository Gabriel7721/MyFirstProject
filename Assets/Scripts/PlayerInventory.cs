using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private CoinCounterUI coinCounterUI;
    [SerializeField]
    private InventoryUI inventoryUI;
    private int coinCount;
    public int CoinCount
    {
        get { return coinCount; }
    }

    private List<Item> items = new();

    public IReadOnlyList<Item> Items
    {
        get { return items; }
    }

    public void SetCoinCount(int amount)
    {
        coinCount = Mathf.Max(amount, 0);

        SaveInventory();

        if (coinCounterUI != null)
        {
            coinCounterUI.SetCoinCount(coinCount);
        }
    }

    public void ImportItems(ItemSaveData[] savedItems)
    {
        items.Clear();

        if (savedItems == null)
        {
            RefreshUI();
            return;
        }

        foreach (ItemSaveData savedItem in savedItems)
        {
            if (savedItem.quantity <= 0) continue;

            Item item = new(
                savedItem.id,
                savedItem.itemName,
                savedItem.quantity
            );

            items.Add(item);
        }
        SaveInventory();
        RefreshUI();
    }

    public ItemSaveData[] ExportItems()
    {
        ItemSaveData[] result =
            new ItemSaveData[items.Count];

        for (int i = 0; i < items.Count; i++)
        {
            ItemSaveData itemData =
                new()
                {
                    id = items[i].Id,
                    itemName = items[i].ItemName,
                    quantity = items[i].Quantity
                };

            result[i] = itemData;
        }
        return result;
    }
    private void Start()
    {
        LoadInventory();
        if (coinCounterUI != null)
        {
            coinCounterUI.SetCoinCount(coinCount);
        }
        RefreshUI();
    }
    public void AddCoin(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        coinCount += amount;
        SaveInventory();
        if (coinCounterUI != null)
        {
            coinCounterUI.SetCoinCount(coinCount);
        }
    }
    public Item FindItem(int id)
    {
        foreach (Item item in items)
        {
            if (item.Id == id)
            {
                return item;
            }
        }

        return null;
    }
    public void AddItem(Item newItem)
    {
        if (newItem == null) return;
        Item existingItem = FindItem(newItem.Id);

        if (existingItem != null)
        {
            existingItem.Quantity += newItem.Quantity;
        }
        else
        {
            items.Add(newItem);
        }
        SaveInventory();
        RefreshUI();
    }
    public bool RemoveItem(int id, int amount)
    {
        if (amount <= 0) return false;

        Item item = FindItem(id);
        if (item == null) return false;
        if (item.Quantity < amount) return false;

        item.Quantity -= amount;
        if (item.Quantity == 0)
        {
            items.Remove(item);
        }
        SaveInventory();
        RefreshUI();
        return true;
    }

    public void UseHealthPotion(PlayerHealth playerHealth)
    {
        if (playerHealth == null) return;

        const int lesserPotionId = 1;
        const int greaterPotionId = 2;

        Item lesserPotion = FindItem(lesserPotionId);
        Item greaterPotion = FindItem(greaterPotionId);

        if (lesserPotion != null && lesserPotion.Quantity > 0)
        {
            if (RemoveItem(lesserPotionId, 1))
            {
                playerHealth.Heal(25);
                Debug.Log("Used Lesser Health Potion: +25 HP");
            }
        }
        else if (greaterPotion != null && greaterPotion.Quantity > 0)
        {
            if (RemoveItem(greaterPotionId, 1))
            {
                playerHealth.Heal(50);
                Debug.Log("Used Greater Health Potion: +50 HP");
            }
        }
        else
        {
            Debug.Log("No Health Potions in Inventory!");
        }
    }


    private void RefreshUI()
    {
        if (inventoryUI != null)
        {
            inventoryUI.Refresh(items);
        }
    }
    private void LoadInventory()
    {
        if (GameSession.Instance == null)
        {
            return;
        }

        coinCount = GameSession.Instance.CoinCount;
        items = GameSession.Instance.LoadItems();
    }
    private void SaveInventory()
    {
        if (GameSession.Instance == null)
        {
            return;
        }

        GameSession.Instance.CoinCount = coinCount;
        GameSession.Instance.SaveItems(items);
    }
}
