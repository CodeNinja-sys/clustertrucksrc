using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x020002E4 RID: 740
[ExecuteInEditMode]
[AddComponentMenu("Ultimate Game Tools/Colliders/Concave Collider")]
public class ConcaveCollider : MonoBehaviour
{
	// Token: 0x06001160 RID: 4448 RVA: 0x00070C24 File Offset: 0x0006EE24
	private void OnDestroy()
	{
		this.DestroyHulls();
	}

	// Token: 0x06001161 RID: 4449 RVA: 0x00070C2C File Offset: 0x0006EE2C
	private void Reset()
	{
		this.DestroyHulls();
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x00070C34 File Offset: 0x0006EE34
	private void Update()
	{
		if (this.PhysMaterial != this.LastMaterial)
		{
			foreach (GameObject gameObject in this.m_aGoHulls)
			{
				if (gameObject)
				{
					Collider component = gameObject.GetComponent<Collider>();
					if (component)
					{
						component.material = this.PhysMaterial;
						this.LastMaterial = this.PhysMaterial;
					}
				}
			}
		}
		if (this.IsTrigger != this.LastIsTrigger)
		{
			foreach (GameObject gameObject2 in this.m_aGoHulls)
			{
				if (gameObject2)
				{
					Collider component2 = gameObject2.GetComponent<Collider>();
					if (component2)
					{
						component2.isTrigger = this.IsTrigger;
						this.LastIsTrigger = this.IsTrigger;
					}
				}
			}
		}
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x00070D20 File Offset: 0x0006EF20
	public void DestroyHulls()
	{
		this.LargestHullVertices = 0;
		this.LargestHullFaces = 0;
		if (this.m_aGoHulls == null)
		{
			return;
		}
		if (Application.isEditor && !Application.isPlaying)
		{
			foreach (GameObject gameObject in this.m_aGoHulls)
			{
				if (gameObject)
				{
					UnityEngine.Object.DestroyImmediate(gameObject);
				}
			}
		}
		else
		{
			foreach (GameObject gameObject2 in this.m_aGoHulls)
			{
				if (gameObject2)
				{
					UnityEngine.Object.Destroy(gameObject2);
				}
			}
		}
		this.m_aGoHulls = null;
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x00070DD0 File Offset: 0x0006EFD0
	public void CancelComputation()
	{
		ConcaveCollider.CancelConvexDecomposition();
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x00070DD8 File Offset: 0x0006EFD8
	public int GetLargestHullVertices()
	{
		return this.LargestHullVertices;
	}

	// Token: 0x06001166 RID: 4454 RVA: 0x00070DE0 File Offset: 0x0006EFE0
	public int GetLargestHullFaces()
	{
		return this.LargestHullFaces;
	}

	// Token: 0x06001167 RID: 4455
	[DllImport("ConvexDecompositionDll")]
	private static extern void DllInit(bool bUseMultithreading);

	// Token: 0x06001168 RID: 4456
	[DllImport("ConvexDecompositionDll")]
	private static extern void DllClose();

	// Token: 0x06001169 RID: 4457
	[DllImport("ConvexDecompositionDll")]
	private static extern void SetLogFunctionPointer(IntPtr pfnUnity3DLog);

	// Token: 0x0600116A RID: 4458
	[DllImport("ConvexDecompositionDll")]
	private static extern void SetProgressFunctionPointer(IntPtr pfnUnity3DProgress);

	// Token: 0x0600116B RID: 4459
	[DllImport("ConvexDecompositionDll")]
	private static extern void CancelConvexDecomposition();

	// Token: 0x0600116C RID: 4460
	[DllImport("ConvexDecompositionDll")]
	private static extern bool DoConvexDecomposition(ref ConcaveCollider.SConvexDecompositionInfoInOut infoInOut, Vector3[] pfVertices, int[] puIndices);

	// Token: 0x0600116D RID: 4461
	[DllImport("ConvexDecompositionDll")]
	private static extern bool GetHullInfo(uint uHullIndex, ref ConcaveCollider.SConvexDecompositionHullInfo infoOut);

	// Token: 0x0600116E RID: 4462
	[DllImport("ConvexDecompositionDll")]
	private static extern bool FillHullMeshData(uint uHullIndex, ref float pfVolumeOut, int[] pnIndicesOut, Vector3[] pfVerticesOut);

	// Token: 0x04000E7E RID: 3710
	public ConcaveCollider.EAlgorithm Algorithm = ConcaveCollider.EAlgorithm.Fast;

	// Token: 0x04000E7F RID: 3711
	public int MaxHullVertices = 64;

	// Token: 0x04000E80 RID: 3712
	public int MaxHulls = 128;

	// Token: 0x04000E81 RID: 3713
	public float InternalScale = 10f;

	// Token: 0x04000E82 RID: 3714
	public float Precision = 0.8f;

	// Token: 0x04000E83 RID: 3715
	public bool CreateMeshAssets;

	// Token: 0x04000E84 RID: 3716
	public bool CreateHullMesh;

	// Token: 0x04000E85 RID: 3717
	public bool DebugLog;

	// Token: 0x04000E86 RID: 3718
	public int LegacyDepth = 6;

	// Token: 0x04000E87 RID: 3719
	public bool ShowAdvancedOptions;

	// Token: 0x04000E88 RID: 3720
	public float MinHullVolume = 1E-05f;

	// Token: 0x04000E89 RID: 3721
	public float BackFaceDistanceFactor = 0.2f;

	// Token: 0x04000E8A RID: 3722
	public bool NormalizeInputMesh;

	// Token: 0x04000E8B RID: 3723
	public bool ForceNoMultithreading;

	// Token: 0x04000E8C RID: 3724
	public PhysicMaterial PhysMaterial;

	// Token: 0x04000E8D RID: 3725
	public bool IsTrigger;

	// Token: 0x04000E8E RID: 3726
	public GameObject[] m_aGoHulls;

	// Token: 0x04000E8F RID: 3727
	[SerializeField]
	private PhysicMaterial LastMaterial;

	// Token: 0x04000E90 RID: 3728
	[SerializeField]
	private bool LastIsTrigger;

	// Token: 0x04000E91 RID: 3729
	[SerializeField]
	private int LargestHullVertices;

	// Token: 0x04000E92 RID: 3730
	[SerializeField]
	private int LargestHullFaces;

	// Token: 0x020002E5 RID: 741
	public enum EAlgorithm
	{
		// Token: 0x04000E94 RID: 3732
		Normal,
		// Token: 0x04000E95 RID: 3733
		Fast,
		// Token: 0x04000E96 RID: 3734
		Legacy
	}

	// Token: 0x020002E6 RID: 742
	private struct SConvexDecompositionInfoInOut
	{
		// Token: 0x04000E97 RID: 3735
		public uint uMaxHullVertices;

		// Token: 0x04000E98 RID: 3736
		public uint uMaxHulls;

		// Token: 0x04000E99 RID: 3737
		public float fPrecision;

		// Token: 0x04000E9A RID: 3738
		public float fBackFaceDistanceFactor;

		// Token: 0x04000E9B RID: 3739
		public uint uLegacyDepth;

		// Token: 0x04000E9C RID: 3740
		public uint uNormalizeInputMesh;

		// Token: 0x04000E9D RID: 3741
		public uint uUseFastVersion;

		// Token: 0x04000E9E RID: 3742
		public uint uTriangleCount;

		// Token: 0x04000E9F RID: 3743
		public uint uVertexCount;

		// Token: 0x04000EA0 RID: 3744
		public int nHullsOut;
	}

	// Token: 0x020002E7 RID: 743
	private struct SConvexDecompositionHullInfo
	{
		// Token: 0x04000EA1 RID: 3745
		public int nVertexCount;

		// Token: 0x04000EA2 RID: 3746
		public int nTriangleCount;
	}

	// Token: 0x0200031A RID: 794
	// (Invoke) Token: 0x06001276 RID: 4726
	public delegate void LogDelegate([MarshalAs(UnmanagedType.LPStr)] string message);

	// Token: 0x0200031B RID: 795
	// (Invoke) Token: 0x0600127A RID: 4730
	public delegate void ProgressDelegate([MarshalAs(UnmanagedType.LPStr)] string message, float fPercent);
}
