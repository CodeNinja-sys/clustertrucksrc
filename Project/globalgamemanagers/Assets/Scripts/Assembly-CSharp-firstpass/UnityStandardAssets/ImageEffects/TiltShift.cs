using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	internal class TiltShift : PostEffectsBase
	{
		public TiltShift.TiltShiftMode mode;
		public TiltShift.TiltShiftQuality quality;
		public float blurArea;
		public float maxBlurSize;
		public int downsample;
		public Shader tiltShiftShader;
	}
}
