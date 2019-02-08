using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectCrop : MonoBehaviour {

    public GameObject plaguePrefab;
    [SerializeField]
    private float spawnHeight = 3f;
    [Header("Random Spawn Timer")]
    [SerializeField]
    private int minTime = 5;
    [SerializeField]
    private int maxTime = 15;
    public GameObject[] crops;
    public LayerMask cropLayer;

    private int index;

    GameManager gameManager;
    FarmerStats farmerStats;
    CropPlacer cropPlacer;

    private void Start() {
        gameManager = GameManager.instance;
        cropPlacer = gameManager.GetComponent<CropPlacer>();
        farmerStats = gameManager.GetComponent<FarmerStats>();
        spawnHeight = transform.position.y;

        Invoke("Infect", 2);
    }

    private void Update() {
        crops = GameObject.FindGameObjectsWithTag("Interactable");
        index = Random.Range(0, crops.Length);
    }

    private void Infect() {
        // Check if there are crops in the array
        if (crops.Length > 0) {
            // If parent does not have a plague already, instantiate one
            if (crops[index].transform.childCount <= 1) {
                Vector3 plaguePos = plaguePrefab.transform.position;
                plaguePos.y = spawnHeight;
                plaguePos = new Vector3(crops[index].transform.position.x, plaguePos.y, crops[index].transform.position.z);

                GameObject GO = Instantiate(plaguePrefab, plaguePos, plaguePrefab.transform.rotation);
                GO.transform.parent = crops[index].transform;
                farmerStats.plagueAmount += 1;
            }
        }
        // Start a new timer for the next random spawn
        Invoke("Infect", Random.Range(minTime, maxTime));
    }
}
