using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GDS3
{
    [CustomEditor(typeof(AudioEvent), true)]
    public class AudioEventEditor : Editor
    {
        private AudioSource _previewer;

        public void OnEnable()
        {
            _previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
        }

        public void OnDisable()
        {
            DestroyImmediate(_previewer.gameObject);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
            if (GUILayout.Button("Preview"))
            {
                ((AudioEvent)target).Play(_previewer, 0.75f);
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}
