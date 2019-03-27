using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayTimer : MonoBehaviour {

    [Header("Time until Endstate:")]
    [Unit("seconds")]
    [SerializeField]
    private float secondsInDay = 60f;
    [Unit("days")]
    [SerializeField]
    private float amountOfDays = 3f;
    [SerializeField]
    private TextMeshProUGUI dayTextFarmer, dayTextBeetle;
    [HideInInspector]
    private float currentDays;

    private void Start() {
        InvokeRepeating("CountdownDays", secondsInDay, secondsInDay);
    }

    private void CountdownDays() {
        currentDays += 1f;
    }

    private void Update() {
        if (currentDays == amountOfDays) {
            CancelInvoke("CountdownDays");
            timerEnded();
        }

        dayTextFarmer.text = "Day " + currentDays;
        dayTextBeetle.text = "Day " + currentDays;
    }

    private void timerEnded() {
        // start endscreen
        Debug.Log("finish!");
    }
}
