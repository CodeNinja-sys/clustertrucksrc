using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	public class VignetteAndChromaticAberration : PostEffectsBase
	{
		public VignetteAndChromaticAberration.AberrationMode mode;
		public float intensity;
		public float chromaticAberration;
		public float axialAberration;
		public float blur;
		public float blurSpread;
		public float luminanceDependency;
		public float blurDistance;
		public Shader vignetteShader;
		public Shader separableBlurShader;
		public Shader chromAberrationShader;
	}
}
