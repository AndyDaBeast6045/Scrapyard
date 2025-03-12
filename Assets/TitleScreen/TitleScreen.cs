using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pressAnyKeyText;  
    public GameObject menuPanel;        // contains menu w buttons

    private bool hasPressedKey = false;

    void Start()
    {
        // Start with the menu hidden and the text visible
        menuPanel.SetActive(false);
        pressAnyKeyText.SetActive(true);
    }

    void Update()
    {
        // no key pressed yet
        if (!hasPressedKey)
        {
            // Detect any key press
            if (Input.anyKeyDown)
            {
                ShowMenu();
            }
        }
    }

    void ShowMenu()
    {
        hasPressedKey = true;

        // Hide the text
        pressAnyKeyText.SetActive(false);

        // Show the menu panel
        menuPanel.SetActive(true);
    }

    // === Button Functions ===
    public void StartGame()
    {
        Debug.Log("Start Game Pressed");
        // Example: Load the next scene (replace "GameScene" with your scene name)
        // SceneManager.LoadScene("GameScene");
    }

    public void OpenOptions()
    {
        Debug.Log("Options Pressed");
        // Open options menu here if you have one!
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game Pressed");
        // Quit the application
        Application.Quit();

        // UnityEditor.EditorApplication.isPlaying = false;
    }
}
