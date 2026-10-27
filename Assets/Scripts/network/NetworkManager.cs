using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    [SerializeField]
    private string apiBaseUrl = "http://localhost:9999/api";
    public event System.Action<LeaderboardResponse> LeaderboardLoaded;
    public event System.Action<string> LeaderboardFailed;
    public void LoadLeaderboard()
    {
        StartCoroutine(GetLeaderboard());
    }
    private IEnumerator GetLeaderboard()
    {
        string url = apiBaseUrl + "/leaderboard";

        using UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Leaderboard request failed: " + request.error);
            LeaderboardFailed?.Invoke(request.error);
            yield break;
        }

        string json = request.downloadHandler.text;

        LeaderboardResponse response =
            JsonUtility.FromJson<LeaderboardResponse>(json);

        if (response == null || response.players == null)
        {
            LeaderboardFailed?.Invoke("Invalid leaderboard response.");
            yield break;
        }

        foreach (LeaderboardEntry entry in response.players)
        {
            Debug.Log(entry.playerName + ": " + entry.score);
        }

        LeaderboardLoaded?.Invoke(response);
    }

    public void SendScore(string playerName, int score)
    {
        StartCoroutine(PostScore(playerName, score));
    }

    private IEnumerator PostScore(string playerName, int score)
    {
        string url = apiBaseUrl + "/scores";

        ScoreRequest scoreRequest = new()
        {
            playerName = playerName,
            score = score
        };

        string json = JsonUtility.ToJson(scoreRequest);
        byte[] body = Encoding.UTF8.GetBytes(json);
        using UnityWebRequest request = new(url, "POST");

        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Save score failed: " + request.error);
            yield break;
        }

        Debug.Log(request.downloadHandler.text);
        Debug.Log("Score saved successfully!");
    }
}
