using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Interactable {

    public override void Interact() {
        Debug.Log("Greatly interacting with: " + transform.name);
        if (transform.childCount > 1) farmerStats.CurrentMoney += cropPlacer.carrotSellCost / cropPlacer.infectedSellCostDivider;
        else farmerStats.CurrentMoney += cropPlacer.carrotSellCost;
        gameObject.tag = "Untagged";
        Destroy(gameObject);
    }
}
