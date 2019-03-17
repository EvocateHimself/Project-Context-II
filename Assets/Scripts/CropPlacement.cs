using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CropPlacement : MonoBehaviour {

    public AudioSource interactSound;
    public AudioSource sellSound;
    public AudioSource pesticideSound;
    public float infectedSellCostDivider = 2;
    [HideInInspector]
    public bool isPlanting = false;

    [Header("Cabbage")]
    public GameObject cabbagePrefab;
    public TextMeshProUGUI cabbageText;
    public float cabbageHealthImpact;
    public float cabbageGrowTime;
    public float cabbageGrowCost;
    public float cabbageSellCost;

    [Header("Carrot")]
    public GameObject carrotPrefab;
    public TextMeshProUGUI carrotText;
    public float carrotHealthImpact;
    public float carrotGrowTime;
    public float carrotGrowCost;
    public float carrotSellCost;

    [Header("Apple")]
    public GameObject applePrefab;
    public TextMeshProUGUI appleText;
    public float appleHealthImpact;
    public float appleGrowTime;
    public float appleGrowCost;
    public float appleSellCost;

    [Header("Pesticide")]
    public GameObject pesticidePrefab;
    public TextMeshProUGUI pesticideText;
    public float pesticideDuration = 10f;
    public float pesticideTime;
    public float pesticideCost;

    private void Start() {
        cabbageText.text = cabbageGrowCost.ToString();
        carrotText.text = carrotGrowCost.ToString();
        appleText.text = appleGrowCost.ToString();
        pesticideText.text = pesticideCost.ToString();
    }
}
