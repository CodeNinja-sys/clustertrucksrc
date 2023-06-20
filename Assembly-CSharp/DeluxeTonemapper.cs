using System;
using UnityEngine;

// Token: 0x02000171 RID: 369
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class DeluxeTonemapper : MonoBehaviour
{
	// Token: 0x1700017D RID: 381
	// (get) Token: 0x06000844 RID: 2116 RVA: 0x00036C60 File Offset: 0x00034E60
	public FTDLut LutTool
	{
		get
		{
			return this.m_LutTool;
		}
	}

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x06000845 RID: 2117 RVA: 0x00036C68 File Offset: 0x00034E68
	public Shader TonemappingShader
	{
		get
		{
			return this.m_Shader;
		}
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x00036C70 File Offset: 0x00034E70
	private void DestroyMaterial(Material mat)
	{
		if (mat)
		{
			UnityEngine.Object.DestroyImmediate(mat);
			mat = null;
		}
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x00036C88 File Offset: 0x00034E88
	private void CreateMaterials()
	{
		if (this.m_Shader == null)
		{
			if (this.m_Mode == DeluxeTonemapper.Mode.Color)
			{
				this.m_Shader = Shader.Find("Hidden/Deluxe/TonemapperColor");
			}
			if (this.m_Mode == DeluxeTonemapper.Mode.Luminance)
			{
				this.m_Shader = Shader.Find("Hidden/Deluxe/TonemapperLuminosity");
			}
			if (this.m_Mode == DeluxeTonemapper.Mode.ExtendedLuminance)
			{
				this.m_Shader = Shader.Find("Hidden/Deluxe/TonemapperLuminosityExtended");
			}
			if (this.m_Mode == DeluxeTonemapper.Mode.ColorHD)
			{
				this.m_Shader = Shader.Find("Hidden/Deluxe/TonemapperColorHD");
			}
		}
		if (this.m_Material == null && this.m_Shader != null && this.m_Shader.isSupported)
		{
			this.m_Material = this.CreateMaterial(this.m_Shader);
		}
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x00036D5C File Offset: 0x00034F5C
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

	// Token: 0x06000849 RID: 2121 RVA: 0x00036D88 File Offset: 0x00034F88
	private void OnDisable()
	{
		this.DestroyMaterial(this.m_Material);
		this.m_Material = null;
		this.m_Shader = null;
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x00036DA4 File Offset: 0x00034FA4
	public void ClearLutTextures()
	{
		if (this.m_3DLut0 != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_3DLut0);
			this.m_3DLut0 = null;
		}
		this.m_2DLut0 = null;
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x00036DDC File Offset: 0x00034FDC
	public void ConvertAndApply2dLut()
	{
		if (this.m_2DLut0 != null)
		{
			this.m_3DLut0 = this.LutTool.Allocate3DLut(this.m_2DLut0);
			this.LutTool.ConvertLUT(this.m_2DLut0, this.m_3DLut0);
		}
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x00036E2C File Offset: 0x0003502C
	public void SetLut0(Texture2D lut0)
	{
		this.ClearLutTextures();
		this.m_2DLut0 = lut0;
		this.ConvertAndApply2dLut();
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x00036E44 File Offset: 0x00035044
	public void VerifyAndUpdateShaderVariations(bool forceUpdate)
	{
		if (this.m_Material == null)
		{
			return;
		}
		if (!SystemInfo.supports3DTextures)
		{
			this.m_Use2DLut = true;
		}
		if (this.m_DisableColorGrading != this.m_LastDisableColorGrading || forceUpdate)
		{
			this.m_Material.DisableKeyword("FTD_DISABLE_CGRADING");
			if (this.m_DisableColorGrading)
			{
				this.m_Material.EnableKeyword("FTD_DISABLE_CGRADING");
			}
		}
		if (this.m_Use2DLut != this.m_LastUse2DLut || forceUpdate)
		{
			this.m_Material.DisableKeyword("FTD_2D_LUT");
			if (this.m_Use2DLut)
			{
				this.m_Material.EnableKeyword("FTD_2D_LUT");
			}
		}
		this.m_LastUse2DLut = this.m_Use2DLut;
		this.m_LastDisableColorGrading = this.m_DisableColorGrading;
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x00036F10 File Offset: 0x00035110
	public void UpdateCoefficients()
	{
		if (this.m_Mode == DeluxeTonemapper.Mode.ColorHD)
		{
			this.m_MainCurve.m_UseLegacyCurve = false;
		}
		else
		{
			this.m_MainCurve.m_UseLegacyCurve = true;
		}
		this.m_MainCurve.UpdateCoefficients();
		if (this.m_Material == null)
		{
			return;
		}
		if (this.m_2DLut0 == null)
		{
			this.ClearLutTextures();
		}
		this.m_Material.SetFloat("_K", this.m_MainCurve.m_k);
		this.m_Material.SetFloat("_Crossover", this.m_MainCurve.m_CrossOverPoint);
		this.m_Material.SetVector("_Toe", this.m_MainCurve.m_ToeCoef);
		this.m_Material.SetVector("_Shoulder", this.m_MainCurve.m_ShoulderCoef);
		this.m_Material.SetFloat("_LuminosityWhite", this.m_MainCurve.m_LuminositySaturationPoint * this.m_MainCurve.m_LuminositySaturationPoint);
		this.m_Material.SetFloat("_Sharpness", this.m_Sharpness);
		this.m_Material.SetFloat("_Saturation", this.m_Saturation);
		if (this.m_2DLut0 != null && this.m_3DLut0 == null && this.LutTool.Validate2DLut(this.m_2DLut0))
		{
			this.ConvertAndApply2dLut();
		}
		float value = (QualitySettings.activeColorSpace != ColorSpace.Linear) ? 1f : 0.5f;
		if (this.m_Use2DLut && this.m_2DLut0 != null)
		{
			this.m_2DLut0.filterMode = FilterMode.Bilinear;
			float num = Mathf.Sqrt((float)this.m_2DLut0.width);
			this.m_Material.SetTexture("_2dLut0", this.m_2DLut0);
			this.m_Material.SetVector("_Lut0Params", new Vector4(this.m_Lut0Amount, 1f / (float)this.m_2DLut0.width, 1f / (float)this.m_2DLut0.height, num - 1f));
			this.m_Material.SetFloat("_IsLinear", value);
		}
		else if (this.m_3DLut0 != null)
		{
			int width = this.m_3DLut0.width;
			float y = (float)(width - 1) / (1f * (float)width);
			float z = 1f / (2f * (float)width);
			this.m_3DLut0.filterMode = FilterMode.Trilinear;
			this.m_Material.SetVector("_Lut0Params", new Vector4(this.m_Lut0Amount, y, z, 0f));
			this.m_Material.SetTexture("_Lut0", this.m_3DLut0);
			this.m_Material.SetFloat("_IsLinear", value);
		}
		else
		{
			this.m_Material.SetVector("_Lut0Params", Vector4.zero);
		}
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x000371EC File Offset: 0x000353EC
	public void ReloadShaders()
	{
		this.OnDisable();
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x000371F4 File Offset: 0x000353F4
	public void SetCurveParameters(float range, float crossover, float toe, float shoulder)
	{
		this.SetCurveParameters(0f, range, crossover, toe, shoulder);
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x00037208 File Offset: 0x00035408
	public void SetCurveParameters(float crossover, float toe, float shoulder)
	{
		this.SetCurveParameters(0f, 1f, crossover, toe, shoulder);
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x00037220 File Offset: 0x00035420
	public void SetCurveParameters(float blackPoint, float whitePoint, float crossover, float toe, float shoulder)
	{
		this.m_MainCurve.m_BlackPoint = blackPoint;
		this.m_MainCurve.m_WhitePoint = whitePoint;
		this.m_MainCurve.m_ToeStrength = -1f * toe;
		this.m_MainCurve.m_ShoulderStrength = shoulder;
		this.m_MainCurve.m_CrossOverPoint = crossover * (whitePoint - blackPoint) + blackPoint;
		this.UpdateCoefficients();
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x00037280 File Offset: 0x00035480
	private void OnEnable()
	{
		if (this.m_MainCurve == null)
		{
			this.m_MainCurve = new FilmicCurve();
		}
		this.CreateMaterials();
		this.UpdateCoefficients();
		this.VerifyAndUpdateShaderVariations(true);
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x000372AC File Offset: 0x000354AC
	private void Initialize()
	{
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x000372B0 File Offset: 0x000354B0
	[ImageEffectTransformsToLDR]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.Initialize();
		if (this.m_Mode != this.m_LastMode)
		{
			this.ReloadShaders();
			this.CreateMaterials();
			this.VerifyAndUpdateShaderVariations(true);
		}
		this.m_LastMode = this.m_Mode;
		this.UpdateCoefficients();
		this.VerifyAndUpdateShaderVariations(false);
		this.m_Material.SetVector("_Tint", this.m_Tint);
		this.m_Material.SetVector("_Offsets", new Vector4(this.m_SharpnessKernel / (float)source.width, this.m_SharpnessKernel / (float)source.height));
		if (this.m_EnableLut0)
		{
			Graphics.Blit(source, destination, this.m_Material, 1);
		}
		else
		{
			Graphics.Blit(source, destination, this.m_Material, 0);
		}
	}

	// Token: 0x04000688 RID: 1672
	[SerializeField]
	public FilmicCurve m_MainCurve = new FilmicCurve();

	// Token: 0x04000689 RID: 1673
	[SerializeField]
	public Color m_Tint = new Color(1f, 1f, 1f, 1f);

	// Token: 0x0400068A RID: 1674
	[SerializeField]
	public float m_MoodAmount;

	// Token: 0x0400068B RID: 1675
	[SerializeField]
	public float m_Sharpness;

	// Token: 0x0400068C RID: 1676
	[SerializeField]
	public float m_SharpnessKernel = 0.5f;

	// Token: 0x0400068D RID: 1677
	[SerializeField]
	public DeluxeTonemapper.Mode m_Mode = DeluxeTonemapper.Mode.ColorHD;

	// Token: 0x0400068E RID: 1678
	public bool m_EnableLut0;

	// Token: 0x0400068F RID: 1679
	public float m_Saturation = 1f;

	// Token: 0x04000690 RID: 1680
	public bool m_DisableColorGrading;

	// Token: 0x04000691 RID: 1681
	private bool m_LastDisableColorGrading;

	// Token: 0x04000692 RID: 1682
	public float m_Lut0Amount = 1f;

	// Token: 0x04000693 RID: 1683
	public Texture2D m_2DLut0;

	// Token: 0x04000694 RID: 1684
	public Texture3D m_3DLut0;

	// Token: 0x04000695 RID: 1685
	private FTDLut m_LutTool = new FTDLut();

	// Token: 0x04000696 RID: 1686
	public bool m_FilmicCurveOpened = true;

	// Token: 0x04000697 RID: 1687
	public bool m_ColorGradingOpened;

	// Token: 0x04000698 RID: 1688
	public bool m_HDREffectsgOpened;

	// Token: 0x04000699 RID: 1689
	public bool m_BannerIsOpened = true;

	// Token: 0x0400069A RID: 1690
	public bool m_Use2DLut;

	// Token: 0x0400069B RID: 1691
	private bool m_LastUse2DLut;

	// Token: 0x0400069C RID: 1692
	private bool m_IsInitialized;

	// Token: 0x0400069D RID: 1693
	private DeluxeTonemapper.Mode m_LastMode;

	// Token: 0x0400069E RID: 1694
	private Material m_Material;

	// Token: 0x0400069F RID: 1695
	private Shader m_Shader;

	// Token: 0x02000172 RID: 370
	public enum Mode
	{
		// Token: 0x040006A1 RID: 1697
		Color,
		// Token: 0x040006A2 RID: 1698
		Luminance,
		// Token: 0x040006A3 RID: 1699
		ExtendedLuminance,
		// Token: 0x040006A4 RID: 1700
		ColorHD
	}
}
