using System;
using UnityEngine;

// Token: 0x020001A8 RID: 424
public static class RectExtensions
{
	// Token: 0x0600098B RID: 2443 RVA: 0x0003BFC4 File Offset: 0x0003A1C4
	public static Vector2 GetClosestPointToPoint(this Rect rectangle, Vector2 point)
	{
		Vector2[] cornerAndCenterPoints = rectangle.GetCornerAndCenterPoints();
		int num = 0;
		float num2 = float.MaxValue;
		for (int i = 0; i < cornerAndCenterPoints.Length; i++)
		{
			float magnitude = (cornerAndCenterPoints[i] - point).magnitude;
			if (magnitude < num2)
			{
				num2 = magnitude;
				num = i;
			}
		}
		return cornerAndCenterPoints[num];
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0003C02C File Offset: 0x0003A22C
	public static Vector2[] GetCornerAndCenterPoints(this Rect rectangle)
	{
		return new Vector2[]
		{
			new Vector2(rectangle.xMin, rectangle.yMin),
			new Vector2(rectangle.xMax, rectangle.yMin),
			new Vector2(rectangle.xMax, rectangle.yMax),
			new Vector2(rectangle.xMin, rectangle.yMax),
			rectangle.center
		};
	}
}
