using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    [SerializeField]
    private Camera farmerCamera;

    // Always face the camera's position and rotation
    private void Update() {
        transform.LookAt(transform.position + farmerCamera.transform.rotation * Vector3.forward,
            farmerCamera.transform.rotation * Vector3.up);
    }
}