using UnityEngine;

public class WorldItemManager : MonoBehaviour
{
    public void RestoreWorldItems(SaveData data)
    {
        WorldItem[] worldItems =
                FindObjectsByType<WorldItem>();

        if (data.remainWorldItemIds == null)
        {
            Debug.LogWarning("No World Item data found.");
            return;
        }

        foreach (WorldItem worldItem in worldItems)
        {
            bool shouldExist = false;

            foreach (string id in data.remainWorldItemIds)
            {
                if (worldItem.UniqueId == id)
                {
                    shouldExist = true;
                    break;
                }
            }

            if (!shouldExist) Destroy(worldItem.gameObject);
        }
    }
    public string[] GetRemainingWorldItemIds()
    {
        WorldItem[] worldItems =
            FindObjectsByType<WorldItem>();

        string[] ids = new string[worldItems.Length];

        for (int i = 0; i < worldItems.Length; i++)
        {
            ids[i] = worldItems[i].UniqueId;
        }

        return ids;
    }
}
