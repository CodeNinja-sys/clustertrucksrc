using System;
using UnityEngine;

// Token: 0x020002D7 RID: 727
[AddComponentMenu("Image Effects/Sonic Ether/SE Natural Bloom and Dirty Lens")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class SENaturalBloomAndDirtyLens : MonoBehaviour
{
	// Token: 0x06001121 RID: 4385 RVA: 0x0006F4DC File Offset: 0x0006D6DC
	private void Start()
	{
		this.isSupported = true;
		if (!this.material)
		{
			this.material = new Material(this.shader);
		}
		if (!SystemInfo.supportsImageEffects || !SystemInfo.supportsRenderTextures || !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf))
		{
			this.isSupported = false;
		}
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x0006F538 File Offset: 0x0006D738
	private void OnDisable()
	{
		if (this.material)
		{
			UnityEngine.Object.DestroyImmediate(this.material);
		}
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x0006F558 File Offset: 0x0006D758
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.isSupported)
		{
			Graphics.Blit(source, destination);
			return;
		}
		if (!this.material)
		{
			this.material = new Material(this.shader);
		}
		this.material.hideFlags = HideFlags.HideAndDontSave;
		this.material.SetFloat("_BloomIntensity", Mathf.Exp(this.bloomIntensity) - 1f);
		this.material.SetFloat("_LensDirtIntensity", Mathf.Exp(this.lensDirtIntensity) - 1f);
		source.filterMode = FilterMode.Bilinear;
		RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
		Graphics.Blit(source, temporary, this.material, 4);
		int num = source.width / 2;
		int num2 = source.height / 2;
		RenderTexture source2 = temporary;
		int num3 = 2;
		for (int i = 0; i < 6; i++)
		{
			RenderTexture renderTexture = RenderTexture.GetTemporary(num, num2, 0, source.format);
			renderTexture.filterMode = FilterMode.Bilinear;
			Graphics.Blit(source2, renderTexture, this.material, 1);
			source2 = renderTexture;
			float num4;
			if (i > 1)
			{
				num4 = 1f;
			}
			else
			{
				num4 = 0.5f;
			}
			if (i == 2)
			{
				num4 = 0.75f;
			}
			for (int j = 0; j < num3; j++)
			{
				this.material.SetFloat("_BlurSize", (this.blurSize * 0.5f + (float)j) * num4);
				RenderTexture temporary2 = RenderTexture.GetTemporary(num, num2, 0, source.format);
				temporary2.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture, temporary2, this.material, 2);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary2;
				temporary2 = RenderTexture.GetTemporary(num, num2, 0, source.format);
				temporary2.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture, temporary2, this.material, 3);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary2;
			}
			switch (i)
			{
			case 0:
				this.material.SetTexture("_Bloom0", renderTexture);
				break;
			case 1:
				this.material.SetTexture("_Bloom1", renderTexture);
				break;
			case 2:
				this.material.SetTexture("_Bloom2", renderTexture);
				break;
			case 3:
				this.material.SetTexture("_Bloom3", renderTexture);
				break;
			case 4:
				this.material.SetTexture("_Bloom4", renderTexture);
				break;
			case 5:
				this.material.SetTexture("_Bloom5", renderTexture);
				break;
			}
			RenderTexture.ReleaseTemporary(renderTexture);
			num /= 2;
			num2 /= 2;
		}
		this.material.SetTexture("_LensDirt", this.lensDirtTexture);
		Graphics.Blit(temporary, destination, this.material, 0);
		RenderTexture.ReleaseTemporary(temporary);
	}

	// Token: 0x04000E42 RID: 3650
	[Range(0f, 0.4f)]
	public float bloomIntensity = 0.05f;

	// Token: 0x04000E43 RID: 3651
	public Shader shader;

	// Token: 0x04000E44 RID: 3652
	private Material material;

	// Token: 0x04000E45 RID: 3653
	public Texture2D lensDirtTexture;

	// Token: 0x04000E46 RID: 3654
	[Range(0f, 0.95f)]
	public float lensDirtIntensity = 0.05f;

	// Token: 0x04000E47 RID: 3655
	private bool isSupported;

	// Token: 0x04000E48 RID: 3656
	private float blurSize = 4f;

	// Token: 0x04000E49 RID: 3657
	public bool inputIsHDR;
}
