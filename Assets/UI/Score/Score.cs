using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard Instance;
    public int score = 0;
    public TMP_Text scoreText;

    private float scoreMultiplier = 1.0f;
    private int comboCounter = 0;
    private string currentRank = "Base";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(string attackType, bool isUsingWeapon = false)
    {
        int basePoints = GetAttackPoints(attackType, isUsingWeapon);
        int finalPoints = Mathf.RoundToInt(basePoints * scoreMultiplier);
        score += finalPoints;

        //This will only increase combo for attacks
        comboCounter++;
        UpdateMultiplier();
        UpdateScoreText();
    }

    private int GetAttackPoints(string attackType, bool isUsingWeapon)
    {
        if (isUsingWeapon) return 200;

        switch (attackType)
        {
            case "light": return 100;
            case "heavy": return 150;
            case "command": return 200;
            default: return 0;
        }
    }

    private void UpdateMultiplier()
    {
        if (comboCounter < 5)
        {
            scoreMultiplier = 1.0f;
            currentRank = "Base";
        }
        else if (comboCounter < 10)
        {
            scoreMultiplier = 1.25f;
            currentRank = "D";
        }
        else if (comboCounter < 15)
        {
            scoreMultiplier = 1.5f;
            currentRank = "C";
        }
        else if (comboCounter < 20)
        {
            scoreMultiplier = 1.75f;
            currentRank = "B";
        }
        else if (comboCounter < 25)
        {
            scoreMultiplier = 2.0f;
            currentRank = "A";
        }
        else
        {
            scoreMultiplier = 3.0f;
            currentRank = "S";
        }
    }

    public void EnemyDefeated(bool isBoss)
    {
        // Shouldn't be affected by the multiplier
        score += isBoss ? 10000 : 500;
        UpdateScoreText();
    }

    public void PickUpItem()
    {
        //Same for this
        score += 50;
        UpdateScoreText();
    }

    public void PerfectBlock()
    {
        //Same for this
        score += 300;
        UpdateScoreText();
    }

    public void ResetCombo()
    {
        comboCounter = 0;
        scoreMultiplier = 1.0f;
        currentRank = "Base";
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score} | Combo: {comboCounter} | Rank: {currentRank}";
        }
    }
}
