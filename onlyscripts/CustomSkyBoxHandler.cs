using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020001FA RID: 506
public class CustomSkyBoxHandler : MonoBehaviour
{
	// Token: 0x17000250 RID: 592
	// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00049EFC File Offset: 0x000480FC
	public int CurrentCustomSkyboxType
	{
		get
		{
			return this.skyBoxType.value;
		}
	}

	// Token: 0x17000251 RID: 593
	// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00049F0C File Offset: 0x0004810C
	private Material GradientSkyBox
	{
		get
		{
			return levelEditorManager.Instance().GradientSkyBox;
		}
	}

	// Token: 0x17000252 RID: 594
	// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00049F18 File Offset: 0x00048118
	private Material HorizonSkyBox
	{
		get
		{
			return levelEditorManager.Instance().HorizonSkyBox;
		}
	}

	// Token: 0x17000253 RID: 595
	// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x00049F24 File Offset: 0x00048124
	public static CustomSkyBoxHandler Instance
	{
		get
		{
			return CustomSkyBoxHandler._CustomSkyBoxHandler;
		}
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x00049F2C File Offset: 0x0004812C
	private void Awake()
	{
		if (CustomSkyBoxHandler._CustomSkyBoxHandler)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		CustomSkyBoxHandler._CustomSkyBoxHandler = this;
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x00049F4C File Offset: 0x0004814C
	private void UpdateSkyBoxColorsAndSliders()
	{
		this.grad_topColor.color = extensionMethods.newAlphaColor(this.GradientSkyBox.GetColor("_Color2"), 1f);
		this.grad_bottomColor.color = extensionMethods.newAlphaColor(this.GradientSkyBox.GetColor("_Color1"), 1f);
		this.grad_exponent.value = this.GradientSkyBox.GetFloat("_Exponent");
		this.grad_intensity.value = this.GradientSkyBox.GetFloat("_Intensity");
		this.grad_pitch.value = this.GradientSkyBox.GetFloat("_UpVectorPitch");
		this.grad_yaw.value = this.GradientSkyBox.GetFloat("_UpVectorYaw");
		this.hori_topColor.color = extensionMethods.newAlphaColor(this.HorizonSkyBox.GetColor("_SkyColor1"), 1f);
		this.hori_horizonColor.color = extensionMethods.newAlphaColor(this.HorizonSkyBox.GetColor("_SkyColor2"), 1f);
		this.hori_bottomColor.color = extensionMethods.newAlphaColor(this.HorizonSkyBox.GetColor("_SkyColor3"), 1f);
		this.horiSun_Color.color = extensionMethods.newAlphaColor(this.HorizonSkyBox.GetColor("_SunColor"), 1f);
		this.horiSun_Intensity.value = this.HorizonSkyBox.GetFloat("_SunIntensity");
		this.horiSun_Alpha.value = this.HorizonSkyBox.GetFloat("_SunAlpha");
		this.horiSun_Beta.value = this.HorizonSkyBox.GetFloat("_SunBeta");
		this.horiSun_Azimuth.value = this.HorizonSkyBox.GetFloat("_SunAzimuth");
		this.horiSun_Altitude.value = this.HorizonSkyBox.GetFloat("_SunAltitude");
		this.hori_bottomFactor.value = this.HorizonSkyBox.GetFloat("_SkyExponent2");
		this.hori_intensity.value = this.HorizonSkyBox.GetFloat("_SkyIntensity");
		this.hori_topFactor.value = this.HorizonSkyBox.GetFloat("_SkyExponent1");
		foreach (Slider slider in base.GetComponentsInChildren<Slider>())
		{
			slider.SendMessage("OnValueChanged");
		}
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x0004A1A8 File Offset: 0x000483A8
	private void Start()
	{
		this.colorPicker.onValueChanged.AddListener(new UnityAction<Color>(this.OnColorPickerChanged));
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x0004A1C8 File Offset: 0x000483C8
	private void OnEnable()
	{
		this.UpdateSkyBoxColorsAndSliders();
		Debug.Log(settingsPanelOnSet.Instance().CurrentSkybox);
		if (settingsPanelOnSet.Instance().CurrentSkybox == settingsPanelOnSet.Instance().SkyboxDropdown.options.Count + 1)
		{
			this.skyBoxType.value = 1;
			this.ActivateMenu(this.horizonMenu);
		}
		else
		{
			this.skyBoxType.value = 0;
			this.ActivateMenu(this.gradientMenu);
		}
		this.skyBoxType.RefreshShownValue();
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x0004A254 File Offset: 0x00048454
	private void Update()
	{
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x0004A258 File Offset: 0x00048458
	public void OnSkyBoxTypeChanged()
	{
		if (this.skyBoxType.value == 0)
		{
			this.ActivateMenu(this.gradientMenu);
		}
		else
		{
			this.ActivateMenu(this.horizonMenu);
		}
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x0004A288 File Offset: 0x00048488
	private void ActivateMenu(GameObject menu)
	{
		this.deActivateColorPicker();
		this.gradientMenu.SetActive(menu == this.gradientMenu);
		this.horizonMenu.SetActive(!this.gradientMenu.activeSelf);
		RenderSettings.skybox = ((!this.gradientMenu.activeSelf) ? this.HorizonSkyBox : this.GradientSkyBox);
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x0004A300 File Offset: 0x00048500
	public void OnSubmit(BaseEventData b)
	{
		if (!this._currentColorButton)
		{
			this.CHANGEFLAG = true;
			this._currentColorButton = b.selectedObject.GetComponent<Image>();
			Debug.Log("Current First Button!", this._currentColorButton);
			this.activateColorPicker();
			return;
		}
		if (this._currentColorButton.Equals(b.selectedObject.GetComponent<Image>()))
		{
			this.deActivateColorPicker();
			return;
		}
		this.CHANGEFLAG = true;
		this._currentColorButton = b.selectedObject.GetComponent<Image>();
		Debug.Log("Current Button!", this._currentColorButton);
		this.activateColorPicker();
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x0004A39C File Offset: 0x0004859C
	private void OnColorPickerChanged(Color newColor)
	{
		if (!this.CHANGEFLAG)
		{
			this._currentColorButton.color = newColor;
			if (this.gradientMenu.activeSelf)
			{
				if (this._currentColorButton == this.grad_topColor)
				{
					Debug.Log("Gradient: TopColor");
					this.GradientSkyBox.SetColor("_Color2", this.grad_topColor.color);
				}
				else if (this._currentColorButton == this.grad_bottomColor)
				{
					Debug.Log("Gradient: BottomColor");
					this.GradientSkyBox.SetColor("_Color1", this.grad_bottomColor.color);
				}
			}
			else if (this._currentColorButton == this.hori_topColor)
			{
				this.HorizonSkyBox.SetColor("_SkyColor1", this.hori_topColor.color);
			}
			else if (this._currentColorButton == this.hori_bottomColor)
			{
				this.HorizonSkyBox.SetColor("_SkyColor3", this.hori_bottomColor.color);
			}
			else if (this._currentColorButton == this.hori_horizonColor)
			{
				this.HorizonSkyBox.SetColor("_SkyColor2", this.hori_horizonColor.color);
			}
			else if (this._currentColorButton == this.horiSun_Color)
			{
				this.HorizonSkyBox.SetColor("_SunColor", this.horiSun_Color.color);
			}
		}
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x0004A524 File Offset: 0x00048724
	private void activateColorPicker()
	{
		this.popUpColorPicker.SetActive(true);
		this.colorPicker.AssignColor(ColorValues.R, this._currentColorButton.color.r);
		this.colorPicker.AssignColor(ColorValues.G, this._currentColorButton.color.g);
		this.colorPicker.AssignColor(ColorValues.B, this._currentColorButton.color.b);
		this.colorPicker.AssignColor(ColorValues.A, this._currentColorButton.color.a);
		this.CHANGEFLAG = false;
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x0004A5C0 File Offset: 0x000487C0
	public void deActivateColorPicker()
	{
		if (!this.popUpColorPicker.activeSelf)
		{
			Debug.Log("ColorPicker Already Deactivated!");
		}
		this._currentColorButton = null;
		this.popUpColorPicker.SetActive(false);
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x0004A5F0 File Offset: 0x000487F0
	public void SkyBoxValueChanged(bool gradient, Slider slider)
	{
		Debug.Log("SkyBox Value Changed!", slider);
		if (gradient)
		{
			if (slider == this.grad_exponent)
			{
				this.GradientSkyBox.SetFloat("_Exponent", this.grad_exponent.value);
			}
			else if (slider == this.grad_intensity)
			{
				this.GradientSkyBox.SetFloat("_Intensity", this.grad_intensity.value);
			}
			else if (slider == this.grad_pitch)
			{
				this.GradientSkyBox.SetFloat("_UpVectorPitch", this.grad_pitch.value);
			}
			else if (slider == this.grad_yaw)
			{
				this.GradientSkyBox.SetFloat("_UpVectorYaw", this.grad_yaw.value);
			}
		}
		else if (slider == this.hori_bottomFactor)
		{
			this.HorizonSkyBox.SetFloat("_SkyExponent2", this.hori_bottomFactor.value);
		}
		else if (slider == this.hori_intensity)
		{
			this.HorizonSkyBox.SetFloat("_SkyIntensity", this.hori_intensity.value);
		}
		else if (slider == this.hori_topFactor)
		{
			this.HorizonSkyBox.SetFloat("_SkyExponent1", this.hori_topFactor.value);
		}
		else if (slider == this.horiSun_Alpha)
		{
			this.HorizonSkyBox.SetFloat("_SunAlpha", this.horiSun_Alpha.value);
		}
		else if (slider == this.horiSun_Beta)
		{
			this.HorizonSkyBox.SetFloat("_SunBeta", this.horiSun_Beta.value);
		}
		else if (slider == this.horiSun_Intensity)
		{
			this.HorizonSkyBox.SetFloat("_SunIntensity", this.horiSun_Intensity.value);
		}
		else if (slider == this.horiSun_Azimuth)
		{
			this.HorizonSkyBox.SetFloat("_SunAzimuth", this.horiSun_Azimuth.value);
		}
		else if (slider == this.horiSun_Altitude)
		{
			this.HorizonSkyBox.SetFloat("_SunAltitude", this.horiSun_Altitude.value);
		}
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x0004A858 File Offset: 0x00048A58
	public void OnClickDone()
	{
		string[] array = new string[(!this.gradientMenu.activeSelf) ? Enum.GetNames(typeof(levelEditorManager.customSkyBoxHorizonSettings)).Length : Enum.GetNames(typeof(levelEditorManager.customSkyBoxGradientSettings)).Length];
		if (this.gradientMenu.activeSelf)
		{
			array[1] = extensionMethods.ColorToHex(this.grad_bottomColor.color);
			array[0] = extensionMethods.ColorToHex(this.grad_topColor.color);
			array[3] = this.grad_exponent.value.ToString("F2");
			array[2] = this.grad_intensity.value.ToString("F2");
			array[4] = this.grad_pitch.value.ToString("F2");
			array[5] = this.grad_yaw.value.ToString("F2");
		}
		else
		{
			array[3] = extensionMethods.ColorToHex(this.hori_bottomColor.color);
			array[0] = extensionMethods.ColorToHex(this.hori_topColor.color);
			array[2] = extensionMethods.ColorToHex(this.hori_horizonColor.color);
			array[6] = extensionMethods.ColorToHex(this.horiSun_Color.color);
			array[8] = this.horiSun_Alpha.value.ToString("F2");
			array[11] = this.horiSun_Altitude.value.ToString("F2");
			array[10] = this.horiSun_Azimuth.value.ToString("F2");
			array[9] = this.horiSun_Beta.value.ToString("F2");
			array[4] = this.hori_bottomFactor.value.ToString("F2");
			array[5] = this.hori_intensity.value.ToString("F2");
			array[7] = this.horiSun_Intensity.value.ToString("F2");
			array[1] = this.hori_topFactor.value.ToString("F2");
		}
		settingsPanelOnSet.Instance().RecieveCustomSkyBox(array);
	}

	// Token: 0x06000C03 RID: 3075 RVA: 0x0004AAA4 File Offset: 0x00048CA4
	public void OnClickDefault(bool gradient)
	{
		Debug.Log("Defaulting Custom SkyBoxes!");
		Material material = Resources.Load("Materials/Skyboxes/Help/_GradientStandard") as Material;
		Material material2 = Resources.Load("Materials/Skyboxes/Help/_HorizonStandard") as Material;
		Debug.Log("Gradient SkyBox: " + material.name);
		Debug.Log("Horizon SkyBox: " + material2.name);
		if (gradient)
		{
			levelEditorManager.Instance().GradientSkyBox = new Material(material);
		}
		else
		{
			levelEditorManager.Instance().HorizonSkyBox = new Material(material2);
		}
		this.UpdateSkyBoxColorsAndSliders();
		RenderSettings.skybox = ((!gradient) ? material2 : material);
	}

	// Token: 0x04000883 RID: 2179
	public Dropdown skyBoxType;

	// Token: 0x04000884 RID: 2180
	public GameObject gradientMenu;

	// Token: 0x04000885 RID: 2181
	public GameObject horizonMenu;

	// Token: 0x04000886 RID: 2182
	public GameObject popUpColorPicker;

	// Token: 0x04000887 RID: 2183
	public ColorPicker colorPicker;

	// Token: 0x04000888 RID: 2184
	[Header("Gradient Props")]
	public Image grad_topColor;

	// Token: 0x04000889 RID: 2185
	public Image grad_bottomColor;

	// Token: 0x0400088A RID: 2186
	public Slider grad_intensity;

	// Token: 0x0400088B RID: 2187
	public Slider grad_exponent;

	// Token: 0x0400088C RID: 2188
	public Slider grad_pitch;

	// Token: 0x0400088D RID: 2189
	public Slider grad_yaw;

	// Token: 0x0400088E RID: 2190
	[Header("Horizon Props")]
	public Image hori_topColor;

	// Token: 0x0400088F RID: 2191
	public Image hori_horizonColor;

	// Token: 0x04000890 RID: 2192
	public Image hori_bottomColor;

	// Token: 0x04000891 RID: 2193
	public Slider hori_topFactor;

	// Token: 0x04000892 RID: 2194
	public Slider hori_bottomFactor;

	// Token: 0x04000893 RID: 2195
	public Slider hori_intensity;

	// Token: 0x04000894 RID: 2196
	[Header("Horizon Sun Props")]
	public Image horiSun_Color;

	// Token: 0x04000895 RID: 2197
	public Slider horiSun_Intensity;

	// Token: 0x04000896 RID: 2198
	public Slider horiSun_Alpha;

	// Token: 0x04000897 RID: 2199
	public Slider horiSun_Beta;

	// Token: 0x04000898 RID: 2200
	public Slider horiSun_Azimuth;

	// Token: 0x04000899 RID: 2201
	public Slider horiSun_Altitude;

	// Token: 0x0400089A RID: 2202
	private Image _currentColorButton;

	// Token: 0x0400089B RID: 2203
	private bool CHANGEFLAG;

	// Token: 0x0400089C RID: 2204
	private static CustomSkyBoxHandler _CustomSkyBoxHandler;
}
