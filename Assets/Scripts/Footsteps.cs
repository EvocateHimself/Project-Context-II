using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {

    private void Step() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Farmer/Footsteps");
    }

    private void Fly() {
        //FMODUnity.RuntimeManager.PlayOneShot("event:/Kever/Take Off");
        
    }
}
