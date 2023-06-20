using System;
using UnityEngine;

// Token: 0x0200030A RID: 778
[AddComponentMenu("Playdead/FrustumJitter")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class FrustumJitter : MonoBehaviour
{
	// Token: 0x06001238 RID: 4664 RVA: 0x000742C0 File Offset: 0x000724C0
	static FrustumJitter()
	{
		if (!FrustumJitter._initialized)
		{
			FrustumJitter._initialized = true;
			Vector2 to = new Vector2(FrustumJitter.points_Pentagram[0] - FrustumJitter.points_Pentagram[2], FrustumJitter.points_Pentagram[1] - FrustumJitter.points_Pentagram[3]);
			Vector2 from = new Vector2(0f, 1f);
			FrustumJitter.TransformPattern(FrustumJitter.points_Pentagram, 0.017453292f * (0.5f * Vector2.Angle(from, to)), 1f);
			FrustumJitter.InitializeHalton_2_3(FrustumJitter.points_Halton_2_3_x8);
			FrustumJitter.InitializeHalton_2_3(FrustumJitter.points_Halton_2_3_x16);
			FrustumJitter.InitializeHalton_2_3(FrustumJitter.points_Halton_2_3_x32);
			FrustumJitter.InitializeHalton_2_3(FrustumJitter.points_Halton_2_3_x256);
		}
	}

	// Token: 0x06001239 RID: 4665 RVA: 0x000744AC File Offset: 0x000726AC
	private static void TransformPattern(float[] seq, float theta, float scale)
	{
		float num = Mathf.Cos(theta);
		float num2 = Mathf.Sin(theta);
		int num3 = 0;
		int num4 = 1;
		int num5 = seq.Length;
		while (num3 != num5)
		{
			float num6 = scale * seq[num3];
			float num7 = scale * seq[num4];
			seq[num3] = num6 * num - num7 * num2;
			seq[num4] = num6 * num2 + num7 * num;
			num3 += 2;
			num4 += 2;
		}
	}

	// Token: 0x0600123A RID: 4666 RVA: 0x0007450C File Offset: 0x0007270C
	private static float HaltonSeq(int prime, int index = 1)
	{
		float num = 0f;
		float num2 = 1f;
		for (int i = index; i > 0; i = (int)Mathf.Floor((float)i / (float)prime))
		{
			num2 /= (float)prime;
			num += num2 * (float)(i % prime);
		}
		return num;
	}

	// Token: 0x0600123B RID: 4667 RVA: 0x00074550 File Offset: 0x00072750
	private static void InitializeHalton_2_3(float[] seq)
	{
		int num = 0;
		int num2 = seq.Length / 2;
		while (num != num2)
		{
			float num3 = FrustumJitter.HaltonSeq(2, num + 1) - 0.5f;
			float num4 = FrustumJitter.HaltonSeq(3, num + 1) - 0.5f;
			seq[2 * num] = num3;
			seq[2 * num + 1] = num4;
			num++;
		}
	}

	// Token: 0x0600123C RID: 4668 RVA: 0x000745A4 File Offset: 0x000727A4
	private static float[] AccessPointData(FrustumJitter.Pattern pattern)
	{
		switch (pattern)
		{
		case FrustumJitter.Pattern.Still:
			return FrustumJitter.points_Still;
		case FrustumJitter.Pattern.Uniform2:
			return FrustumJitter.points_Uniform2;
		case FrustumJitter.Pattern.Uniform4:
			return FrustumJitter.points_Uniform4;
		case FrustumJitter.Pattern.Uniform4_Helix:
			return FrustumJitter.points_Uniform4_Helix;
		case FrustumJitter.Pattern.Uniform4_DoubleHelix:
			return FrustumJitter.points_Uniform4_DoubleHelix;
		case FrustumJitter.Pattern.SkewButterfly:
			return FrustumJitter.points_SkewButterfly;
		case FrustumJitter.Pattern.Rotated4:
			return FrustumJitter.points_Rotated4;
		case FrustumJitter.Pattern.Rotated4_Helix:
			return FrustumJitter.points_Rotated4_Helix;
		case FrustumJitter.Pattern.Rotated4_Helix2:
			return FrustumJitter.points_Rotated4_Helix2;
		case FrustumJitter.Pattern.Poisson10:
			return FrustumJitter.points_Poisson10;
		case FrustumJitter.Pattern.Pentagram:
			return FrustumJitter.points_Pentagram;
		case FrustumJitter.Pattern.Halton_2_3_X8:
			return FrustumJitter.points_Halton_2_3_x8;
		case FrustumJitter.Pattern.Halton_2_3_X16:
			return FrustumJitter.points_Halton_2_3_x16;
		case FrustumJitter.Pattern.Halton_2_3_X32:
			return FrustumJitter.points_Halton_2_3_x32;
		case FrustumJitter.Pattern.Halton_2_3_X256:
			return FrustumJitter.points_Halton_2_3_x256;
		case FrustumJitter.Pattern.MotionPerp2:
			return FrustumJitter.points_MotionPerp2;
		default:
			Debug.LogError("missing point distribution");
			return FrustumJitter.points_Halton_2_3_x16;
		}
	}

	// Token: 0x0600123D RID: 4669 RVA: 0x00074670 File Offset: 0x00072870
	public static int AccessLength(FrustumJitter.Pattern pattern)
	{
		return FrustumJitter.AccessPointData(pattern).Length / 2;
	}

	// Token: 0x0600123E RID: 4670 RVA: 0x0007467C File Offset: 0x0007287C
	public Vector2 Sample(FrustumJitter.Pattern pattern, int index)
	{
		float[] array = FrustumJitter.AccessPointData(pattern);
		int num = array.Length / 2;
		int num2 = index % num;
		float x = this.patternScale * array[2 * num2];
		float y = this.patternScale * array[2 * num2 + 1];
		if (pattern != FrustumJitter.Pattern.MotionPerp2)
		{
			return new Vector2(x, y);
		}
		return new Vector2(x, y).Rotate(Vector2.right.SignedAngle(this.focalMotionDir));
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x000746EC File Offset: 0x000728EC
	private void OnPreCull()
	{
		Camera component = base.GetComponent<Camera>();
		if (component != null && !component.orthographic)
		{
			Vector3 v = this.focalMotionPos;
			Vector3 v2 = component.transform.TransformVector(component.nearClipPlane * Vector3.forward);
			Vector3 b = component.worldToCameraMatrix * v;
			Vector3 a = component.worldToCameraMatrix * v2;
			Vector3 a2 = (a - b).WithZ(0f);
			float magnitude = a2.magnitude;
			if (magnitude != 0f)
			{
				Vector3 b2 = a2 / magnitude;
				if (b2.sqrMagnitude != 0f)
				{
					this.focalMotionPos = v2;
					this.focalMotionDir = Vector3.Slerp(this.focalMotionDir, b2, 0.2f);
				}
			}
			this.activeIndex++;
			this.activeIndex %= FrustumJitter.AccessLength(this.pattern);
			Vector2 vector = this.Sample(this.pattern, this.activeIndex);
			this.activeSample.z = this.activeSample.x;
			this.activeSample.w = this.activeSample.y;
			this.activeSample.x = vector.x;
			this.activeSample.y = vector.y;
			component.projectionMatrix = component.GetPerspectiveProjection(vector.x, vector.y);
		}
		else
		{
			this.activeSample = Vector4.zero;
			this.activeIndex = -1;
		}
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x00074888 File Offset: 0x00072A88
	private void OnDisable()
	{
		Camera component = base.GetComponent<Camera>();
		if (component != null)
		{
			component.ResetProjectionMatrix();
		}
		this.activeSample = Vector4.zero;
		this.activeIndex = -1;
	}

	// Token: 0x04000F51 RID: 3921
	private static float[] points_Still = new float[]
	{
		0.5f,
		0.5f
	};

	// Token: 0x04000F52 RID: 3922
	private static float[] points_Uniform2 = new float[]
	{
		-0.25f,
		-0.25f,
		0.25f,
		0.25f
	};

	// Token: 0x04000F53 RID: 3923
	private static float[] points_Uniform4 = new float[]
	{
		-0.25f,
		-0.25f,
		0.25f,
		-0.25f,
		0.25f,
		0.25f,
		-0.25f,
		0.25f
	};

	// Token: 0x04000F54 RID: 3924
	private static float[] points_Uniform4_Helix = new float[]
	{
		-0.25f,
		-0.25f,
		0.25f,
		0.25f,
		0.25f,
		-0.25f,
		-0.25f,
		0.25f
	};

	// Token: 0x04000F55 RID: 3925
	private static float[] points_Uniform4_DoubleHelix = new float[]
	{
		-0.25f,
		-0.25f,
		0.25f,
		0.25f,
		0.25f,
		-0.25f,
		-0.25f,
		0.25f,
		-0.25f,
		-0.25f,
		0.25f,
		-0.25f,
		-0.25f,
		0.25f,
		0.25f,
		0.25f
	};

	// Token: 0x04000F56 RID: 3926
	private static float[] points_SkewButterfly = new float[]
	{
		-0.25f,
		-0.25f,
		0.25f,
		0.25f,
		0.125f,
		-0.125f,
		-0.125f,
		0.125f
	};

	// Token: 0x04000F57 RID: 3927
	private static float[] points_Rotated4 = new float[]
	{
		-0.125f,
		-0.375f,
		0.375f,
		-0.125f,
		0.125f,
		0.375f,
		-0.375f,
		0.125f
	};

	// Token: 0x04000F58 RID: 3928
	private static float[] points_Rotated4_Helix = new float[]
	{
		-0.125f,
		-0.375f,
		0.125f,
		0.375f,
		0.375f,
		-0.125f,
		-0.375f,
		0.125f
	};

	// Token: 0x04000F59 RID: 3929
	private static float[] points_Rotated4_Helix2 = new float[]
	{
		-0.125f,
		-0.375f,
		0.125f,
		0.375f,
		-0.375f,
		0.125f,
		0.375f,
		-0.125f
	};

	// Token: 0x04000F5A RID: 3930
	private static float[] points_Poisson10 = new float[]
	{
		-0.0419899f,
		0.16386227f,
		-0.17274007f,
		0.14753993f,
		0.12460955f,
		0.2077493f,
		0.043075375f,
		-0.009706758f,
		-0.15193167f,
		-0.015033968f,
		0.16401598f,
		0.060019f,
		0.20087093f,
		-0.12024225f,
		0.08359135f,
		-0.18251757f,
		-0.1195988f,
		-0.14001325f,
		-0.0309703f,
		-0.24158497f
	};

	// Token: 0x04000F5B RID: 3931
	private static float[] points_Pentagram = new float[]
	{
		0f,
		0.2628655f,
		-0.1545085f,
		-0.2126625f,
		0.25f,
		0.08123f,
		-0.25f,
		0.08123f,
		0.1545085f,
		-0.2126625f
	};

	// Token: 0x04000F5C RID: 3932
	private static float[] points_Halton_2_3_x8 = new float[16];

	// Token: 0x04000F5D RID: 3933
	private static float[] points_Halton_2_3_x16 = new float[32];

	// Token: 0x04000F5E RID: 3934
	private static float[] points_Halton_2_3_x32 = new float[64];

	// Token: 0x04000F5F RID: 3935
	private static float[] points_Halton_2_3_x256 = new float[512];

	// Token: 0x04000F60 RID: 3936
	private static float[] points_MotionPerp2 = new float[]
	{
		0f,
		-0.25f,
		0f,
		0.25f
	};

	// Token: 0x04000F61 RID: 3937
	private static bool _initialized = false;

	// Token: 0x04000F62 RID: 3938
	private Vector3 focalMotionPos = Vector3.zero;

	// Token: 0x04000F63 RID: 3939
	private Vector3 focalMotionDir = Vector3.right;

	// Token: 0x04000F64 RID: 3940
	public FrustumJitter.Pattern pattern = FrustumJitter.Pattern.Halton_2_3_X16;

	// Token: 0x04000F65 RID: 3941
	public float patternScale = 1f;

	// Token: 0x04000F66 RID: 3942
	public Vector4 activeSample = Vector4.zero;

	// Token: 0x04000F67 RID: 3943
	public int activeIndex = -1;

	// Token: 0x0200030B RID: 779
	public enum Pattern
	{
		// Token: 0x04000F69 RID: 3945
		Still,
		// Token: 0x04000F6A RID: 3946
		Uniform2,
		// Token: 0x04000F6B RID: 3947
		Uniform4,
		// Token: 0x04000F6C RID: 3948
		Uniform4_Helix,
		// Token: 0x04000F6D RID: 3949
		Uniform4_DoubleHelix,
		// Token: 0x04000F6E RID: 3950
		SkewButterfly,
		// Token: 0x04000F6F RID: 3951
		Rotated4,
		// Token: 0x04000F70 RID: 3952
		Rotated4_Helix,
		// Token: 0x04000F71 RID: 3953
		Rotated4_Helix2,
		// Token: 0x04000F72 RID: 3954
		Poisson10,
		// Token: 0x04000F73 RID: 3955
		Pentagram,
		// Token: 0x04000F74 RID: 3956
		Halton_2_3_X8,
		// Token: 0x04000F75 RID: 3957
		Halton_2_3_X16,
		// Token: 0x04000F76 RID: 3958
		Halton_2_3_X32,
		// Token: 0x04000F77 RID: 3959
		Halton_2_3_X256,
		// Token: 0x04000F78 RID: 3960
		MotionPerp2
	}
}
