using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	public class DepthOfFieldDeprecated : PostEffectsBase
	{
		public DepthOfFieldDeprecated.Dof34QualitySetting quality;
		public DepthOfFieldDeprecated.DofResolution resolution;
		public bool simpleTweakMode;
		public float focalPoint;
		public float smoothness;
		public float focalZDistance;
		public float focalZStartCurve;
		public float focalZEndCurve;
		public Transform objectFocus;
		public float focalSize;
		public DepthOfFieldDeprecated.DofBlurriness bluriness;
		public float maxBlurSpread;
		public float foregroundBlurExtrude;
		public Shader dofBlurShader;
		public Shader dofShader;
		public bool visualize;
		public DepthOfFieldDeprecated.BokehDestination bokehDestination;
		public bool bokeh;
		public bool bokehSupport;
		public Shader bokehShader;
		public Texture2D bokehTexture;
		public float bokehScale;
		public float bokehIntensity;
		public float bokehThresholdContrast;
		public float bokehThresholdLuminance;
		public int bokehDownsample;
	}
}
