using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBed : MonoBehaviour {

    private Collider crop;

    bool startEating = false;

    GameManager gameManager;
    FarmerStats farmerStats;
    BeetleStats beetleStats;

    private void Start() {
        gameManager = GameManager.instance;
        farmerStats = gameManager.GetComponent<FarmerStats>();
        beetleStats = gameManager.GetComponent<BeetleStats>();
    }

    private void Update() {
        if (startEating) {
            beetleStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(true);
            beetleStats.progressBar.fillAmount += 1.0f / beetleStats.eatSpeed * Time.deltaTime;
            beetleStats.beetleMovementEnabled = false;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Interactable") {
            if (GlobalInputManager.TriangleButtonBeetle() == true) {
                crop = other;
                StartCoroutine(EatPlague());
            }
        }
    }

    private IEnumerator EatPlague() { // Cabbage
        foreach (Transform child in crop.transform) {
            if (child.name == "Plague") {
                startEating = true;
                beetleStats.notifyText.text = "+" + beetleStats.plagueFood + " stamina";

                yield return new WaitForSeconds(beetleStats.eatSpeed);

                if (child != null) {
                    beetleStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(false);
                    beetleStats.beetleMovementEnabled = true;
                    beetleStats.progressBar.fillAmount = 0;
                    startEating = false;

                    beetleStats.CurrentStamina += beetleStats.plagueFood;
                    farmerStats.plagueAmount -= 1;
                    beetleStats.CurrentFood += beetleStats.plagueFood;

                    crop.gameObject.tag = "Interactable";
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Kever/Eat Plague");
                    Destroy(child.gameObject);
                }
            }
        }
    }
}
