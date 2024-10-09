using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshDeformReceiver))]
public class MeshDeformReceiverEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MeshDeformReceiver meshDeformReceiver = (MeshDeformReceiver)target;
        EditorGUILayout.Space();
        if (GUILayout.Button("Reset Mesh"))
        {
            meshDeformReceiver.ResetMesh();
        }
    }
}
