using System;
using UnityEngine;

// Token: 0x020001A9 RID: 425
public static class Vector3Extensions
{
	// Token: 0x0600098D RID: 2445 RVA: 0x0003C0D0 File Offset: 0x0003A2D0
	public static bool IsInsideTriangle(this Vector3 point, Vector3[] trianglePoints)
	{
		Vector3 lhs = trianglePoints[1] - trianglePoints[0];
		Vector3 rhs = trianglePoints[2] - trianglePoints[0];
		Vector3 rhs2 = Vector3.Cross(lhs, rhs);
		for (int i = 0; i < 3; i++)
		{
			Vector3 lhs2 = trianglePoints[(i + 1) % 3] - trianglePoints[i];
			Vector3 lhs3 = Vector3.Cross(lhs2, rhs2);
			Vector3 rhs3 = point - trianglePoints[i];
			float num = Vector3.Dot(lhs3, rhs3);
			if (num > 0f)
			{
				return false;
			}
		}
		return true;
	}
}
