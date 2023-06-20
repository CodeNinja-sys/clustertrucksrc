using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200030E RID: 782
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[AddComponentMenu("Playdead/VelocityBuffer")]
public class VelocityBuffer : EffectBase
{
	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x06001247 RID: 4679 RVA: 0x00074E24 File Offset: 0x00073024
	// (set) Token: 0x06001248 RID: 4680 RVA: 0x00074E2C File Offset: 0x0007302C
	public float timeScale { get; private set; }

	// Token: 0x06001249 RID: 4681 RVA: 0x00074E38 File Offset: 0x00073038
	private void Awake()
	{
		this._camera = base.GetComponent<Camera>();
	}

	// Token: 0x0600124A RID: 4682 RVA: 0x00074E48 File Offset: 0x00073048
	private void Start()
	{
		this.timeScaleNextFrame = Time.timeScale;
	}

	// Token: 0x0600124B RID: 4683 RVA: 0x00074E58 File Offset: 0x00073058
	private void OnPostRender()
	{
		base.EnsureMaterial(ref this.velocityMaterial, this.velocityShader);
		if (this._camera.orthographic || this._camera.depthTextureMode == DepthTextureMode.None || this.velocityMaterial == null)
		{
			if (this._camera.depthTextureMode == DepthTextureMode.None)
			{
				this._camera.depthTextureMode = DepthTextureMode.Depth;
			}
			return;
		}
		this.timeScale = this.timeScaleNextFrame;
		this.timeScaleNextFrame = ((Time.timeScale != 0f) ? Time.timeScale : this.timeScaleNextFrame);
		int pixelWidth = this._camera.pixelWidth;
		int pixelHeight = this._camera.pixelHeight;
		base.EnsureRenderTarget(ref this.velocityBuffer, pixelWidth, pixelHeight, RenderTextureFormat.RGFloat, FilterMode.Point, 16);
		base.EnsureKeyword(this.velocityMaterial, "TILESIZE_10", this.neighborMaxSupport == VelocityBuffer.NeighborMaxSupport.TileSize10);
		base.EnsureKeyword(this.velocityMaterial, "TILESIZE_20", this.neighborMaxSupport == VelocityBuffer.NeighborMaxSupport.TileSize20);
		base.EnsureKeyword(this.velocityMaterial, "TILESIZE_40", this.neighborMaxSupport == VelocityBuffer.NeighborMaxSupport.TileSize40);
		Matrix4x4 projectionMatrix = this._camera.projectionMatrix;
		Matrix4x4 worldToCameraMatrix = this._camera.worldToCameraMatrix;
		Matrix4x4 matrix = projectionMatrix * worldToCameraMatrix;
		Matrix4x4? matrix4x = this.velocityViewMatrix;
		if (matrix4x == null)
		{
			this.velocityViewMatrix = new Matrix4x4?(worldToCameraMatrix);
		}
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = this.velocityBuffer;
		GL.Clear(true, true, Color.black);
		FrustumJitter component = base.GetComponent<FrustumJitter>();
		if (component != null)
		{
			this.velocityMaterial.SetVector("_Corner", this._camera.GetPerspectiveProjectionCornerRay(component.activeSample.x, component.activeSample.y));
		}
		else
		{
			this.velocityMaterial.SetVector("_Corner", this._camera.GetPerspectiveProjectionCornerRay());
		}
		this.velocityMaterial.SetMatrix("_CurrV", worldToCameraMatrix);
		this.velocityMaterial.SetMatrix("_CurrVP", matrix);
		this.velocityMaterial.SetMatrix("_PrevVP", projectionMatrix * this.velocityViewMatrix.Value);
		this.velocityMaterial.SetPass(0);
		base.FullScreenQuad();
		List<VelocityBufferTag> activeObjects = VelocityBufferTag.activeObjects;
		int num = 0;
		int count = activeObjects.Count;
		while (num != count)
		{
			VelocityBufferTag velocityBufferTag = activeObjects[num];
			if (velocityBufferTag != null && !velocityBufferTag.sleeping && velocityBufferTag.mesh != null)
			{
				this.velocityMaterial.SetMatrix("_CurrM", velocityBufferTag.localToWorldCurr);
				this.velocityMaterial.SetMatrix("_PrevM", velocityBufferTag.localToWorldPrev);
				this.velocityMaterial.SetPass((!velocityBufferTag.useSkinnedMesh) ? 1 : 2);
				for (int num2 = 0; num2 != velocityBufferTag.mesh.subMeshCount; num2++)
				{
					Graphics.DrawMeshNow(velocityBufferTag.mesh, Matrix4x4.identity, num2);
				}
			}
			num++;
		}
		if (this.neighborMaxGen)
		{
			int num3 = 1;
			switch (this.neighborMaxSupport)
			{
			case VelocityBuffer.NeighborMaxSupport.TileSize10:
				num3 = 10;
				break;
			case VelocityBuffer.NeighborMaxSupport.TileSize20:
				num3 = 20;
				break;
			case VelocityBuffer.NeighborMaxSupport.TileSize40:
				num3 = 40;
				break;
			}
			int num4 = pixelWidth / num3;
			int num5 = pixelHeight / num3;
			base.EnsureRenderTarget(ref this.velocityNeighborMax, num4, num5, RenderTextureFormat.RGFloat, FilterMode.Bilinear, 0);
			RenderTexture temporary = RenderTexture.GetTemporary(num4, num5, 0, RenderTextureFormat.RGFloat);
			RenderTexture.active = temporary;
			this.velocityMaterial.SetTexture("_VelocityTex", this.velocityBuffer);
			this.velocityMaterial.SetVector("_VelocityTex_TexelSize", new Vector4(1f / (float)pixelWidth, 1f / (float)pixelHeight, 0f, 0f));
			this.velocityMaterial.SetPass(3);
			base.FullScreenQuad();
			RenderTexture.active = this.velocityNeighborMax;
			this.velocityMaterial.SetTexture("_VelocityTex", temporary);
			this.velocityMaterial.SetVector("_VelocityTex_TexelSize", new Vector4(1f / (float)num4, 1f / (float)num5, 0f, 0f));
			this.velocityMaterial.SetPass(4);
			base.FullScreenQuad();
			RenderTexture.ReleaseTemporary(temporary);
		}
		else if (this.velocityNeighborMax != null)
		{
			RenderTexture.ReleaseTemporary(this.velocityNeighborMax);
			this.velocityNeighborMax = null;
		}
		RenderTexture.active = active;
		this.velocityViewMatrix = new Matrix4x4?(worldToCameraMatrix);
	}

