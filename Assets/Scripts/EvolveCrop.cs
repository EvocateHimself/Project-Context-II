using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolveCrop : MonoBehaviour {

    public int currentPhase = 0;
    bool hasEvolved = false;

    [SerializeField]
    private GameObject[] phasePrefabs;

    private float evolutionDelayOne, evolutionDelayTwo;
    [SerializeField]
    private float minEvolutionDelay, maxEvolutionDelay;

    private void Start() {
        Random.InitState((int)System.DateTime.Now.Ticks); // Makes things more random
    }

    private void Update() {
        evolutionDelayOne = Random.Range(minEvolutionDelay, maxEvolutionDelay);
        evolutionDelayTwo = Random.Range(minEvolutionDelay, maxEvolutionDelay);

        if (!hasEvolved) {
            StartCoroutine(Evolve());
        }
    }

    private IEnumerator Evolve() {
        hasEvolved = true;

        // If crops have three evolution phases
        if (phasePrefabs.Length == 3) {
            phasePrefabs[0].SetActive(true);
            phasePrefabs[1].SetActive(false);
            phasePrefabs[2].SetActive(false);
            yield return new WaitForSeconds(evolutionDelayOne);
            currentPhase = 1;
            phasePrefabs[0].SetActive(false);
            phasePrefabs[1].SetActive(true);
            phasePrefabs[2].SetActive(false);
            yield return new WaitForSeconds(evolutionDelayTwo);
            currentPhase = 2;
            phasePrefabs[0].SetActive(false);
            phasePrefabs[1].SetActive(false);
            phasePrefabs[2].SetActive(true);
        }
        // If crops have two evolution phases
        else {
            phasePrefabs[0].SetActive(true);
            phasePrefabs[1].SetActive(false);
            yield return new WaitForSeconds(evolutionDelayOne);
            currentPhase = 1;
            phasePrefabs[0].SetActive(false);
            phasePrefabs[1].SetActive(true);
        }
    }

}
