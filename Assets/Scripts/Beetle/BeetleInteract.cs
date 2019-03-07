using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleInteract : MonoBehaviour {

    [SerializeField]
    private AudioSource eatSound;

    GameManager gameManager;
    FarmerStats farmerStats;

    private void Start() {
        gameManager = GameManager.instance;
        farmerStats = gameManager.GetComponent<FarmerStats>();
    }

    private void Update() {

    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Infected") {
            if (GlobalInputManager.TriangleButtonBeetle() == true) {
                foreach(Transform child in other.transform) {
                    if (child.name == "Plague") {
                        farmerStats.plagueAmount -= 1;
                        other.gameObject.tag = "Interactable";
                        eatSound.Play();
                        Destroy(child.gameObject);
                    }
                }
            }
        }
    }
}
