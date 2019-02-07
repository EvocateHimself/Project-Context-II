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
    [SerializeField]
    private float placeRadius = 100f;

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
    [SerializeField]
    private float carrotHealthImpact;
    [SerializeField]
    private float carrotGrowCost;
    [SerializeField]
    private float carrotSellCost;

    [Header("Apple")]
    [SerializeField]
    private Image appleTexture;
    [SerializeField]
    private GameObject applePrefab;
    [SerializeField]
    private float appleHealthImpact;
    [SerializeField]
    private float appleGrowCost;
    [SerializeField]
    private float appleSellCost;

    [Header("Poison")]
    [SerializeField]
    private Image poisonTexture;
    public GameObject poisonPrefab;
    [SerializeField]
    private float poisonDuration = 10f;
    [SerializeField]
    private float poisonGrowCost;

    bool selectedCabbage = false;
    bool selectedCarrot = false;
    bool selectedApple = false;
    bool selectedPoison = false;

    GameObject poisonObj;
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
            selectedPoison = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            selectedCabbage = false;
            selectedCarrot = true;
            selectedApple = false;
            selectedPoison = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            selectedCabbage = false;
            selectedCarrot = false;
            selectedApple = true;
            selectedPoison = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            selectedCabbage = false;
            selectedCarrot = false;
            selectedApple = false;
            selectedPoison = true;
        }

        PlaceCrop();
    }

    public void PlaceCrop() {
        EventSystem.current.SetSelectedGameObject(null); // Disable button hover properly * requires using UnityEngine.EventSystems;
        if (!EventSystem.current.IsPointerOverGameObject()) { // Prevents raycast from passing through UI * requires using UnityEngine.EventSystems;
            Ray ray = farmerCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (selectedCabbage) {
                if (Input.GetMouseButtonDown(0)) {
                    if (Physics.Raycast(ray, out hit, placeRadius, groundLayer) && hit.transform != null) {
                        if (farmerStats.CurrentMoney >= cabbageGrowCost) {
                            Instantiate(cabbagePrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                            farmerStats.CurrentMoney -= cabbageGrowCost;
                            farmerStats.CurrentHealth += cabbageHealthImpact;
                        }
                    }
                }
                poisonTexture.color = new Color32(255, 255, 255, 255);
                cabbageTexture.color = new Color32(0, 0, 0, 50);
                carrotTexture.color = new Color32(255, 255, 255, 255);
                appleTexture.color = new Color32(255, 255, 255, 255);
            }

            if (selectedCarrot) {
                if (Input.GetMouseButtonDown(0)) {
                    if (Physics.Raycast(ray, out hit, placeRadius, groundLayer) && hit.transform != null) {
                        if (farmerStats.CurrentMoney >= carrotGrowCost) {
                            Instantiate(carrotPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                            farmerStats.CurrentMoney -= carrotGrowCost;
                            farmerStats.CurrentHealth += carrotHealthImpact;
                        }
                    }
                }
                poisonTexture.color = new Color32(255, 255, 255, 255);
                carrotTexture.color = new Color32(0, 0, 0, 50);
                cabbageTexture.color = new Color32(255, 255, 255, 255);
                appleTexture.color = new Color32(255, 255, 255, 255);
            }

            if (selectedApple) {
                if (Input.GetMouseButtonDown(0)) {
                    if (Physics.Raycast(ray, out hit, placeRadius, groundLayer) && hit.transform != null) {
                        if (farmerStats.CurrentMoney >= appleGrowCost) {
                            Instantiate(applePrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                            farmerStats.CurrentMoney -= appleGrowCost;
                            farmerStats.CurrentHealth += appleHealthImpact;
                        }
                    }
                }
                poisonTexture.color = new Color32(255, 255, 255, 255);
                appleTexture.color = new Color32(0, 0, 0, 50);
                carrotTexture.color = new Color32(255, 255, 255, 255);
                cabbageTexture.color = new Color32(255, 255, 255, 255);
            }

            if (selectedPoison) {
                if (Input.GetMouseButtonDown(0)) {
                    if (Physics.Raycast(ray, out hit, placeRadius, groundLayer) && hit.transform != null) {
                        if (farmerStats.CurrentMoney >= poisonGrowCost) {
                            poisonObj = (GameObject)Instantiate(poisonPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                            Destroy(poisonObj, poisonDuration);
                            farmerStats.CurrentMoney -= poisonGrowCost;
                        }
                    }
                }
                poisonTexture.color = new Color32(0, 0, 100, 50);
                appleTexture.color = new Color32(255, 255, 255, 255);
                carrotTexture.color = new Color32(255, 255, 255, 255);
                cabbageTexture.color = new Color32(255, 255, 255, 255);
            }
        }
    }

    // suspend execution for waitTime seconds
    private IEnumerator SpawnPoison() {
        Ray ray = farmerCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, placeRadius, groundLayer) && hit.transform != null) {
            poisonObj = (GameObject) Instantiate(poisonPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
        }

        yield return new WaitForSeconds(poisonDuration);
        Destroy(poisonObj);
    }
}
