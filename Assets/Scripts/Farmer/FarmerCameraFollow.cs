using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerCameraFollow : MonoBehaviour {

    [Header("Mouse Pan Settings")]
    [SerializeField]
    private float panSpeed = 20f;
    [SerializeField]
    private float panBorderThickness = 50f;
    [SerializeField]
    private Vector2 panLimit;

    [Header("ScrollWheel Settings")]
    [SerializeField]
    private float scrollSpeed = 20f;
    [SerializeField]
    private float minY = -10;
    [SerializeField]
    private float maxY = 20;

    // Update is called once per frame
    void Update() {
        Vector3 pos = transform.position;

        if (Input.mousePosition.y >= Screen.height - panBorderThickness) {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= panBorderThickness) {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThickness) {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= panBorderThickness) {
            pos.x -= panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;
    }
}
