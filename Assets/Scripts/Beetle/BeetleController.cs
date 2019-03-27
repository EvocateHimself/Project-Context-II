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

    private FMOD.Studio.EventInstance KeverFlight;

    bool playingSound = false;
    bool isFlying = false;

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

    private void Update() {
        if (!playingSound) {
            if (GlobalInputManager.RightTriggerBeetle() != 0 && isFlying) {
                KeverFlight = FMODUnity.RuntimeManager.CreateInstance("event:/Kever/Flight");
                KeverFlight.start();

                playingSound = true;
            }
        }

        if (GlobalInputManager.RightTriggerBeetle() == 0 && !isFlying) {
            playingSound = false;
            KeverFlight.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
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
        if (beetleStats.CurrentStamina >= 20f) {
            accelerationSpeed = beetleStats.CurrentStamina / 8f;

            if (beetleStats.isWalkingInPesticide) {
                accelerationSpeed = beetleStats.beetleWalkInPesticideSpeed;
            }
        }

        else {
            accelerationSpeed = 3f;

            if (beetleStats.isWalkingInPesticide) {
                accelerationSpeed = beetleStats.beetleWalkInPesticideSpeed;
            }
        }

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
            isFlying = false;
            anim.speed = translationBeetle * 20;
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isFlying", false);

            if (translationBeetle < 0) {
                anim.speed = -translationBeetle * 20;
                isFlying = false;
            }

            if (ascensionBeetle != 0) {
                isFlying = true;
                anim.speed = ascensionBeetle * 2;
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isFlying", true);
                beetleStats.CurrentFlight -= ascendSpeed * Time.deltaTime;
            }
        }

        else if (ascensionBeetle != 0) {
            isFlying = true;
            anim.speed = ascensionBeetle * 2;
            beetleStats.CurrentFlight -= ascendSpeed * Time.deltaTime;
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isFlying", true);
        }

        else {
            isFlying = false;
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);
            anim.SetBool("isFlying", false);
        }
    }
}
