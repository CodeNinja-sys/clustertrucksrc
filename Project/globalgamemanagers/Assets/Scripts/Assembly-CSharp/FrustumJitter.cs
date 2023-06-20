using UnityEngine;

public class FrustumJitter : MonoBehaviour
{
	public enum Pattern
	{
		Still = 0,
		Uniform2 = 1,
		Uniform4 = 2,
		Uniform4_Helix = 3,
		Uniform4_DoubleHelix = 4,
		SkewButterfly = 5,
		Rotated4 = 6,
		Rotated4_Helix = 7,
		Rotated4_Helix2 = 8,
		Poisson10 = 9,
		Pentagram = 10,
		Halton_2_3_X8 = 11,
		Halton_2_3_X16 = 12,
		Halton_2_3_X32 = 13,
		Halton_2_3_X256 = 14,
		MotionPerp2 = 15,
	}

	public Pattern pattern;
	public float patternScale;
	public Vector4 activeSample;
	public int activeIndex;
}
