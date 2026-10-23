using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadUI : MonoBehaviour
{
    [SerializeField] private SaveSystem saveSystem;
    [SerializeField] private Transform player;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameScore gameScore;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private WorldItemManager worldItemManager;
    private static SaveData pendingLoad;

    public void SaveGame()
    {
        if (saveSystem == null || player == null ||
        playerHealth == null || gameScore == null ||
        playerInventory == null || worldItemManager == null)
        {
            Debug.LogWarning("Missing Save references");
            return;
        }

        SaveData data = new()
        {
            playerX = player.position.x,
            playerY = player.position.y,
            health = playerHealth.CurrentHealth,
            score = gameScore.Score,
            coinCount = playerInventory.CoinCount,
            items = playerInventory.ExportItems(),
            sceneName = SceneManager.GetActiveScene().name,
            remainWorldItemIds =
                worldItemManager.GetRemainingWorldItemIds()
        };
        saveSystem.SaveGame(data);
    }

    public void LoadGame()
    {
        if (saveSystem == null)
        {
            Debug.LogWarning("Missing Load references");
            return;
        }

        SaveData data = saveSystem.LoadGame();
        if (data == null)
        {
            Debug.LogWarning("No save file found");
            return;
        }
        if (string.IsNullOrEmpty(data.sceneName))
        {
            Debug.LogWarning("Missing saved scene name.");
            return;
        }
        pendingLoad = data;
        Time.timeScale = 1f;
        SceneManager.LoadScene(data.sceneName);
    }

    private IEnumerator Start()
    {
        if (pendingLoad == null) yield break;
        yield return null;

        SaveData data = pendingLoad;

        if (player == null || playerHealth == null ||
            gameScore == null || playerInventory == null ||
            worldItemManager == null)
        {
            Debug.LogError("Missing Restore references");
            yield break;
        }

        player.position = new Vector3(
            data.playerX, data.playerY, 0
        );
        playerHealth.SetHealth(data.health);
        gameScore.SetScore(data.score);
        playerInventory.SetCoinCount(data.coinCount);
        playerInventory.ImportItems(data.items);
        worldItemManager.RestoreWorldItems(data);

        pendingLoad = null;
        Debug.Log("Game loaded successfully");
    }
}
