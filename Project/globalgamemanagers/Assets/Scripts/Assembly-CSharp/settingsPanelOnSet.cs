using UnityEngine;
using UnityEngine.UI;

public class settingsPanelOnSet : MonoBehaviour
{
	public enum menustate
	{
		standard = 0,
		advanced = 1,
		customskybox = 2,
	}

	public Tonemapping ToneMapping;
	public GameObject[] colorSettingButtons;
	[SerializeField]
	private menustate _currentMenuState;
	public GameObject standardMenu;
	public GameObject AdvancedMenu;
	public GameObject CustomSkyBoxMenu;
	public ColorPicker colorPicker;
	public Slider color_slider;
	public Text color_ValueText;
	public Text color_typeText;
	public Text color_titleTypeText;
	public Slider killHeightSlider;
	public Text killHeightValueText;
	public Transform killHeightGameObject;
	public Toggle WindToggle;
	public Dropdown TerrainDropdown;
	public Dropdown SkyboxDropdown;
	public Slider brightnessSlider;
	public Text brightnessValueText;
	public Slider gravitySlider;
	public Text gravityValueText;
	public Dropdown weatherDropDown;
	public Dropdown ObjectiveDropDown;
}
