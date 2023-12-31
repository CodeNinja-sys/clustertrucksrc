﻿using System;
using UnityEngine;

// Token: 0x0200016F RID: 367
[ExecuteInEditMode]
public class DeluxeEyeAdaptation : MonoBehaviour
{
	// Token: 0x1700017B RID: 379
	// (get) Token: 0x06000832 RID: 2098 RVA: 0x00035CD8 File Offset: 0x00033ED8
	public bool RenderDebugInfos
	{
		get
		{
			return this.m_ShowExposure || this.m_ShowHistogram;
		}
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x00035CF0 File Offset: 0x00033EF0
	private Material CreateMaterial(Shader shader)
	{
		if (!shader)
		{
			return null;
		}
		return new Material(shader)
		{
			hideFlags = HideFlags.HideAndDontSave
		};
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x00035D1C File Offset: 0x00033F1C
	private void DestroyMaterial(Material mat)
	{
		if (mat)
		{
			UnityEngine.Object.DestroyImmediate(mat);
			mat = null;
		}
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x00035D34 File Offset: 0x00033F34
	public void OnDisable()
	{
		if (this.m_Logic != null)
		{
			this.m_Logic.ClearMeshes();
		}
		this.DestroyMaterial(this.m_HistogramMaterial);
		this.m_HistogramMaterial = null;
		this.m_HistogramShader = null;
		this.DestroyMaterial(this.m_BrightnessMaterial);
		this.m_BrightnessMaterial = null;
		this.m_BrightnessShader = null;
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x00035D8C File Offset: 0x00033F8C
	private void CreateMaterials()
	{
		if (this.m_HistogramShader == null)
		{
			this.m_HistogramShader = Shader.Find("Hidden/Deluxe/EyeAdaptation");
		}
		if (this.m_HistogramMaterial == null && this.m_HistogramShader != null && this.m_HistogramShader.isSupported)
		{
			this.m_HistogramMaterial = this.CreateMaterial(this.m_HistogramShader);
		}
		if (this.m_BrightnessShader == null)
		{
			this.m_BrightnessShader = Shader.Find("Hidden/Deluxe/EyeAdaptationBright");
		}
		if (this.m_BrightnessMaterial == null && this.m_BrightnessShader != null && this.m_BrightnessShader.isSupported)
		{
			this.m_BrightnessMaterial = this.CreateMaterial(this.m_BrightnessShader);
		}
		else if (this.m_BrightnessShader == null)
		{
			Debug.LogError("Cant find brightness shader");
		}
		else if (!this.m_BrightnessShader.isSupported)
		{
			Debug.LogError("Brightness shader unsupported");
		}
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x00035EA4 File Offset: 0x000340A4
	public void OnEnable()
	{
		this.CreateMaterials();
		this.VerifyAndUpdateShaderVariations(true);
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x00035EB4 File Offset: 0x000340B4
	public void VerifyAndUpdateShaderVariations(bool forceUpdate)
	{
		if (this.m_HistogramMaterial == null)
		{
			return;
		}
		if (this.m_ShowHistogram != this.m_LastShowHistogram || forceUpdate)
		{
			this.m_HistogramMaterial.DisableKeyword("DLX_DEBUG_HISTOGRAM");
			if (this.m_ShowHistogram)
			{
				this.m_HistogramMaterial.EnableKeyword("DLX_DEBUG_HISTOGRAM");
			}
		}
		this.m_LastShowHistogram = this.m_ShowHistogram;
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x00035F24 File Offset: 0x00034124
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.CreateMaterials();
		this.VerifyAndUpdateShaderVariations(false);
		if (this.m_Logic == null)
		{
			this.m_Logic = new DeluxeEyeAdaptationLogic();
		}
		RenderTexture temporary = RenderTexture.GetTemporary(source.width / 2, source.height / 2, 0, source.format);
		RenderTexture temporary2 = RenderTexture.GetTemporary(temporary.width / 2, temporary.height / 2, 0, source.format);
		if (this.m_BrightnessMaterial == null)
		{
			Graphics.Blit(source, destination);
			return;
		}
		this.m_BrightnessMaterial.SetTexture("_UpTex", source);
		source.filterMode = FilterMode.Bilinear;
		this.m_BrightnessMaterial.SetVector("_PixelSize", new Vector4(1f / (float)source.width * 0.5f, 1f / (float)source.height * 0.5f));
		Graphics.Blit(source, temporary, this.m_BrightnessMaterial, 1);
		this.m_BrightnessMaterial.SetTexture("_UpTex", temporary);
		temporary.filterMode = FilterMode.Bilinear;
		this.m_BrightnessMaterial.SetVector("_PixelSize", new Vector4(1f / (float)temporary.width * 0.5f, 1f / (float)temporary.height * 0.5f));
		Graphics.Blit(temporary, temporary2, this.m_BrightnessMaterial, 1);
		source.filterMode = FilterMode.Point;
		if (this.m_LowResolution)
		{
			RenderTexture temporary3 = RenderTexture.GetTemporary(temporary2.width / 2, temporary2.height / 2, 0, source.format);
			this.m_BrightnessMaterial.SetTexture("_UpTex", temporary2);
			temporary2.filterMode = FilterMode.Bilinear;
			this.m_BrightnessMaterial.SetVector("_PixelSize", new Vector4(1f / (float)temporary2.width * 0.5f, 1f / (float)temporary2.height * 0.5f));
			Graphics.Blit(temporary2, temporary3, this.m_BrightnessMaterial, 1);
			this.m_HistogramMaterial.SetTexture("_FrameTex", temporary3);
			this.m_Logic.ComputeExposure(temporary3.width, temporary3.height, this.m_HistogramMaterial);
			RenderTexture.ReleaseTemporary(temporary3);
		}
		else
		{
			this.m_HistogramMaterial.SetTexture("_FrameTex", temporary2);
			this.m_Logic.ComputeExposure(temporary2.width, temporary2.height, this.m_HistogramMaterial);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
		if (!Application.isPlaying)
		{
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = this.m_Logic.m_BrightnessRT;
			GL.Clear(false, true, Color.white);
			RenderTexture.active = active;
		}
		this.m_BrightnessMaterial.SetFloat("_ExposureOffset", this.m_Logic.m_ExposureOffset);
		this.m_BrightnessMaterial.SetFloat("_BrightnessMultiplier", this.m_Logic.m_BrightnessMultiplier);
		this.m_BrightnessMaterial.SetTexture("_BrightnessTex", this.m_Logic.m_BrightnessRT);
		this.m_BrightnessMaterial.SetTexture("_ColorTex", source);
		if (this.RenderDebugInfos)
		{
			RenderTexture temporary4 = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
			Graphics.Blit(source, temporary4, this.m_BrightnessMaterial, 0);
			this.m_BrightnessMaterial.SetFloat("_VisualizeExposure", (!this.m_ShowExposure) ? 0f : 1f);
			this.m_BrightnessMaterial.SetFloat("_VisualizeHistogram", (!this.m_ShowHistogram) ? 0f : 1f);
			this.m_BrightnessMaterial.SetVector("_MinMaxSpeedDt", this.m_Logic.MinMaxSpeedDT);
			this.m_BrightnessMaterial.SetTexture("_Histogram", this.m_Logic.m_HistogramList[0]);
			this.m_BrightnessMaterial.SetTexture("_ColorTex", temporary4);
			this.m_BrightnessMaterial.SetFloat("_LuminanceRange", this.m_Logic.m_Range);
			this.m_BrightnessMaterial.SetVector("_HistogramCoefs", this.m_Logic.m_HistCoefs);
			this.m_BrightnessMaterial.SetVector("_MinMax", new Vector4(0.01f, this.m_HistogramSize * 0.75f, 0.01f, this.m_HistogramSize * 0.5f));
			this.m_BrightnessMaterial.SetFloat("_TotalPixelNumber", (float)(this.m_Logic.m_CurrentWidth * this.m_Logic.m_CurrentHeight));
			Graphics.Blit(temporary4, destination, this.m_BrightnessMaterial, 2);
			RenderTexture.ReleaseTemporary(temporary4);
		}
		else
		{
			Graphics.Blit(source, destination, this.m_BrightnessMaterial, 0);
		}
	}

	// Token: 0x0400065B RID: 1627
	[SerializeField]
	public DeluxeEyeAdaptationLogic m_Logic;

	// Token: 0x0400065C RID: 1628
	[SerializeField]
	public bool m_LowResolution;

	// Token: 0x0400065D RID: 1629
	[SerializeField]
	public bool m_ShowHistogram;

	// Token: 0x0400065E RID: 1630
	private bool m_LastShowHistogram;

	// Token: 0x0400065F RID: 1631
	[SerializeField]
	public bool m_ShowExposure;

	// Token: 0x04000660 RID: 1632
	[SerializeField]
	public float m_HistogramSize = 0.5f;

	// Token: 0x04000661 RID: 1633
	private Material m_HistogramMaterial;

	// Token: 0x04000662 RID: 1634
	private Shader m_HistogramShader;

	// Token: 0x04000663 RID: 1635
	private Material m_BrightnessMaterial;

	// Token: 0x04000664 RID: 1636
	private Shader m_BrightnessShader;

	// Token: 0x04000665 RID: 1637
	public bool m_ExposureOpened = true;

	// Token: 0x04000666 RID: 1638
	public bool m_SpeedOpened = true;

	// Token: 0x04000667 RID: 1639
	public bool m_HistogramOpened = true;

	// Token: 0x04000668 RID: 1640
	public bool m_OptimizationOpened = true;

	// Token: 0x04000669 RID: 1641
	public bool m_DebugOpened = true;

	// Token: 0x0400066A RID: 1642
	public bool m_BannerIsOpened = true;
}
