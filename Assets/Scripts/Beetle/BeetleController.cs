using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleController : MonoBehaviour {

    [SerializeField]
    private float rotateSpeed = 5f;

    public float accelerationSpeed = 5f;
    [SerializeField]
    private float ascendSpeed = 5f;

    private float startAscendSpeed;

    Animator anim;
    Rigidbody rb;
    GameManager gameManager;
    BeetleStats beetleStats;

    private void Start () {
        rb = GetComponent<Rigidbody>();
        anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        gameManager = GameManager.instance;
        beetleStats = gameManager.GetComponent<BeetleStats>();

        startAscendSpeed = ascendSpeed;
    }
	
	private void FixedUpdate () {
        if (beetleStats.beetleMovementEnabled) {
            Move();
        }
        else {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isFlying", false);
        }
    }

    private void Move() {
        if (beetleStats.CurrentStamina >= 20f) { accelerationSpeed = beetleStats.CurrentStamina / 8f; }
        else { accelerationSpeed = 3f; }

        if (beetleStats.CurrentFlight > 0) { ascendSpeed = startAscendSpeed; }
        else { ascendSpeed = 0; }

        float translationBeetle = GlobalInputManager.MainHorizontalBeetle() * accelerationSpeed * Time.deltaTime;
        float rotationBeetle = GlobalInputManager.MainVerticalBeetle() * rotateSpeed * Time.deltaTime;
        float ascensionBeetle = GlobalInputManager.RightTriggerBeetle() * ascendSpeed;

        transform.Translate(0, 0, translationBeetle);
        transform.Rotate(0, rotationBeetle, 0);
        //rb.AddRelativeForce(Vector3.up * ascension);
        rb.AddForce(transform.up * ascensionBeetle, ForceMode.Acceleration);

        if (translationBeetle != 0) {
            //anim.speed = translationBeetle * 40;
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isFlying", false);

            if (ascensionBeetle != 0) {
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isFlying", true);
                beetleStats.CurrentFlight -= ascendSpeed * Time.deltaTime;
            }
        } else if (ascensionBeetle != 0) {
            //anim.speed = ascensionBeetle * 5;
            beetleStats.CurrentFlight -= ascendSpeed * Time.deltaTime;
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isFlying", true);
        } 
        
        else {
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);
            anim.SetBool("isFlying", false);
        }
    }
}
