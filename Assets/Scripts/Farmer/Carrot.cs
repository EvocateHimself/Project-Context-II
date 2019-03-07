using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Interactable {

    public override void Interact() {
        foreach (Transform child in transform) {
            if (child.name == "Plague") {
                farmerStats.CurrentMoney += cropPlacement.carrotSellCost / cropPlacement.infectedSellCostDivider;
                farmerStats.plagueAmount -= 1;
            } else {
                farmerStats.CurrentMoney += cropPlacement.carrotSellCost;
            }
        }
        gameObject.tag = "Untagged";
        Destroy(gameObject);
    }
}
