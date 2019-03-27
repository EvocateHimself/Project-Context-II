using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FarmerStats : MonoBehaviour {

    // END SCREEN STATS
    public GameObject endScreenFarmer;
    [HideInInspector]
    public float totalCoins;
    [HideInInspector]
    public float totalCabbagesPlanted, totalCarrotsPlanted, totalApplesPlanted, totalPesticideUsed;
    [HideInInspector]
    public float totalBugs;

    public GameObject farmerPrefab;
    public Animator farmerAnim;
    [HideInInspector]
    public bool farmerMovementEnabled = true;
    public Animator placeUI, cannotPlaceUI, notEnoughCoinsUI;
    public ParticleSystem coinsParticle;
    public Image progressBar;
    public TextMeshProUGUI notifyText;
    [Unit("seconds")]
    public float sellSpeed = 1.5f;

    [Header("Farm Income")]
    [SerializeField]
    [Unit("coins")]
    private float currentMoney = 50f;
    [SerializeField]
    private TextMeshProUGUI moneyText;

    [Header("Farm Health")]
    [SerializeField]
    [Unit("health")]
    private float maxHealth = 100f;
    [HideInInspector]
    private float currentHealth = 0f;
    [SerializeField]
    [Unit("seconds")]
    private float healthBarLerpSpeed = 2f;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private TextMeshProUGUI healthText;

    
    [HideInInspector]
    public float plagueAmount;
    public TextMeshProUGUI plagueAmountText;

    private float currentHealthValue;

    public float CurrentMoney {
        get {
            return currentMoney;
        }

        set {
            currentMoney = value;
        }
    }

    public float CurrentHealth {
        get {
            return currentHealth;
        }

        set {
            currentHealth = Mathf.Clamp(value, 0, MaxHealth);
        }
    }

    public float MaxHealth {
        get {
            return maxHealth;
        }

        set {
            maxHealth = value;
        }
    }

    public float LerpSpeed {
        get {
            return healthBarLerpSpeed;
        }

        set {
            healthBarLerpSpeed = value;
        }
    }

    // Initialize object variables
    private void Start() {
        //CurrentHealth = MaxHealth;
        progressBar.fillAmount = 0;
        endScreenFarmer.SetActive(false);
    }

    // Update is called once per frame
    public virtual void Update() {
        HandleHealthbar();
        HandleMoneybar();
        HandlePesticidebar();

        totalCoins = CurrentMoney;
    }


    // Decrease health upon taking damage
    public void TakeDamage(float damage) {
        //damage -= defense.BaseValue();
        damage = Mathf.Clamp(damage, 0, float.MaxValue);

        CurrentHealth -= damage;

        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (CurrentHealth <= 0) {
            Die();
        }
    }


    // Do something if the player is dead
    public virtual void Die() {
        Debug.Log(transform.name + " died.");
    }


    public void HandleHealthbar() {
        healthText.text = CurrentHealth + "%";
        currentHealthValue = Map(CurrentHealth, 0, MaxHealth, 0, 1);
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentHealthValue, Time.deltaTime * LerpSpeed);
    }

    public void HandleMoneybar() {
        moneyText.text = CurrentMoney.ToString();
        //currentHealthValue = Map(CurrentHealth, 0, MaxHealth, 0, 1);
    }

    public void HandlePesticidebar() {
        plagueAmountText.text = plagueAmount.ToString();
        //currentHealthValue = Map(CurrentHealth, 0, MaxHealth, 0, 1);
    }

    // This method maps a range of numbers into another range
    public float Map(float x, float in_min, float in_max, float out_min, float out_max) {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
