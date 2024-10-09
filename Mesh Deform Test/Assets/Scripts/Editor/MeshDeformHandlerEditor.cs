using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshDeformHandler))]
public class MeshDeformHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MeshDeformHandler meshDeformHandler = (MeshDeformHandler)target;
        EditorGUILayout.Space();
        if (GUILayout.Button("Reset Mesh"))
        {
            meshDeformHandler.ResetMesh();
        }
    }
}
