using UnityEngine;

public class DeluxeTonemapper : MonoBehaviour
{
	public enum Mode
	{
		Color = 0,
		Luminance = 1,
		ExtendedLuminance = 2,
		ColorHD = 3,
	}

	[SerializeField]
	public FilmicCurve m_MainCurve;
	[SerializeField]
	public Color m_Tint;
	[SerializeField]
	public float m_MoodAmount;
	[SerializeField]
	public float m_Sharpness;
	[SerializeField]
	public float m_SharpnessKernel;
	[SerializeField]
	public Mode m_Mode;
	public bool m_EnableLut0;
	public float m_Saturation;
	public bool m_DisableColorGrading;
	public float m_Lut0Amount;
	public Texture2D m_2DLut0;
	public Texture3D m_3DLut0;
	public bool m_FilmicCurveOpened;
	public bool m_ColorGradingOpened;
	public bool m_HDREffectsgOpened;
	public bool m_BannerIsOpened;
	public bool m_Use2DLut;
}
