using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200023E RID: 574
public class settingsPanelOnSet : MonoBehaviour
{
	// Token: 0x17000288 RID: 648
	// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0005A678 File Offset: 0x00058878
	// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x0005A680 File Offset: 0x00058880
	public int CurrentSkybox
	{
		get
		{
			return this._currentSkybox;
		}
		set
		{
			this._currentSkybox = value;
			Debug.Log("Current Skybox value changed to : " + this.CurrentSkybox);
		}
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x0005A6A4 File Offset: 0x000588A4
	public static settingsPanelOnSet Instance()
	{
		if (!settingsPanelOnSet._settingsPanelOnSet)
		{
			settingsPanelOnSet._settingsPanelOnSet = (UnityEngine.Object.FindObjectOfType(typeof(settingsPanelOnSet)) as settingsPanelOnSet);
			if (!settingsPanelOnSet._settingsPanelOnSet)
			{
				Debug.LogError("There needs to be one active settingsPanelOnSet script on a GameObject in your scene.");
			}
		}
		return settingsPanelOnSet._settingsPanelOnSet;
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x0005A6F8 File Offset: 0x000588F8
	private void Awake()
	{
		this.LoadSkyBoxes();
		this.LoadTerrainMaterials();
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x0005A708 File Offset: 0x00058908
	public static void Init()
	{
		float num = -10f;
		float num2 = -9.82f;
		float num3 = 0.0025f;
		float num4 = 1f;
		bool value = false;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 1;
		Color fogColor = RenderSettings.fogColor;
		Color ambientSkyColor = RenderSettings.ambientSkyColor;
		Color ambientGroundColor = RenderSettings.ambientGroundColor;
		Color ambientEquatorColor = RenderSettings.ambientEquatorColor;
		settingsPanelOnSet.defaultSettings[0] = num.ToString();
		settingsPanelOnSet.defaultSettings[1] = Convert.ToInt32(value).ToString();
		settingsPanelOnSet.defaultSettings[11] = num5.ToString();
		settingsPanelOnSet.defaultSettings[10] = num2.ToString();
		settingsPanelOnSet.defaultSettings[2] = num8.ToString();
		settingsPanelOnSet.defaultSettings[9] = num4.ToString();
		settingsPanelOnSet.defaultSettings[12] = num6.ToString();
		settingsPanelOnSet.defaultSettings[3] = num7.ToString();
		settingsPanelOnSet.defaultSettings[4] = extensionMethods.ColorToHex(fogColor);
		settingsPanelOnSet.defaultSettings[5] = num3.ToString();
		settingsPanelOnSet.defaultSettings[6] = extensionMethods.ColorToHex(ambientSkyColor);
		settingsPanelOnSet.defaultSettings[7] = extensionMethods.ColorToHex(ambientGroundColor);
		settingsPanelOnSet.defaultSettings[8] = extensionMethods.ColorToHex(ambientEquatorColor);
		Debug.Log("Default Color: " + settingsPanelOnSet.defaultSettings[4]);
		settingsPanelOnSet.InitializeSkyBoxes();
		settingsPanelOnSet.InitializeTerrainMaterials();
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x0005A850 File Offset: 0x00058A50
	private static void InitializeSkyBoxes()
	{
		UnityEngine.Object[] array = Resources.LoadAll("Materials/Skyboxes/Presets");
		levelEditorManager.Instance().SkyBoxes = new Material[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			levelEditorManager.Instance().SkyBoxes[i] = (array[i] as Material);
			Debug.Log("AddingSkyBox: " + array[i].name.Split(new char[]
			{
				'_'
			})[1]);
		}
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x0005A8CC File Offset: 0x00058ACC
	private static void InitializeTerrainMaterials()
	{
		UnityEngine.Object[] array = Resources.LoadAll("Materials/Terrains");
		levelEditorManager.Instance().TerrainMaterials = new Material[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			levelEditorManager.Instance().TerrainMaterials[i] = (array[i] as Material);
			Debug.Log("AddingTerrain: " + array[i].name.Split(new char[]
			{
				'_'
			})[1]);
		}
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x0005A948 File Offset: 0x00058B48
	private void LoadSkyBoxes()
	{
		List<string> list = new List<string>();
		foreach (Material material in levelEditorManager.Instance().SkyBoxes)
		{
			list.Add(material.name.Split(new char[]
			{
				'_'
			})[1]);
		}
		list.Add("Create Custom");
		this.SkyboxDropdown.AddOptions(list);
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x0005A9B4 File Offset: 0x00058BB4
	private void LoadTerrainMaterials()
	{
		List<string> list = new List<string>();
		foreach (Material material in levelEditorManager.Instance().TerrainMaterials)
		{
			list.Add(material.name.Split(new char[]
			{
				'_'
			})[1]);
		}
		this.TerrainDropdown.AddOptions(list);
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x0005AA14 File Offset: 0x00058C14
	private void Start()
	{
		this.colorPicker.onValueChanged.AddListener(new UnityAction<Color>(this.OnColorPickerChanged));
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x0005AA34 File Offset: 0x00058C34
	private void OnDisable()
	{
		if (!this.DISABLEFLAG)
		{
			return;
		}
		Debug.Log("Disable Cancel!");
		this.Reset();
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x0005AA54 File Offset: 0x00058C54
	private void OnEnable()
	{
		this.DISABLEFLAG = true;
		this.CustomSkyBoxMenu.SetActive(false);
		this._currentMenuState = settingsPanelOnSet.menustate.advanced;
		if (this.SkyboxDropdown.transform.FindChild("Dropdown List"))
		{
			UnityEngine.Object.Destroy(this.SkyboxDropdown.transform.FindChild("Dropdown List").gameObject);
		}
		this.OnClickChangeMenu(0);
		string[] settings = levelEditorManager.Instance().getCurrentMap.getSettings();
		if (settings.Length < 1)
		{
			Debug.Log("Creating Default Settings");
			settings = settingsPanelOnSet.defaultSettings;
		}
		this.killHeightSlider.value = float.Parse(settings[0]);
		this.WindToggle.isOn = (int.Parse(settings[1]) == 1);
		this.TerrainDropdown.value = int.Parse(settings[2]);
		Debug.Log("SkyBox: " + int.Parse(settings[3]));
		this.CurrentSkybox = int.Parse(settings[3]);
		if (this.CurrentSkybox < this.SkyboxDropdown.options.Count - 1)
		{
			this.SkyboxDropdown.value = this.CurrentSkybox;
		}
		this.gravitySlider.value = float.Parse(settings[10]);
		this.brightnessSlider.value = float.Parse(settings[9]);
		this.weatherDropDown.value = int.Parse(settings[11]);
		this.ObjectiveDropDown.value = int.Parse(settings[12]);
		this.color_slider.value = float.Parse(settings[5]) * 100f;
		Debug.Log(string.Concat(new object[]
		{
			"Fog SLider Value: ",
			this.color_slider.value,
			" FogDens: ",
			settings[5]
		}));
		Color color = extensionMethods.HexToColor(settings[4]);
		this.colorPicker.AssignColor(ColorValues.R, color.r);
		this.colorPicker.AssignColor(ColorValues.G, color.g);
		this.colorPicker.AssignColor(ColorValues.B, color.b);
		this.colorPicker.AssignColor(ColorValues.A, color.a);
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x0005AC74 File Offset: 0x00058E74
	public void OnClickDone()
	{
		int currentSkybox = this.CurrentSkybox;
		if (currentSkybox >= levelEditorManager.Instance().SkyBoxes.Length)
		{
			Debug.Log("Saving Custom SkyBox From Standard Menu!");
			levelEditorManager.Instance().saveCustomSkyBoxSettingsFor(currentSkybox);
		}
		Debug.Log("Done! SkyBox: " + currentSkybox);
		string[] array = new string[Enum.GetNames(typeof(levelEditorManager.settings)).Length];
		array[0] = this.killHeightSlider.value.ToString();
		array[1] = Convert.ToInt32(this.WindToggle.isOn).ToString();
		array[2] = this.TerrainDropdown.value.ToString();
		array[3] = currentSkybox.ToString();
		array[10] = this.gravitySlider.value.ToString("F2");
		array[9] = this.brightnessSlider.value.ToString("F2");
		array[11] = this.weatherDropDown.value.ToString();
		array[12] = this.ObjectiveDropDown.value.ToString();
		array[4] = extensionMethods.ColorToHex(RenderSettings.fogColor);
		array[5] = (this.color_slider.value / 100f).ToString();
		array[6] = extensionMethods.ColorToHex(RenderSettings.ambientSkyColor);
		array[7] = extensionMethods.ColorToHex(RenderSettings.ambientGroundColor);
		array[8] = extensionMethods.ColorToHex(RenderSettings.ambientEquatorColor);
		Debug.Log("Saving Fog Color: " + this.colorPicker.CurrentColor.ToString());
		levelEditorManager.Instance().getCurrentMap.setSettings(array);
		this.clean();
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x0005AE38 File Offset: 0x00059038
	public void OnClickBackToMainSettingsMenu()
	{
		this.CurrentSkybox = this.SkyboxDropdown.options.Count + CustomSkyBoxHandler.Instance.CurrentCustomSkyboxType;
		this._currentMenuState = settingsPanelOnSet.menustate.standard;
		this.CustomSkyBoxMenu.SetActive(false);
		this.AdvancedMenu.SetActive(false);
		this.standardMenu.SetActive(true);
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x0005AE94 File Offset: 0x00059094
	public void OnClickCancel()
	{
		Debug.Log("Click Cancel!");
		this.clean();
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x0005AEA8 File Offset: 0x000590A8
	public void OnClickDefault()
	{
		Debug.Log("Default!");
		this.CurrentSkybox = 0;
		levelEditorManager.Instance().getCurrentMap.setSettings(settingsPanelOnSet.defaultSettings);
		levelEditorManager.Instance().ApplySettingsToScene();
		this.OnEnable();
	}

	// Token: 0x06000DF2 RID: 3570 RVA: 0x0005AEEC File Offset: 0x000590EC
	private void clean()
	{
		this.Reset();
		this.DISABLEFLAG = false;
		base.gameObject.SetActive(false);
		TutorialHandler.Instance.SpecialEvents[17].Clicked();
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x0005AF24 File Offset: 0x00059124
	public void OnSkyBoxDropDownChanged()
	{
		if (this.SkyboxDropdown.value == this.SkyboxDropdown.options.Count - 1)
		{
			Debug.Log("Custom!!");
			this.OnClickChangeMenu(2);
			return;
		}
		RenderSettings.skybox = levelEditorManager.Instance().SkyBoxes[this.SkyboxDropdown.value];
		this.CurrentSkybox = this.SkyboxDropdown.value;
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x0005AF94 File Offset: 0x00059194
	public void OnTerrainDropDownChanged()
	{
		levelEditorManager.Instance().changeWorldMaterial(this.TerrainDropdown.value);
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x0005AFAC File Offset: 0x000591AC
	public void OnGravitySliderValueChange()
	{
		this.gravityValueText.text = this.gravitySlider.value.ToString("F2");
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x0005AFDC File Offset: 0x000591DC
	public void OnBrightnessSliderValueChange()
	{
		this.brightnessValueText.text = this.brightnessSlider.value.ToString("F2");
		this.ToneMapping.middleGrey = this.brightnessSlider.value;
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x0005B024 File Offset: 0x00059224
	private void Reset()
	{
		levelEditorManager.Instance().ApplySettingsToScene();
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x0005B030 File Offset: 0x00059230
	public void OnKillHeightSliderValueChange()
	{
		this.killHeightValueText.text = this.killHeightSlider.value.ToString();
		this.killHeightGameObject.position = this.killHeightGameObject.position.setYValue(this.killHeightSlider.value);
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x0005B084 File Offset: 0x00059284
	public void OnSliderValueChanged()
	{
		switch (this._currentColorSettingType)
		{
		case settingsPanelOnSet.ColorSettingType.Fog:
			this.color_ValueText.text = (this.color_slider.value * 100f).ToString("F0") + "%";
			RenderSettings.fogDensity = this.color_slider.value / 100f;
			break;
		}
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x0005B118 File Offset: 0x00059318
	private void AssignButtonUIColor()
	{
		for (int i = 0; i < this.colorSettingButtons.Length; i++)
		{
			if (i != (int)this._currentColorSettingType)
			{
				this.colorSettingButtons[i].GetComponent<Button>().image.color = Color.white;
			}
			else
			{
				this.colorSettingButtons[i].GetComponent<Button>().image.color = Color.red;
			}
		}
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x0005B188 File Offset: 0x00059388
	private void FixColorPickerColor()
	{
		string[] settings = levelEditorManager.Instance().getCurrentMap.getSettings();
		if (settings.Length < 1)
		{
			Debug.Log("Creating Default Settings");
			settings = settingsPanelOnSet.defaultSettings;
		}
		Color color = Color.clear;
		switch (this._currentColorSettingType)
		{
		case settingsPanelOnSet.ColorSettingType.Fog:
			color = RenderSettings.fogColor;
			break;
		case settingsPanelOnSet.ColorSettingType.Sky:
			color = RenderSettings.ambientSkyColor;
			break;
		case settingsPanelOnSet.ColorSettingType.Ground:
			color = RenderSettings.ambientGroundColor;
			break;
		case settingsPanelOnSet.ColorSettingType.Horizon:
			color = RenderSettings.ambientEquatorColor;
			break;
		}
		this.colorPicker.AssignColor(ColorValues.R, color.r);
		this.colorPicker.AssignColor(ColorValues.G, color.g);
		this.colorPicker.AssignColor(ColorValues.B, color.b);
		this.colorPicker.AssignColor(ColorValues.A, color.a);
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x0005B264 File Offset: 0x00059464
	public void changeColorPickerSetting(int i)
	{
		if (i == (int)this._currentColorSettingType)
		{
			return;
		}
		this._currentColorSettingType = (settingsPanelOnSet.ColorSettingType)i;
		this.color_titleTypeText.text = this._currentColorSettingType.ToString();
		this.AssignButtonUIColor();
		this.FixColorPickerColor();
		bool active = this._currentColorSettingType == settingsPanelOnSet.ColorSettingType.Fog;
		this.color_slider.gameObject.SetActive(active);
		this.color_ValueText.gameObject.SetActive(active);
		this.color_typeText.gameObject.SetActive(active);
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x0005B2F4 File Offset: 0x000594F4
	public void OnColorPickerChanged(Color color)
	{
		Debug.Log("Color Changed!" + color.ToString());
		switch (this._currentColorSettingType)
		{
		case settingsPanelOnSet.ColorSettingType.Fog:
			RenderSettings.fogColor = color;
			break;
		case settingsPanelOnSet.ColorSettingType.Sky:
			RenderSettings.ambientSkyColor = color;
			break;
		case settingsPanelOnSet.ColorSettingType.Ground:
			RenderSettings.ambientGroundColor = color;
			break;
		case settingsPanelOnSet.ColorSettingType.Horizon:
			RenderSettings.ambientEquatorColor = color;
			break;
		}
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x0005B36C File Offset: 0x0005956C
	public void OnClickChangeMenu(int menuToChange)
	{
		this._currentMenuState = (settingsPanelOnSet.menustate)menuToChange;
		this.AdvancedMenu.SetActive(this._currentMenuState == settingsPanelOnSet.menustate.advanced);
		this.standardMenu.SetActive(this._currentMenuState == settingsPanelOnSet.menustate.standard);
		this.CustomSkyBoxMenu.SetActive(this._currentMenuState == settingsPanelOnSet.menustate.customskybox);
		if (this._currentMenuState == settingsPanelOnSet.menustate.advanced)
		{
			this.AssignButtonUIColor();
		}
		Debug.Log("Menu chnged To: " + this._currentMenuState.ToString());
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x0005B3F0 File Offset: 0x000595F0
	public void RecieveCustomSkyBox(string[] skyboxSettings)
	{
		this.CurrentSkybox = ((skyboxSettings.Length != Enum.GetNames(typeof(levelEditorManager.customSkyBoxGradientSettings)).Length) ? (this.SkyboxDropdown.options.Count + 1) : this.SkyboxDropdown.options.Count);
		levelEditorManager.Instance().getCurrentMap.setCustomSkyBox(skyboxSettings);
		this.OnClickDone();
	}

	// Token: 0x04000A73 RID: 2675
	private const string SKYBOXES_PATH_STRING = "Materials/Skyboxes/Presets";

	// Token: 0x04000A74 RID: 2676
	private const string SKYBOXES_CUSTOM_PATH_STRING = "Materials/Skyboxes/Custom";

	// Token: 0x04000A75 RID: 2677
	private const string TERRAINMATERIALS_PATH_STRING = "Materials/Terrains";

	// Token: 0x04000A76 RID: 2678
	public Tonemapping ToneMapping;

	// Token: 0x04000A77 RID: 2679
	public static string[] defaultSettings = new string[Enum.GetNames(typeof(levelEditorManager.settings)).Length];

	// Token: 0x04000A78 RID: 2680
	public GameObject[] colorSettingButtons;

	// Token: 0x04000A79 RID: 2681
	[SerializeField]
	private settingsPanelOnSet.menustate _currentMenuState;

	// Token: 0x04000A7A RID: 2682
	private bool DISABLEFLAG = true;

	// Token: 0x04000A7B RID: 2683
	private int _currentSkybox;

	// Token: 0x04000A7C RID: 2684
	public GameObject standardMenu;

	// Token: 0x04000A7D RID: 2685
	public GameObject AdvancedMenu;

	// Token: 0x04000A7E RID: 2686
	public GameObject CustomSkyBoxMenu;

	// Token: 0x04000A7F RID: 2687
	public ColorPicker colorPicker;

	// Token: 0x04000A80 RID: 2688
	public Slider color_slider;

	// Token: 0x04000A81 RID: 2689
	public Text color_ValueText;

	// Token: 0x04000A82 RID: 2690
	public Text color_typeText;

	// Token: 0x04000A83 RID: 2691
	public Text color_titleTypeText;

	// Token: 0x04000A84 RID: 2692
	public Slider killHeightSlider;

	// Token: 0x04000A85 RID: 2693
	public Text killHeightValueText;

	// Token: 0x04000A86 RID: 2694
	public Transform killHeightGameObject;

	// Token: 0x04000A87 RID: 2695
	public Toggle WindToggle;

	// Token: 0x04000A88 RID: 2696
	public Dropdown TerrainDropdown;

	// Token: 0x04000A89 RID: 2697
	public Dropdown SkyboxDropdown;

	// Token: 0x04000A8A RID: 2698
	public Slider brightnessSlider;

	// Token: 0x04000A8B RID: 2699
	public Text brightnessValueText;

	// Token: 0x04000A8C RID: 2700
	public Slider gravitySlider;

	// Token: 0x04000A8D RID: 2701
	public Text gravityValueText;

	// Token: 0x04000A8E RID: 2702
	public Dropdown weatherDropDown;

	// Token: 0x04000A8F RID: 2703
	public Dropdown ObjectiveDropDown;

	// Token: 0x04000A90 RID: 2704
	private settingsPanelOnSet.ColorSettingType _currentColorSettingType;

	// Token: 0x04000A91 RID: 2705
	private static settingsPanelOnSet _settingsPanelOnSet;

	// Token: 0x0200023F RID: 575
	private enum ColorSettingType
	{
		// Token: 0x04000A93 RID: 2707
		Fog,
		// Token: 0x04000A94 RID: 2708
		Sky,
		// Token: 0x04000A95 RID: 2709
		Ground,
		// Token: 0x04000A96 RID: 2710
		Horizon
	}

	// Token: 0x02000240 RID: 576
	[Serializable]
	public enum menustate
	{
		// Token: 0x04000A98 RID: 2712
		standard,
		// Token: 0x04000A99 RID: 2713
		advanced,
		// Token: 0x04000A9A RID: 2714
		customskybox
	}
}
