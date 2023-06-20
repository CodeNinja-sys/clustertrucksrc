using UnityEngine;
using System;

namespace UnityStandardAssets.CinematicEffects
{
	public class ScreenSpaceReflection : MonoBehaviour
	{
		[Serializable]
		public struct BasicSettings
		{
			public float reflectionMultiplier;
			public float maxDistance;
			public float fadeDistance;
			public float screenEdgeFading;
			public bool enableHDR;
			public bool additiveReflection;
		}

		[Serializable]
		public struct ReflectionSettings
		{
			public int maxSteps;
			public int rayStepSize;
			public float widthModifier;
			public float smoothFallbackThreshold;
			public float smoothFallbackDistance;
			public float fresnelFade;
			public float fresnelFadePower;
			public float distanceBlur;
		}

		[Serializable]
		public struct AdvancedSettings
		{
			public float temporalFilterStrength;
			public bool useTemporalConfidence;
			public bool traceBehindObjects;
			public bool highQualitySharpReflections;
			public bool traceEverywhere;
			public bool treatBackfaceHitAsMiss;
			public bool allowBackwardsRays;
			public bool improveCorners;
			public ScreenSpaceReflection.SSRResolution resolution;
			public bool bilateralUpsample;
			public bool reduceBanding;
			public bool highlightSuppression;
		}

		[Serializable]
		public struct DebugSettings
		{
			public ScreenSpaceReflection.SSRDebugMode debugMode;
		}

		[Serializable]
		public struct SSRSettings
		{
			public ScreenSpaceReflection.BasicSettings basicSettings;
			public ScreenSpaceReflection.ReflectionSettings reflectionSettings;
			public ScreenSpaceReflection.AdvancedSettings advancedSettings;
			public ScreenSpaceReflection.DebugSettings debugSettings;
		}

		public enum SSRResolution
		{
			FullResolution = 0,
			HalfTraceFullResolve = 1,
			HalfResolution = 2,
		}

		public enum SSRDebugMode
		{
			None = 0,
			IncomingRadiance = 1,
			SSRResult = 2,
			FinalGlossyTerm = 3,
			SSRMask = 4,
			Roughness = 5,
			BaseColor = 6,
			SpecColor = 7,
			Reflectivity = 8,
			ReflectionProbeOnly = 9,
			ReflectionProbeMinusSSR = 10,
			SSRMinusReflectionProbe = 11,
			NoGlossy = 12,
			NegativeNoGlossy = 13,
			MipLevel = 14,
		}

		[SerializeField]
		public SSRSettings settings;
		public Shader ssrShader;
	}
}
