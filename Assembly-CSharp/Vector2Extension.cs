using System;
using UnityEngine;

// Token: 0x02000306 RID: 774
public static class Vector2Extension
{
	// Token: 0x0600122D RID: 4653 RVA: 0x00073EE8 File Offset: 0x000720E8
	public static float SignedAngle(this Vector2 v1, Vector2 v2)
	{
		Vector2 normalized = v1.normalized;
		Vector2 normalized2 = v2.normalized;
		float num = Vector2.Dot(normalized, normalized2);
		if (num > 1f)
		{
			num = 1f;
		}
		if (num < -1f)
		{
			num = -1f;
		}
		float num2 = Mathf.Acos(num);
		float num3 = Vector2.Dot(new Vector2(-normalized.y, normalized.x), normalized2);
		if (num3 >= 0f)
		{
			return num2;
		}
		return -num2;
	}

	// Token: 0x0600122E RID: 4654 RVA: 0x00073F64 File Offset: 0x00072164
	public static Vector2 Rotate(this Vector2 v, float theta)
	{
		float num = Mathf.Cos(theta);
		float num2 = Mathf.Sin(theta);
		float x = v.x * num - v.y * num2;
		float y = v.x * num2 + v.y * num;
		return new Vector2(x, y);
	}
}
