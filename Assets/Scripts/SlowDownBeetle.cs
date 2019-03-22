using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownBeetle : MonoBehaviour {

    GameManager gameManager;
    BeetleStats beetleStats;

    private void Start() {
        gameManager = GameManager.instance;
        beetleStats = gameManager.GetComponent<BeetleStats>();
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Beetle") {
            beetleStats.isWalkingInPesticide = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Beetle") {
            beetleStats.isWalkingInPesticide = false;
        }
    }
}
