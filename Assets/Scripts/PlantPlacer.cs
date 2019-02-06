using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlantPlacer : MonoBehaviour {

    [SerializeField]
    private Camera farmerCam;
    [SerializeField]
    private LayerMask groundLayer;

    [Header("Crop")]
    [SerializeField]
    private Image cropTexture;
    [SerializeField]
    private GameObject cropPrefab;

    [Header("Carrot")]
    [SerializeField]
    private Image carrotTexture;
    [SerializeField]
    private GameObject carrotPrefab;

    [Header("Apple")]
    [SerializeField]
    private Image appleTexture;
    [SerializeField]
    private GameObject applePrefab;

    bool selectedCrop = false;
    bool selectedCarrot = false;
    bool selectedApple = false;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            selectedCrop = true;
            selectedCarrot = false;
            selectedApple = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            selectedCrop = false;
            selectedCarrot = true;
            selectedApple = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            selectedCrop = false;
            selectedCarrot = false;
            selectedApple = true;
        }

        PlacePlant();
    }

    public void PlacePlant() {
        EventSystem.current.SetSelectedGameObject(null); // Disable button hover properly * requires using UnityEngine.EventSystems;
        if (!EventSystem.current.IsPointerOverGameObject()) { // Prevents raycast from passing through UI * requires using UnityEngine.EventSystems;
            Ray ray = farmerCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (selectedCrop) {
                if (Input.GetMouseButtonDown(0)) {
                    if (Physics.Raycast(ray, out hit, 100, groundLayer) && hit.transform != null) {
                        Instantiate(cropPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                        //cropPrefab.transform.position = hit.point;
                    }
                }
                cropTexture.color = new Color32(0, 0, 0, 50);
                carrotTexture.color = new Color32(255, 255, 255, 255);
                appleTexture.color = new Color32(255, 255, 255, 255);
            }

            if (selectedCarrot) {
                if (Input.GetMouseButtonDown(0)) {
                    if (Physics.Raycast(ray, out hit, 100, groundLayer) && hit.transform != null) {
                        Instantiate(carrotPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                        //cropPrefab.transform.position = hit.point;
                    }
                }
                carrotTexture.color = new Color32(0, 0, 0, 50);
                cropTexture.color = new Color32(255, 255, 255, 255);
                appleTexture.color = new Color32(255, 255, 255, 255);
            }

            if (selectedApple) {
                if (Input.GetMouseButtonDown(0)) {
                    if (Physics.Raycast(ray, out hit, 100, groundLayer) && hit.transform != null) {
                        Instantiate(applePrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                        //cropPrefab.transform.position = hit.point;
                    }
                }
                appleTexture.color = new Color32(0, 0, 0, 50);
                carrotTexture.color = new Color32(255, 255, 255, 255);
                cropTexture.color = new Color32(255, 255, 255, 255);
            }
        }
    }

        /*
    public void PlacePlant() {
        Ray ray = farmerCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, groundLayer) && hit.collider != null) {
            //Instantiate(plantPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
        }
    }
    */
}
