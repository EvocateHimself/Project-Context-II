using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class GlobalInputManager : MonoBehaviour {

    // Farmer input mapping

    public static float MainHorizontalFarmer() {
        var farmer = InputManager.Devices[0];
        float r = 0.0f;
        r += farmer.LeftStick.Y;
        r += Input.GetAxis("K_MainHorizontalFarmer");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float MainVerticalFarmer() {
        var farmer = InputManager.Devices[0];
        float r = 0.0f;
        r += farmer.LeftStick.X;
        r += Input.GetAxis("K_MainVerticalFarmer");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float CamHorizontalFarmer() {
        var farmer = InputManager.Devices[0];
        float r = 0.0f;
        r += farmer.RightStick.Y;
        //r += Input.GetAxis("Mouse Y");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float CamVerticalFarmer() {
        var farmer = InputManager.Devices[0];
        float r = 0.0f;
        r += farmer.RightStick.X;
        //r += Input.GetAxis("Mouse X");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float DPADHorizontalFarmer() {
        var farmer = InputManager.Devices[0];
        float r = 0.0f;
        r += farmer.Direction.X;
        r += Input.GetAxis("K_DPADHorizontalFarmer");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static Vector3 MainJoystickFarmer() {
        return new Vector3(MainHorizontalFarmer(), 0, MainVerticalFarmer());
    }

    public static bool TriangleButtonFarmer() {
        var farmer = InputManager.Devices[0];
        return farmer.Action4;
        //return Input.GetButtonDown("Triangle");
    }

    public static bool CrossButtonFarmer() {
        var farmer = InputManager.Devices[0];
        return farmer.Action1;
    }

    public static bool CircleButtonFarmer() {
        var farmer = InputManager.Devices[0];
        return farmer.Action2.WasPressed;
    }

    // Beetle input mapping

    public static float MainHorizontalBeetle() {
        var beetle = InputManager.Devices[1];
        float r = 0.0f;
        r += beetle.LeftStick.Y;
        r += Input.GetAxis("K_MainHorizontalBeetle");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float MainVerticalBeetle() {
        var beetle = InputManager.Devices[1];
        float r = 0.0f;
        r += beetle.LeftStick.X;
        r += Input.GetAxis("K_MainVerticalBeetle");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float CamHorizontalBeetle() {
        var beetle = InputManager.Devices[1];
        float r = 0.0f;
        r += beetle.RightStick.Y;
        //r += Input.GetAxis("Mouse Y");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float CamVerticalBeetle() {
        var beetle = InputManager.Devices[1];
        float r = 0.0f;
        r += beetle.RightStick.X;
        //r += Input.GetAxis("Mouse X");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float RightTriggerBeetle() {
        var beetle = InputManager.Devices[1];
        float r = 0.0f;
        r += beetle.RightTrigger;
        r += Input.GetAxis("K_AscensionBeetle");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static bool TriangleButtonBeetle() {
        var beetle = InputManager.Devices[1];
        return beetle.Action4;
        //return Input.GetButtonDown("Triangle");
    }

    public static Vector3 MainJoystickBeetle() {
        return new Vector3(MainHorizontalBeetle(), 0, MainVerticalBeetle());
    }
}
