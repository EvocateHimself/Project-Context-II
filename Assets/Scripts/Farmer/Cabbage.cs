using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabbage : Interactable {

    public override void Interact() {
        Debug.Log("Interacting! " + transform.name);
    }
}
