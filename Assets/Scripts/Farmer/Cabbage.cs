using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabbage : Interactable {

    public override void Interact() {

        Debug.Log("Interacting! " + transform.name);
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(1)) {
            Debug.Log("clicked");
            //pesticidePrefab.transform.position = new Vector3(crops[index].transform.position.x, pesticidePos.y, crops[index].transform.position.z);
            //farmerStats.pesticideAmount += 1;
        }
    }
}
