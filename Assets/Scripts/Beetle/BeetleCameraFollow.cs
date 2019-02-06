using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleCameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float height;
    [SerializeField]
    private float rotationDamping;
    [SerializeField]
    private float heightDamping;
    [SerializeField]
    private float zoomRatio;

    private float rotationVector;

    private void FixedUpdate() {
        Vector3 localVelocity = target.InverseTransformDirection(target.GetComponent<Rigidbody>().velocity);
        if(localVelocity.z < -0.5f) {
            rotationVector = target.eulerAngles.y + 100;
        }
        else {
            rotationVector = target.eulerAngles.y;
        }

        float acceleration = target.GetComponent<Rigidbody>().velocity.magnitude;

    }

    private void LateUpdate() {
        float wantedAngle = rotationVector;
        float wantedHeight = target.position.y + height;
        float myAngle = transform.eulerAngles.y;
        float myHeight = transform.position.y;

        myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping * Time.deltaTime);
        myHeight = Mathf.LerpAngle(myHeight, wantedHeight, heightDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0, myAngle, 0);
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        Vector3 temp = transform.position;
        temp.y = myHeight;
        transform.position = temp;

        transform.LookAt(target);
    }

    private void RotateCamera() {
        float cameraRotation = Input.GetAxis("Camera");
        transform.Rotate(0, cameraRotation, 0);
    }
}
