using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001BC RID: 444
	public static class GLPrimitives
	{
		// Token: 0x06000AB8 RID: 2744 RVA: 0x000435BC File Offset: 0x000417BC
		public static void Draw3DLine(Vector3 firstPoint, Vector3 secondPoint, Color lineColor, Material lineMaterial)
		{
			lineMaterial.SetColor("_Color", lineColor);
			lineMaterial.SetPass(0);
			GL.Begin(1);
			GL.Color(lineColor);
			GL.Vertex3(firstPoint.x, firstPoint.y, firstPoint.z);
			GL.Vertex3(secondPoint.x, secondPoint.y, secondPoint.z);
			GL.End();
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00043624 File Offset: 0x00041824
		public static void Draw3DLines(Vector3[] linePoints, Color[] lineColors, bool drawConnectedLines, Material lineMaterial, bool loop, Color loopLineColor)
		{
			lineMaterial.SetPass(0);
			int num = (!drawConnectedLines) ? (linePoints.Length / 2) : (linePoints.Length - 1);
			GL.Begin(1);
			if (!drawConnectedLines)
			{
				for (int i = 0; i < num; i++)
				{
					int num2 = i * 2;
					Vector3 vector = linePoints[num2];
					Vector3 vector2 = linePoints[num2 + 1];
					lineMaterial.SetColor("_Color", lineColors[i]);
					GL.Color(lineColors[i]);
					GL.Vertex3(vector.x, vector.y, vector.z);
					GL.Vertex3(vector2.x, vector2.y, vector2.z);
				}
			}
			else
			{
				for (int j = 0; j < num; j++)
				{
					Vector3 vector3 = linePoints[j];
					Vector3 vector4 = linePoints[j + 1];
					lineMaterial.SetColor("_Color", lineColors[j]);
					GL.Color(lineColors[j]);
					GL.Vertex3(vector3.x, vector3.y, vector3.z);
					GL.Vertex3(vector4.x, vector4.y, vector4.z);
				}
			}
			if (loop)
			{
				Vector3 vector5 = linePoints[0];
				Vector3 vector6 = linePoints[linePoints.Length - 1];
				lineMaterial.SetColor("_Color", loopLineColor);
				GL.Color(loopLineColor);
				GL.Vertex3(vector5.x, vector5.y, vector5.z);
				GL.Vertex3(vector6.x, vector6.y, vector6.z);
			}
			GL.End();
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x000437F8 File Offset: 0x000419F8
		public static void Draw3DLine(Vector3 firstPoint, Vector3 secondPoint, Color firstPointColor, Color secondPointColor, Material lineMaterial)
		{
			lineMaterial.SetColor("_Color", firstPointColor);
			lineMaterial.SetPass(0);
			GL.Begin(1);
			GL.Color(firstPointColor);
			GL.Vertex3(firstPoint.x, firstPoint.y, firstPoint.z);
			GL.Color(secondPointColor);
			GL.Vertex3(secondPoint.x, secondPoint.y, secondPoint.z);
			GL.End();
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00043868 File Offset: 0x00041A68
		public static void Draw2DLine(Vector2 firstPoint, Vector2 secondPoint, Color lineColor, Material lineMaterial)
		{
			lineMaterial.SetColor("_Color", lineColor);
			lineMaterial.SetPass(0);
			GL.PushMatrix();
			GL.LoadOrtho();
			float num = 1f / (float)Screen.width;
			float num2 = 1f / (float)Screen.height;
			GL.Begin(1);
			GL.Color(lineColor);
			GL.Vertex(new Vector3(firstPoint.x * num, firstPoint.y * num2, 0f));
			GL.Vertex(new Vector3(secondPoint.x * num, secondPoint.y * num2, 0f));
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00043908 File Offset: 0x00041B08
		public static void Draw2DCircleBorderLines(Vector3[] borderLinePoints, Vector3 circleCenter, Color borderLineColor, float radiusScale, Material borderLineMaterial)
		{
			borderLineMaterial.SetColor("_Color", borderLineColor);
			borderLineMaterial.SetPass(0);
			GL.PushMatrix();
			GL.LoadOrtho();
			float d = (borderLinePoints[0] - circleCenter).magnitude * radiusScale;
			float num = 1f / (float)Screen.width;
			float num2 = 1f / (float)Screen.height;
			GL.Begin(1);
			GL.Color(borderLineColor);
			for (int i = 0; i < borderLinePoints.Length; i++)
			{
				Vector3 a = borderLinePoints[i] - circleCenter;
				Vector3 a2 = borderLinePoints[(i + 1) % borderLinePoints.Length] - circleCenter;
				a.Normalize();
				a2.Normalize();
				Vector3 vector = circleCenter + a * d;
				Vector3 vector2 = circleCenter + a2 * d;
				GL.Vertex(new Vector3(vector.x * num, vector.y * num2, 0f));
				GL.Vertex(new Vector3(vector2.x * num, vector2.y * num2, 0f));
			}
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00043A38 File Offset: 0x00041C38
		public static void Draw2DRectangleBorderLines(Vector2[] borderLinePoints, Color borderLineColor, Material borderLineMaterial)
		{
			borderLineMaterial.SetColor("_Color", borderLineColor);
			borderLineMaterial.SetPass(0);
			GL.PushMatrix();
			GL.LoadOrtho();
			float num = 1f / (float)Screen.width;
			float num2 = 1f / (float)Screen.height;
			GL.Begin(1);
			GL.Color(borderLineColor);
			for (int i = 0; i < borderLinePoints.Length; i++)
			{
				Vector3 vector = borderLinePoints[i];
				Vector3 vector2 = borderLinePoints[(i + 1) % borderLinePoints.Length];
				GL.Vertex(new Vector3(vector.x * num, vector.y * num2, 0f));
				GL.Vertex(new Vector3(vector2.x * num, vector2.y * num2, 0f));
			}
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00043B14 File Offset: 0x00041D14
		public static void Draw2DRectangleBorderLines(Rect rectangle, Color borderLineColor, Material borderLineMaterial)
		{
			Vector2[] borderLinePoints = new Vector2[]
			{
				new Vector2(rectangle.xMin, rectangle.yMin),
				new Vector2(rectangle.xMax, rectangle.yMin),
				new Vector2(rectangle.xMax, rectangle.yMax),
				new Vector2(rectangle.xMin, rectangle.yMax)
			};
			GLPrimitives.Draw2DRectangleBorderLines(borderLinePoints, borderLineColor, borderLineMaterial);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00043BAC File Offset: 0x00041DAC
		public static void Draw2DFilledRectangle(Rect rectangle, Color rectangleColor, Material rectangleMaterial)
		{
			rectangleMaterial.SetColor("_Color", rectangleColor);
			rectangleMaterial.SetPass(0);
			GL.PushMatrix();
			GL.LoadOrtho();
			GL.Begin(7);
			GL.Color(rectangleColor);
			float num = 1f / (float)Screen.width;
			float num2 = 1f / (float)Screen.height;
			GL.Vertex3(rectangle.xMin * num, rectangle.yMin * num2, 0f);
			GL.Vertex3(rectangle.xMax * num, rectangle.yMin * num2, 0f);
			GL.Vertex3(rectangle.xMax * num, rectangle.yMax * num2, 0f);
			GL.Vertex3(rectangle.xMin * num, rectangle.yMax * num2, 0f);
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00043C78 File Offset: 0x00041E78
		public static void Draw3DFilledDisc(Vector3 discCenter, Vector3 firstPoint, Vector3 secondPoint, Vector3 discPlaneNormal, Color discColor, Material discMaterial)
		{
			Vector3 vector = firstPoint - discCenter;
			Vector3 rhs = secondPoint - discCenter;
			float magnitude = vector.magnitude;
			vector.Normalize();
			rhs.Normalize();
			float num;
			if (Vector3.Dot(Vector3.Cross(vector, rhs), discPlaneNormal) < 0f)
			{
				num = -1f;
			}
			else
			{
				num = 1f;
			}
			discPlaneNormal.Normalize();
			float num2 = MathHelper.SafeAcos(Vector3.Dot(vector, rhs)) * 57.29578f * num;
			Quaternion b = Quaternion.AngleAxis(num2, discPlaneNormal);
			int num3 = (int)(180f * Mathf.Abs(num2) / 180f);
			if (num3 < 2)
			{
				return;
			}
			discMaterial.SetColor("_Color", discColor);
			discMaterial.SetPass(0);
			GL.Begin(4);
			GL.Color(discColor);
			float num4 = 1f / (float)(num3 - 1);
			Vector3 v = discCenter + vector * magnitude;
			for (int i = 0; i < num3; i++)
			{
				Quaternion rotation = Quaternion.Slerp(Quaternion.identity, b, num4 * (float)i);
				Vector3 a = rotation * vector;
				a.Normalize();
				Vector3 vector2 = discCenter + a * magnitude;
				GL.Vertex(discCenter);
				GL.Vertex(vector2);
				GL.Vertex(v);
				v = vector2;
			}
			GL.End();
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00043DC8 File Offset: 0x00041FC8
		public static void Draw2DFilledDisc(Vector2 discCenter, Vector2 firstPoint, Vector2 secondPoint, Color discColor, Material discMaterial)
		{
			Vector2 vector = firstPoint - discCenter;
			Vector2 v = secondPoint - discCenter;
			float magnitude = vector.magnitude;
			vector.Normalize();
			v.Normalize();
			float num;
			if (Vector3.Dot(Vector3.Cross(vector, v), Vector3.forward) < 0f)
			{
				num = -1f;
			}
			else
			{
				num = 1f;
			}
			float num2 = MathHelper.SafeAcos(Vector3.Dot(vector, v)) * 57.29578f * num;
			Quaternion b = Quaternion.AngleAxis(num2, Vector3.forward);
			int num3 = (int)(180f * Mathf.Abs(num2) / 180f);
			if (num3 < 2)
			{
				return;
			}
			discMaterial.SetColor("_Color", discColor);
			discMaterial.SetPass(0);
			GL.PushMatrix();
			GL.LoadOrtho();
			GL.Begin(4);
			GL.Color(discColor);
			float num4 = 1f / (float)Screen.width;
			float num5 = 1f / (float)Screen.height;
			float num6 = 1f / (float)(num3 - 1);
			Vector3 vector2 = discCenter + vector * magnitude;
			for (int i = 0; i < num3; i++)
			{
				Quaternion rotation = Quaternion.Slerp(Quaternion.identity, b, num6 * (float)i);
				Vector2 a = rotation * vector;
				a.Normalize();
				Vector3 vector3 = discCenter + a * magnitude;
				GL.Vertex(new Vector3(discCenter.x * num4, discCenter.y * num5, 0f));
				GL.Vertex(new Vector3(vector3.x * num4, vector3.y * num5, 0f));
				GL.Vertex(new Vector3(vector2.x * num4, vector2.y * num5, 0f));
				vector2 = vector3;
			}
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00043FC0 File Offset: 0x000421C0
		public static void DrawWireBoxes(List<Bounds> boxes, List<Matrix4x4> boxTransformMatrices, float boxSizeScaleFactor, Camera camera, Color lineColor, Material boxLineMaterial)
		{
			GL.PushMatrix();
			boxLineMaterial.SetColor("_Color", lineColor);
			boxLineMaterial.SetPass(0);
			Matrix4x4 identity = Matrix4x4.identity;
			ref Matrix4x4 ptr = ref identity;
			int row2;
			int row = row2 = 2;
			int column2;
			int column = column2 = 2;
			float num = ptr[row2, column2];
			identity[row, column] = num * -1f;
			Matrix4x4 worldToLocalMatrix = camera.transform.worldToLocalMatrix;
			for (int i = 0; i < boxes.Count; i++)
			{
				Bounds bounds = boxes[i];
				bounds.size *= boxSizeScaleFactor;
				GL.LoadIdentity();
				GL.MultMatrix(identity * worldToLocalMatrix * boxTransformMatrices[i]);
				GL.Begin(1);
				GL.Color(lineColor);
				GL.Vertex3(bounds.min.x, bounds.min.y, bounds.min.z);
				GL.Vertex3(bounds.max.x, bounds.min.y, bounds.min.z);
				GL.Vertex3(bounds.max.x, bounds.min.y, bounds.min.z);
				GL.Vertex3(bounds.max.x, bounds.max.y, bounds.min.z);
				GL.Vertex3(bounds.max.x, bounds.max.y, bounds.min.z);
				GL.Vertex3(bounds.min.x, bounds.max.y, bounds.min.z);
				GL.Vertex3(bounds.min.x, bounds.max.y, bounds.min.z);
				GL.Vertex3(bounds.min.x, bounds.min.y, bounds.min.z);
				GL.Vertex3(bounds.min.x, bounds.min.y, bounds.max.z);
				GL.Vertex3(bounds.max.x, bounds.min.y, bounds.max.z);
				GL.Vertex3(bounds.max.x, bounds.min.y, bounds.max.z);
				GL.Vertex3(bounds.max.x, bounds.max.y, bounds.max.z);
				GL.Vertex3(bounds.max.x, bounds.max.y, bounds.max.z);
				GL.Vertex3(bounds.min.x, bounds.max.y, bounds.max.z);
				GL.Vertex3(bounds.min.x, bounds.max.y, bounds.max.z);
				GL.Vertex3(bounds.min.x, bounds.min.y, bounds.max.z);
				GL.Vertex3(bounds.min.x, bounds.min.y, bounds.min.z);
				GL.Vertex3(bounds.min.x, bounds.min.y, bounds.max.z);
				GL.Vertex3(bounds.max.x, bounds.min.y, bounds.min.z);
				GL.Vertex3(bounds.max.x, bounds.min.y, bounds.max.z);
				GL.Vertex3(bounds.min.x, bounds.max.y, bounds.min.z);
				GL.Vertex3(bounds.min.x, bounds.max.y, bounds.max.z);
				GL.Vertex3(bounds.max.x, bounds.max.y, bounds.min.z);
				GL.Vertex3(bounds.max.x, bounds.max.y, bounds.max.z);
				GL.End();
			}
			GL.PopMatrix();
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00044580 File Offset: 0x00042780
		public static void DrawCornerLinesForBoxes(List<Bounds> boxes, List<Matrix4x4> boxTransformMatrices, float boxSizeScaleFactor, float cornerLineLength, Camera camera, Color cornerLineColor, Material boxLineMaterial)
		{
			GL.PushMatrix();
			boxLineMaterial.SetColor("_Color", cornerLineColor);
			boxLineMaterial.SetPass(0);
			Matrix4x4 identity = Matrix4x4.identity;
			ref Matrix4x4 ptr = ref identity;
			int row2;
			int row = row2 = 2;
			int column2;
			int column = column2 = 2;
			float num = ptr[row2, column2];
			identity[row, column] = num * -1f;
			Matrix4x4 worldToLocalMatrix = camera.transform.worldToLocalMatrix;
			for (int i = 0; i < boxes.Count; i++)
			{
				Bounds bounds = boxes[i];
				Matrix4x4 matrix4x = boxTransformMatrices[i];
				bounds.size *= boxSizeScaleFactor;
				Vector3 scaleTransform = matrix4x.GetScaleTransform();
				bounds.size = Vector3.Scale(bounds.size, scaleTransform);
				bounds.center = Vector3.Scale(bounds.center, scaleTransform);
				matrix4x = matrix4x.SetScaleToOneOnAllAxes();
				GL.LoadIdentity();
				GL.MultMatrix(identity * worldToLocalMatrix * matrix4x);
				GL.Begin(1);
				GL.Color(cornerLineColor);
				float d = Mathf.Min(cornerLineLength, bounds.extents.x * 0.8f);
				float d2 = Mathf.Min(cornerLineLength, bounds.extents.y * 0.8f);
				float d3 = Mathf.Min(cornerLineLength, bounds.extents.z * 0.8f);
				Vector3 vector = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
				Vector3 v = vector + Vector3.right * d;
				GL.Vertex(vector);
				GL.Vertex(v);
				v = vector - Vector3.up * d2;
				GL.Vertex(vector);
				GL.Vertex(v);
				v = vector + Vector3.forward * d3;
				GL.Vertex(vector);
				GL.Vertex(v);
				vector = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
				v = vector - Vector3.right * d;
				GL.Vertex(vector);
				GL.Vertex(v);
				v = vector - Vector3.up * d2;
				GL.Vertex(vector);
				GL.Vertex(v);
				v = vector + Vector3.forward * d3;
				GL.Vertex(vector);
				GL.Vertex(v);
				vector = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
				v = vector - Vector3.right * d;
				GL.Vertex(vector);
				GL.Vertex(v);
				v = vector + Vector3.up * d2;
				GL.Vertex(vector);
				GL.Vertex(v);
				v = vector + Vector3.forward * d3;
				GL.Vertex(vector);
				GL.Vertex(v);
				vector = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
				v = vector + Vector3.right * d;
				GL.Vertex(vector);
				GL.Vertex(v);
				v = vector + Vector3.up * d2;
				GL.Vertex(vector);
				GL.Vertex(v);
				v = vector + Vector3.forward * d3;
				GL.Vertex(vector);
				GL.Vertex(v);
				vector = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
				v = vector + Vector3.right * d;
				GL.Vertex(vector);
				GL.Vertex(v);
				v = vector - Vector3.up * d2;
				GL.Vertex(vector);
				GL.Vertex(v);
				v = vector - Vector3.forward * d3;
				GL.Vertex(vector);
				GL.Vertex(v);
				vector = new Vector3(bounds.max.x, bounds.max.y, bounds.max.z);
				v = vector - Vector3.right * d;
				GL.Vertex(vector);
				GL.Vertex(v);
				v = vector - Vector3.up * d2;
				GL.Vertex(vector);
				GL.Vertex(v);
				v = vector - Vector3.forward * d3;
				GL.Vertex(vector);
				GL.Vertex(v);
				vector = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
				v = vector - Vector3.right * d;
				GL.Vertex(vector);
				GL.Vertex(v);
				v = vector + Vector3.up * d2;
				GL.Vertex(vector);
				GL.Vertex(v);
				v = vector - Vector3.forward * d3;
				GL.Vertex(vector);
				GL.Vertex(v);
				vector = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
				v = vector + Vector3.right * d;
				GL.Vertex(vector);
				GL.Vertex(v);
				v = vector + Vector3.up * d2;
				GL.Vertex(vector);
				GL.Vertex(v);
				v = vector - Vector3.forward * d3;
				GL.Vertex(vector);
				GL.Vertex(v);
				GL.End();
			}
			GL.PopMatrix();
		}
	}
}
