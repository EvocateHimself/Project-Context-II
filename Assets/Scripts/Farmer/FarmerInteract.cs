using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerInteract : MonoBehaviour {

    GameManager gameManager;
    CropPlacer cropPlacer;

    private void Start() {
        gameManager = GameManager.instance;
        cropPlacer = gameManager.GetComponent<CropPlacer>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = cropPlacer.farmerCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, cropPlacer.placeRadius) && hit.collider.tag == "Interactable" && hit.collider != null || hit.collider.tag == "InteractableClean" && hit.collider != null) {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null) {
                    interactable.Interact();
                }
            }
        }
    }
}
