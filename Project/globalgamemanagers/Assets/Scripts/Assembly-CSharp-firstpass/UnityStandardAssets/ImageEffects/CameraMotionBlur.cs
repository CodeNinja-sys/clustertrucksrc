using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	public class CameraMotionBlur : PostEffectsBase
	{
		public CameraMotionBlur.MotionBlurFilter filterType;
		public bool preview;
		public Vector3 previewScale;
		public float movementScale;
		public float rotationScale;
		public float maxVelocity;
		public float minVelocity;
		public float velocityScale;
		public float softZDistance;
		public int velocityDownsample;
		public LayerMask excludeLayers;
		public Shader shader;
		public Shader dx11MotionBlurShader;
		public Shader replacementClear;
		public Texture2D noiseTexture;
		public float jitter;
		public bool showVelocity;
		public float showVelocityScale;
	}
}
