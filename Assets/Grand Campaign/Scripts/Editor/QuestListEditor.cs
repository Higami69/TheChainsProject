using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(QuestList))]
[CanEditMultipleObjects]
public class QuestListEditor : Editor {
    QuestList list;
    int listSize;
    public SerializedProperty List_Prop;
    SerializedObject GetTarget;
    List<bool> QuestFoldouts;
    List<int> ObjectiveListSizes;
    List<List<bool>> ObjectiveListFoldouts;

    private void OnEnable()
    {
        list = (QuestList)target;
        GetTarget = new SerializedObject(list);
        List_Prop = GetTarget.FindProperty("Quests");
        QuestFoldouts = new List<bool>();
        ObjectiveListFoldouts = new List<List<bool>>();
        ObjectiveListSizes = new List<int>();
    }

    public override void OnInspectorGUI()
    {
        GetTarget.Update();

        listSize = List_Prop.arraySize;
        listSize = EditorGUILayout.IntField(new GUIContent("Quest List Size"), listSize);

        //Check if inspector and actual sizes match
        if (listSize != List_Prop.arraySize)
        {
            while (listSize > List_Prop.arraySize)
            {
                List_Prop.InsertArrayElementAtIndex(List_Prop.arraySize);
            }
            while (listSize < List_Prop.arraySize)
            {
                List_Prop.DeleteArrayElementAtIndex(List_Prop.arraySize - 1);
            }
        }

        if(listSize != QuestFoldouts.Count)
        {
            while(listSize > QuestFoldouts.Count)
            {
                QuestFoldouts.Add(false);
            }
            while(listSize < QuestFoldouts.Count)
            {
                QuestFoldouts.RemoveAt(QuestFoldouts.Count - 1);
            }
        }

        if(listSize != ObjectiveListSizes.Count)
        {
            while(listSize > ObjectiveListSizes.Count)
            {
                ObjectiveListSizes.Add(0);
            }
            while(listSize < ObjectiveListSizes.Count)
            {
                ObjectiveListSizes.RemoveAt(ObjectiveListSizes.Count - 1);
            }
        }

        if(listSize != ObjectiveListFoldouts.Count)
        {
            while(listSize > ObjectiveListFoldouts.Count)
            {
                ObjectiveListFoldouts.Add(new List<bool>());
            }
            while(listSize < ObjectiveListFoldouts.Count)
            {
                ObjectiveListFoldouts.RemoveAt(ObjectiveListFoldouts.Count - 1);
            }
        }

        EditorGUI.indentLevel++;
        for(int i = 0; i < List_Prop.arraySize; i++)
        {
            QuestFoldouts[i] = EditorGUILayout.Foldout(QuestFoldouts[i], new GUIContent("Element " + i.ToString()));
            if(QuestFoldouts[i])
            {
                SerializedProperty quest_Prop = List_Prop.GetArrayElementAtIndex(i);
                SerializedProperty title_Prop = quest_Prop.FindPropertyRelative("Title");
                SerializedProperty desc_Prop = quest_Prop.FindPropertyRelative("Description");
                SerializedProperty objList_Prop = quest_Prop.FindPropertyRelative("Objectives").FindPropertyRelative("Objectives");

                EditorGUILayout.PropertyField(title_Prop, new GUIContent("Title"));
                EditorGUILayout.PropertyField(desc_Prop, new GUIContent("Description"));

                ObjectiveListSizes[i] = objList_Prop.arraySize;
                ObjectiveListSizes[i] = EditorGUILayout.IntField(new GUIContent("Objective List Size"), ObjectiveListSizes[i]);

                //Check if inspector and actual size match
                if (ObjectiveListSizes[i] != objList_Prop.arraySize)
                {
                    while (ObjectiveListSizes[i] > objList_Prop.arraySize)
                    {
                        objList_Prop.InsertArrayElementAtIndex(objList_Prop.arraySize);
                    }
                    while (ObjectiveListSizes[i] < objList_Prop.arraySize)
                    {
                        objList_Prop.DeleteArrayElementAtIndex(objList_Prop.arraySize - 1);
                    }
                }

                if(ObjectiveListSizes[i] != ObjectiveListFoldouts[i].Count)
                {
                    while(ObjectiveListSizes[i] > ObjectiveListFoldouts[i].Count)
                    {
                        ObjectiveListFoldouts[i].Add(false);
                    }
                    while(ObjectiveListSizes[i] < ObjectiveListFoldouts[i].Count)
                    {
                        ObjectiveListFoldouts[i].RemoveAt(ObjectiveListFoldouts[i].Count - 1);
                    }
                }

                EditorGUI.indentLevel++;
                for (int j = 0; j < ObjectiveListSizes[i]; j++)
                {
                    ObjectiveListFoldouts[i][j] = EditorGUILayout.Foldout(ObjectiveListFoldouts[i][j], "Element " + j.ToString());
                    if(ObjectiveListFoldouts[i][j])
                    {
                        SerializedProperty objective_Prop = objList_Prop.GetArrayElementAtIndex(j);
                        SerializedProperty type_Prop = objective_Prop.FindPropertyRelative("Type");
                        SerializedProperty target_Prop = objective_Prop.FindPropertyRelative("Target");
                        SerializedProperty buildingID_Prop = objective_Prop.FindPropertyRelative("BuildingID");
                        SerializedProperty unitName_Prop = objective_Prop.FindPropertyRelative("UnitName");
                        SerializedProperty unitAmount_Prop = objective_Prop.FindPropertyRelative("UnitAmount");

                        EditorGUILayout.PropertyField(type_Prop, new GUIContent("Objective Type"));
                        switch((ObjectiveType)type_Prop.enumValueIndex)
                        {
                            case ObjectiveType.BUILDING:
                                EditorGUILayout.PropertyField(target_Prop, new GUIContent("Target Town"));
                                EditorGUILayout.PropertyField(buildingID_Prop, new GUIContent("Building ID"));
                                break;
                            case ObjectiveType.UNIT:
                                EditorGUILayout.PropertyField(unitName_Prop, new GUIContent("Unit Name"));
                                EditorGUILayout.PropertyField(unitAmount_Prop, new GUIContent("Unit Amount"));
                                break;
                            case ObjectiveType.CONQUER:
                                EditorGUILayout.PropertyField(target_Prop, new GUIContent("Target Town"));
                                break;
                            case ObjectiveType.DEFEAT:
                                EditorGUILayout.PropertyField(target_Prop, new GUIContent("Target Army"));
                                break;
                        }
                    }
                }
                EditorGUI.indentLevel--;
            }
        }
        EditorGUI.indentLevel--;

        GetTarget.ApplyModifiedProperties();
    }
}
