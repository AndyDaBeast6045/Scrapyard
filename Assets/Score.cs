using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    int score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = score.ToString() + "";
    }

    // Update is called once per frame
    void AddPoint()
    {
        for (int i = 0; i < 5; i++) {
            score += 1;
        }
        scoreText.text = score.ToString() + "";
    }
}
