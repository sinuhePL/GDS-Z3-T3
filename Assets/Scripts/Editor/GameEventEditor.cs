using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GDS3
{
    [CustomEditor(typeof(GameEvent), true)]
    public class GameEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
            if (GUILayout.Button("Raise"))
            {
                ((GameEvent)target).Raise();
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}
