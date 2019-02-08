using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleInteract : MonoBehaviour {

    GameManager gameManager;
    FarmerStats farmerStats;

    private void Start() {
        gameManager = GameManager.instance;
        farmerStats = gameManager.GetComponent<FarmerStats>();
    }

    private void Update() {

    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Interactable") {
            if (Input.GetButton("Jump")) {
                /*
                if (other.transform.childCount > 0) {
                    farmerStats.plagueAmount -= 1;
                    other.gameObject.tag = "InteractableClean";
                    Destroy(other.transform.GetChild(1).gameObject);
                } else {
                    Debug.Log("pressed jump");
                    Destroy(other.gameObject);
                }*/
                Destroy(other.gameObject);

            }
        }
    }
}
