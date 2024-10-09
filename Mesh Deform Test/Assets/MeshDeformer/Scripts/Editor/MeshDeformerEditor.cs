using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshDeformer))]
public class MeshDeformerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        MeshDeformer meshDeformer = (MeshDeformer)target;
        
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_canRunInEditMode"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_canDeformMesh"));
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Deform Data", EditorStyles.boldLabel);
        EditorGUILayout.Space(2f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_deformType"));
        
        if (!meshDeformer.CurrentDeformData)
        {
            EditorGUILayout.Space();
            GUIStyle style = new();
            style.normal.textColor = Color.red;
            style.fontStyle = FontStyle.Bold;
            style.fontSize = 15;
            EditorGUILayout.LabelField($"Insert {meshDeformer.CurrentDeformType} data", style);
            EditorGUILayout.Space(5f);
        }
        switch (meshDeformer.CurrentDeformType)
        {
            case DeformType.DEFORM:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_deformMeshData"));
                break;
            case DeformType.UNIFORM:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_fixMeshData"));
                break;
        }
        
        serializedObject.ApplyModifiedProperties();
        
    }
}
