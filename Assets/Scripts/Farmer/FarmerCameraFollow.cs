using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerCameraFollow : MonoBehaviour {

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

    [Header("ScrollWheel Settings")]
    [SerializeField]
    private float scrollSpeed = 20f;
    [SerializeField]
    private float minY = -10;
    [SerializeField]
    private float maxY = 20;

    private void FixedUpdate() {
        Vector3 localVelocity = target.InverseTransformDirection(target.GetComponent<Rigidbody>().velocity);
        if (localVelocity.z < -0.5f) {
            rotationVector = target.eulerAngles.y + 100;
        } else {
            rotationVector = target.eulerAngles.y;
        }

        float acceleration = target.GetComponent<Rigidbody>().velocity.magnitude;
    }

    private void LateUpdate() {
        float wantedAngle = rotationVector;
        float myAngle = transform.eulerAngles.y;

        myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0, myAngle, 0);
        transform.position -= currentRotation * Vector3.forward * distance;

        Vector3 pos = transform.position;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, -minY, maxY);
        transform.position = pos;

        transform.LookAt(target);
    }

    private void RotateCamera() {
        float cameraRotation = Input.GetAxis("Camera");
        transform.Rotate(0, cameraRotation, 0);
    }
}
