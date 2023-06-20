using UnityEngine;

public class TemporalReprojection : EffectBase
{
	public enum Neighborhood
	{
		MinMax3x3 = 0,
		MinMax3x3Rounded = 1,
		MinMax4TapVarying = 2,
	}

	public Shader reprojectionShader;
	public Neighborhood neighborhood;
	public bool unjitterColorSamples;
	public bool unjitterNeighborhood;
	public bool unjitterReprojection;
	public bool useYCoCg;
	public bool useClipping;
	public bool useDilation;
	public bool useMotionBlur;
	public bool useOptimizations;
	public float feedbackMin;
	public float feedbackMax;
	public float motionBlurStrength;
	public bool motionBlurIgnoreFF;
}
