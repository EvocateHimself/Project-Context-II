using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabbage : Interactable {

    public override void Interact() {
        foreach (Transform child in transform) {
            if (child.name == "Plague") {
                farmerStats.CurrentMoney += cropPlacement.cabbageSellCost / cropPlacement.infectedSellCostDivider;
                farmerStats.plagueAmount -= 1;
            } else {
                farmerStats.CurrentMoney += cropPlacement.cabbageSellCost;
            }
        }

        gameObject.tag = "Untagged";
        Destroy(gameObject);
    }
}
