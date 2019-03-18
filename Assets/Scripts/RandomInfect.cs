using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomInfect : MonoBehaviour {

    public List<GameObject> crops = new List<GameObject>();
    //public GameObject[] crops;
    [SerializeField]
    private GameObject plaguePrefab;
    [SerializeField]
    private int startDelay;
    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    private float spawnMinTime;
    [SerializeField]
    private float spawnMaxTime;
    [SerializeField]
    private bool stop;

    int randomCrop;

    GameManager gameManager;
    FarmerStats farmerStats;
    CropPlacement cropPlacer;

    private void Start() {
        gameManager = GameManager.instance;
        cropPlacer = gameManager.GetComponent<CropPlacement>();
        farmerStats = gameManager.GetComponent<FarmerStats>();
        Random.InitState((int)System.DateTime.Now.Ticks); // Makes things more random
        StartCoroutine(PlagueSpawner());
    }

    private void Update() {
        spawnDelay = Random.Range(spawnMinTime, spawnMaxTime);

        // TO DO: Find replacement for findobjectswithtag
        //crops = GameObject.FindGameObjectsWithTag("Interactable");
        randomCrop = Random.Range(0, crops.Count);
    }

    private IEnumerator PlagueSpawner() {
        yield return new WaitForSeconds(startDelay);

        while (!stop) {
            if (crops.Count > 0 && !crops[randomCrop].transform.Find("Plague")) {
                Vector3 plaguePos = new Vector3(crops[randomCrop].transform.position.x, 1, crops[randomCrop].transform.position.z);

                GameObject plague = Instantiate(plaguePrefab, plaguePos, plaguePrefab.transform.rotation);
                plague.transform.parent = crops[randomCrop].transform;
                plague.name = "Plague";
                farmerStats.plagueAmount += 1;
                //crops[randomCrop].tag = "Infected";
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
