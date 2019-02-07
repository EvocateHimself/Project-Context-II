using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Interactable {

    public override void Interact() {
        Debug.Log("Interacting! " + transform.name);
    }
}
