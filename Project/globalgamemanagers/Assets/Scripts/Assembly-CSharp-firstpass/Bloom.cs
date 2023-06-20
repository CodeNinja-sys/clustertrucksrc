using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class Bloom : PostEffectsBase
{
	public Bloom.TweakMode tweakMode;
	public Bloom.BloomScreenBlendMode screenBlendMode;
	public Bloom.HDRBloomMode hdr;
	public float sepBlurSpread;
	public Bloom.BloomQuality quality;
	public float bloomIntensity;
	public float bloomThreshhold;
	public Color bloomThreshholdColor;
	public int bloomBlurIterations;
	public int hollywoodFlareBlurIterations;
	public float flareRotation;
	public Bloom.LensFlareStyle lensflareMode;
	public float hollyStretchWidth;
	public float lensflareIntensity;
	public float lensflareThreshhold;
	public float lensFlareSaturation;
	public Color flareColorA;
	public Color flareColorB;
	public Color flareColorC;
	public Color flareColorD;
	public Texture2D lensFlareVignetteMask;
	public Shader lensFlareShader;
	public Shader screenBlendShader;
	public Shader blurAndFlaresShader;
	public Shader brightPassFilterShader;
}
