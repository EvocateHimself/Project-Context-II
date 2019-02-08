using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CropPlacer : MonoBehaviour {

    public Camera farmerCam;
    public LayerMask groundLayer;
    public float placeRadius = 100f;

    [SerializeField]
    private AudioSource interactSound;
    [SerializeField]
    private AudioSource poisonSound;
    public float infectedSellCostDivider = 2;

    [Header("Cabbage")]
    [SerializeField]
    private Image cabbageTexture;
    [SerializeField]
    private Sprite cabbageTextureSelected;
    [SerializeField]
    private GameObject cabbagePrefab;
    public float cabbageHealthImpact;
    public float cabbageGrowCost;
    public float cabbageSellCost;

    [Header("Carrot")]
    [SerializeField]
    private Image carrotTexture;
    [SerializeField]
    private Sprite carrotTextureSelected;
    [SerializeField]
    private GameObject carrotPrefab;
    public float carrotHealthImpact;
    public float carrotGrowCost;
    public float carrotSellCost;

    [Header("Apple")]
    [SerializeField]
    private Image appleTexture;
    [SerializeField]
    private Sprite appleTextureSelected;
    [SerializeField]
    private GameObject applePrefab;
    public float appleHealthImpact;
    public float appleGrowCost;
    public float appleSellCost;

    [Header("Poison")]
    [SerializeField]
    private Image poisonTexture;
    [SerializeField]
    private Sprite poisonTextureSelected;
    public GameObject poisonPrefab;
    public float poisonDuration = 10f;
    public float poisonGrowCost;

    bool selectedCabbage = false;
    bool selectedCarrot = false;
    bool selectedApple = false;
    bool selectedPoison = false;

    private Sprite defaultCabbageTexture, defaultCarrotTexture, defaultAppleTexture, defaultPoisonTexture;

    GameObject poisonObj;
    GameManager gameManager;
    FarmerStats farmerStats;

    private void Start() {
        gameManager = GameManager.instance;
        farmerStats = gameManager.GetComponent<FarmerStats>();
        defaultCabbageTexture = cabbageTexture.sprite;
        defaultCarrotTexture = carrotTexture.sprite;
        defaultAppleTexture = appleTexture.sprite;
        defaultPoisonTexture = poisonTexture.sprite;
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
                            interactSound.Play();
                        }
                    }
                }
                cabbageTexture.sprite = cabbageTextureSelected;
                carrotTexture.sprite = defaultCarrotTexture;
                appleTexture.sprite = defaultAppleTexture;
                poisonTexture.sprite = defaultPoisonTexture;
            }

            if (selectedCarrot) {
                if (Input.GetMouseButtonDown(0)) {
                    if (Physics.Raycast(ray, out hit, placeRadius, groundLayer) && hit.transform != null) {
                        if (farmerStats.CurrentMoney >= carrotGrowCost) {
                            Instantiate(carrotPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                            farmerStats.CurrentMoney -= carrotGrowCost;
                            farmerStats.CurrentHealth += carrotHealthImpact;
                            interactSound.Play();
                        }
                    }
                }
                cabbageTexture.sprite = defaultCabbageTexture;
                carrotTexture.sprite = carrotTextureSelected;
                appleTexture.sprite = defaultAppleTexture;
                poisonTexture.sprite = defaultPoisonTexture;
            }

            if (selectedApple) {
                if (Input.GetMouseButtonDown(0)) {
                    if (Physics.Raycast(ray, out hit, placeRadius, groundLayer) && hit.transform != null) {
                        if (farmerStats.CurrentMoney >= appleGrowCost) {
                            Instantiate(applePrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                            farmerStats.CurrentMoney -= appleGrowCost;
                            farmerStats.CurrentHealth += appleHealthImpact;
                            interactSound.Play();
                        }
                    }
                }
                cabbageTexture.sprite = defaultCabbageTexture;
                carrotTexture.sprite = defaultCarrotTexture;
                appleTexture.sprite = appleTextureSelected;
                poisonTexture.sprite = defaultPoisonTexture;
            }

            if (selectedPoison) {
                if (Input.GetMouseButtonDown(0)) {
                    if (Physics.Raycast(ray, out hit, placeRadius, groundLayer) && hit.transform != null) {
                        if (farmerStats.CurrentMoney >= poisonGrowCost) {
                            poisonObj = (GameObject)Instantiate(poisonPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                            Destroy(poisonObj, poisonDuration);
                            farmerStats.CurrentMoney -= poisonGrowCost;
                            poisonSound.Play();
                        }
                    }
                }
                cabbageTexture.sprite = defaultCabbageTexture;
                carrotTexture.sprite = defaultCarrotTexture;
                appleTexture.sprite = defaultAppleTexture;
                poisonTexture.sprite = poisonTextureSelected;
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