	// Token: 0x04000F93 RID: 3987
	private const RenderTextureFormat velocityFormat = RenderTextureFormat.RGFloat;

	// Token: 0x04000F94 RID: 3988
	public Shader velocityShader;

	// Token: 0x04000F95 RID: 3989
	private Material velocityMaterial;

	// Token: 0x04000F96 RID: 3990
	private Matrix4x4? velocityViewMatrix;

	// Token: 0x04000F97 RID: 3991
	[HideInInspector]
	[NonSerialized]
	public RenderTexture velocityBuffer;

	// Token: 0x04000F98 RID: 3992
	[HideInInspector]
	[NonSerialized]
	public RenderTexture velocityNeighborMax;

	// Token: 0x04000F99 RID: 3993
	private float timeScaleNextFrame;

	// Token: 0x04000F9A RID: 3994
	public bool neighborMaxGen;

	// Token: 0x04000F9B RID: 3995
	public VelocityBuffer.NeighborMaxSupport neighborMaxSupport = VelocityBuffer.NeighborMaxSupport.TileSize20;

	// Token: 0x04000F9C RID: 3996
	private Camera _camera;

	// Token: 0x0200030F RID: 783
	public enum NeighborMaxSupport
	{
		// Token: 0x04000F9F RID: 3999
		TileSize10,
		// Token: 0x04000FA0 RID: 4000
		TileSize20,
		// Token: 0x04000FA1 RID: 4001
		TileSize40
	}
}
