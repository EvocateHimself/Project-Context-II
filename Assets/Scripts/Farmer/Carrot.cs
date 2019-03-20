﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Interactable {

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
        if (transform.Find("Plague") && evolveCrop.currentPhase == 2) {
            farmerStats.notifyText.text = "+" + cropPlacement.carrotSellCost / cropPlacement.infectedSellCostDivider + " coins";
            farmerStats.CurrentMoney += cropPlacement.carrotSellCost / cropPlacement.infectedSellCostDivider;
            farmerStats.plagueAmount -= 1;
        }
        else if (evolveCrop.currentPhase == 2) {
            farmerStats.notifyText.text = "+" + cropPlacement.carrotSellCost + " coins";
            farmerStats.CurrentMoney += cropPlacement.carrotSellCost;
        }
        else {
            farmerStats.notifyText.text = "+" + 0 + " coins";
            farmerStats.CurrentMoney += 0;
        }

        while (farmerStats.progressBar.fillAmount < 1.0f) {
            farmerStats.notifyText.gameObject.transform.parent.gameObject.SetActive(true);
            farmerStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(true);
            farmerStats.progressBar.fillAmount += 1.0f / farmerStats.sellSpeed * Time.deltaTime;
            farmerStats.farmerMovementEnabled = false;
            cropPlacement.isPlanting = true;

            yield return new WaitForEndOfFrame();
        }

        farmerStats.notifyText.gameObject.transform.parent.gameObject.SetActive(false);
        farmerStats.progressBar.gameObject.transform.parent.parent.gameObject.SetActive(false);
        farmerStats.farmerMovementEnabled = true;
        farmerStats.progressBar.fillAmount = 0;
        cropPlacement.isPlanting = false;

        //cropPlacement.sellSound.pitch = Random.Range(0.9f, 1.1f);
        //cropPlacement.sellSound.Play();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Farmer/Coins");
        randomInfect.crops.Remove(gameObject);
        Destroy(gameObject);
    }
}
