using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard Instance;
    public int score = 0;
    public TMP_Text scoreText;

    private float scoreMultiplier = 1.0f;
    private int comboCounter = 0;
    
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

    public void AddScore(string attackType)
    {
        int basePoints = GetAttackPoints(attackType);
        int finalPoints = Mathf.RoundToInt(basePoints * scoreMultiplier);
        score += finalPoints;
        comboCounter++;
        UpdateMultiplier();
        UpdateScoreText();
    }

    private int GetAttackPoints(string attackType)
    {
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
        if (comboCounter < 5) scoreMultiplier = 1.0f; 
        else if (comboCounter < 10) scoreMultiplier = 1.25f; 
        else if (comboCounter < 15) scoreMultiplier = 1.5f;
        else if (comboCounter < 20) scoreMultiplier = 1.75f;
        else if (comboCounter < 25) scoreMultiplier = 2.0f;
        else scoreMultiplier = 3.0f;
    }

    public void EnemyDefeated(bool isBoss)
    {
        score += isBoss ? 10000 : 500;
        UpdateScoreText();
    }

    public void PickUpItem()
    {
        score += 50;
        UpdateScoreText();
    }

    public void ResetCombo()
    {
        comboCounter = 0;
        scoreMultiplier = 1.0f;
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
