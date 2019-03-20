using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerController : MonoBehaviour {

    [SerializeField]
    private float rotateSpeed = 5f;
    [SerializeField]
    private float accelerationSpeed = 5f;

    Rigidbody rb;
    GameManager gameManager;
    FarmerStats farmerStats;

    private void Start() {        
        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.instance;
        farmerStats = gameManager.GetComponent<FarmerStats>();
    }

    private void FixedUpdate() {
        if (farmerStats.farmerMovementEnabled) {
            Move();
        } else {
            farmerStats.farmerAnim.SetBool("isIdle", true);
            farmerStats.farmerAnim.SetBool("isWalkingFront", false);
            farmerStats.farmerAnim.SetBool("isWalkingBack", false);
            farmerStats.farmerAnim.SetBool("isPlanting", false);
        }
    }

    private void Move() {
        float translationFarmer = GlobalInputManager.MainHorizontalFarmer() * accelerationSpeed * Time.deltaTime;
        float rotationFarmer = GlobalInputManager.MainVerticalFarmer() * rotateSpeed * Time.deltaTime;

        transform.Translate(0, 0, translationFarmer);
        transform.Rotate(0, rotationFarmer, 0);

        if (translationFarmer > 0) {
            farmerStats.farmerAnim.SetBool("isWalkingFront", true);
            farmerStats.farmerAnim.SetBool("isWalkingBack", false);
            farmerStats.farmerAnim.SetBool("isIdle", false);
            farmerStats.farmerAnim.SetBool("isPlanting", false);
        }
        else if (translationFarmer < 0) {
            farmerStats.farmerAnim.SetBool("isWalkingFront", false);
            farmerStats.farmerAnim.SetBool("isWalkingBack", true);
            farmerStats.farmerAnim.SetBool("isIdle", false);
            farmerStats.farmerAnim.SetBool("isPlanting", false);
        }
        else {
            farmerStats.farmerAnim.SetBool("isWalkingFront", false);
            farmerStats.farmerAnim.SetBool("isWalkingBack", false);
            farmerStats.farmerAnim.SetBool("isIdle", true);
            farmerStats.farmerAnim.SetBool("isPlanting", false);
        }
    }
}
