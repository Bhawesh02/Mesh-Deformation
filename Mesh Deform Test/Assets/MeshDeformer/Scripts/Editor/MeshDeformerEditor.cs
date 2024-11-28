using Codice.CM.Common.Merge;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshDeformer))]
public class MeshDeformerEditor : Editor
{

    private bool m_canEditRayCastEndPosition;
    private string m_buttonText;
    private float m_buttonWidth;
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        MeshDeformer meshDeformer = (MeshDeformer)target;
        
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_canRunInEditMode"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_canDeformMesh"));
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_rayCastEndOffset"));
        m_buttonText = m_canEditRayCastEndPosition ? "Stop Edit" : "Edit";
        m_buttonWidth = m_canEditRayCastEndPosition ? 60f : 40f;
        if (GUILayout.Button(m_buttonText, GUILayout.Width(m_buttonWidth)))
        {
            m_canEditRayCastEndPosition = !m_canEditRayCastEndPosition;
        }
        EditorGUILayout.EndHorizontal();
        
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
    #if UNITY_EDITOR
    private void OnSceneGUI()
    {
        MeshDeformer meshDeformer = (MeshDeformer)target;
        Vector3 currentPosition = meshDeformer.transform.position;
        Handles.DrawLine(currentPosition, currentPosition+meshDeformer.RayCastEndOffset, 1.5f);
        if (!m_canEditRayCastEndPosition)
        {
            return;
        }
        EditorGUI.BeginChangeCheck();
        Vector3 newRayCastEndPosition = Handles.PositionHandle(currentPosition + meshDeformer.RayCastEndOffset, Quaternion.identity);
        newRayCastEndPosition -= currentPosition;
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(meshDeformer, "Change Raycast end Position");
            meshDeformer.SetRayCastEndOffset(newRayCastEndPosition);
            serializedObject.Update();
        } 
    }
    #endif
}
