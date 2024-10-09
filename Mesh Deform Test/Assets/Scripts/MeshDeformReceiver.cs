using System;

using UnityEngine;

[ExecuteAlways]
public class MeshDeformReceiver : MonoBehaviour
{
    [SerializeField] private MeshFilter m_meshFilter;
    
    private Mesh m_mesh;
    private Vector3[] m_originalVertices;
    private Vector3[] m_modifiedVertices;

    private void Awake()
    {
        m_mesh = m_meshFilter.sharedMesh;
        m_originalVertices = m_mesh.vertices;
        m_modifiedVertices = new Vector3[m_originalVertices.Length];
        Array.Copy(m_originalVertices, m_modifiedVertices, m_originalVertices.Length);
        ResetMesh();
    }
    
    
    public void DeformMesh(Vector2 hitUV, DeformType deformType, MeshDeformerData currentDeformerDatarmData)
    {
        Vector2[] uvCoords = m_mesh.uv;
        for (int uvIndex = 0; uvIndex < uvCoords.Length; uvIndex++)
        {
            float distance = Vector2.Distance(hitUV, uvCoords[uvIndex]);
            if (distance >= currentDeformerDatarmData.Radius || IsVertexMaxDeformed(uvIndex, currentDeformerDatarmData.DeformHeight))
            {
                continue;
            }

            if (deformType == DeformType.DEFORM)
            {
                IncreaseVertexHeight(uvIndex, distance, currentDeformerDatarmData);
            }
            else
            {
                FixVertexHeight(uvIndex);
            }
        }
        m_mesh.SetVertices(m_modifiedVertices);
        m_mesh.RecalculateNormals();
    }

    private void FixVertexHeight(int vertexIndex)
    {
        m_modifiedVertices[vertexIndex].y = m_originalVertices[vertexIndex].y;
    }

    private void IncreaseVertexHeight(int vertexIndex, float distance, MeshDeformerData currentDeformerDatarmData)
    {
        float distanceRadiusRatio = Mathf.Clamp01(distance / currentDeformerDatarmData.Radius);
        float newHeight = m_originalVertices[vertexIndex].y +
                          (currentDeformerDatarmData.DeformHeight * currentDeformerDatarmData.DeformSmoothCurve.Evaluate(distanceRadiusRatio));
        m_modifiedVertices[vertexIndex].y = Mathf.Max(m_modifiedVertices[vertexIndex].y, newHeight);
    }

    private bool IsVertexMaxDeformed(int deformVertexIndex, float deformHeight)
    {
        return m_modifiedVertices[deformVertexIndex].y >= m_originalVertices[deformVertexIndex].y + deformHeight;
    }

    public void ResetMesh()
    {
        m_mesh.SetVertices(m_originalVertices);
        Array.Copy(m_originalVertices, m_modifiedVertices, m_originalVertices.Length);
        m_mesh.RecalculateNormals();
    }
}
