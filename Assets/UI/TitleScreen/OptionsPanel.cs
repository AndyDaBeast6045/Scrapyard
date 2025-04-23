using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class OptionsPanel : MonoBehaviour
{
    [Header("Volume Settings")]
    public AudioSource musicSource;
    public Button increaseVolButton;
    public Button decreaseVolButton;
    public Image[] volumeBars; // array of volume bar images to indicate volume level (e.g., 10 bars)
    private int volumeLevel = 5; // between 0 - volumeBars.Length
    [Header("Control Settings")]
    public Button sprintKeyButton;
    public TMP_Text sprintKeyText;

    public Button attackKeyButton;
    public TMP_Text attackKeyText;

    private KeyCode sprintKey = KeyCode.LeftShift;
    private KeyCode attackKey = KeyCode.Z;
    private bool waitingForKey = false;
    private string currentKeyBinding = "";
    [SerializeField] private InputActionReference movementRebind;

    void Start()
    {
        // Hook up volume buttons
        increaseVolButton.onClick.AddListener(RaiseVol);
        decreaseVolButton.onClick.AddListener(LowerVol);

        // Hook up key rebind buttons
        sprintKeyButton.onClick.AddListener(() => StartRebinding("Sprint"));
        attackKeyButton.onClick.AddListener(() => StartRebinding("Attack"));

        // Initialize UI
        UpdateVolumeBars();
        UpdateKeyTexts();
    }

    void Update()
    {
        if (waitingForKey)
        {
            ListenForKey();
        }
    }

    // Volume Methods
    public void LowerVol()
    {
        if (volumeLevel > 0)
        {
            volumeLevel--;
            UpdateVolume();
        }
    }

    public void RaiseVol()
    {
        if (volumeLevel < volumeBars.Length)
        {
            volumeLevel++;
            UpdateVolume();
        }
    }

    private void UpdateVolume()
    {
        float volPercent = (float)volumeLevel / volumeBars.Length;
        musicSource.volume = volPercent;

        UpdateVolumeBars();
    }

    private void UpdateVolumeBars()
    {
        for (int i = 0; i < volumeBars.Length; i++)
        {
            volumeBars[i].enabled = i < volumeLevel;
        }
    }

    // Control Rebinding
    private void StartRebinding(string action)
    {
        currentKeyBinding = action;
        waitingForKey = true;

        // Update the button text itself
        if (action == "Sprint")
        {
            sprintKeyText.text = $"Press new key";
        }
        else if (action == "Attack")
        {
            attackKeyText.text = $"Press new key";
        }
    }

    private void ListenForKey()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode kc in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kc))
                {
                    AssignKey(currentKeyBinding, kc);
                    break;
                }
            }
        }
    }

    private void AssignKey(string action, KeyCode newKey)
    {
        if (action == "Sprint")
        {
            sprintKey = newKey;
            sprintKeyText.text = sprintKey.ToString();
        }
        else if (action == "Attack")
        {
            attackKey = newKey;
            attackKeyText.text = attackKey.ToString();
        }

        waitingForKey = false;
        currentKeyBinding = "";
    }

    private void UpdateKeyTexts()
    {
        sprintKeyText.text = sprintKey.ToString();
        attackKeyText.text = attackKey.ToString();
    }

    // Public getter for sprint key
    public KeyCode GetSprintKey()
    {
        return sprintKey;
    }
}
