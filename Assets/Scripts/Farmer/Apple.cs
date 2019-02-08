using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Interactable {

    public override void Interact() {
        Debug.Log("Greatly interacting with: " + transform.name);
        if (transform.childCount > 1) farmerStats.CurrentMoney += cropPlacer.appleSellCost / cropPlacer.infectedSellCostDivider;
        else farmerStats.CurrentMoney += cropPlacer.appleSellCost;
        gameObject.tag = "Untagged";
        Destroy(gameObject);
    }
}
