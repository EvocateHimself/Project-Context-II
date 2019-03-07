using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerController : MonoBehaviour {

    [SerializeField]
    private float rotateSpeed = 5f;
    [SerializeField]
    private float accelerationSpeed = 5f;

    Animator anim;
    Rigidbody rb;
    GameManager gameManager;
    FarmerStats farmerStats;

    private void Start() {        
        rb = GetComponent<Rigidbody>();
        anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        gameManager = GameManager.instance;
        farmerStats = gameManager.GetComponent<FarmerStats>();
    }

    private void FixedUpdate() {
        if (farmerStats.farmerMovementEnabled) {
            Move();
        } else {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalkingFront", false);
            anim.SetBool("isWalkingBack", false);
        }
    }

    private void Move() {
        float translationFarmer = GlobalInputManager.MainHorizontalFarmer() * accelerationSpeed * Time.deltaTime;
        float rotationFarmer = GlobalInputManager.MainVerticalFarmer() * rotateSpeed * Time.deltaTime;

        transform.Translate(0, 0, translationFarmer);
        transform.Rotate(0, rotationFarmer, 0);

        if (translationFarmer > 0) {
            anim.SetBool("isWalkingFront", true);
            anim.SetBool("isWalkingBack", false);
            anim.SetBool("isIdle", false);
        }
        else if (translationFarmer < 0) {
            anim.SetBool("isWalkingFront", false);
            anim.SetBool("isWalkingBack", true);
            anim.SetBool("isIdle", false);
        }
        else {
            anim.SetBool("isWalkingFront", false);
            anim.SetBool("isWalkingBack", false);
            anim.SetBool("isIdle", true);
        }
    }
}
