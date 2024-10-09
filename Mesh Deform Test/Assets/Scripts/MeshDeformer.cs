using UnityEngine;

[ExecuteAlways]
public class MeshDeformer : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private bool m_canRunInEditMode;
#endif
    
    [SerializeField] private bool m_canDeformMesh;
    [SerializeField] private TypeOfDeform m_typeOfDeform;

    [SerializeField] private MeshDeformerData m_deformMeshData;
    [SerializeField] private MeshDeformerData m_fixMeshData;
    
    private RaycastHit m_raycastHit;
    private MeshDeformHandler m_meshDeformHandler;
    private MeshDeformerData m_currentDeformerData;

    public TypeOfDeform CurrentDeformType => m_typeOfDeform;
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
        m_currentDeformerData = m_typeOfDeform switch
        {
            TypeOfDeform.FIX => m_fixMeshData,
            TypeOfDeform.DEFORM => m_deformMeshData
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
            Debug.LogError($"{m_typeOfDeform.ToString()} Data is null");
            return;
        }
        if (!m_canDeformMesh)
        {
            return;
        }
        if (!Physics.Raycast(transform.position,Vector3.down, out m_raycastHit,Mathf.Infinity,m_currentDeformerData.DeformMeshLayer))
            return;
        m_meshDeformHandler = m_raycastHit.transform.GetComponent<MeshDeformHandler>();
        if (m_meshDeformHandler)
        {
            m_meshDeformHandler.DeformMesh(m_raycastHit.textureCoord, m_typeOfDeform, m_currentDeformerData);
        }
    }
}
