using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deformer Data", menuName = "Mesh Deformer/ New Deformer Data")]
public class MeshDeformerData : ScriptableObject
{
    public float DeformHeight;
    public float Radius;
    public AnimationCurve DeformSmoothCurve;
    public LayerMask DeformMeshLayer;
}