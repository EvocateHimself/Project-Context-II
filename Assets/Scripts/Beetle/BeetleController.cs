using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleController : MonoBehaviour {

    [SerializeField]
    private float rotateSpeed = 5f;
    [SerializeField]
    private float accelerationSpeed = 5f;
    [SerializeField]
    private float ascendSpeed = 5f;

    Rigidbody rb;

	// Use this for initialization
	private void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	private void FixedUpdate () {
        Move();
    }

    private void Move() {
        float translation = Input.GetAxis("Vertical_joy") * accelerationSpeed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal_joy") * rotateSpeed * Time.deltaTime;
        float ascension = Input.GetAxis("Ascend_joy") * ascendSpeed;

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
        //rb.AddRelativeForce(Vector3.up * ascension);
        rb.AddForce(transform.up * ascension, ForceMode.Acceleration);
    }
}
