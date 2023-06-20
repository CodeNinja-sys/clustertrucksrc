using System;
using UnityEngine;

[Serializable]
public class DeluxeEyeAdaptationLogic
{
	[SerializeField]
	public float m_MinimumExposure;
	[SerializeField]
	public float m_MaximumExposure;
	[SerializeField]
	public float m_Range;
	[SerializeField]
	public float m_AdaptationSpeedUp;
	[SerializeField]
	public float m_AdaptationSpeedDown;
	[SerializeField]
	public float m_BrightnessMultiplier;
	[SerializeField]
	public float m_ExposureOffset;
	[SerializeField]
	public float m_AverageThresholdMin;
	[SerializeField]
	public float m_AverageThresholdMax;
	[SerializeField]
	private float m_HistLogMin;
	[SerializeField]
	private float m_HistLogMax;
	[SerializeField]
	public Vector4 m_HistCoefs;
	[SerializeField]
	public float m_Attenuation;
	public int m_CurrentWidth;
	public int m_CurrentHeight;
	public RenderTexture[] m_HistogramList;
	public RenderTexture m_BrightnessRT;
	public bool m_SetTargetExposure;
}
