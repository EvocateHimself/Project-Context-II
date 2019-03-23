using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CropPlacement : MonoBehaviour {

    public float infectedSellCostDivider = 2;
    [HideInInspector]
    public bool isPlanting = false;

    [Header("Cabbage")]
    public GameObject cabbagePrefab;
    public TextMeshProUGUI cabbageText;
    [Unit("seconds")]
    public float cabbageGrowTime;
    [Unit("coins")]
    public float cabbageGrowCost;
    [Unit("coins")]
    public float cabbageSellCost;

    [Header("Carrot")]
    public GameObject carrotPrefab;
    public TextMeshProUGUI carrotText;
    [Unit("seconds")]
    public float carrotGrowTime;
    [Unit("coins")]
    public float carrotGrowCost;
    [Unit("coins")]
    public float carrotSellCost;

    [Header("Apple")]
    public GameObject applePrefab;
    public TextMeshProUGUI appleText;
    [Unit("seconds")]
    public float appleGrowTime;
    [Unit("coins")]
    public float appleGrowCost;
    [Unit("coins")]
    public float appleSellCost;

    [Header("Pesticide")]
    public GameObject pesticidePrefab;
    public TextMeshProUGUI pesticideText;
    [Unit("seconds")]
    public float pesticidePlaceTime;
    [Unit("seconds")]
    public float pesticideDuration = 10f;
    [Unit("coins")]
    public float pesticideCost;

    private void Start() {
        cabbageText.text = cabbageGrowCost.ToString();
        carrotText.text = carrotGrowCost.ToString();
        appleText.text = appleGrowCost.ToString();
        pesticideText.text = pesticideCost.ToString();
    }
}
