using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPesticide : MonoBehaviour {

    GameManager gameManager;
    FarmerStats farmerStats;

    private void Start() {
        gameManager = GameManager.instance;
        farmerStats = gameManager.GetComponent<FarmerStats>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Pesticide") {
            Destroy(other.gameObject);
            farmerStats.pesticideAmount -= 1;
        }
    }
}
