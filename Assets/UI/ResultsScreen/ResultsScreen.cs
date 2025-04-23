using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultsScreen : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject resultsPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeTakenText;
    public TextMeshProUGUI maxComboText;
    public GameObject pressAnyKeyText;

    private TextMeshProUGUI pressAnyKeyTMP;
    private bool hasPressedKey = false;

    // These values will be set by your gameplay system
    public static int score = 0;
    public static float timeTaken = 0f;
    public static int maxCombo = 0;

    // Tracks which stage is next
    public static int nextSceneIndex;

    void Start()
    {
        resultsPanel.SetActive(true);
        pressAnyKeyText.SetActive(false);

        // Grab the TMP component for flickering later
        pressAnyKeyTMP = pressAnyKeyText.GetComponent<TextMeshProUGUI>();

        // Set labels without values
        timeTakenText.text = $"<color=#c0c0c0ff><align=left>Timer: <line-height=0></color>";;
        scoreText.text = $"<color=#c0c0c0ff><align=left>Score: <line-height=0></color>";
        maxComboText.text = $"<color=#c0c0c0ff><align=left>Max Combo: <line-height=0></color>";;

        Invoke("ShowResults", 1f);
    }

    void Update()
    {
        if (!hasPressedKey && Input.anyKeyDown)
        {
            hasPressedKey = true;
            ContinueToNextScene();
        }

        // Flicker the "Press Any Key" text if it's active
        if (pressAnyKeyText.activeSelf && pressAnyKeyTMP != null)
        {
            float alpha = Mathf.PingPong(Time.time * 1f, 1f); // flickering
            Color c = pressAnyKeyTMP.color;
            pressAnyKeyTMP.color = new Color(c.r, c.g, c.b, alpha);
        }
    }

    void ShowResults()
    {
        pressAnyKeyText.SetActive(true);
        StartCoroutine(DisplayResultsWithDelay());
    }

    System.Collections.IEnumerator DisplayResultsWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        string seconds = timeTaken.ToString("F2") + "s";
        timeTakenText.text += $"\n<align=right>{seconds}</align><line-height=1em>";

        yield return new WaitForSeconds(0.5f);
        scoreText.text += $"\n<align=right>{score}</align><line-height=1em>";

        yield return new WaitForSeconds(0.5f);
        maxComboText.text += $"\n<align=right>{maxCombo}</align><line-height=1em>";;
    }

    void ContinueToNextScene()
    {
        //if (currentSceneIndex <= )
        SceneManager.LoadScene(2);
    }
}
