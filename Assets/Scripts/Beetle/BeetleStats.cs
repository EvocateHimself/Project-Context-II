using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BeetleStats : MonoBehaviour {

    public bool beetleMovementEnabled = true;
    public Image progressBar;
    public TextMeshProUGUI notifyText;
    public float eatSpeed = 2f;

    [Header("Stamina")]
    [SerializeField]
    private float maxStamina = 100f;
    [HideInInspector]
    private float currentStamina = 0f;

    public float eatPlagueBooster = 3f;

    public float eatResourcesBooster = 2f;

    [SerializeField]
    private float staminaBarLerpSpeed = 2f;
    [SerializeField]
    private Image staminaBar;
    [SerializeField]
    private TextMeshProUGUI staminaText;

    private float currentStaminaValue;

    public float CurrentStamina {
        get {
            return currentStamina;
        }

        set {
            currentStamina = Mathf.Clamp(value, 0, MaxStamina);
        }
    }

    public float MaxStamina {
        get {
            return maxStamina;
        }

        set {
            maxStamina = value;
        }
    }

    public float LerpSpeedStamina {
        get {
            return staminaBarLerpSpeed;
        }

        set {
            staminaBarLerpSpeed = value;
        }
    }

    [Header("Flight Meter")]
    [SerializeField]
    private float maxFlight = 100f;
    [HideInInspector]
    private float currentFlight = 0f;
    [SerializeField]
    private float flightBarLerpSpeed = 2f;
    [SerializeField]
    private Image flightBar;
    [SerializeField]
    private TextMeshProUGUI flightText;

    private float currentFlightValue;

    public float CurrentFlight {
        get {
            return currentFlight;
        }

        set {
            currentFlight = Mathf.Clamp(value, 0, MaxFlight);
        }
    }

    public float MaxFlight {
        get {
            return maxFlight;
        }

        set {
            maxFlight = value;
        }
    }

    public float LerpSpeedFlight {
        get {
            return flightBarLerpSpeed;
        }

        set {
            flightBarLerpSpeed = value;
        }
    }


    // Initialize object variables
    private void Start() {
        CurrentStamina = MaxStamina;
        CurrentFlight = MaxFlight;
        InvokeRepeating("RegenerateStamina", 0f, 2f);
        InvokeRepeating("RegenerateFlight", 0f, 0.5f);
    }


    // Update is called once per frame
    public virtual void Update() {
        HandleStaminaBar();
        HandleFlightBar();
        //HandlePesticidebar();
    }

    private void RegenerateStamina() {
        CurrentStamina += 1f;
    }

    private void RegenerateFlight() {
        CurrentFlight += 1f;
    }

    // Do something if the player is dead
    public virtual void Die() {
        Debug.Log(transform.name + " died.");
    }


    public void HandleStaminaBar() {
        staminaText.text = CurrentStamina.ToString("F0") + "%";
        currentStaminaValue = Map(CurrentStamina, 0, MaxStamina, 0, 1);
        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, currentStaminaValue, Time.deltaTime * LerpSpeedStamina);
    }

    public void HandleFlightBar() {
        flightText.text = CurrentFlight.ToString("F0") + "%";
        currentFlightValue = Map(CurrentFlight, 0, MaxFlight, 0, 1);
        flightBar.fillAmount = Mathf.Lerp(flightBar.fillAmount, currentFlightValue, Time.deltaTime * LerpSpeedFlight);
    }

    /*
    public void HandlePesticidebar() {
        plagueAmountText.text = plagueAmount.ToString();
    }*/

    // This method maps a range of numbers into another range
    public float Map(float x, float in_min, float in_max, float out_min, float out_max) {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
