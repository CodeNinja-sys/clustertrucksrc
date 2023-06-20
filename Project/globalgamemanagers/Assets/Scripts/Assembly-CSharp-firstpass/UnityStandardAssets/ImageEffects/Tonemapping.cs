using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	public class Tonemapping : PostEffectsBase
	{
		public Tonemapping.TonemapperType type;
		public Tonemapping.AdaptiveTexSize adaptiveTextureSize;
		public AnimationCurve remapCurve;
		public float exposureAdjustment;
		public float middleGrey;
		public float white;
		public float adaptionSpeed;
		public Shader tonemapper;
		public bool validRenderTextureFormat;
	}
}
