using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour {

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

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, -minY, maxY);

        transform.position = pos;
    }
}
