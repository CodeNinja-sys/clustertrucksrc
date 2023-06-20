using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class SunShafts : PostEffectsBase
{
	public SunShafts.SunShaftsResolution resolution;
	public SunShafts.ShaftsScreenBlendMode screenBlendMode;
	public Transform sunTransform;
	public int radialBlurIterations;
	public Color sunColor;
	public Color sunThreshold;
	public float sunShaftBlurRadius;
	public float sunShaftIntensity;
	public float maxRadius;
	public bool useDepthTexture;
	public Shader sunShaftsShader;
	public Shader simpleClearShader;
}
