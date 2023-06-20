using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	public class ScreenOverlay : PostEffectsBase
	{
		public ScreenOverlay.OverlayBlendMode blendMode;
		public float intensity;
		public Texture2D texture;
		public Shader overlayShader;
	}
}
