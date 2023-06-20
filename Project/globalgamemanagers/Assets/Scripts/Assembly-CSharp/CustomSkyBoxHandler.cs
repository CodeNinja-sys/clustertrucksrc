using UnityEngine;
using UnityEngine.UI;

public class CustomSkyBoxHandler : MonoBehaviour
{
	public Dropdown skyBoxType;
	public GameObject gradientMenu;
	public GameObject horizonMenu;
	public GameObject popUpColorPicker;
	public ColorPicker colorPicker;
	public Image grad_topColor;
	public Image grad_bottomColor;
	public Slider grad_intensity;
	public Slider grad_exponent;
	public Slider grad_pitch;
	public Slider grad_yaw;
	public Image hori_topColor;
	public Image hori_horizonColor;
	public Image hori_bottomColor;
	public Slider hori_topFactor;
	public Slider hori_bottomFactor;
	public Slider hori_intensity;
	public Image horiSun_Color;
	public Slider horiSun_Intensity;
	public Slider horiSun_Alpha;
	public Slider horiSun_Beta;
	public Slider horiSun_Azimuth;
	public Slider horiSun_Altitude;
}
