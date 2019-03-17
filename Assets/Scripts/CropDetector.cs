using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropDetector : MonoBehaviour {

    bool isUsed = false;
    bool isComplete = false;

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
            if (cycleCrop.selectedCrop == 0 && GlobalInputManager.CrossButtonFarmer() && !isUsed && !cropPlacement.isPlanting) {
                StartCoroutine(PlaceCrop(cropPlacement.cabbageGrowCost, cropPlacement.cabbageGrowTime, cropPlacement.cabbagePrefab));
            }
            if (cycleCrop.selectedCrop == 1 && GlobalInputManager.CrossButtonFarmer() && !isUsed && !cropPlacement.isPlanting) {
                StartCoroutine(PlaceCrop(cropPlacement.carrotGrowCost, cropPlacement.carrotGrowTime, cropPlacement.carrotPrefab));

            }
            if (cycleCrop.selectedCrop == 2 && GlobalInputManager.CrossButtonFarmer() && !isUsed && !cropPlacement.isPlanting) {
                StartCoroutine(PlaceCrop(cropPlacement.appleGrowCost, cropPlacement.appleGrowTime, cropPlacement.applePrefab));
            }

            if (GlobalInputManager.TriangleButtonFarmer() && isUsed && !cropPlacement.isPlanting) { // Pick up crops
                Interactable interactable = gameObject.transform.GetChild(2).GetComponent<Interactable>();

                if (interactable != null) {
                    interactable.Interact();
                    isUsed = false;
                }
            }
        }
    }

    // Place crop
    private IEnumerator PlaceCrop(float cropCost, float processingTime, GameObject cropPrefab) {
        if (farmerStats.CurrentMoney >= cropCost) {

            isUsed = true;
            GameObject crop = Instantiate(cropPrefab, gameObject.transform.position, Quaternion.identity);
            crop.transform.parent = gameObject.transform;
            crop.tag = "Untagged";

            Vector3 beginScale = Vector3.zero;
            Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);
            Quaternion randomRotation = Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0));

            crop.transform.localScale = beginScale;

            while (farmerStats.progressBar.fillAmount < 1.0f && crop.transform.localScale.y < 1.0f) {
                cropPlacement.isPlanting = true;
                farmerStats.notifyText.text = "-" + cropCost + " coins";
                farmerStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(true);
                farmerStats.progressBar.fillAmount += 1.0f / processingTime * Time.deltaTime;
                farmerStats.farmerMovementEnabled = false;

                crop.transform.localScale += Vector3.one / processingTime * Time.deltaTime;
                crop.transform.rotation = Quaternion.Lerp(crop.transform.rotation, randomRotation, processingTime * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }

            farmerStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(false);
            farmerStats.farmerMovementEnabled = true;
            farmerStats.progressBar.fillAmount = 0;
            cropPlacement.isPlanting = false;

            farmerStats.CurrentMoney -= cropCost;
            //farmerStats.CurrentHealth += cropPlacement.cabbageHealthImpact;
            cropPlacement.interactSound.Play();
            crop.tag = "Interactable";
        }
    }
}
