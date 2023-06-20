using System;
using System.Collections.Generic;
using AmplifyColor;
using UnityEngine;

// Token: 0x02000006 RID: 6
[AddComponentMenu("")]
public class AmplifyColorBase : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x0600000C RID: 12 RVA: 0x000026B8 File Offset: 0x000008B8
	public Texture2D DefaultLut
	{
		get
		{
			return (!(this.defaultLut == null)) ? this.defaultLut : this.CreateDefaultLut();
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600000D RID: 13 RVA: 0x000026E8 File Offset: 0x000008E8
	public bool IsBlending
	{
		get
		{
			return this.blending;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600000E RID: 14 RVA: 0x000026F0 File Offset: 0x000008F0
	private float effectVolumesBlendAdjusted
	{
		get
		{
			return Mathf.Clamp01((this.effectVolumesBlendAdjust >= 0.99f) ? 1f : ((this.volumesBlendAmount - this.effectVolumesBlendAdjust) / (1f - this.effectVolumesBlendAdjust)));
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x0600000F RID: 15 RVA: 0x0000272C File Offset: 0x0000092C
	public string SharedInstanceID
	{
		get
		{
			return this.sharedInstanceID;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000010 RID: 16 RVA: 0x00002734 File Offset: 0x00000934
	public bool WillItBlend
	{
		get
		{
			return this.LutTexture != null && this.LutBlendTexture != null && !this.blending;
		}
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002770 File Offset: 0x00000970
	public void NewSharedInstanceID()
	{
		this.sharedInstanceID = Guid.NewGuid().ToString();
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002790 File Offset: 0x00000990
	private void ReportMissingShaders()
	{
		Debug.LogError("[AmplifyColor] Failed to initialize shaders. Please attempt to re-enable the Amplify Color Effect component. If that fails, please reinstall Amplify Color.");
	}

	// Token: 0x06000013 RID: 19 RVA: 0x0000279C File Offset: 0x0000099C
	private void ReportNotSupported()
	{
		Debug.LogError("[AmplifyColor] This image effect is not supported on this platform. Please make sure your Unity license supports Full-Screen Post-Processing Effects which is usually reserved forn Pro licenses.");
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000027A8 File Offset: 0x000009A8
	private bool CheckShader(Shader s)
	{
		if (s == null)
		{
			this.ReportMissingShaders();
			return false;
		}
		if (!s.isSupported)
		{
			this.ReportNotSupported();
			return false;
		}
		return true;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000027E0 File Offset: 0x000009E0
	private bool CheckShaders()
	{
		return this.CheckShader(this.shaderBase) && this.CheckShader(this.shaderBlend) && this.CheckShader(this.shaderBlendCache) && this.CheckShader(this.shaderMask) && this.CheckShader(this.shaderBlendMask);
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002840 File Offset: 0x00000A40
	private bool CheckSupport()
	{
		if (!SystemInfo.supportsImageEffects || !SystemInfo.supportsRenderTextures)
		{
			this.ReportNotSupported();
			return false;
		}
		return true;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002860 File Offset: 0x00000A60
	private void OnEnable()
	{
		if (!this.CheckSupport())
		{
			return;
		}
		if (!this.CreateMaterials())
		{
			return;
		}
		Texture2D texture2D = this.LutTexture as Texture2D;
		Texture2D texture2D2 = this.LutBlendTexture as Texture2D;
		if ((texture2D != null && texture2D.mipmapCount > 1) || (texture2D2 != null && texture2D2.mipmapCount > 1))
		{
			Debug.LogError("[AmplifyColor] Please disable \"Generate Mip Maps\" import settings on all LUT textures to avoid visual glitches. Change Texture Type to \"Advanced\" to access Mip settings.");
		}
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000028D8 File Offset: 0x00000AD8
	private void OnDisable()
	{
		if (this.actualTriggerProxy != null)
		{
			UnityEngine.Object.DestroyImmediate(this.actualTriggerProxy.gameObject);
			this.actualTriggerProxy = null;
		}
		this.ReleaseMaterials();
		this.ReleaseTextures();
	}

	// Token: 0x06000019 RID: 25 RVA: 0x0000291C File Offset: 0x00000B1C
	private void VolumesBlendTo(Texture blendTargetLUT, float blendTimeInSec)
	{
		this.volumesLutBlendTexture = blendTargetLUT;
		this.volumesBlendAmount = 0f;
		this.volumesBlendingTime = blendTimeInSec;
		this.volumesBlendingTimeCountdown = blendTimeInSec;
		this.volumesBlending = true;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002948 File Offset: 0x00000B48
	public void BlendTo(Texture blendTargetLUT, float blendTimeInSec, Action onFinishBlend)
	{
		this.LutBlendTexture = blendTargetLUT;
		this.BlendAmount = 0f;
		this.onFinishBlend = onFinishBlend;
		this.blendingTime = blendTimeInSec;
		this.blendingTimeCountdown = blendTimeInSec;
		this.blending = true;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002984 File Offset: 0x00000B84
	private void Start()
	{
		this.worldLUT = this.LutTexture;
		this.worldVolumeEffects = this.EffectFlags.GenerateEffectData(this);
		this.blendVolumeEffects = (this.currentVolumeEffects = this.worldVolumeEffects);
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000029C4 File Offset: 0x00000BC4
	private void Update()
	{
		if (this.volumesBlending)
		{
			this.volumesBlendAmount = (this.volumesBlendingTime - this.volumesBlendingTimeCountdown) / this.volumesBlendingTime;
			this.volumesBlendingTimeCountdown -= Time.smoothDeltaTime;
			if (this.volumesBlendAmount >= 1f)
			{
				this.LutTexture = this.volumesLutBlendTexture;
				this.volumesBlendAmount = 0f;
				this.volumesBlending = false;
				this.volumesLutBlendTexture = null;
				this.effectVolumesBlendAdjust = 0f;
				this.currentVolumeEffects = this.blendVolumeEffects;
				this.currentVolumeEffects.SetValues(this);
				if (this.blendingFromMidBlend && this.midBlendLUT != null)
				{
					this.midBlendLUT.DiscardContents();
				}
				this.blendingFromMidBlend = false;
			}
		}
		else
		{
			this.volumesBlendAmount = Mathf.Clamp01(this.volumesBlendAmount);
		}
		if (this.blending)
		{
			this.BlendAmount = (this.blendingTime - this.blendingTimeCountdown) / this.blendingTime;
			this.blendingTimeCountdown -= Time.smoothDeltaTime;
			if (this.BlendAmount >= 1f)
			{
				this.LutTexture = this.LutBlendTexture;
				this.BlendAmount = 0f;
				this.blending = false;
				this.LutBlendTexture = null;
				if (this.onFinishBlend != null)
				{
					this.onFinishBlend();
				}
			}
		}
		else
		{
			this.BlendAmount = Mathf.Clamp01(this.BlendAmount);
		}
		if (this.UseVolumes)
		{
			if (this.actualTriggerProxy == null)
			{
				GameObject gameObject = new GameObject(base.name + "+ACVolumeProxy")
				{
					hideFlags = HideFlags.HideAndDontSave
				};
				this.actualTriggerProxy = gameObject.AddComponent<AmplifyColorTriggerProxy>();
				this.actualTriggerProxy.OwnerEffect = this;
			}
			this.UpdateVolumes();
		}
		else if (this.actualTriggerProxy != null)
		{
			UnityEngine.Object.DestroyImmediate(this.actualTriggerProxy.gameObject);
			this.actualTriggerProxy = null;
		}
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002BC4 File Offset: 0x00000DC4
	public void EnterVolume(AmplifyColorVolumeBase volume)
	{
		if (!this.enteredVolumes.Contains(volume))
		{
			this.enteredVolumes.Insert(0, volume);
		}
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002BE4 File Offset: 0x00000DE4
	public void ExitVolume(AmplifyColorVolumeBase volume)
	{
		if (this.enteredVolumes.Contains(volume))
		{
			this.enteredVolumes.Remove(volume);
		}
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002C04 File Offset: 0x00000E04
	private void UpdateVolumes()
	{
		if (this.volumesBlending)
		{
			this.currentVolumeEffects.BlendValues(this, this.blendVolumeEffects, this.effectVolumesBlendAdjusted);
		}
		Transform transform = (!(this.TriggerVolumeProxy == null)) ? this.TriggerVolumeProxy : base.transform;
		if (this.actualTriggerProxy.transform.parent != transform)
		{
			this.actualTriggerProxy.Reference = transform;
			this.actualTriggerProxy.gameObject.layer = transform.gameObject.layer;
		}
		AmplifyColorVolumeBase amplifyColorVolumeBase = null;
		int num = int.MinValue;
		foreach (AmplifyColorVolumeBase amplifyColorVolumeBase2 in this.enteredVolumes)
		{
			if (amplifyColorVolumeBase2.Priority > num)
			{
				amplifyColorVolumeBase = amplifyColorVolumeBase2;
				num = amplifyColorVolumeBase2.Priority;
			}
		}
		if (amplifyColorVolumeBase != this.currentVolumeLut)
		{
			this.currentVolumeLut = amplifyColorVolumeBase;
			Texture texture = (!(amplifyColorVolumeBase == null)) ? amplifyColorVolumeBase.LutTexture : this.worldLUT;
			float num2 = (!(amplifyColorVolumeBase == null)) ? amplifyColorVolumeBase.EnterBlendTime : this.ExitVolumeBlendTime;
			if (this.volumesBlending && !this.blendingFromMidBlend && texture == this.LutTexture)
			{
				this.LutTexture = this.volumesLutBlendTexture;
				this.volumesLutBlendTexture = texture;
				this.volumesBlendingTimeCountdown = num2 * ((this.volumesBlendingTime - this.volumesBlendingTimeCountdown) / this.volumesBlendingTime);
				this.volumesBlendingTime = num2;
				this.currentVolumeEffects = VolumeEffect.BlendValuesToVolumeEffect(this.EffectFlags, this.currentVolumeEffects, this.blendVolumeEffects, this.effectVolumesBlendAdjusted);
				this.effectVolumesBlendAdjust = 1f - this.volumesBlendAmount;
				this.volumesBlendAmount = 1f - this.volumesBlendAmount;
			}
			else
			{
				if (this.volumesBlending)
				{
					this.materialBlendCache.SetFloat("_lerpAmount", this.volumesBlendAmount);
					if (this.blendingFromMidBlend)
					{
						Graphics.Blit(this.midBlendLUT, this.blendCacheLut);
						this.materialBlendCache.SetTexture("_RgbTex", this.blendCacheLut);
					}
					else
					{
						this.materialBlendCache.SetTexture("_RgbTex", this.LutTexture);
					}
					this.materialBlendCache.SetTexture("_LerpRgbTex", (!(this.volumesLutBlendTexture != null)) ? this.defaultLut : this.volumesLutBlendTexture);
					Graphics.Blit(this.midBlendLUT, this.midBlendLUT, this.materialBlendCache);
					this.blendCacheLut.DiscardContents();
					this.currentVolumeEffects = VolumeEffect.BlendValuesToVolumeEffect(this.EffectFlags, this.currentVolumeEffects, this.blendVolumeEffects, this.effectVolumesBlendAdjusted);
					this.effectVolumesBlendAdjust = 0f;
					this.blendingFromMidBlend = true;
				}
				this.VolumesBlendTo(texture, num2);
			}
			this.blendVolumeEffects = ((!(amplifyColorVolumeBase == null)) ? amplifyColorVolumeBase.EffectContainer.GetVolumeEffect(this) : this.worldVolumeEffects);
			if (this.blendVolumeEffects == null)
			{
				this.blendVolumeEffects = this.worldVolumeEffects;
			}
		}
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002F50 File Offset: 0x00001150
	private void SetupShader()
	{
		this.colorSpace = QualitySettings.activeColorSpace;
		this.qualityLevel = this.QualityLevel;
		string str = (this.colorSpace != ColorSpace.Linear) ? string.Empty : "Linear";
		this.shaderBase = Shader.Find("Hidden/Amplify Color/Base" + str);
		this.shaderBlend = Shader.Find("Hidden/Amplify Color/Blend" + str);
		this.shaderBlendCache = Shader.Find("Hidden/Amplify Color/BlendCache");
		this.shaderMask = Shader.Find("Hidden/Amplify Color/Mask" + str);
		this.shaderBlendMask = Shader.Find("Hidden/Amplify Color/BlendMask" + str);
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002FF8 File Offset: 0x000011F8
	private void ReleaseMaterials()
	{
		if (this.materialBase != null)
		{
			UnityEngine.Object.DestroyImmediate(this.materialBase);
			this.materialBase = null;
		}
		if (this.materialBlend != null)
		{
			UnityEngine.Object.DestroyImmediate(this.materialBlend);
			this.materialBlend = null;
		}
		if (this.materialBlendCache != null)
		{
			UnityEngine.Object.DestroyImmediate(this.materialBlendCache);
			this.materialBlendCache = null;
		}
		if (this.materialMask != null)
		{
			UnityEngine.Object.DestroyImmediate(this.materialMask);
			this.materialMask = null;
		}
		if (this.materialBlendMask != null)
		{
			UnityEngine.Object.DestroyImmediate(this.materialBlendMask);
			this.materialBlendMask = null;
		}
	}

	// Token: 0x06000022 RID: 34 RVA: 0x000030B4 File Offset: 0x000012B4
	private Texture2D CreateDefaultLut()
	{
		this.defaultLut = new Texture2D(1024, 32, TextureFormat.RGB24, false, true)
		{
			hideFlags = HideFlags.HideAndDontSave
		};
		this.defaultLut.name = "DefaultLut";
		this.defaultLut.hideFlags = HideFlags.DontSave;
		this.defaultLut.anisoLevel = 1;
		this.defaultLut.filterMode = FilterMode.Bilinear;
		Color32[] array = new Color32[32768];
		for (int i = 0; i < 32; i++)
		{
			int num = i * 32;
			for (int j = 0; j < 32; j++)
			{
				int num2 = num + j * 1024;
				for (int k = 0; k < 32; k++)
				{
					float num3 = (float)k / 31f;
					float num4 = (float)j / 31f;
					float num5 = (float)i / 31f;
					byte r = (byte)(num3 * 255f);
					byte g = (byte)(num4 * 255f);
					byte b = (byte)(num5 * 255f);
					array[num2 + k] = new Color32(r, g, b, byte.MaxValue);
				}
			}
		}
		this.defaultLut.SetPixels32(array);
		this.defaultLut.Apply();
		return this.defaultLut;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x000031F4 File Offset: 0x000013F4
	private void CreateHelperTextures()
	{
		this.ReleaseTextures();
		this.blendCacheLut = new RenderTexture(1024, 32, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear)
		{
			hideFlags = HideFlags.HideAndDontSave
		};
		this.blendCacheLut.name = "BlendCacheLut";
		this.blendCacheLut.wrapMode = TextureWrapMode.Clamp;
		this.blendCacheLut.useMipMap = false;
		this.blendCacheLut.anisoLevel = 0;
		this.blendCacheLut.Create();
		this.midBlendLUT = new RenderTexture(1024, 32, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear)
		{
			hideFlags = HideFlags.HideAndDontSave
		};
		this.midBlendLUT.name = "MidBlendLut";
		this.midBlendLUT.wrapMode = TextureWrapMode.Clamp;
		this.midBlendLUT.useMipMap = false;
		this.midBlendLUT.anisoLevel = 0;
		this.midBlendLUT.Create();
		this.CreateDefaultLut();
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000032CC File Offset: 0x000014CC
	private bool CheckMaterialAndShader(Material material, string name)
	{
		if (material == null || material.shader == null)
		{
			Debug.LogError("[AmplifyColor] Error creating " + name + " material. Effect disabled.");
		}
		else if (!material.shader.isSupported)
		{
			Debug.LogError("[AmplifyColor] " + name + " shader not supported on this platform. Effect disabled.");
		}
		else
		{
			material.hideFlags = HideFlags.HideAndDontSave;
		}
		return base.enabled;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00003348 File Offset: 0x00001548
	private void SwitchToMobile(Material mat)
	{
		mat.EnableKeyword("AC_QUALITY_MOBILE");
		mat.DisableKeyword("AC_QUALITY_STANDARD");
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00003360 File Offset: 0x00001560
	private void SwitchToStandard(Material mat)
	{
		mat.EnableKeyword("AC_QUALITY_STANDARD");
		mat.DisableKeyword("AC_QUALITY_MOBILE");
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00003378 File Offset: 0x00001578
	private bool CreateMaterials()
	{
		this.SetupShader();
		if (!this.CheckShaders())
		{
			return false;
		}
		this.ReleaseMaterials();
		this.materialBase = new Material(this.shaderBase);
		this.materialBlend = new Material(this.shaderBlend);
		this.materialBlendCache = new Material(this.shaderBlendCache);
		this.materialMask = new Material(this.shaderMask);
		this.materialBlendMask = new Material(this.shaderBlendMask);
		bool flag = true;
		flag = (flag && this.CheckMaterialAndShader(this.materialBase, "BaseMaterial"));
		flag = (flag && this.CheckMaterialAndShader(this.materialBlend, "BlendMaterial"));
		flag = (flag && this.CheckMaterialAndShader(this.materialBlendCache, "BlendCacheMaterial"));
		flag = (flag && this.CheckMaterialAndShader(this.materialMask, "MaskMaterial"));
		if (!flag || !this.CheckMaterialAndShader(this.materialBlendMask, "BlendMaskMaterial"))
		{
			return false;
		}
		if (this.QualityLevel == Quality.Mobile)
		{
			this.SwitchToMobile(this.materialBase);
			this.SwitchToMobile(this.materialBlend);
			this.SwitchToMobile(this.materialBlendCache);
			this.SwitchToMobile(this.materialMask);
			this.SwitchToMobile(this.materialBlendMask);
		}
		else
		{
			this.SwitchToStandard(this.materialBase);
			this.SwitchToStandard(this.materialBlend);
			this.SwitchToStandard(this.materialBlendCache);
			this.SwitchToStandard(this.materialMask);
			this.SwitchToStandard(this.materialBlendMask);
		}
		this.CreateHelperTextures();
		return true;
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00003514 File Offset: 0x00001714
	private void ReleaseTextures()
	{
		if (this.blendCacheLut != null)
		{
			UnityEngine.Object.DestroyImmediate(this.blendCacheLut);
			this.blendCacheLut = null;
		}
		if (this.midBlendLUT != null)
		{
			UnityEngine.Object.DestroyImmediate(this.midBlendLUT);
			this.midBlendLUT = null;
		}
		if (this.defaultLut != null)
		{
			UnityEngine.Object.DestroyImmediate(this.defaultLut);
			this.defaultLut = null;
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x0000358C File Offset: 0x0000178C
	public static bool ValidateLutDimensions(Texture lut)
	{
		bool result = true;
		if (lut != null)
		{
			if (lut.width / lut.height != lut.height)
			{
				Debug.LogWarning("[AmplifyColor] Lut " + lut.name + " has invalid dimensions.");
				result = false;
			}
			else if (lut.anisoLevel != 0)
			{
				lut.anisoLevel = 0;
			}
		}
		return result;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x000035F4 File Offset: 0x000017F4
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.BlendAmount = Mathf.Clamp01(this.BlendAmount);
		if (this.colorSpace != QualitySettings.activeColorSpace || this.qualityLevel != this.QualityLevel)
		{
			this.CreateMaterials();
		}
		bool flag = AmplifyColorBase.ValidateLutDimensions(this.LutTexture);
		bool flag2 = AmplifyColorBase.ValidateLutDimensions(this.LutBlendTexture);
		bool flag3 = this.LutTexture == null && this.LutBlendTexture == null && this.volumesLutBlendTexture == null;
		if (!flag || !flag2 || flag3)
		{
			Graphics.Blit(source, destination);
			return;
		}
		Texture texture = (!(this.LutTexture == null)) ? this.LutTexture : this.defaultLut;
		Texture lutBlendTexture = this.LutBlendTexture;
		int pass = base.GetComponent<Camera>().hdr ? 1 : 0;
		bool flag4 = this.BlendAmount != 0f || this.blending;
		bool flag5 = flag4 || (flag4 && lutBlendTexture != null);
		bool flag6 = flag5;
		Material material;
		if (flag5 || this.volumesBlending)
		{
			if (this.MaskTexture != null)
			{
				material = this.materialBlendMask;
			}
			else
			{
				material = this.materialBlend;
			}
		}
		else if (this.MaskTexture != null)
		{
			material = this.materialMask;
		}
		else
		{
			material = this.materialBase;
		}
		material.SetFloat("_lerpAmount", this.BlendAmount);
		if (this.MaskTexture != null)
		{
			material.SetTexture("_MaskTex", this.MaskTexture);
		}
		if (this.volumesBlending)
		{
			this.volumesBlendAmount = Mathf.Clamp01(this.volumesBlendAmount);
			this.materialBlendCache.SetFloat("_lerpAmount", this.volumesBlendAmount);
			if (this.blendingFromMidBlend)
			{
				this.materialBlendCache.SetTexture("_RgbTex", this.midBlendLUT);
			}
			else
			{
				this.materialBlendCache.SetTexture("_RgbTex", texture);
			}
			this.materialBlendCache.SetTexture("_LerpRgbTex", (!(this.volumesLutBlendTexture != null)) ? this.defaultLut : this.volumesLutBlendTexture);
			Graphics.Blit(texture, this.blendCacheLut, this.materialBlendCache);
		}
		if (flag6)
		{
			this.materialBlendCache.SetFloat("_lerpAmount", this.BlendAmount);
			RenderTexture renderTexture = null;
			if (this.volumesBlending)
			{
				renderTexture = RenderTexture.GetTemporary(this.blendCacheLut.width, this.blendCacheLut.height, this.blendCacheLut.depth, this.blendCacheLut.format, RenderTextureReadWrite.Linear);
				Graphics.Blit(this.blendCacheLut, renderTexture);
				this.materialBlendCache.SetTexture("_RgbTex", renderTexture);
			}
			else
			{
				this.materialBlendCache.SetTexture("_RgbTex", texture);
			}
			this.materialBlendCache.SetTexture("_LerpRgbTex", (!(lutBlendTexture != null)) ? this.defaultLut : lutBlendTexture);
			Graphics.Blit(texture, this.blendCacheLut, this.materialBlendCache);
			if (renderTexture != null)
			{
				RenderTexture.ReleaseTemporary(renderTexture);
			}
			material.SetTexture("_RgbBlendCacheTex", this.blendCacheLut);
		}
		else if (this.volumesBlending)
		{
			material.SetTexture("_RgbBlendCacheTex", this.blendCacheLut);
		}
		else
		{
			if (texture != null)
			{
				material.SetTexture("_RgbTex", texture);
			}
			if (lutBlendTexture != null)
			{
				material.SetTexture("_LerpRgbTex", lutBlendTexture);
			}
		}
		Graphics.Blit(source, destination, material, pass);
		if (flag6 || this.volumesBlending)
		{
			this.blendCacheLut.DiscardContents();
		}
	}

	// Token: 0x0400001B RID: 27
	public const int LutSize = 32;

	// Token: 0x0400001C RID: 28
	public const int LutWidth = 1024;

	// Token: 0x0400001D RID: 29
	public const int LutHeight = 32;

	// Token: 0x0400001E RID: 30
	public Quality QualityLevel = Quality.Standard;

	// Token: 0x0400001F RID: 31
	public float BlendAmount;

	// Token: 0x04000020 RID: 32
	public Texture LutTexture;

	// Token: 0x04000021 RID: 33
	public Texture LutBlendTexture;

	// Token: 0x04000022 RID: 34
	public Texture MaskTexture;

	// Token: 0x04000023 RID: 35
	public bool UseVolumes;

	// Token: 0x04000024 RID: 36
	public float ExitVolumeBlendTime = 1f;

	// Token: 0x04000025 RID: 37
	public Transform TriggerVolumeProxy;

	// Token: 0x04000026 RID: 38
	public LayerMask VolumeCollisionMask = -1;

	// Token: 0x04000027 RID: 39
	private Shader shaderBase;

	// Token: 0x04000028 RID: 40
	private Shader shaderBlend;

	// Token: 0x04000029 RID: 41
	private Shader shaderBlendCache;

	// Token: 0x0400002A RID: 42
	private Shader shaderMask;

	// Token: 0x0400002B RID: 43
	private Shader shaderBlendMask;

	// Token: 0x0400002C RID: 44
	private RenderTexture blendCacheLut;

	// Token: 0x0400002D RID: 45
	private Texture2D defaultLut;

	// Token: 0x0400002E RID: 46
	private ColorSpace colorSpace = ColorSpace.Uninitialized;

	// Token: 0x0400002F RID: 47
	private Quality qualityLevel = Quality.Standard;

	// Token: 0x04000030 RID: 48
	private Material materialBase;

	// Token: 0x04000031 RID: 49
	private Material materialBlend;

	// Token: 0x04000032 RID: 50
	private Material materialBlendCache;

	// Token: 0x04000033 RID: 51
	private Material materialMask;

	// Token: 0x04000034 RID: 52
	private Material materialBlendMask;

	// Token: 0x04000035 RID: 53
	private bool blending;

	// Token: 0x04000036 RID: 54
	private float blendingTime;

	// Token: 0x04000037 RID: 55
	private float blendingTimeCountdown;

	// Token: 0x04000038 RID: 56
	private Action onFinishBlend;

	// Token: 0x04000039 RID: 57
	private bool volumesBlending;

	// Token: 0x0400003A RID: 58
	private float volumesBlendingTime;

	// Token: 0x0400003B RID: 59
	private float volumesBlendingTimeCountdown;

	// Token: 0x0400003C RID: 60
	private Texture volumesLutBlendTexture;

	// Token: 0x0400003D RID: 61
	private float volumesBlendAmount;

	// Token: 0x0400003E RID: 62
	private Texture worldLUT;

	// Token: 0x0400003F RID: 63
	private AmplifyColorVolumeBase currentVolumeLut;

	// Token: 0x04000040 RID: 64
	private RenderTexture midBlendLUT;

	// Token: 0x04000041 RID: 65
	private bool blendingFromMidBlend;

	// Token: 0x04000042 RID: 66
	private VolumeEffect worldVolumeEffects;

	// Token: 0x04000043 RID: 67
	private VolumeEffect currentVolumeEffects;

	// Token: 0x04000044 RID: 68
	private VolumeEffect blendVolumeEffects;

	// Token: 0x04000045 RID: 69
	private float effectVolumesBlendAdjust;

	// Token: 0x04000046 RID: 70
	private List<AmplifyColorVolumeBase> enteredVolumes = new List<AmplifyColorVolumeBase>();

	// Token: 0x04000047 RID: 71
	private AmplifyColorTriggerProxy actualTriggerProxy;

	// Token: 0x04000048 RID: 72
	[HideInInspector]
	public VolumeEffectFlags EffectFlags = new VolumeEffectFlags();

	// Token: 0x04000049 RID: 73
	[SerializeField]
	[HideInInspector]
	private string sharedInstanceID = string.Empty;
}
