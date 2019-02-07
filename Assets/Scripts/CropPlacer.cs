using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CropPlacer : MonoBehaviour {

    [SerializeField]
    private Camera farmerCam;
    [SerializeField]
    private LayerMask groundLayer;

    [Header("Cabbage")]
    [SerializeField]
    private Image cabbageTexture;
    [SerializeField]
    private GameObject cabbagePrefab;
    [SerializeField]
    private float cabbageHealthImpact;
    [SerializeField]
    private float cabbageGrowCost;
    [SerializeField]
    private float cabbageSellCost;

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

    bool selectedCabbage = false;
    bool selectedCarrot = false;
    bool selectedApple = false;

    GameManager gameManager;
    FarmerStats farmerStats;

    private void Start() {
        gameManager = GameManager.instance;
        farmerStats = gameManager.GetComponent<FarmerStats>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            selectedCabbage = true;
            selectedCarrot = false;
            selectedApple = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            selectedCabbage = false;
            selectedCarrot = true;
            selectedApple = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            selectedCabbage = false;
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

            if (selectedCabbage) {
                if (Input.GetMouseButtonDown(0)) {
                    if (Physics.Raycast(ray, out hit, 100, groundLayer) && hit.transform != null) {
                        if (farmerStats.CurrentMoney >= cabbageGrowCost) {
                            Instantiate(cabbagePrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                            farmerStats.CurrentMoney -= cabbageGrowCost;
                            farmerStats.CurrentHealth += cabbageHealthImpact;
                        }
                    }
                }
                cabbageTexture.color = new Color32(0, 0, 0, 50);
                carrotTexture.color = new Color32(255, 255, 255, 255);
                appleTexture.color = new Color32(255, 255, 255, 255);
            }

            if (selectedCarrot) {
                if (Input.GetMouseButtonDown(0)) {
                    if (Physics.Raycast(ray, out hit, 100, groundLayer) && hit.transform != null) {
                        Instantiate(carrotPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                    }
                }
                carrotTexture.color = new Color32(0, 0, 0, 50);
                cabbageTexture.color = new Color32(255, 255, 255, 255);
                appleTexture.color = new Color32(255, 255, 255, 255);
            }

            if (selectedApple) {
                if (Input.GetMouseButtonDown(0)) {
                    if (Physics.Raycast(ray, out hit, 100, groundLayer) && hit.transform != null) {
                        Instantiate(applePrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                    }
                }
                appleTexture.color = new Color32(0, 0, 0, 50);
                carrotTexture.color = new Color32(255, 255, 255, 255);
                cabbageTexture.color = new Color32(255, 255, 255, 255);
            }
        }
    }
}
