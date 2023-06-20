using UnityEngine;

public class ConcaveCollider : MonoBehaviour
{
	public enum EAlgorithm
	{
		Normal = 0,
		Fast = 1,
		Legacy = 2,
	}

	public EAlgorithm Algorithm;
	public int MaxHullVertices;
	public int MaxHulls;
	public float InternalScale;
	public float Precision;
	public bool CreateMeshAssets;
	public bool CreateHullMesh;
	public bool DebugLog;
	public int LegacyDepth;
	public bool ShowAdvancedOptions;
	public float MinHullVolume;
	public float BackFaceDistanceFactor;
	public bool NormalizeInputMesh;
	public bool ForceNoMultithreading;
	public PhysicMaterial PhysMaterial;
	public bool IsTrigger;
	public GameObject[] m_aGoHulls;
	[SerializeField]
	private PhysicMaterial LastMaterial;
	[SerializeField]
	private bool LastIsTrigger;
	[SerializeField]
	private int LargestHullVertices;
	[SerializeField]
	private int LargestHullFaces;
}
