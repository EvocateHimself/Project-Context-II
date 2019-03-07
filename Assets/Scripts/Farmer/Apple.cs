using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Interactable {

    public override void Interact() {
        foreach (Transform child in transform) {
            if (child.name == "Plague") {
                farmerStats.CurrentMoney += cropPlacement.appleSellCost / cropPlacement.infectedSellCostDivider;
                farmerStats.plagueAmount -= 1;
            } else {
                farmerStats.CurrentMoney += cropPlacement.appleSellCost;
            }
        }
        gameObject.tag = "Untagged";
        Destroy(gameObject);
    }
}
