using UnityEngine;

public class DeluxeEyeAdaptation : MonoBehaviour
{
	[SerializeField]
	public DeluxeEyeAdaptationLogic m_Logic;
	[SerializeField]
	public bool m_LowResolution;
	[SerializeField]
	public bool m_ShowHistogram;
	[SerializeField]
	public bool m_ShowExposure;
	[SerializeField]
	public float m_HistogramSize;
	public bool m_ExposureOpened;
	public bool m_SpeedOpened;
	public bool m_HistogramOpened;
	public bool m_OptimizationOpened;
	public bool m_DebugOpened;
	public bool m_BannerIsOpened;
}
