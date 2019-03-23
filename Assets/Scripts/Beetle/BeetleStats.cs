using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class BeetleStats : MonoBehaviour {

    [HideInInspector]
    public bool beetleMovementEnabled = true;
    [HideInInspector]
    public bool isWalkingInPesticide = false;
    [HideInInspector]
    public float beetleWalkInPesticideSpeed = 100f;
    public Image progressBar;
    public TextMeshProUGUI notifyText;
    [Unit("seconds")]
    public float eatSpeed = 2f;

    [Header("Stamina")]
    [SerializeField]
    [Unit("stamina")]
    private float maxStamina = 100f;
    private float currentStamina = 0f;
    [SerializeField]
    [Unit("seconds")]
    private float staminaRegenerationSpeed = 2f;
    [SerializeField]
    [Unit("seconds")]
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
    [Unit("flight")]
    private float maxFlight = 100f;
    private float currentFlight = 0f;
    [SerializeField]
    [Unit("seconds")]
    private float flightRegenerationSpeed = 0.5f;
    [SerializeField]
    [Unit("seconds")]
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

    [Header("Resources & Food")]
    [Unit("food")]
    public float plagueFood = 3f;
    [HideInInspector]
    [Unit("food")]
    public float flowerbedFood;
    [Unit("min food")]
    public float flowerbedMinFood = 1f;
    [Unit("max food")]
    public float flowerbedMaxFood = 3f;
    
    private float maxFood = 10f;
    private float currentFood = 1f;
    [Unit("seconds")]
    public float flowerbedRespawnTime;
    [SerializeField]
    private Image foodBar;
    [SerializeField]
    [Unit("resources")]
    private float maxResources = 100f;
    private float currentResources = 0f;
    [SerializeField]
    [Unit("seconds")]
    private float resourcesDecreaseSpeed = 5f;
    [SerializeField]
    [Unit("seconds")]
    private float resourceBarLerpSpeed = 2f;
    [SerializeField]
    private Image resourceBar;

    public Image moodStatusBar;
    public Sprite moodStatusHappy;
    public Sprite moodStatusSad;
    public Sprite moodStatusPoisoned;

    private float currentFoodValue;
    private float currentResourcesValue;

    public float CurrentFood {
        get {
            return currentFood;
        }

        set {
            currentFood = Mathf.Clamp(value, 0, MaxFood);
        }
    }

    public float MaxFood {
        get {
            return maxFood;
        }

        set {
            maxFood = value;
        }
    }

    public float CurrentResources {
        get {
            return currentResources;
        }

        set {
            currentResources = Mathf.Clamp(value, 0, MaxResources);
        }
    }

    public float MaxResources {
        get {
            return maxResources;
        }

        set {
            maxResources = value;
        }
    }

    // Initialize object variables
    private void Start() {
        CurrentStamina = MaxStamina;
        CurrentFlight = MaxFlight;
        CurrentFood = 0;
        CurrentResources = 0;
        moodStatusBar.sprite = moodStatusHappy;
        InvokeRepeating("RegenerateStamina", 0f, staminaRegenerationSpeed);
        InvokeRepeating("RegenerateFlight", 0f, flightRegenerationSpeed);
        InvokeRepeating("DecreaseResources", 0f, resourcesDecreaseSpeed);
        progressBar.gameObject.transform.parent.parent.gameObject.SetActive(false);
    }


    // Update is called once per frame
    public virtual void Update() {
        HandleStaminaBar();
        HandleFlightBar();
        HandleFoodBar();
        HandleResourcesBar();
    }

    private void DecreaseResources() {
        CurrentResources -= 1f;
    }

    private void RegenerateStamina() {
        CurrentStamina += 1f;
    }

    private void RegenerateFlight() {
        CurrentFlight += 1f;
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

    public void HandleFoodBar() {
        currentFoodValue = Map(CurrentFood, 0, MaxFood, 0, 1);
        foodBar.fillAmount = currentFoodValue;
    }

    public void HandleResourcesBar() {
        currentResourcesValue = Map(CurrentResources, 0, MaxResources, 0, 0.8f);
        resourceBar.fillAmount = Mathf.Lerp(resourceBar.fillAmount, currentResourcesValue, Time.deltaTime * resourceBarLerpSpeed);
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
