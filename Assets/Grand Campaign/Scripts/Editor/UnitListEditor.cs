using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(UnitList))]
[CanEditMultipleObjects]
public class UnitListEditor : Editor {
    UnitList _Data;
    int _ListSize;
    public SerializedProperty Keys_Prop;
    public SerializedProperty Values_Prop;
    SerializedObject _GetTarget;
    List<bool> _Foldouts;

    private void OnEnable()
    {
        _Data = (UnitList)target;
        _GetTarget = new SerializedObject(_Data);
        Keys_Prop = _GetTarget.FindProperty("Keys");
        Values_Prop = _GetTarget.FindProperty("Values");
        _Foldouts = new List<bool>();
    }

    public override void OnInspectorGUI()
    {
        _GetTarget.Update();

        _ListSize = Keys_Prop.arraySize;
        _ListSize = EditorGUILayout.IntField(new GUIContent("Unit List Size"), _ListSize);

        //Check if inspector and actual sizes match
        if (_ListSize != Keys_Prop.arraySize)
        {
            while (_ListSize > Keys_Prop.arraySize)
            {
                Keys_Prop.InsertArrayElementAtIndex(Keys_Prop.arraySize);
            }
            while (_ListSize < Keys_Prop.arraySize)
            {
                Keys_Prop.DeleteArrayElementAtIndex(Keys_Prop.arraySize - 1);
            }
        }

        if (_ListSize != Values_Prop.arraySize)
        {
            while (_ListSize > Values_Prop.arraySize)
            {
                Values_Prop.InsertArrayElementAtIndex(Values_Prop.arraySize);
            }
            while (_ListSize < Values_Prop.arraySize)
            {
                Values_Prop.DeleteArrayElementAtIndex(Values_Prop.arraySize - 1);
            }
        }

        if (_ListSize != _Foldouts.Count)
        {
            while (_ListSize > _Foldouts.Count)
            {
                _Foldouts.Add(false);
            }
            while (_ListSize < _Foldouts.Count)
            {
                _Foldouts.RemoveAt(_Foldouts.Count - 1);
            }
        }

        EditorGUI.indentLevel++;

        for(int i = 0; i < _ListSize; i++)
        {
            _Foldouts[i] = EditorGUILayout.Foldout(_Foldouts[i], "Element " + i.ToString());
            if(_Foldouts[i])
            {
                SerializedProperty key = Keys_Prop.GetArrayElementAtIndex(i);
                SerializedProperty value = Values_Prop.GetArrayElementAtIndex(i);

                EditorGUILayout.PropertyField(key, new GUIContent("Unit Name"));
                EditorGUILayout.PropertyField(value, new GUIContent("Amount"));
            }
        }

        EditorGUI.indentLevel--;
        _GetTarget.ApplyModifiedProperties();
    }
}
