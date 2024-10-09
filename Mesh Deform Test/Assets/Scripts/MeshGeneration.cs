using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer)), ExecuteInEditMode]
public class MeshGeneration : MonoBehaviour
{
    [SerializeField] private MeshFilter m_meshFilter;
    [Min(1)][SerializeField] private Vector2 m_meshSize;
    [SerializeField] private Vector2Int m_polygonGridSize;
    
    private List<Vector3> m_vertexList = new ();
    private List<int> m_triangleList = new ();
    private List<Vector2> m_uvList = new ();

    
    private void Start()
    {
       GenerateMesh();
    }

    private void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        m_vertexList.Clear();
        m_uvList.Clear();
        m_triangleList.Clear();
        float xOffset = m_meshSize.x / m_polygonGridSize.x;
        float yOffset = m_meshSize.y / m_polygonGridSize.y;
        
        for (int vertexYIndex = 0; vertexYIndex <= m_polygonGridSize.y; vertexYIndex++)
        {
            for (int vertexXIndex = 0; vertexXIndex <= m_polygonGridSize.x; vertexXIndex++)
            {
                m_vertexList.Add(new Vector3(( xOffset * vertexXIndex) - (m_meshSize.x * 0.5f),0, ( yOffset * vertexYIndex) - (m_meshSize.y * 0.5f)));
                m_uvList.Add(new Vector2(((float)vertexXIndex) / m_polygonGridSize.x, ((float)vertexYIndex) / m_polygonGridSize.y));
                if (vertexXIndex == m_polygonGridSize.x || vertexYIndex == m_polygonGridSize.y)
                {
                    continue;
                }
                AddPolygonTriangles(vertexYIndex, vertexXIndex);
            }
        }
        mesh.SetVertices(m_vertexList);
        mesh.SetTriangles(m_triangleList, 0);
        mesh.SetUVs(0,m_uvList);
        mesh.RecalculateNormals();
        m_meshFilter.mesh = mesh;
    }

    private void AddPolygonTriangles(int vertexYIndex, int vertexXIndex)
    {
        int bottomLeft = (vertexYIndex * (m_polygonGridSize.x + 1)) + vertexXIndex;
        int topLeft = bottomLeft + m_polygonGridSize.x + 1;
        int bottomRight = bottomLeft + 1;
        int topRight = topLeft + 1;
        m_triangleList.Add(bottomLeft);
        m_triangleList.Add(topLeft);
        m_triangleList.Add(bottomRight);
        m_triangleList.Add(topLeft);
        m_triangleList.Add(topRight);
        m_triangleList.Add(bottomRight);
    }

}
