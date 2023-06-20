using UnityStandardAssets.ImageEffects;
using UnityEngine;

internal class BloomOptimized : PostEffectsBase
{
	public float threshhold;
	public float intensity;
	public float blurSize;
	public int blurIterations;
	public BloomOptimized.BlurType blurType;
	public Shader fastBloomShader;
}
