using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerInteract : MonoBehaviour {

    [HideInInspector]
    public Camera farmerCam;

    private void Start() {
        farmerCam = GetComponent<Camera>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = farmerCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100) && hit.collider.tag == "Interactable" && hit.collider != null) {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null) {
                    interactable.Interact();
                }
            }
        }
    }
}
