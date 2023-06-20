using UnityEngine;

namespace UnityStandardAssets.CinematicEffects
{
	public class AntiAliasing : MonoBehaviour
	{
		public enum DebugDisplay
		{
			Off = 0,
			Edges = 1,
			Weights = 2,
			Depth = 3,
			Accumulation = 4,
		}

		public enum EdgeType
		{
			Luminance = 0,
			Color = 1,
			Depth = 2,
		}

		public enum TemporalType
		{
			Off = 0,
			SMAA_2x = 1,
			Standard_2x = 2,
			Standard_4x = 3,
			Standard_8x = 4,
			Standard_16x = 5,
		}

		public DebugDisplay displayType;
		public EdgeType edgeType;
		public Texture2D areaTex;
		public Texture2D searchTex;
		public float K;
		public TemporalType temporalType;
		public float temporalAccumulationWeight;
		public float depthThreshold;
		public Shader smaaShader;
	}
}
