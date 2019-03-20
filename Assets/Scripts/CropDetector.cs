using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropDetector : MonoBehaviour {

    public enum CropType { Cabbage, Carrot, Apple }
    public CropType allowedCropType;

    bool isUsed = false;
    bool isComplete = false;

    Animator anim;
    GameManager gameManager;
    CropPlacement cropPlacement;
    FarmerStats farmerStats;
    CycleCrop cycleCrop;
    RandomInfect randomInfect;

    private void Start() {
        gameManager = GameManager.instance;
        cropPlacement = gameManager.GetComponent<CropPlacement>();
        farmerStats = gameManager.GetComponent<FarmerStats>();
        cycleCrop = gameManager.GetComponent<CycleCrop>();
        randomInfect = gameManager.GetComponent<RandomInfect>();
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            // Place cabbage
            if (cycleCrop.selectedCrop == 0 && allowedCropType == CropType.Cabbage && GlobalInputManager.CrossButtonFarmer() && !isUsed && !cropPlacement.isPlanting) {
                StartCoroutine(PlaceCrop(cropPlacement.cabbageGrowCost, cropPlacement.cabbageGrowTime, cropPlacement.cabbagePrefab, "event:/Farmer/Plant Crop"));
            }
            // Place carrot
            else if (cycleCrop.selectedCrop == 1 && allowedCropType == CropType.Carrot && GlobalInputManager.CrossButtonFarmer() && !isUsed && !cropPlacement.isPlanting) {
                StartCoroutine(PlaceCrop(cropPlacement.carrotGrowCost, cropPlacement.carrotGrowTime, cropPlacement.carrotPrefab, "event:/Farmer/Plant Carrot"));
            }
            // Place apple tree
            else if (cycleCrop.selectedCrop == 2 && allowedCropType == CropType.Apple && GlobalInputManager.CrossButtonFarmer() && !isUsed && !cropPlacement.isPlanting) {
                StartCoroutine(PlaceCrop(cropPlacement.appleGrowCost, cropPlacement.appleGrowTime, cropPlacement.applePrefab, "event:/Farmer/Plant Apple"));
            }
            // Show warning if on wrong farming ground
            else if ((cycleCrop.selectedCrop == 0 && allowedCropType != CropType.Cabbage && GlobalInputManager.CrossButtonFarmer() && !cropPlacement.isPlanting) ||
                    (cycleCrop.selectedCrop == 1 && allowedCropType != CropType.Carrot && GlobalInputManager.CrossButtonFarmer() && !cropPlacement.isPlanting) ||
                    (cycleCrop.selectedCrop == 2 && allowedCropType != CropType.Apple && GlobalInputManager.CrossButtonFarmer() && !cropPlacement.isPlanting)) {

                StartCoroutine(Warning());
            }

            // Pick up crops
            if (GlobalInputManager.TriangleButtonFarmer() && isUsed && !cropPlacement.isPlanting) {
                Interactable interactable = gameObject.transform.GetChild(2).GetComponent<Interactable>();

                if (interactable != null) {
                    interactable.Interact();
                    isUsed = false;
                }
            }
        }
    }

    // Place crop
    private IEnumerator PlaceCrop(float cropCost, float processingTime, GameObject cropPrefab, string audioEvent) {
        if (farmerStats.CurrentMoney >= cropCost) {

            isUsed = true;
            GameObject crop = Instantiate(cropPrefab, gameObject.transform.position, Quaternion.identity);
            crop.transform.parent = gameObject.transform;
            FMODUnity.RuntimeManager.PlayOneShot(audioEvent);
            crop.tag = "Untagged";

            Vector3 beginScale = Vector3.zero;
            Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);
            Quaternion randomRotation = Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0));

            crop.transform.localScale = beginScale;

            while (farmerStats.progressBar.fillAmount < 1.0f && crop.transform.localScale.y < 1.0f) {
                cropPlacement.isPlanting = true;
                farmerStats.notifyText.text = "-" + cropCost + " coins";
                farmerStats.notifyText.gameObject.transform.parent.gameObject.SetActive(true);
                farmerStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(true);
                farmerStats.progressBar.fillAmount += 1.0f / processingTime * Time.deltaTime;
                farmerStats.farmerMovementEnabled = false;

                crop.transform.localScale += Vector3.one / processingTime * Time.deltaTime;
                crop.transform.rotation = Quaternion.Lerp(crop.transform.rotation, randomRotation, processingTime * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }

            farmerStats.notifyText.gameObject.transform.parent.gameObject.SetActive(false);
            farmerStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(false);
            farmerStats.farmerMovementEnabled = true;
            farmerStats.progressBar.fillAmount = 0;
            cropPlacement.isPlanting = false;

            farmerStats.CurrentMoney -= cropCost;
            //farmerStats.CurrentHealth += cropPlacement.cabbageHealthImpact;
            randomInfect.crops.Add(crop);
            crop.tag = "Interactable";
        }
    }

    private IEnumerator Warning() {
        farmerStats.notifyText.text = "Cannot place that here!";
        farmerStats.notifyText.gameObject.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        farmerStats.notifyText.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
