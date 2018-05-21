using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(RotationMap))]
public class RotationMapWindow : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        RotationMap map = (RotationMap)target;

        if (map.backUpSize != map.mapSize)
        {
            map.pointArray = new Boolean2D(map.mapSize);
            map.backUpSize = map.mapSize;
        }

        if (map.pointArray == null) return;

        GUILayout.BeginVertical();

        for (int i = 0; i < map.mapSize; i++)
        {
            GUILayout.BeginHorizontal();

            for (int j = 0; j < map.mapSize; j++)
            {
                bool value;
                if (map.pointArray.GetElement(i, j, out value))
                    map.pointArray.SetElement(i, j, EditorGUILayout.Toggle(value, GUILayout.MaxWidth(15)));
            }

            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();
    }
}
