using UnityEngine;

[CreateAssetMenu(fileName = "New Deformer Data", menuName = "Mesh Deformer/ New Deformer Data")]
public class MeshDeformerData : ScriptableObject
{
    [Min(0.00001f)]
    public float DeformHeight;
    [Min(0.00001f)]
    public float Radius;
    public AnimationCurve DeformSmoothCurve;
    public LayerMask DeformMeshLayer;
}