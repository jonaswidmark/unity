using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MissionScriptableObject))]
public class MissionScriptableObjectEditor : Editor
{
    private SerializedProperty taskListProperty;

    private void OnEnable()
    {
        taskListProperty = serializedObject.FindProperty("missionTasks");
    }
    /**
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(taskListProperty, true);

        if (taskListProperty.isArray && taskListProperty.objectReferenceValue != null)
        {
            // Hämta listan från det bakomliggande objektet
            List<ITask> taskList = new List<ITask>((IEnumerable<ITask>)taskListProperty.objectReferenceValue);

            // Gör ändringar i listan här om det behövs

            // Uppdatera SerializedProperty med listan av gränssnitts ID
            int[] instanceIDs = new int[taskList.Count];
            for (int i = 0; i < taskList.Count; i++)
            {
                instanceIDs[i] = taskList[i].GetInstanceID();
            }

            // Sätt listan av instans-ID separat
            for (int i = 0; i < instanceIDs.Length; i++)
            {
                taskListProperty.InsertArrayElementAtIndex(i);
                taskListProperty.GetArrayElementAtIndex(i).objectReferenceInstanceIDValue = instanceIDs[i];
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
    **/
}
