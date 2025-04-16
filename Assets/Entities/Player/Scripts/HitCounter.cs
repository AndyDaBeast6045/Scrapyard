using System;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class HitCounter : MonoBehaviour
{

    private InputAction lightAttackAction;
    private InputAction heavyAttackAction;
    public int hitCounter;
    public int hitTime;
    public String[] hitRanks;
    public String currentHitRank;
    private bool isHitting;
    private float timer;
    public TMP_Text hitText;
    private Vector2 originalPosition;

    void Start()
    {
        isHitting = false;
        timer = hitTime;
        lightAttackAction = InputSystem.actions.FindAction("LightAttack");
        heavyAttackAction = InputSystem.actions.FindAction("HeavyAttack");
        originalPosition = this.gameObject.transform.position;
    } // Start

    void Update()
    {

        hitText.text = hitCounter.ToString() + " - Hit" + ((hitCounter > 1) ? "s" : "");
        
        if (isHitting) {
            timer -= Time.deltaTime;
        }


        // Resets timer after 
        if (timer <= 0f) { 
            isHitting = false;
            hitCounter = 0;
            timer = hitTime;
            Debug.Log("Hit counter: " + hitCounter);
        }

        
        // if(lightAttackAction.WasPressedThisFrame() || heavyAttackAction.WasPressedThisFrame())
        if(Input.GetKeyDown(KeyCode.F)) {
            
            if (!isHitting) isHitting = true;
            timer = hitTime;
            hitCounter++;
            Debug.Log("Hit counter: " + hitCounter);
        }

        currentHitRank = GetCurrentHitRank();

        
    } // Update

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
