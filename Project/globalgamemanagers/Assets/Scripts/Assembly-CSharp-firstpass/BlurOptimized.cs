using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class BlurOptimized : PostEffectsBase
{
	public int downsample;
	public float blurSize;
	public int blurIterations;
	public BlurOptimized.BlurType blurType;
	public Shader blurShader;
}
