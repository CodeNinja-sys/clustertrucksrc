using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000310 RID: 784
[AddComponentMenu("Playdead/VelocityBufferTag")]
public class VelocityBufferTag : MonoBehaviour
{
	// Token: 0x0600124E RID: 4686 RVA: 0x0007530C File Offset: 0x0007350C
	private void Start()
	{
		if (this.useSkinnedMesh)
		{
			SkinnedMeshRenderer component = base.GetComponent<SkinnedMeshRenderer>();
			if (component != null)
			{
				this.mesh = new Mesh();
				this.skinnedMesh = component;
				this.skinnedMesh.BakeMesh(this.mesh);
			}
		}
		else
		{
			MeshFilter component2 = base.GetComponent<MeshFilter>();
			if (component2 != null)
			{
				this.mesh = component2.sharedMesh;
			}
		}
		this.localToWorldCurr = base.transform.localToWorldMatrix;
		this.localToWorldPrev = this.localToWorldCurr;
	}

	// Token: 0x0600124F RID: 4687 RVA: 0x0007539C File Offset: 0x0007359C
	private void VelocityUpdate()
	{
		if (this.useSkinnedMesh)
		{
			if (this.skinnedMesh == null)
			{
				Debug.LogWarning("vbuf skinnedMesh not set", this);
				return;
			}
			if (this.sleeping)
			{
				this.skinnedMesh.BakeMesh(this.mesh);
				this.mesh.normals = this.mesh.vertices;
			}
			else
			{
				Vector3[] vertices = this.mesh.vertices;
				this.skinnedMesh.BakeMesh(this.mesh);
				this.mesh.normals = vertices;
			}
		}
		if (this.sleeping)
		{
			this.localToWorldCurr = base.transform.localToWorldMatrix;
			this.localToWorldPrev = this.localToWorldCurr;
		}
		else
		{
			this.localToWorldPrev = this.localToWorldCurr;
			this.localToWorldCurr = base.transform.localToWorldMatrix;
		}
		this.sleeping = false;
	}

	// Token: 0x06001250 RID: 4688 RVA: 0x00075484 File Offset: 0x00073684
	private void LateUpdate()
	{
		if (this.framesNotRendered < 60)
		{
			this.framesNotRendered++;
			this.VelocityUpdate();
			return;
		}
		this.sleeping = true;
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x000754C0 File Offset: 0x000736C0
	private void OnWillRenderObject()
	{
		if (Camera.current != Camera.main)
		{
			return;
		}
		if (this.sleeping)
		{
			this.VelocityUpdate();
		}
		this.framesNotRendered = 0;
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x000754F0 File Offset: 0x000736F0
	private void OnEnable()
	{
		VelocityBufferTag.activeObjects.Add(this);
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x00075500 File Offset: 0x00073700
	private void OnDisable()
	{
		VelocityBufferTag.activeObjects.Remove(this);
	}

	// Token: 0x04000FA2 RID: 4002
	public const int framesNotRenderedThreshold = 60;

	// Token: 0x04000FA3 RID: 4003
	public static List<VelocityBufferTag> activeObjects = new List<VelocityBufferTag>(128);

	// Token: 0x04000FA4 RID: 4004
	[HideInInspector]
	[NonSerialized]
	public Mesh mesh;

	// Token: 0x04000FA5 RID: 4005
	[HideInInspector]
	[NonSerialized]
	public Matrix4x4 localToWorldPrev;

	// Token: 0x04000FA6 RID: 4006
	[HideInInspector]
	[NonSerialized]
	public Matrix4x4 localToWorldCurr;

	// Token: 0x04000FA7 RID: 4007
	private SkinnedMeshRenderer skinnedMesh;

	// Token: 0x04000FA8 RID: 4008
	public bool useSkinnedMesh;

	// Token: 0x04000FA9 RID: 4009
	private int framesNotRendered = 60;

	// Token: 0x04000FAA RID: 4010
	[NonSerialized]
	public bool sleeping;
}
