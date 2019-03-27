using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleInteract : MonoBehaviour {

    private Collider crop;

    bool startEating = false;
    bool startStoring = false;
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
            beetleStats.eatUI.Play("OpenEatBoard");
            beetleStats.progressBar.fillAmount += 1.0f / beetleStats.eatSpeed * Time.deltaTime;
            beetleStats.beetleMovementEnabled = false;
        }

        if (startStoring) {
            beetleStats.nestUI.Play("OpenNestBoard");
            beetleStats.progressBarNest.fillAmount += 1.0f / beetleStats.storeResourcesSpeed * Time.deltaTime;
            beetleStats.beetleMovementEnabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            // Oof
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
                beetleStats.progressBar.color = new Color32(69, 226, 35, 255); // green
                beetleStats.notifyText.text = "+" + beetleStats.plagueFood;
                startEating = true;

                yield return new WaitForSeconds(beetleStats.eatSpeed);
                
                if (child != null) {
                    beetleStats.eatUI.Play("CloseEatBoard");
                    beetleStats.beetleMovementEnabled = true;
                    beetleStats.progressBar.fillAmount = 0;
                    startEating = false;

                    //beetleStats.CurrentStamina += beetleStats.plagueFood;
                    beetleStats.CurrentStamina -= beetleStats.plagueFood * beetleStats.staminaCarryFoodImpact;

                    farmerStats.plagueAmount -= 1;
                    beetleStats.CurrentFood += beetleStats.plagueFood;
                    beetleStats.totalPlagueEaten += 1;

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
                    beetleStats.notifyText.text = "+" + Mathf.RoundToInt(beetleStats.flowerbedFood);
                    startEating = true;
                    int eatFoodBoosterToInt = Mathf.RoundToInt(beetleStats.flowerbedFood);

                    yield return new WaitForSeconds(beetleStats.eatSpeed);

                    if (child != null) {
                        beetleStats.eatUI.Play("CloseEatBoard");
                        beetleStats.beetleMovementEnabled = true;
                        beetleStats.progressBar.fillAmount = 0;
                        startEating = false;

                        beetleStats.CurrentStamina -= eatFoodBoosterToInt * beetleStats.staminaCarryFoodImpact;
                        beetleStats.CurrentFood += eatFoodBoosterToInt;
                        beetleStats.totalFlowerbedsEaten += 1;

                        FMODUnity.RuntimeManager.PlayOneShot("event:/Kever/Eat Plague");
                        child.gameObject.SetActive(false);
                        hasEaten = false;

                        yield return new WaitForSeconds(beetleStats.flowerbedRespawnTime);

                        child.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    private IEnumerator StoreResourcesInNest() {
        hasEaten = true;
        beetleStats.progressBarNest.color = new Color32(255, 192, 0, 255); // green
        beetleStats.notifyTextNest.text = "+" + beetleStats.CurrentFood;
        startStoring = true;

        yield return new WaitForSeconds(beetleStats.storeResourcesSpeed);

        beetleStats.nestUI.Play("CloseNestBoard");
        beetleStats.nestFullUI.Play("CloseNestFull");
        beetleStats.beetleMovementEnabled = true;
        beetleStats.progressBarNest.fillAmount = 0;
        startStoring = false;

        beetleStats.CurrentResources += beetleStats.CurrentFood;
        beetleStats.CurrentFood = 0;
        beetleStats.CurrentStamina = beetleStats.MaxStamina;

        FMODUnity.RuntimeManager.PlayOneShot("event:/Kever/Eat Plague");
        hasEaten = false;
    }
}
