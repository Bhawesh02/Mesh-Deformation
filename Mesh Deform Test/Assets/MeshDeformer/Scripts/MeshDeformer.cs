using System;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteAlways]
public class MeshDeformer : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private bool m_canRunInEditMode;
#endif

    [SerializeField] private Vector3 m_rayCastEndOffset;
    [SerializeField] private bool m_canDeformMesh;
    [SerializeField] private DeformType m_deformType;

    [SerializeField] private MeshDeformerData m_deformMeshData;
    [SerializeField] private MeshDeformerData m_fixMeshData;
    
    private RaycastHit m_rayCastHit;
    private MeshDeformReceiver m_meshDeformReceiver;
    private MeshDeformerData m_currentDeformerData;
    private float m_rayCastDistance;
    private Transform m_meshDeformReceiverTransform;
    private Vector3 m_currentPosition;
    private Vector3 m_rayCastEndPosition;

    public DeformType CurrentDeformType => m_deformType;
    public MeshDeformerData CurrentDeformData => m_currentDeformerData;
    public Vector3 RayCastEndOffset => m_rayCastEndOffset;
    
    public void SetRayCastEndOffset(Vector3 newEndPosition) => m_rayCastEndOffset = newEndPosition;

    private void Start()
    {
        GetCurrentDeformerData();
    }

    private void OnValidate()
    {
        GetCurrentDeformerData();
    }

    private void GetCurrentDeformerData()
    {
        m_currentDeformerData = m_deformType switch
        {
            DeformType.UNIFORM => m_fixMeshData,
            DeformType.DEFORM => m_deformMeshData,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying && !m_canRunInEditMode)
        {
            return;
        }
#endif
        if (!m_currentDeformerData)
        {
            Debug.LogError($"{m_deformType.ToString()} Data is null");
            return;
        }
        if (!m_canDeformMesh)
        {
            return;
        }
        m_currentPosition = transform.position;
        m_rayCastEndPosition = m_currentPosition + m_rayCastEndOffset;
        m_rayCastDistance = Vector3.Distance(m_currentPosition, m_rayCastEndPosition);
        if (!Physics.Raycast(m_currentPosition,m_rayCastEndOffset, out m_rayCastHit,m_rayCastDistance,m_currentDeformerData.DeformMeshLayer))
            return;
        if (m_rayCastHit.transform != m_meshDeformReceiverTransform)
        {
            m_meshDeformReceiverTransform = m_rayCastHit.transform;
            m_meshDeformReceiver = m_rayCastHit.transform.GetComponent<MeshDeformReceiver>();
        }
        else if (!m_meshDeformReceiver)
        {
            m_meshDeformReceiver = m_rayCastHit.transform.GetComponent<MeshDeformReceiver>();
        }
        if (m_meshDeformReceiver)
        {
            m_meshDeformReceiver.DeformMesh(m_rayCastHit.textureCoord, m_deformType, m_currentDeformerData);
        }
    }

    
}
