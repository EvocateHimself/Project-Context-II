using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleInteract : MonoBehaviour {

    [SerializeField]
    private AudioSource eatSound;

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
                beetleStats.notifyText.text = "+" + beetleStats.eatPlagueBooster + " stamina";

                yield return new WaitForSeconds(beetleStats.eatSpeed);
                beetleStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(false);
                beetleStats.beetleMovementEnabled = true;
                beetleStats.progressBar.fillAmount = 0;
                startEating = false;

                beetleStats.CurrentStamina += beetleStats.eatPlagueBooster;
                farmerStats.plagueAmount -= 1;
                eatSound.Play();

                crop.gameObject.tag = "Interactable";

                if (child != null) {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}
