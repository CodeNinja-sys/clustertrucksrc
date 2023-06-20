using UnityEngine;

namespace UnityStandardAssets.CinematicEffects
{
	public class DepthOfField : MonoBehaviour
	{
		public enum FilterQuality
		{
			None = 0,
			Normal = 1,
			High = 2,
		}

		public enum ApertureShape
		{
			Circular = 0,
			Hexagonal = 1,
			Octogonal = 2,
		}

		public enum UIMode
		{
			Basic = 0,
			Advanced = 1,
			Explicit = 2,
		}

		public bool visualizeBluriness;
		public bool customizeQualitySettings;
		public bool prefilterBlur;
		public FilterQuality medianFilter;
		public bool dilateNearBlur;
		public bool highQualityUpsampling;
		public float quality;
		public float focusPlane;
		public float focusRange;
		public float nearPlane;
		public float nearRadius;
		public float farPlane;
		public float farRadius;
		public float radius;
		public float boostPoint;
		public float nearBoostAmount;
		public float farBoostAmount;
		public float fStops;
		public float textureBokehScale;
		public float textureBokehIntensity;
		public float textureBokehThreshold;
		public float textureBokehSpawnHeuristic;
		public Transform focusTransform;
		public Texture2D bokehTexture;
		public ApertureShape apertureShape;
		public float apertureOrientation;
		public bool useBokehTexture;
		public UIMode uiMode;
		public Shader filmicDepthOfFieldShader;
		public Shader medianFilterShader;
		public Shader textureBokehShader;
	}
}
