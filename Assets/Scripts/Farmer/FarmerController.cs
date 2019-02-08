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

    // Use this for initialization
    private void Start() {
        rb = GetComponent<Rigidbody>();
        anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        float translation = Input.GetAxis("Vertical") * accelerationSpeed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        if (Input.GetKey(KeyCode.W)) {
            anim.SetBool("isWalkingFront", true);
            anim.SetBool("isWalkingBack", false);
            anim.SetBool("isIdle", false);
        }
        else if (Input.GetKey(KeyCode.S)) {
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
