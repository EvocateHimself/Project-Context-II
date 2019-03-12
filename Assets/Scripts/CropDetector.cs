using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropDetector : MonoBehaviour {

    public bool isUsed = false;
    private bool isPlanting = false;
    private bool startProgressCabbage = false;
    private bool startProgressCarrot = false;
    private bool startProgressApple = false;

    GameManager gameManager;
    CropPlacement cropPlacement;
    FarmerStats farmerStats;
    CycleCrop cycleCrop;

    private void Start() {
        gameManager = GameManager.instance;
        cropPlacement = gameManager.GetComponent<CropPlacement>();
        farmerStats = gameManager.GetComponent<FarmerStats>();
        cycleCrop = gameManager.GetComponent<CycleCrop>();
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            if (cycleCrop.selectedCrop == 0 && GlobalInputManager.CrossButtonFarmer() && !isUsed && !isPlanting) { // Place crops
                StartCoroutine(ProgressCabbage());
            }
            if (cycleCrop.selectedCrop == 1 && GlobalInputManager.CrossButtonFarmer() && !isUsed && !isPlanting) { // Place crops
                StartCoroutine(ProgressCarrot());
            }
            if (cycleCrop.selectedCrop == 2 && GlobalInputManager.CrossButtonFarmer() && !isUsed && !isPlanting) { // Place crops
                StartCoroutine(ProgressApple());
            }

            if (GlobalInputManager.TriangleButtonFarmer() && isUsed) { // Pick up crops
                // TO DO: Add to inventory/sell crop
                Interactable interactable = gameObject.transform.GetChild(2).GetComponent<Interactable>();

                if (interactable != null) {
                    interactable.Interact();
                    cropPlacement.sellSound.pitch = Random.Range(0.9f, 1.1f);
                    cropPlacement.sellSound.Play();
                    isUsed = false;
                }
            }
        }
    }

    private void Update() {
        if (startProgressCabbage) {
            farmerStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(true);
            farmerStats.progressBar.fillAmount += 1.0f / cropPlacement.cabbageGrowTime * Time.deltaTime;
            farmerStats.farmerMovementEnabled = false;
        }
        if (startProgressCarrot) {
            farmerStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(true);
            farmerStats.progressBar.fillAmount += 1.0f / cropPlacement.carrotGrowTime * Time.deltaTime;
            farmerStats.farmerMovementEnabled = false;
        }
        if (startProgressApple) {
            farmerStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(true);
            farmerStats.progressBar.fillAmount += 1.0f / cropPlacement.appleGrowTime * Time.deltaTime;
            farmerStats.farmerMovementEnabled = false;
        }
    }

    private IEnumerator ProgressCabbage() { // Cabbage
        if (farmerStats.CurrentMoney >= cropPlacement.cabbageGrowCost) {
            startProgressCabbage = true;
            isPlanting = true;
            farmerStats.notifyText.text = "-" + cropPlacement.cabbageGrowCost + " coins";
            yield return new WaitForSeconds(cropPlacement.cabbageGrowTime);
            farmerStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(false);
            farmerStats.farmerMovementEnabled = true;
            farmerStats.progressBar.fillAmount = 0;
            startProgressCabbage = false;
            isPlanting = false;
            GameObject Crop = Instantiate(cropPlacement.cabbagePrefab, gameObject.transform.position, Quaternion.identity);
            Crop.transform.parent = gameObject.transform;
            farmerStats.CurrentMoney -= cropPlacement.cabbageGrowCost;
            farmerStats.CurrentHealth += cropPlacement.cabbageHealthImpact;
            cropPlacement.interactSound.Play();
            isUsed = true;
        }
    }

    private IEnumerator ProgressCarrot() { // Carrot
        if (farmerStats.CurrentMoney >= cropPlacement.carrotGrowCost) {
            startProgressCarrot = true;
            isPlanting = true;
            farmerStats.notifyText.text = "-" + cropPlacement.carrotGrowCost + " coins";
            yield return new WaitForSeconds(cropPlacement.carrotGrowTime);
            farmerStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(false);
            farmerStats.farmerMovementEnabled = true;
            farmerStats.progressBar.fillAmount = 0;
            startProgressCarrot = false;
            isPlanting = false;
            GameObject Crop = Instantiate(cropPlacement.carrotPrefab, gameObject.transform.position, Quaternion.identity);
            Crop.transform.parent = gameObject.transform;
            farmerStats.CurrentMoney -= cropPlacement.carrotGrowCost;
            farmerStats.CurrentHealth += cropPlacement.carrotHealthImpact;
            cropPlacement.interactSound.Play();
            isUsed = true;
        }
    }

    private IEnumerator ProgressApple() { // Apple
        if (farmerStats.CurrentMoney >= cropPlacement.appleGrowCost) {
            startProgressApple = true;
            isPlanting = true;
            farmerStats.notifyText.text = "-" + cropPlacement.appleGrowCost + " coins";
            yield return new WaitForSeconds(cropPlacement.appleGrowTime);
            farmerStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(false);
            farmerStats.farmerMovementEnabled = true;
            farmerStats.progressBar.fillAmount = 0;
            startProgressApple = false;
            isPlanting = false;
            GameObject Crop = Instantiate(cropPlacement.applePrefab, gameObject.transform.position, Quaternion.identity);
            Crop.transform.parent = gameObject.transform;
            farmerStats.CurrentMoney -= cropPlacement.appleGrowCost;
            farmerStats.CurrentHealth += cropPlacement.appleHealthImpact;
            cropPlacement.interactSound.Play();
            isUsed = true;
        }
    }
}
