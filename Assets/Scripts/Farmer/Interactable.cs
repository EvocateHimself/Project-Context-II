using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    protected GameManager gameManager;
    protected CropPlacer cropPlacer;
    protected FarmerStats farmerStats;

    private void Start() {
        gameManager = GameManager.instance;
        cropPlacer = gameManager.GetComponent<CropPlacer>();
        farmerStats = gameManager.GetComponent<FarmerStats>();
    }

    public virtual void Interact() {
        Debug.Log("Interacting with: " + transform.name);
    }
}
