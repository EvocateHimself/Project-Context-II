﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabbage : Interactable {

    EvolveCrop evolveCrop;
    RandomInfect randomInfect;

    private void Start() {
        gameManager = GameManager.instance;
        cropPlacement = gameManager.GetComponent<CropPlacement>();
        farmerStats = gameManager.GetComponent<FarmerStats>();
        evolveCrop = GetComponent<EvolveCrop>();
        randomInfect = gameManager.GetComponent<RandomInfect>();
    }

    public override void Interact() {
        StartCoroutine(SellCrop());
    }

    private IEnumerator SellCrop() {

        Transform plague = transform.Find("Plague");

        if (plague != null) {
            if (evolveCrop.currentPhase == 1) {
                farmerStats.notifyText.text = "+" + cropPlacement.cabbageSellCost / cropPlacement.infectedSellCostDivider + " coins";
            } else {
                farmerStats.notifyText.text = "+" + 0 + " coins";
            }
        }

        else {
            if (evolveCrop.currentPhase == 1) {
                farmerStats.notifyText.text = "+" + cropPlacement.cabbageSellCost + " coins";
            }
            else {
                farmerStats.notifyText.text = "+" + 0 + " coins";
            }
        }

        while (farmerStats.progressBar.fillAmount < 1.0f) {
            farmerStats.notifyText.gameObject.transform.parent.gameObject.SetActive(true);
            farmerStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(true);
            farmerStats.progressBar.fillAmount += 1.0f / farmerStats.sellSpeed * Time.deltaTime;
            farmerStats.farmerMovementEnabled = false;
            cropPlacement.isPlanting = true;

            yield return new WaitForEndOfFrame();
        }

        if (plague != null) {
            farmerStats.plagueAmount -= 1;

            if (evolveCrop.currentPhase == 1) {
                farmerStats.CurrentMoney += cropPlacement.cabbageSellCost / cropPlacement.infectedSellCostDivider;
            }
            else {
                farmerStats.CurrentMoney += 0;
            }
        }

        else {
            if (evolveCrop.currentPhase == 1) {
                farmerStats.CurrentMoney += cropPlacement.cabbageSellCost;
            }
            else {
                farmerStats.CurrentMoney += 0;
            }
        }

        farmerStats.notifyText.gameObject.transform.parent.gameObject.SetActive(false);
        farmerStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(false);
        farmerStats.farmerMovementEnabled = true;
        farmerStats.progressBar.fillAmount = 0;
        cropPlacement.isPlanting = false;

        FMODUnity.RuntimeManager.PlayOneShot("event:/Farmer/Coins");

        randomInfect.crops.Remove(gameObject);
        Destroy(gameObject);
    }
}
