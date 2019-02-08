using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour {

    [SerializeField]
    private List<Transform> ignoreObject;

    void Awake() {
        for (int i = 0; i < ignoreObject.Count; i++) {
            Physics.IgnoreCollision(ignoreObject[i].GetComponent<Collider>(), GetComponent<Collider>());

            foreach (Transform child in ignoreObject[i].transform) {
                if (null == child)
                    continue;
                Physics.IgnoreCollision(child.GetComponent<Collider>(), GetComponent<Collider>());
            }
        }
    }
}