using UnityEngine;

internal class ContrastEnhance : PostEffectsBase
{
	public float intensity;
	public float threshhold;
	public float blurSpread;
	public Shader separableBlurShader;
	public Shader contrastCompositeShader;
}
