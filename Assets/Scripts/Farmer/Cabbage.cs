using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabbage : Interactable {

    public override void Interact() {
        Debug.Log("Greatly interacting with: " + transform.name);
        if (transform.childCount > 1) farmerStats.CurrentMoney += cropPlacer.cabbageSellCost / cropPlacer.infectedSellCostDivider;
        else farmerStats.CurrentMoney += cropPlacer.cabbageSellCost;
        gameObject.tag = "Untagged";
        Destroy(gameObject);
    }
}
