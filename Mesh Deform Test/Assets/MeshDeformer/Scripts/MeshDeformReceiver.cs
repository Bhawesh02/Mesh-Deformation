using System;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class MeshDeformReceiver : MonoBehaviour
{
    [SerializeField] private MeshFilter m_meshFilter;
    [SerializeField] private Mesh m_originalMesh;
    
    private Mesh m_mesh;
    private Vector3[] m_originalVertices;
    private Vector3[] m_modifiedVertices;

    
    
    private void Start()
    {
        Init();
    }

    private void OnValidate()
    {
        Init();
    }

    private void Init()
    {
        MakeDuplicateOfOriginalMesh();
        m_originalVertices = new Vector3[m_mesh.vertices.Length];
        m_modifiedVertices = new Vector3[m_originalVertices.Length];
        ResetMesh();
    }

    private void MakeDuplicateOfOriginalMesh()
    {
        m_mesh = new Mesh
        {
            vertices = (Vector3[])m_originalMesh.vertices.Clone(),
            triangles = (int[])m_originalMesh.triangles.Clone(),
            normals = (Vector3[])m_originalMesh.normals.Clone(),
            uv = (Vector2[])m_originalMesh.uv.Clone()
        };
        if (m_originalMesh.tangents != null && m_originalMesh.tangents.Length > 0)
        {
            m_mesh.tangents = (Vector4[])m_originalMesh.tangents.Clone();
        }
        if (m_originalMesh.colors != null && m_originalMesh.colors.Length > 0)
        {
            m_mesh.colors = (Color[])m_originalMesh.colors.Clone();
        }
        m_mesh.RecalculateBounds();
        m_meshFilter.sharedMesh = m_mesh;
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
        m_originalVertices = m_originalMesh.vertices;
        Array.Copy(m_originalVertices, m_modifiedVertices, m_originalVertices.Length);
        m_mesh.SetVertices(m_originalVertices);
        m_mesh.RecalculateNormals();
    }
}
