using TMPro;
using UnityEngine;

public class LeaderboardRow : MonoBehaviour
{
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text scoreText;

    public void SetData(int rank, LeaderboardEntry entry)
    {
        rankText.text = rank.ToString();
        playerNameText.text = entry.playerName;
        scoreText.text = entry.score.ToString();
    }
}
