using System;
using RTEditor;
using UnityEngine;

// Token: 0x020001A7 RID: 423
public static class RayExtensions
{
	// Token: 0x06000985 RID: 2437 RVA: 0x0003B930 File Offset: 0x00039B30
	public static bool Intersects3DCircle(this Ray ray, Vector3 circleCenter, float circleRadius, Vector3 circlePlaneNormal, bool acceptOnlyCircumference, float circumferenceEpsilon, out float t)
	{
		t = 0f;
		Plane plane = new Plane(circlePlaneNormal, circleCenter);
		if (!plane.Raycast(ray, out t))
		{
			return false;
		}
		Vector3 a = ray.origin + ray.direction * t;
		if (acceptOnlyCircumference)
		{
			Vector3 a2 = a - circleCenter;
			a2.Normalize();
			Vector3 b = circleCenter + a2 * circleRadius;
			bool flag = (a - b).magnitude <= circumferenceEpsilon;
			if (!flag)
			{
				t = 0f;
			}
			return flag;
		}
		bool flag2 = (a - circleCenter).magnitude <= circleRadius;
		if (!flag2)
		{
			t = 0f;
		}
		return flag2;
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x0003B9F4 File Offset: 0x00039BF4
	public static bool IntersectsSphere(this Ray ray, Vector3 sphereCenter, float sphereRadius, out float t)
	{
		t = 0f;
		Vector3 vector = ray.origin - sphereCenter;
		float a = Vector3.SqrMagnitude(ray.direction);
		float b = 2f * Vector3.Dot(ray.direction, vector);
		float c = Vector3.SqrMagnitude(vector) - sphereRadius * sphereRadius;
		float num;
		float num2;
		if (!Equation.SolveQuadratic(a, b, c, out num, out num2))
		{
			return false;
		}
		if (num < 0f && num2 < 0f)
		{
			return false;
		}
		if (num < 0f)
		{
			float num3 = num;
			num = num2;
			num2 = num3;
		}
		t = num;
		return true;
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0003BA8C File Offset: 0x00039C8C
	public static bool IntersectsCylinder(this Ray ray, Vector3 cylinderAxisFirstPoint, Vector3 cylinderAxisSecondPoint, float cylinderRadius, out float t)
	{
		t = 0f;
		Vector3 vector = cylinderAxisSecondPoint - cylinderAxisFirstPoint;
		float magnitude = vector.magnitude;
		vector.Normalize();
		Vector3 lhs = Vector3.Cross(ray.direction, vector);
		Vector3 rhs = Vector3.Cross(ray.origin - cylinderAxisFirstPoint, vector);
		float sqrMagnitude = lhs.sqrMagnitude;
		float b = 2f * Vector3.Dot(lhs, rhs);
		float c = rhs.sqrMagnitude - cylinderRadius * cylinderRadius;
		float num;
		float num2;
		if (!Equation.SolveQuadratic(sqrMagnitude, b, c, out num, out num2))
		{
			return false;
		}
		if (num < 0f && num2 < 0f)
		{
			return false;
		}
		if (num < 0f)
		{
			float num3 = num;
			num = num2;
			num2 = num3;
		}
		t = num;
		Vector3 a = ray.origin + ray.direction * t;
		float num4 = Vector3.Dot(vector, a - cylinderAxisFirstPoint);
		return num4 >= 0f && num4 <= magnitude;
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x0003BB94 File Offset: 0x00039D94
	public static bool IntersectsCone(this Ray ray, float coneBaseRadius, float coneHeight, Matrix4x4 coneTransformMatrix, out float t)
	{
		t = 0f;
		Ray ray2 = ray.InverseTransform(coneTransformMatrix);
		Plane plane = new Plane(-Vector3.up, Vector3.zero);
		float num;
		if (plane.Raycast(ray2, out num) && (ray2.origin + ray2.direction * num).magnitude <= coneBaseRadius)
		{
			t = num;
			return true;
		}
		float num2 = coneBaseRadius / coneHeight;
		num2 *= num2;
		float a = ray2.direction.x * ray2.direction.x + ray2.direction.z * ray2.direction.z - num2 * ray2.direction.y * ray2.direction.y;
		float b = 2f * (ray2.origin.x * ray2.direction.x + ray2.origin.z * ray2.direction.z - num2 * ray2.direction.y * (ray2.origin.y - coneHeight));
		float c = ray2.origin.x * ray2.origin.x + ray2.origin.z * ray2.origin.z - num2 * (ray2.origin.y - coneHeight) * (ray2.origin.y - coneHeight);
		float num3;
		float num4;
		if (!Equation.SolveQuadratic(a, b, c, out num3, out num4))
		{
			return false;
		}
		if (num3 < 0f && num4 < 0f)
		{
			return false;
		}
		if (num3 < 0f)
		{
			float num5 = num3;
			num3 = num4;
			num4 = num5;
		}
		t = num3;
		Vector3 vector = ray2.origin + ray2.direction * t;
		return vector.y >= 0f && vector.y <= coneHeight;
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0003BDE4 File Offset: 0x00039FE4
	public static bool IntersectsBox(this Ray ray, float boxWidth, float boxHeight, float boxDepth, Matrix4x4 boxTransformMatrix, out float t)
	{
		t = 0f;
		Ray ray2 = ray.InverseTransform(boxTransformMatrix);
		Vector3 origin = ray2.origin;
		Vector3 direction = ray2.direction;
		Vector3 vector = new Vector3(1f / direction.x, 1f / direction.y, 1f / direction.z);
		float num = boxWidth * 0.5f;
		float num2 = boxHeight * 0.5f;
		float num3 = boxDepth * 0.5f;
		Vector3 vector2 = new Vector3(-num, -num2, -num3);
		Vector3 vector3 = new Vector3(num, num2, num3);
		float num4 = float.MinValue;
		float num5 = float.MaxValue;
		for (int i = 0; i < 3; i++)
		{
			if (Mathf.Abs(direction[i]) > 0.0001f)
			{
				float num6 = (vector2[i] - origin[i]) * vector[i];
				float num7 = (vector3[i] - origin[i]) * vector[i];
				if (num6 > num7)
				{
					float num8 = num6;
					num6 = num7;
					num7 = num8;
				}
				if (num6 > num4)
				{
					num4 = num6;
				}
				if (num7 < num5)
				{
					num5 = num7;
				}
				if (num4 > num5)
				{
					return false;
				}
				if (num5 < 0f)
				{
					return false;
				}
			}
			else if (origin[i] < vector2[i] || origin[i] > vector3[i])
			{
				return false;
			}
		}
		t = num4;
		if (t < 0f)
		{
			t = num5;
		}
		return true;
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0003BF88 File Offset: 0x0003A188
	public static Ray InverseTransform(this Ray ray, Matrix4x4 transformMatrix)
	{
		Matrix4x4 inverse = transformMatrix.inverse;
		Vector3 origin = inverse.MultiplyPoint(ray.origin);
		Vector3 direction = inverse.MultiplyVector(ray.direction);
		return new Ray(origin, direction);
	}
}
