using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SaveLoadUI saveLoadUI;
    [SerializeField] private SaveSystem saveSystem;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button deleteSaveButton;
    private void UpdateButton()
    {
        bool hasSave =
            saveSystem != null && saveSystem.HasSaveFile();

        if (continueButton != null)
            continueButton.interactable = hasSave;

        if (deleteSaveButton != null)
            deleteSaveButton.interactable = hasSave;
    }
    private void Start() { UpdateButton(); }
    public void StartGame()
    {
        Time.timeScale = 1f;
        if (GameSession.Instance != null)
        {
            GameSession.Instance.ResetSession();
        }
        SceneManager.LoadScene("GameScene");
    }
    public void ContinueGame()
    {
        if (saveSystem == null || !saveSystem.HasSaveFile())
        {
            Debug.LogWarning("No save file available.");
            UpdateButton();
            return;
        }

        if (saveLoadUI == null)
        {
            Debug.LogWarning("SaveLoadUI is missing.");
            return;
        }

        saveLoadUI.LoadGame();
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void DeleteSaveGame()
    {
        saveSystem.DeleteSave();
        UpdateButton();
    }
}
