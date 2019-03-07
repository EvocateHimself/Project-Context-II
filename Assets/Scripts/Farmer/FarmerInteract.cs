using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerInteract : MonoBehaviour {

    GameManager gameManager;
    CropPlacer cropPlacer;

    [SerializeField]
    private AudioSource sellSound;

    private void Start() {
        gameManager = GameManager.instance;
        cropPlacer = gameManager.GetComponent<CropPlacer>();
    }

    private void Update() {
    }
}
