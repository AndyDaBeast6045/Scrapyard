using System;
using System.Collections;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class HitCounter : MonoBehaviour
{

    public int hitCounter;
    private int maxHitCounter;
    public int hitTime;
    public String[] hitRanks;
    public String currentHitRank;
    private bool isHitting;
    public bool isHit;
    public string attackType;
    private float timer;
    public TMP_Text hitText;
    public TMP_Text hitRankText;

    private GameObject score = null;

    private float scaleTime = 0;

    void Start()
    {
        maxHitCounter = 0;
        isHit = false;
        isHitting = false;
        timer = hitTime;
        currentHitRank = hitRanks[0];

        hitText.enabled = false;
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false); // Don't show HitComboBG
        transform.GetChild(0).GetChild(2).gameObject.SetActive(false); // Don't show "HITS"

        if (score == null) score = GameObject.Find("Score");

    } // Start

    void Update()
    {

        //hitText.text = hitCounter.ToString() + " - Hit" + ((hitCounter > 1) ? "s" : "");
        hitText.text = hitCounter.ToString();
        
        if (isHitting) {
            timer -= Time.deltaTime;
        }


        // Resets timer after 
        if (timer <= 0f) { 
            Reset();
        }

        // When player hits an enemy
        if(isHit) {
            isHit = false;
            if (!isHitting) isHitting = true;
            StartCoroutine("ScaleHitText");
            OnHit();
            if (hitCounter > maxHitCounter) maxHitCounter = hitCounter;
            hitText.enabled = true;
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true); // Show HitComboBG
            transform.GetChild(0).GetChild(2).gameObject.SetActive(true); // Show "HITS"
            score.GetComponent<ScoreBoard>().AddScore(attackType, hitCounter, false);
            ResultsScreen.maxCombo = this.maxHitCounter;
        }

        // Update Hit Ranks
        if (currentHitRank != GetCurrentHitRank()) {
            currentHitRank = GetCurrentHitRank();
            hitRankText.text = currentHitRank;
        }
        
        scaleTime += Time.deltaTime * (hitCounter < 50 ? (hitCounter / 20f) : 3f);
        float scaleFactor = Mathf.PingPong(scaleTime, 0.30f);
        hitRankText.fontSize = Mathf.RoundToInt(8 * (1 + scaleFactor));
        
    } // Update

    public void Reset()
    {
        isHitting = false;
        hitCounter = 0;
        timer = hitTime;
        hitText.enabled = false;
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false); // Don't show HitComboBG
        transform.GetChild(0).GetChild(2).gameObject.SetActive(false); // Don't show "HITS"
                                                                       //Debug.Log("Hit counter: " + hitCounter);
    }
    IEnumerator ScaleHitText() {
        if (hitCounter <= 10)
        {
            hitText.fontSize = 26f;
        }
        else if (hitCounter <= 30)
        {
            hitText.fontSize = 28f;
        }
        else
        {
            hitText.fontSize = 30f;
        }

        //Vector3 originalScale = hitText.transform.localScale;
        //Vector3 currentScale = Vector3.Lerp(hitText.transform.localScale, Vector3.one * 2f, 5f * Time.deltaTime);
        //hitText.transform.localScale = currentScale;
        yield return new WaitForSeconds(0.15f);
        hitText.fontSize = 20f;
    }

    public void OnHit(){
        timer = hitTime;
        hitCounter++;
        //Debug.Log("Hit counter: " + hitCounter);
        if (hitCounter <= 5)
        {
            hitText.color = Color.gray;
            hitRankText.color = Color.gray;
        }
        else if (hitCounter <= 10)
        {
            hitText.color = Color.green;
            hitRankText.color = Color.green;
        }
        else if (hitCounter <= 20)
        {
            hitText.color = Color.blue;
            hitRankText.color = Color.blue;
        }
        else if (hitCounter <= 30)
        {
            hitText.color = Color.red;
            hitRankText.color = Color.red;
        }
        else
        {
            hitText.color = Color.yellow;
            hitRankText.color = Color.yellow;
        }
    }
    private String GetCurrentHitRank() {
        String rank;
        if (hitCounter <= 5) rank = "";
        else if (hitCounter < 10) rank = hitRanks[0];
        else if (hitCounter < 15) rank = hitRanks[1];
        else if (hitCounter < 20) rank = hitRanks[2];
        else if (hitCounter < 30) rank = hitRanks[3];
        else if (hitCounter < 40) rank = hitRanks[4];
        else if (hitCounter < 50) rank = hitRanks[5];
        else rank = hitRanks[6];
        return rank;
    } // GetCurrentHitRank
}
