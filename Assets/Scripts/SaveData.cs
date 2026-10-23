using System;

[Serializable]
public class SaveData
{
    public string sceneName;

    public float playerX;
    public float playerY;

    public int health;
    public int score;
    public int coinCount;
    
    public ItemSaveData[] items;
    public string[] remainWorldItemIds;
}

