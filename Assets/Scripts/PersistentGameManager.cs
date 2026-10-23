using UnityEngine;

public class PersistentGameManager : MonoBehaviour
{
    public static PersistentGameManager Instance
    {
        get;
        private set;
    }
    private int highScore;
    public int HighScore
    {
        get { return highScore; }
    }

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
    public void UpdateHighScore(int currentScore)
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;

            Debug.Log("New High Score: " + highScore);
        }
    }
}
