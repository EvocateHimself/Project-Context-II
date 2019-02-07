using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectCrop : MonoBehaviour {

    public GameObject pesticidePrefab;
    [SerializeField]
    private float spawnHeight = 3f;
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

    void Update() {

    }

    private void Infect() {
        CancelInvoke(); // Stop the timer (I don't think you need it, try without)
        crops = GameObject.FindGameObjectsWithTag("Interactable");
        index = Random.Range(0, crops.Length);

        Vector3 pesticidePos = pesticidePrefab.transform.position;
        pesticidePos.y = spawnHeight;
        pesticidePos = new Vector3(crops[index].transform.position.x, pesticidePos.y, crops[index].transform.position.z);

        // If parent does not have a pesticide already, instantiate one
        if (crops[index].transform.childCount <= 0) {
            GameObject GO = Instantiate(pesticidePrefab, pesticidePos, pesticidePrefab.transform.rotation);
            GO.transform.parent = crops[index].transform;
            farmerStats.pesticideAmount += 1;
        }
        
        // Start a new timer for the next random spawn
        Invoke("Infect", Random.Range(minTime, maxTime));

        if (Input.GetMouseButtonDown(1)) {
            pesticidePrefab.transform.position = new Vector3(crops[index].transform.position.x, pesticidePos.y, crops[index].transform.position.z);
            farmerStats.pesticideAmount += 1;
        }
    }

}
