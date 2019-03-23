using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true)]
public class UnitAttribute : PropertyAttribute {

    public string label;
    public GUIStyle labelStyle;
    public float width;

    public UnitAttribute(string label) {
        this.label = label;
        labelStyle = GUI.skin.GetStyle("miniLabel");
        width = labelStyle.CalcSize(new GUIContent(label)).x;
    }
}
