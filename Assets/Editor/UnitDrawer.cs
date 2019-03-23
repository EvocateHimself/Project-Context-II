using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(UnitAttribute))]
public class UnitDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        UnitAttribute labelAttribute = attribute as UnitAttribute;

        position.Set(position.x, position.y, position.width - labelAttribute.width - 2, position.height);

        EditorGUI.PropertyField(position, property, label);
        float xPos = position.x + position.width + 2f;
        position.Set(xPos, position.y, labelAttribute.width, position.height);
        GUI.Label(position, labelAttribute.label, labelAttribute.labelStyle);
    }
}
