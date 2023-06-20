using System;
using UnityEngine;

// Token: 0x0200030C RID: 780
[AddComponentMenu("Playdead/TemporalReprojection")]
[RequireComponent(typeof(VelocityBuffer))]
[RequireComponent(typeof(FrustumJitter))]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class TemporalReprojection : EffectBase
{
	// Token: 0x06001243 RID: 4675 RVA: 0x00074930 File Offset: 0x00072B30
	private void Awake()
	{
		this._camera = base.GetComponent<Camera>();
		this._jitter = base.GetComponent<FrustumJitter>();
		this._velocity = base.GetComponent<VelocityBuffer>();
	}

	// Token: 0x06001244 RID: 4676 RVA: 0x00074964 File Offset: 0x00072B64
	private void Resolve(RenderTexture source, RenderTexture destination)
	{
		base.EnsureMaterial(ref this.reprojectionMaterial, this.reprojectionShader);
		if (this._camera.orthographic || this._camera.depthTextureMode == DepthTextureMode.None || this.reprojectionMaterial == null)
		{
			Graphics.Blit(source, destination);
			if (this._camera.depthTextureMode == DepthTextureMode.None)
			{
				this._camera.depthTextureMode = DepthTextureMode.Depth;
			}
			return;
		}
		if (this.reprojectionMatrix == null || this.reprojectionMatrix.Length != 2)
		{
			this.reprojectionMatrix = new Matrix4x4[2];
		}
		if (this.reprojectionBuffer == null || this.reprojectionBuffer.Length != 2)
		{
			this.reprojectionBuffer = new RenderTexture[2];
		}
		int width = source.width;
		int height = source.height;
		base.EnsureRenderTarget(ref this.reprojectionBuffer[0], width, height, RenderTextureFormat.ARGB32, FilterMode.Bilinear, 0);
		base.EnsureRenderTarget(ref this.reprojectionBuffer[1], width, height, RenderTextureFormat.ARGB32, FilterMode.Bilinear, 0);
		base.EnsureKeyword(this.reprojectionMaterial, "MINMAX_3X3", this.neighborhood == TemporalReprojection.Neighborhood.MinMax3x3);
		base.EnsureKeyword(this.reprojectionMaterial, "MINMAX_3X3_ROUNDED", this.neighborhood == TemporalReprojection.Neighborhood.MinMax3x3Rounded);
		base.EnsureKeyword(this.reprojectionMaterial, "MINMAX_4TAP_VARYING", this.neighborhood == TemporalReprojection.Neighborhood.MinMax4TapVarying);
		base.EnsureKeyword(this.reprojectionMaterial, "UNJITTER_COLORSAMPLES", this.unjitterColorSamples);
		base.EnsureKeyword(this.reprojectionMaterial, "UNJITTER_NEIGHBORHOOD", this.unjitterNeighborhood);
		base.EnsureKeyword(this.reprojectionMaterial, "UNJITTER_REPROJECTION", this.unjitterReprojection);
		base.EnsureKeyword(this.reprojectionMaterial, "USE_YCOCG", this.useYCoCg);
		base.EnsureKeyword(this.reprojectionMaterial, "USE_CLIPPING", this.useClipping);
		base.EnsureKeyword(this.reprojectionMaterial, "USE_DILATION", this.useDilation);
		base.EnsureKeyword(this.reprojectionMaterial, "USE_MOTION_BLUR", this.useMotionBlur);
		base.EnsureKeyword(this.reprojectionMaterial, "USE_MOTION_BLUR_NEIGHBORMAX", this._velocity.velocityNeighborMax != null);
		base.EnsureKeyword(this.reprojectionMaterial, "USE_OPTIMIZATIONS", this.useOptimizations);
		Matrix4x4 perspectiveProjection = this._camera.GetPerspectiveProjection();
		Matrix4x4 matrix4x = perspectiveProjection * this._camera.worldToCameraMatrix;
		float num = Mathf.Tan(0.008726646f * this._camera.fieldOfView);
		float x = num * this._camera.aspect;
		if (this.reprojectionIndex == -1)
		{
			this.reprojectionIndex = 0;
			this.reprojectionMatrix[this.reprojectionIndex] = matrix4x;
			Graphics.Blit(source, this.reprojectionBuffer[this.reprojectionIndex]);
		}
		int num2 = this.reprojectionIndex;
		int num3 = (this.reprojectionIndex + 1) % 2;
		this.reprojectionMaterial.SetTexture("_VelocityBuffer", this._velocity.velocityBuffer);
		this.reprojectionMaterial.SetTexture("_VelocityNeighborMax", this._velocity.velocityNeighborMax);
		this.reprojectionMaterial.SetVector("_Corner", new Vector4(x, num, 0f, 0f));
		this.reprojectionMaterial.SetVector("_Jitter", this._jitter.activeSample);
		this.reprojectionMaterial.SetMatrix("_PrevVP", this.reprojectionMatrix[num2]);
		this.reprojectionMaterial.SetTexture("_MainTex", source);
		this.reprojectionMaterial.SetTexture("_PrevTex", this.reprojectionBuffer[num2]);
		this.reprojectionMaterial.SetFloat("_FeedbackMin", this.feedbackMin);
		this.reprojectionMaterial.SetFloat("_FeedbackMax", this.feedbackMax);
		this.reprojectionMaterial.SetFloat("_MotionScale", this.motionBlurStrength * ((!this.motionBlurIgnoreFF) ? 1f : Mathf.Min(1f, 1f / this._velocity.timeScale)));
		TemporalReprojection.mrt[0] = this.reprojectionBuffer[num3].colorBuffer;
		TemporalReprojection.mrt[1] = destination.colorBuffer;
		Graphics.SetRenderTarget(TemporalReprojection.mrt, source.depthBuffer);
		this.reprojectionMaterial.SetPass(0);
		base.FullScreenQuad();
		this.reprojectionMatrix[num3] = matrix4x;
		this.reprojectionIndex = num3;
	}

	// Token: 0x06001245 RID: 4677 RVA: 0x00074DC4 File Offset: 0x00072FC4
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (destination != null)
		{
			this.Resolve(source, destination);
		}
		else
		{
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.ARGB32);
			this.Resolve(source, temporary);
			Graphics.Blit(temporary, destination);
			RenderTexture.ReleaseTemporary(temporary);
		}
	}

	// Token: 0x04000F79 RID: 3961
	private static RenderBuffer[] mrt = new RenderBuffer[2];

	// Token: 0x04000F7A RID: 3962
	public Shader reprojectionShader;

	// Token: 0x04000F7B RID: 3963
	private Material reprojectionMaterial;

	// Token: 0x04000F7C RID: 3964
	private Matrix4x4[] reprojectionMatrix;

	// Token: 0x04000F7D RID: 3965
	private RenderTexture[] reprojectionBuffer;

	// Token: 0x04000F7E RID: 3966
	private int reprojectionIndex;

	// Token: 0x04000F7F RID: 3967
	public TemporalReprojection.Neighborhood neighborhood = TemporalReprojection.Neighborhood.MinMax3x3Rounded;

	// Token: 0x04000F80 RID: 3968
	public bool unjitterColorSamples = true;

	// Token: 0x04000F81 RID: 3969
	public bool unjitterNeighborhood;

	// Token: 0x04000F82 RID: 3970
	public bool unjitterReprojection;

	// Token: 0x04000F83 RID: 3971
	public bool useYCoCg;

	// Token: 0x04000F84 RID: 3972
	public bool useClipping = true;

	// Token: 0x04000F85 RID: 3973
	public bool useDilation = true;

	// Token: 0x04000F86 RID: 3974
	public bool useMotionBlur = true;

	// Token: 0x04000F87 RID: 3975
	public bool useOptimizations = true;

	// Token: 0x04000F88 RID: 3976
	[Range(0f, 1f)]
	public float feedbackMin = 0.88f;

	// Token: 0x04000F89 RID: 3977
	[Range(0f, 1f)]
	public float feedbackMax = 0.97f;

	// Token: 0x04000F8A RID: 3978
	public float motionBlurStrength = 1f;

	// Token: 0x04000F8B RID: 3979
	public bool motionBlurIgnoreFF;

	// Token: 0x04000F8C RID: 3980
	private Camera _camera;

	// Token: 0x04000F8D RID: 3981
	private FrustumJitter _jitter;

	// Token: 0x04000F8E RID: 3982
	private VelocityBuffer _velocity;

	// Token: 0x0200030D RID: 781
	public enum Neighborhood
	{
		// Token: 0x04000F90 RID: 3984
		MinMax3x3,
		// Token: 0x04000F91 RID: 3985
		MinMax3x3Rounded,
		// Token: 0x04000F92 RID: 3986
		MinMax4TapVarying
	}
}
