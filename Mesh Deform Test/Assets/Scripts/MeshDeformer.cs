using UnityEngine;

[ExecuteAlways]
public class MeshDeformer : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private bool m_canRunInEditMode;
#endif
    
    [SerializeField] private bool m_canDeformMesh;
    [SerializeField] private DeformType m_deformType;

    [SerializeField] private MeshDeformerData m_deformMeshData;
    [SerializeField] private MeshDeformerData m_fixMeshData;
    
    private RaycastHit m_raycastHit;
    private MeshDeformReceiver m_meshDeformReceiver;
    private MeshDeformerData m_currentDeformerData;

    public DeformType CurrentDeformType => m_deformType;
    public MeshDeformerData CurrentDeformData => m_currentDeformerData;
    
    private void Awake()
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
            DeformType.DEFORM => m_deformMeshData
        };
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (!m_canRunInEditMode && Application.isEditor)
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
        if (!Physics.Raycast(transform.position,Vector3.down, out m_raycastHit,Mathf.Infinity,m_currentDeformerData.DeformMeshLayer))
            return;
        m_meshDeformReceiver = m_raycastHit.transform.GetComponent<MeshDeformReceiver>();
        if (m_meshDeformReceiver)
        {
            m_meshDeformReceiver.DeformMesh(m_raycastHit.textureCoord, m_deformType, m_currentDeformerData);
        }
    }
}
