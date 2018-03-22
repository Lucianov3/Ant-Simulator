using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(KI_Rigina_formica))]
public class Rigina_formicaeditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        KI_Rigina_formica myscript = (KI_Rigina_formica)target;
        if (GUILayout.Button("Bumsen"))
        {
            myscript.SpawnLarva();
        }
    }
}