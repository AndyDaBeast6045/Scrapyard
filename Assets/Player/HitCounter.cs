using System;
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

    void Start()
    {
        isHitting = false;
        timer = hitTime;
        lightAttackAction = InputSystem.actions.FindAction("LightAttack");
        heavyAttackAction = InputSystem.actions.FindAction("HeavyAttack");
    } // Start

    void Update()
    {
        
        if (isHitting) {
            timer -= Time.deltaTime;
        }


        // Resets timer after 
        if (timer <= 0f) { 
            isHitting = false;
            hitCounter = 0;
            timer = hitTime;
        }

        
        // if(lightAttackAction.WasPressedThisFrame() || heavyAttackAction.WasPressedThisFrame())
        if(Input.GetKeyDown(KeyCode.F)) {
            if (!isHitting) isHitting = true;
            timer = hitTime;
            hitCounter++;
        }

        currentHitRank = GetCurrentHitRank();

        Debug.Log("Hit counter: " + hitCounter);
        
    } // Update

    private String GetCurrentHitRank() {
        String rank;
        if (hitCounter <= 0) rank = "";
        else if (hitCounter < 2) rank = hitRanks[0];
        else if (hitCounter < 3) rank = hitRanks[1];
        else if (hitCounter < 4) rank = hitRanks[2];
        else rank = hitRanks[3];
        return rank;
    } // GetCurrentHitRank
}
