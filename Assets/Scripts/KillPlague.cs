using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlague : MonoBehaviour {

    GameManager gameManager;
    FarmerStats farmerStats;

    private void Start() {
        gameManager = GameManager.instance;
        farmerStats = gameManager.GetComponent<FarmerStats>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Plague") {
            farmerStats.plagueAmount -= 1;
            Destroy(other.gameObject);
        }
    }
}
