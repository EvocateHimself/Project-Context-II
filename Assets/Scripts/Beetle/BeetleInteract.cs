using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleInteract : MonoBehaviour {

    private Collider crop;

    bool startEating = false;
    bool hasEaten = false;

    GameManager gameManager;
    FarmerStats farmerStats;
    BeetleStats beetleStats;

    private void Start() {
        gameManager = GameManager.instance;
        farmerStats = gameManager.GetComponent<FarmerStats>();
        beetleStats = gameManager.GetComponent<BeetleStats>();
        Random.InitState((int)System.DateTime.Now.Ticks); // Makes things more random
    }

    private void Update() {
        beetleStats.flowerbedFood = Random.Range(beetleStats.flowerbedMinFood, beetleStats.flowerbedMaxFood);

        if (startEating) {
            beetleStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(true);
            beetleStats.progressBar.fillAmount += 1.0f / beetleStats.eatSpeed * Time.deltaTime;
            beetleStats.beetleMovementEnabled = false;
        }
    }

    private void OnTriggerStay(Collider other) {
        crop = other;

        if (other.tag == "Interactable" && !hasEaten) { // Crops
            if (GlobalInputManager.TriangleButtonBeetle() == true) {
                StartCoroutine(EatPlague());
            }
        }

        if (other.tag == "FlowerBed" && !hasEaten) { // Flowerbed
            if (GlobalInputManager.TriangleButtonBeetle() == true) {
                StartCoroutine(EatFood());
            }
        }

        if (other.tag == "Nest" && !hasEaten) { // Flowerbed
            if (GlobalInputManager.TriangleButtonBeetle() == true) {
                StartCoroutine(StoreResourcesInNest());
            }
        }
    }

    private IEnumerator EatPlague() { // Cabbage
        foreach (Transform child in crop.transform) {
            if (child.name == "Plague") {
                hasEaten = true;
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
                    hasEaten = false;
                }
            }
        }
    }

    private IEnumerator EatFood() {
        foreach (Transform child in crop.transform) {
            if (child.name == "Insect") {
                if (child.gameObject.activeInHierarchy) {
                    hasEaten = true;
                    startEating = true;
                    beetleStats.notifyText.text = "+" + Mathf.RoundToInt(beetleStats.flowerbedFood) + " stamina";
                    int eatFoodBoosterToInt = Mathf.RoundToInt(beetleStats.flowerbedFood);

                    yield return new WaitForSeconds(beetleStats.eatSpeed);

                    if (child != null) {
                        beetleStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(false);
                        beetleStats.beetleMovementEnabled = true;
                        beetleStats.progressBar.fillAmount = 0;
                        startEating = false;

                        beetleStats.CurrentStamina += eatFoodBoosterToInt;
                        beetleStats.CurrentFood += eatFoodBoosterToInt;

                        FMODUnity.RuntimeManager.PlayOneShot("event:/Kever/Eat Plague");
                        child.gameObject.SetActive(false);

                        yield return new WaitForSeconds(beetleStats.flowerbedRespawnTime);

                        child.gameObject.SetActive(true);
                        hasEaten = false;
                    }
                }
            }
        }
    }

    private IEnumerator StoreResourcesInNest() {
        hasEaten = true;
        startEating = true;
        beetleStats.notifyText.text = "+" + beetleStats.CurrentFood + " resources stored in nest";

        yield return new WaitForSeconds(beetleStats.eatSpeed);

        beetleStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(false);
        beetleStats.beetleMovementEnabled = true;
        beetleStats.progressBar.fillAmount = 0;
        startEating = false;

        Debug.Log("Dropping in nest!");

        beetleStats.CurrentResources += beetleStats.CurrentFood;
        beetleStats.CurrentFood = 0;

        FMODUnity.RuntimeManager.PlayOneShot("event:/Kever/Eat Plague");
        hasEaten = false;
    }
}
