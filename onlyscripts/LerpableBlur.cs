using System;
using UnityEngine;

// Token: 0x02000302 RID: 770
[AddComponentMenu("Petter/LerpableBlur")]
[ExecuteInEditMode]
public class LerpableBlur : MonoBehaviour
{
	// Token: 0x170002BF RID: 703
	// (get) Token: 0x0600121E RID: 4638 RVA: 0x0007386C File Offset: 0x00071A6C
	protected Material material
	{
		get
		{
			if (LerpableBlur.m_Material == null)
			{
				LerpableBlur.m_Material = new Material(this.blurShader);
				LerpableBlur.m_Material.hideFlags = HideFlags.DontSave;
			}
			return LerpableBlur.m_Material;
		}
	}

	// Token: 0x0600121F RID: 4639 RVA: 0x000738A0 File Offset: 0x00071AA0
	protected void OnDisable()
	{
		if (LerpableBlur.m_Material)
		{
			UnityEngine.Object.DestroyImmediate(LerpableBlur.m_Material);
		}
	}

	// Token: 0x06001220 RID: 4640 RVA: 0x000738BC File Offset: 0x00071ABC
	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (!this.blurShader || !this.material.shader.isSupported)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001221 RID: 4641 RVA: 0x00073908 File Offset: 0x00071B08
	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = 0.5f + (float)iteration * this.blurSpread;
		num = Mathf.Lerp(0f, num, this.blurSpread);
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x06001222 RID: 4642 RVA: 0x0007399C File Offset: 0x00071B9C
	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		float b = 1f;
		b = Mathf.Lerp(0f, b, this.blurSpread);
		Graphics.Blit(source, dest, this.material);
	}

	// Token: 0x06001223 RID: 4643 RVA: 0x000739D0 File Offset: 0x00071BD0
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		int width = Mathf.FloorToInt((float)source.width / Mathf.Clamp(4f * this.blurSpread, 1f, float.PositiveInfinity));
		int height = Mathf.FloorToInt((float)source.height / Mathf.Clamp(4f * this.blurSpread, 1f, float.PositiveInfinity));
		width = source.width;
		height = source.height;
		RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 0);
		this.DownSample4x(source, renderTexture);
		for (int i = 0; i < this.iterations; i++)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0);
			this.FourTapCone(renderTexture, temporary, i);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
		}
		Graphics.Blit(renderTexture, destination);
		RenderTexture.ReleaseTemporary(renderTexture);
	}

	// Token: 0x04000F43 RID: 3907
	[Range(0f, 10f)]
	public int iterations = 3;

	// Token: 0x04000F44 RID: 3908
	[Range(0f, 1f)]
	public float blurSpread = 0.6f;

	// Token: 0x04000F45 RID: 3909
	public Shader blurShader;

	// Token: 0x04000F46 RID: 3910
	private static Material m_Material;
}
