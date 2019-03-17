using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Interactable {

    private void Start() {
        gameManager = GameManager.instance;
        cropPlacement = gameManager.GetComponent<CropPlacement>();
        farmerStats = gameManager.GetComponent<FarmerStats>();
    }

    public override void Interact() {
        StartCoroutine(SellCrop());
    }

    private IEnumerator SellCrop() {
        if (transform.childCount > 1) {
            farmerStats.notifyText.text = "+" + cropPlacement.appleSellCost / cropPlacement.infectedSellCostDivider + " coins";
            farmerStats.CurrentMoney += cropPlacement.appleSellCost / cropPlacement.infectedSellCostDivider;
            farmerStats.plagueAmount -= 1;
        }
        else {
            farmerStats.notifyText.text = "+" + cropPlacement.appleSellCost + " coins";
            farmerStats.CurrentMoney += cropPlacement.appleSellCost;
        }

        while (farmerStats.progressBar.fillAmount < 1.0f) {
            farmerStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(true);
            farmerStats.progressBar.fillAmount += 1.0f / farmerStats.sellSpeed * Time.deltaTime;
            farmerStats.farmerMovementEnabled = false;
            cropPlacement.isPlanting = true;

            yield return new WaitForEndOfFrame();
        }

        farmerStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(false);
        farmerStats.farmerMovementEnabled = true;
        farmerStats.progressBar.fillAmount = 0;
        cropPlacement.isPlanting = false;

        cropPlacement.sellSound.pitch = Random.Range(0.9f, 1.1f);
        cropPlacement.sellSound.Play();
        Destroy(gameObject);
    }
}
