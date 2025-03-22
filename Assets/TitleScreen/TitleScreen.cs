using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pressAnyKeyText;
    public GameObject menuPanel;        // contains menu w buttons
    public GameObject optionsPanel;    // the options panel that will pop up

    private bool hasPressedKey = false;

    void Start()
    {
        // Start with the menu hidden and the text visible
        menuPanel.SetActive(false);
        pressAnyKeyText.SetActive(true);
        optionsPanel.SetActive(false);  // Make sure options panel is hidden at the start
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

        // Hide the main menu and show the options panel
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        Debug.Log("Closing Options");

        // Hide the options panel and show the main menu again
        optionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game Pressed");
        // Quit the application
        Application.Quit();

        // UnityEditor.EditorApplication.isPlaying = false;
        //^ replace this with the above Application.Quit()
        // if you want to test in the editor. Application.Quit()
        // is for the built exe version of the game.
    }
}
