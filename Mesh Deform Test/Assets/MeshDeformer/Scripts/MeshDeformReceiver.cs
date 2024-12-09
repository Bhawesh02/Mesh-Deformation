using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MeshDeformReceiver : MonoBehaviour
{
    private enum DeformAxis
    {
        X_AXIS,
        MINUS_X_AXIS,
        Y_AXIS,
        MINUS_Y_AXIS,
        Z_AXIS,
        MINUS_Z_AXIS
    }
    
    [SerializeField] private MeshFilter m_meshFilter;
    [SerializeField] private Mesh m_originalMesh;
    [SerializeField] private DeformAxis m_deformAxis;
    
    private Mesh m_mesh;
    private Vector3[] m_originalVertices;
    private Vector3[] m_modifiedVertices;
    private Dictionary<DeformAxis, Vector3> m_deformAxisDatas;
    
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
        m_deformAxisDatas = new()
        {
            { DeformAxis.X_AXIS , Vector3.right},
            { DeformAxis.MINUS_X_AXIS , Vector3.left},
            { DeformAxis.Y_AXIS , Vector3.up},
            { DeformAxis.MINUS_Y_AXIS , Vector3.down},
            { DeformAxis.Z_AXIS , Vector3.forward},
            { DeformAxis.MINUS_Z_AXIS , Vector3.back}
        };
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
            if (distance >= currentDeformerDatarmData.Radius)
            {
                continue;
            }
            if (deformType == DeformType.DEFORM)
            {
                m_deformAxisDatas.TryGetValue(m_deformAxis, out Vector3 deformVector);
                if (IsVertexMaxDeformed(uvIndex, currentDeformerDatarmData.DeformHeight, deformVector))
                {
                    continue;
                }
                IncreaseVertexHeight(uvIndex, distance, currentDeformerDatarmData, deformVector);
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
        m_modifiedVertices[vertexIndex] = m_originalVertices[vertexIndex];
    }

    private void IncreaseVertexHeight(int vertexIndex, float distance, MeshDeformerData currentDeformerDatarmData, Vector3 deformVector)
    {
        float distanceRadiusRatio = Mathf.Clamp01(distance / currentDeformerDatarmData.Radius);
        Vector3 newVertexPosition = m_originalVertices[vertexIndex] +
                          ((currentDeformerDatarmData.DeformHeight * currentDeformerDatarmData.DeformSmoothCurve.Evaluate(distanceRadiusRatio))
                            * deformVector);
        float newHeight = Vector3.Distance(m_originalVertices[vertexIndex], newVertexPosition);
        float oldHeight = Vector3.Distance(m_originalVertices[vertexIndex], m_modifiedVertices[vertexIndex]);
        if (newHeight > oldHeight)
        {
            m_modifiedVertices[vertexIndex] = newVertexPosition;
        }
    }

    private bool IsVertexMaxDeformed(int deformVertexIndex, float deformHeight, Vector3 deformVector)
    {
        Vector3 modifiedVertex = Vector3.Scale(m_modifiedVertices[deformVertexIndex], deformVector);
        Vector3 originalVertex = Vector3.Scale(m_originalVertices[deformVertexIndex], deformVector);
        return Vector3.Distance(modifiedVertex, originalVertex) >= deformHeight;
    }

    public void ResetMesh()
    {
        m_originalVertices = m_originalMesh.vertices;
        Array.Copy(m_originalVertices, m_modifiedVertices, m_originalVertices.Length);
        m_mesh.SetVertices(m_originalVertices);
        m_mesh.RecalculateNormals();
    }
}
