using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Interactable {

    public override void Interact() {
        Debug.Log("Interacting! " + transform.name);
    }
}
