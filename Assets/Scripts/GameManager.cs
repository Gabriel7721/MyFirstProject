using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameScore gameScore;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject player;
    private bool isGameOver;
    private bool isPaused;

    private void Start()
    {
        Time.timeScale = 1f;

        isGameOver = false;
        isPaused = false;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }
    private void Update()
    {
        if (isGameOver) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        if (isGameOver || isPaused) return;

        isPaused = true;
        Time.timeScale = 0f;

        if (pausePanel != null) pausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        if (isGameOver || !isPaused) return;

        isPaused = false;
        Time.timeScale = 1f;

        if (pausePanel != null) pausePanel.SetActive(false);
    }
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        isPaused = false;

        if (
            gameScore != null &&
            finalScoreText != null &&
            networkManager != null &&
            player != null
        )
        {
            finalScoreText.SetText("Final Score: {0:0}", gameScore.Score);
            networkManager.SendScore(player.name, gameScore.Score);
        }
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (pausePanel != null) pausePanel.SetActive(false);
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        if (GameSession.Instance != null)
        {
            GameSession.Instance.ResetSession();
        }
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name);
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
