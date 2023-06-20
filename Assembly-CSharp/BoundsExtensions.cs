using System;
using UnityEngine;

// Token: 0x020001A0 RID: 416
public static class BoundsExtensions
{
	// Token: 0x06000953 RID: 2387 RVA: 0x0003A7E8 File Offset: 0x000389E8
	public static Bounds GetInvalidBoundsInstance()
	{
		return new Bounds(Vector3.zero, BoundsExtensions.GetInvalidBoundsSize());
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x0003A7FC File Offset: 0x000389FC
	public static bool IsValid(this Bounds bounds)
	{
		return bounds.size != BoundsExtensions.GetInvalidBoundsSize();
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x0003A810 File Offset: 0x00038A10
	public static Bounds Transform(this Bounds bounds, Matrix4x4 transformMatrix)
	{
		Vector3 a = transformMatrix.GetColumn(0);
		Vector3 a2 = transformMatrix.GetColumn(1);
		Vector3 a3 = transformMatrix.GetColumn(2);
		Vector3 vector = a * bounds.extents.x;
		Vector3 vector2 = a2 * bounds.extents.y;
		Vector3 vector3 = a3 * bounds.extents.z;
		float x = (Mathf.Abs(vector.x) + Mathf.Abs(vector2.x) + Mathf.Abs(vector3.x)) * 2f;
		float y = (Mathf.Abs(vector.y) + Mathf.Abs(vector2.y) + Mathf.Abs(vector3.y)) * 2f;
		float z = (Mathf.Abs(vector.z) + Mathf.Abs(vector2.z) + Mathf.Abs(vector3.z)) * 2f;
		return new Bounds
		{
			center = transformMatrix.MultiplyPoint(bounds.center),
			size = new Vector3(x, y, z)
		};
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x0003A94C File Offset: 0x00038B4C
	public static Vector2[] GetScreenSpaceCornerPoints(this Bounds bounds, Camera camera)
	{
		Vector3 center = bounds.center;
		Vector3 extents = bounds.extents;
		return new Vector2[]
		{
			camera.WorldToScreenPoint(new Vector3(center.x - extents.x, center.y - extents.y, center.z - extents.z)),
			camera.WorldToScreenPoint(new Vector3(center.x + extents.x, center.y - extents.y, center.z - extents.z)),
			camera.WorldToScreenPoint(new Vector3(center.x + extents.x, center.y + extents.y, center.z - extents.z)),
			camera.WorldToScreenPoint(new Vector3(center.x - extents.x, center.y + extents.y, center.z - extents.z)),
			camera.WorldToScreenPoint(new Vector3(center.x - extents.x, center.y - extents.y, center.z + extents.z)),
			camera.WorldToScreenPoint(new Vector3(center.x + extents.x, center.y - extents.y, center.z + extents.z)),
			camera.WorldToScreenPoint(new Vector3(center.x + extents.x, center.y + extents.y, center.z + extents.z)),
			camera.WorldToScreenPoint(new Vector3(center.x - extents.x, center.y + extents.y, center.z + extents.z))
		};
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x0003ABB8 File Offset: 0x00038DB8
	public static Rect GetScreenRectangle(this Bounds bounds, Camera camera)
	{
		Vector2[] screenSpaceCornerPoints = bounds.GetScreenSpaceCornerPoints(camera);
		Vector3 lhs = screenSpaceCornerPoints[0];
		Vector3 lhs2 = screenSpaceCornerPoints[0];
		for (int i = 1; i < screenSpaceCornerPoints.Length; i++)
		{
			lhs = Vector3.Min(lhs, screenSpaceCornerPoints[i]);
			lhs2 = Vector3.Max(lhs2, screenSpaceCornerPoints[i]);
		}
		return new Rect(lhs.x, lhs.y, lhs2.x - lhs.x, lhs2.y - lhs.y);
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x0003AC68 File Offset: 0x00038E68
	private static Vector3 GetInvalidBoundsSize()
	{
		return new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
	}
}
