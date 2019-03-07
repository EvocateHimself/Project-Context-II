using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    protected GameManager gameManager;
    protected CropPlacement cropPlacement;
    protected FarmerStats farmerStats;

    private void Start() {
        gameManager = GameManager.instance;
        cropPlacement = gameManager.GetComponent<CropPlacement>();
        farmerStats = gameManager.GetComponent<FarmerStats>();
    }

    public virtual void Interact() {
        Debug.Log("Interacting with: " + transform.name);
    }
}
