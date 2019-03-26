using System.Collections;
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
        Transform plague = transform.Find("Plague");

        if (plague != null) {
            if (evolveCrop.currentPhase == 2) {
                farmerStats.notifyText.text = "+" + cropPlacement.carrotSellCost / cropPlacement.infectedSellCostDivider;
            }
            else {
                farmerStats.notifyText.text = "+" + 0;
            }
        }

        else {
            if (evolveCrop.currentPhase == 2) {
                farmerStats.notifyText.text = "+" + cropPlacement.carrotSellCost;
            }
            else {
                farmerStats.notifyText.text = "+" + 0;
            }
        }

        while (farmerStats.progressBar.fillAmount < 1.0f) {
            farmerStats.progressBar.color = new Color32(69, 226, 35, 255);
            farmerStats.placeUI.Play("OpenPlaceBoard");
            farmerStats.progressBar.fillAmount += 1.0f / farmerStats.sellSpeed * Time.deltaTime;
            farmerStats.farmerMovementEnabled = false;
            cropPlacement.isPlanting = true;

            yield return new WaitForEndOfFrame();
        }

        if (plague != null) {
            farmerStats.plagueAmount -= 1;

            if (evolveCrop.currentPhase == 2) {
                farmerStats.CurrentMoney += cropPlacement.carrotSellCost / cropPlacement.infectedSellCostDivider;
            }
            else {
                farmerStats.CurrentMoney += 0;
            }
        }

        else {
            if (evolveCrop.currentPhase == 2) {
                farmerStats.CurrentMoney += cropPlacement.carrotSellCost;
            }
            else {
                farmerStats.CurrentMoney += 0;
            }
        }

        farmerStats.placeUI.Play("ClosePlaceBoard");
        if (!farmerStats.coinsParticle.isPlaying) farmerStats.coinsParticle.Play();
        if (farmerStats.coinsParticle.isPlaying) farmerStats.coinsParticle.Stop();
        if (farmerStats.coinsParticle.isPlaying) farmerStats.coinsParticle.Stop();
        if (!farmerStats.coinsParticle.isPlaying) farmerStats.coinsParticle.Play();
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
