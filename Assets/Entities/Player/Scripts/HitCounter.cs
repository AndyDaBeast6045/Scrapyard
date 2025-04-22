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

    private GameObject score = null;

    void Start()
    {
        maxHitCounter = 0;
        isHit = false;
        isHitting = false;
        timer = hitTime;

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
            isHitting = false;
            hitCounter = 0;
            timer = hitTime;
            hitText.enabled = false;
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false); // Don't show HitComboBG
            transform.GetChild(0).GetChild(2).gameObject.SetActive(false); // Don't show "HITS"
            Debug.Log("Hit counter: " + hitCounter);
        }

        
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

        currentHitRank = GetCurrentHitRank();

        
    } // Update

    IEnumerator ScaleHitText() {

        hitText.fontSize = 26f;
        //Vector3 originalScale = hitText.transform.localScale;
        //Vector3 currentScale = Vector3.Lerp(hitText.transform.localScale, Vector3.one * 2f, 5f * Time.deltaTime);
        //hitText.transform.localScale = currentScale;
        yield return new WaitForSeconds(0.2f);
        hitText.fontSize = 20f;
    }

    public void OnHit(){
        timer = hitTime;
        hitCounter++;
        Debug.Log("Hit counter: " + hitCounter);
    }
    private String GetCurrentHitRank() {
        String rank;
        if (hitCounter <= 0) rank = "";
        else if (hitCounter < 5) rank = hitRanks[0];
        else if (hitCounter < 10) rank = hitRanks[1];
        else if (hitCounter < 15) rank = hitRanks[2];
        else if (hitCounter < 20) rank = hitRanks[3];
        else if (hitCounter < 30) rank = hitRanks[4];
        else if (hitCounter < 40) rank = hitRanks[5];
        else rank = hitRanks[6];
        return rank;
    } // GetCurrentHitRank
}
