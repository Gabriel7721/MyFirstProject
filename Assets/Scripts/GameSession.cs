using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }
    public int CurrentHealth { get; private set; }
    public bool HasSavedHealth { get; private set; }
    public int CoinCount { get; set; }
    public int Score { get; set; }
    private readonly List<Item> savedItems = new();
    public IReadOnlyList<Item> SavedItems => savedItems;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SaveHealth(int health)
    {
        CurrentHealth = health;
        HasSavedHealth = true;
    }
    public void SaveItems(IEnumerable<Item> items)
    {
        savedItems.Clear();
        foreach (Item item in items)
        {
            savedItems.Add(
                new Item(
                    item.Id,
                    item.ItemName,
                    item.Quantity
                )
            );
        }
    }
    public List<Item> LoadItems()
    {
        List<Item> result = new();
        foreach (Item item in savedItems)
        {
            result.Add(
                new Item(
                    item.Id,
                    item.ItemName,
                    item.Quantity
                )
            );
        }
        return result;
    }
    public void ResetSession()
    {
        CurrentHealth = 0;
        HasSavedHealth = false;
        CoinCount = 0;
        Score = 0;
        savedItems.Clear();
    }
}
