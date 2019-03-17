using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationBeetle : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform camTransform;
    [SerializeField]
    private float distance = 10.0f;
    [SerializeField]
    private float yAngleMin = 0.0f;
    [SerializeField]
    private float yAngleMax = 50.0f;
    [SerializeField]
    private float sensitivityX = 4.0f;
    [SerializeField]
    private float sensitivityY = 1.0f;

    private float currentX = 0.0f;
    private float currentY = 0.0f;

    private void Start() {
        camTransform = transform; 
    }

    private void Update() {
        currentX += GlobalInputManager.CamVerticalBeetle() * sensitivityY * Time.deltaTime;
        currentY += -GlobalInputManager.CamHorizontalBeetle() * sensitivityX * Time.deltaTime;

        currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);
    }

    private void LateUpdate() {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = target.position + rotation * dir;
        camTransform.LookAt(target.position);
    }
}
