using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItemPlacer : MonoBehaviour {

    private PlantGrid grid;

    public Camera farmerCam;
    public LayerMask groundLayer;
    public float placeRadius = 100f;

    [SerializeField]
    private GameObject cabbagePrefab;

    // Start is called before the first frame update
    private void Awake() {
        grid = FindObjectOfType<PlantGrid>();
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = farmerCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, placeRadius, groundLayer)) {
                var finalPosition = grid.GetNearestPointOnGrid(hit.point);
                Instantiate(cabbagePrefab, finalPosition, Quaternion.identity);
            }
        }
    }

    /*
    private void PlaceItemNear(Vector3 clickPoint) {
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        Instantiate(cabbagePrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
    }
    */
}
