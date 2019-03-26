using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePesticide : MonoBehaviour {

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

    private void Update() {
        // TO DO: place pesticide
        if (cycleCrop.selectedCrop == 3 && GlobalInputManager.CrossButtonFarmerShort() && !cropPlacement.isPlanting) {
            StartCoroutine(PlacePesticideCan(cropPlacement.pesticideCost, cropPlacement.pesticidePlaceTime, cropPlacement.pesticidePrefab));
        }
    }

    private IEnumerator PlacePesticideCan(float pesticideCost, float processingTime, GameObject pesticidePrefab) {
        if (farmerStats.CurrentMoney >= pesticideCost) {

            Vector3 position = new Vector3(transform.position.x, transform.position.y - 1.3f, transform.position.z);
            GameObject pesticide = Instantiate(pesticidePrefab, position, Quaternion.identity);

            while (farmerStats.progressBar.fillAmount < 1.0f) {
                farmerStats.notifyText.text = "-" + pesticideCost;
                farmerStats.progressBar.color = new Color32(226, 35, 38, 255);
                farmerStats.placeUI.Play("OpenPlaceBoard");
                farmerStats.progressBar.fillAmount += 1.0f / processingTime * Time.deltaTime;
                farmerStats.farmerMovementEnabled = false;

                yield return new WaitForEndOfFrame();
            }

            farmerStats.placeUI.Play("ClosePlaceBoard");
            farmerStats.farmerMovementEnabled = true;
            farmerStats.progressBar.fillAmount = 0;

            farmerStats.CurrentMoney -= pesticideCost;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Farmer/Drop Pesticide");

            yield return new WaitForSeconds(cropPlacement.pesticideDuration);
            pesticide.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
