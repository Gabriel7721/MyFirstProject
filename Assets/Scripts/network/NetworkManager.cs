using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    [SerializeField]
    private string apiBaseUrl = "http://localhost:9999/api";

    public void LoadLeaderboard()
    {
        StartCoroutine(GetLeaderboard());
    }

    private IEnumerator GetLeaderboard()
    {
        string url = apiBaseUrl + "/leaderboard";

        using UnityWebRequest request =
            UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(
                "Leaderboard request failed: " +
                request.error
            );
            yield break;
        }

        string json = request.downloadHandler.text;
        Debug.Log(json);

        LeaderboardResponse response = JsonUtility
            .FromJson<LeaderboardResponse>(json);

        foreach (LeaderboardEntry entry in response.players)
        {
            Debug.Log(entry.playerName + ": " + entry.score);
        }
    }
}
