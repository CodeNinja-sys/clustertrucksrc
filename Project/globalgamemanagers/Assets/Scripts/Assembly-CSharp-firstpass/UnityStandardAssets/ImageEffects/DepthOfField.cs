using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	public class DepthOfField : PostEffectsBase
	{
		public bool visualizeFocus;
		public float focalLength;
		public float focalSize;
		public float aperture;
		public Transform focalTransform;
		public float maxBlurSize;
		public bool highResolution;
		public DepthOfField.BlurType blurType;
		public DepthOfField.BlurSampleCount blurSampleCount;
		public bool nearBlur;
		public float foregroundOverlap;
		public Shader dofHdrShader;
		public Shader dx11BokehShader;
		public float dx11BokehThreshold;
		public float dx11SpawnHeuristic;
		public Texture2D dx11BokehTexture;
		public float dx11BokehScale;
		public float dx11BokehIntensity;
	}
}
