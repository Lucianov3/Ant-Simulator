using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DayNightCycle))]
public class DayNightCycleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DayNightCycle myTarget = (DayNightCycle)target;
        //myTarget.minutes = EditorGUILayout.IntSlider("Minutes",myTarget.minutes, 0, 59);
        //myTarget.hours = EditorGUILayout.IntSlider("Hours",myTarget.hours, 0, 23);
    }
}
