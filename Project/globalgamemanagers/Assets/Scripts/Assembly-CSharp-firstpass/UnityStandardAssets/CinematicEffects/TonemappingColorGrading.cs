using UnityEngine;
using System;

namespace UnityStandardAssets.CinematicEffects
{
	public class TonemappingColorGrading : MonoBehaviour
	{
		[Serializable]
		public struct FilmicCurve
		{
			public bool enabled;
			public float exposureBias;
			public float contrast;
			public float toe;
			public float lutShoulder;
		}

		[Serializable]
		public struct ColorGradingColors
		{
			public Color shadows;
			public Color midtones;
			public Color highlights;
		}

		[Serializable]
		public struct ColorGrading
		{
			public bool enabled;
			public Color whiteBalance;
			public float saturation;
			public float gamma;
			public TonemappingColorGrading.ColorGradingColors lutColors;
		}

		public bool debugClamp;
		[SerializeField]
		private FilmicCurve m_FilmicCurve;
		[SerializeField]
		private ColorGrading m_ColorGrading;
		[SerializeField]
		private Texture2D m_UserLutTexture;
		public Shader tonemapShader;
		public bool validRenderTextureFormat;
	}
}
