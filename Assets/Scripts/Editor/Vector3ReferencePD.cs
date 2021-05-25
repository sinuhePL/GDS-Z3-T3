using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GDS3
{
    [CustomPropertyDrawer(typeof(Vector3Reference))]
    public class Vector3ReferencePD : PropertyDrawer
    {
        private GUIStyle _popupStyle;
        private string[] _myOptions = { "Constant", "Variable" };

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_popupStyle == null)
            {
                _popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
                _popupStyle.imagePosition = ImagePosition.ImageOnly;
            }
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            bool isConstant = property.FindPropertyRelative("_useConstant").boolValue;
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            Rect myRect = new Rect(position);
            myRect.yMin += _popupStyle.margin.top;
            myRect.width = _popupStyle.fixedWidth + _popupStyle.margin.right;
            position.xMin = myRect.xMax;
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            int result = EditorGUI.Popup(myRect, isConstant ? 0 : 1, _myOptions, _popupStyle);
            isConstant = result == 0;
            property.FindPropertyRelative("_useConstant").boolValue = isConstant;
            EditorGUI.PropertyField(position, isConstant ? property.FindPropertyRelative("_constantValue") : property.FindPropertyRelative("_variable"), GUIContent.none);
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}
