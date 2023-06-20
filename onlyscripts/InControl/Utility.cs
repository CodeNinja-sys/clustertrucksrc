using System;
using System.IO;
using Microsoft.Win32;
using UnityEngine;

namespace InControl
{
	// Token: 0x020000FA RID: 250
	public static class Utility
	{
		// Token: 0x06000557 RID: 1367 RVA: 0x000280E0 File Offset: 0x000262E0
		public static void DrawCircleGizmo(Vector2 center, float radius)
		{
			Vector2 v = Utility.circleVertexList[0] * radius + center;
			int num = Utility.circleVertexList.Length;
			for (int i = 1; i < num; i++)
			{
				Gizmos.DrawLine(v, v = Utility.circleVertexList[i] * radius + center);
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00028154 File Offset: 0x00026354
		public static void DrawCircleGizmo(Vector2 center, float radius, Color color)
		{
			Gizmos.color = color;
			Utility.DrawCircleGizmo(center, radius);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00028164 File Offset: 0x00026364
		public static void DrawOvalGizmo(Vector2 center, Vector2 size)
		{
			Vector2 b = size / 2f;
			Vector2 v = Vector2.Scale(Utility.circleVertexList[0], b) + center;
			int num = Utility.circleVertexList.Length;
			for (int i = 1; i < num; i++)
			{
				Gizmos.DrawLine(v, v = Vector2.Scale(Utility.circleVertexList[i], b) + center);
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000281E4 File Offset: 0x000263E4
		public static void DrawOvalGizmo(Vector2 center, Vector2 size, Color color)
		{
			Gizmos.color = color;
			Utility.DrawOvalGizmo(center, size);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000281F4 File Offset: 0x000263F4
		public static void DrawRectGizmo(Rect rect)
		{
			Vector3 vector = new Vector3(rect.xMin, rect.yMin);
			Vector3 vector2 = new Vector3(rect.xMax, rect.yMin);
			Vector3 vector3 = new Vector3(rect.xMax, rect.yMax);
			Vector3 vector4 = new Vector3(rect.xMin, rect.yMax);
			Gizmos.DrawLine(vector, vector2);
			Gizmos.DrawLine(vector2, vector3);
			Gizmos.DrawLine(vector3, vector4);
			Gizmos.DrawLine(vector4, vector);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00028274 File Offset: 0x00026474
		public static void DrawRectGizmo(Rect rect, Color color)
		{
			Gizmos.color = color;
			Utility.DrawRectGizmo(rect);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00028284 File Offset: 0x00026484
		public static void DrawRectGizmo(Vector2 center, Vector2 size)
		{
			float num = size.x / 2f;
			float num2 = size.y / 2f;
			Vector3 vector = new Vector3(center.x - num, center.y - num2);
			Vector3 vector2 = new Vector3(center.x + num, center.y - num2);
			Vector3 vector3 = new Vector3(center.x + num, center.y + num2);
			Vector3 vector4 = new Vector3(center.x - num, center.y + num2);
			Gizmos.DrawLine(vector, vector2);
			Gizmos.DrawLine(vector2, vector3);
			Gizmos.DrawLine(vector3, vector4);
			Gizmos.DrawLine(vector4, vector);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00028334 File Offset: 0x00026534
		public static void DrawRectGizmo(Vector2 center, Vector2 size, Color color)
		{
			Gizmos.color = color;
			Utility.DrawRectGizmo(center, size);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00028344 File Offset: 0x00026544
		public static bool GameObjectIsCulledOnCurrentCamera(GameObject gameObject)
		{
			return (Camera.current.cullingMask & 1 << gameObject.layer) == 0;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0002836C File Offset: 0x0002656C
		public static Color MoveColorTowards(Color color0, Color color1, float maxDelta)
		{
			float r = Mathf.MoveTowards(color0.r, color1.r, maxDelta);
			float g = Mathf.MoveTowards(color0.g, color1.g, maxDelta);
			float b = Mathf.MoveTowards(color0.b, color1.b, maxDelta);
			float a = Mathf.MoveTowards(color0.a, color1.a, maxDelta);
			return new Color(r, g, b, a);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000283D8 File Offset: 0x000265D8
		public static float ApplyDeadZone(float value, float lowerDeadZone, float upperDeadZone)
		{
			if (value < 0f)
			{
				if (value > -lowerDeadZone)
				{
					return 0f;
				}
				if (value < -upperDeadZone)
				{
					return -1f;
				}
				return (value + lowerDeadZone) / (upperDeadZone - lowerDeadZone);
			}
			else
			{
				if (value < lowerDeadZone)
				{
					return 0f;
				}
				if (value > upperDeadZone)
				{
					return 1f;
				}
				return (value - lowerDeadZone) / (upperDeadZone - lowerDeadZone);
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00028438 File Offset: 0x00026638
		public static Vector2 ApplyCircularDeadZone(Vector2 v, float lowerDeadZone, float upperDeadZone)
		{
			float d = Mathf.InverseLerp(lowerDeadZone, upperDeadZone, v.magnitude);
			return v.normalized * d;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00028464 File Offset: 0x00026664
		public static Vector2 ApplyCircularDeadZone(float x, float y, float lowerDeadZone, float upperDeadZone)
		{
			return Utility.ApplyCircularDeadZone(new Vector2(x, y), lowerDeadZone, upperDeadZone);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00028474 File Offset: 0x00026674
		public static float ApplySmoothing(float thisValue, float lastValue, float deltaTime, float sensitivity)
		{
			if (Utility.Approximately(sensitivity, 1f))
			{
				return thisValue;
			}
			float maxDelta = deltaTime * sensitivity * 100f;
			if (Mathf.Sign(lastValue) != Mathf.Sign(thisValue))
			{
				lastValue = 0f;
			}
			return Mathf.MoveTowards(lastValue, thisValue, maxDelta);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000284C0 File Offset: 0x000266C0
		public static float ApplySnapping(float value, float threshold)
		{
			if (value < -threshold)
			{
				return -1f;
			}
			if (value > threshold)
			{
				return 1f;
			}
			return 0f;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000284F0 File Offset: 0x000266F0
		internal static bool TargetIsButton(InputControlType target)
		{
			return (target >= InputControlType.Action1 && target <= InputControlType.Action4) || (target >= InputControlType.Button0 && target <= InputControlType.Button19);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00028524 File Offset: 0x00026724
		internal static bool TargetIsStandard(InputControlType target)
		{
			return target >= InputControlType.LeftStickUp && target <= InputControlType.RightBumper;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00028538 File Offset: 0x00026738
		public static string ReadFromFile(string path)
		{
			StreamReader streamReader = new StreamReader(path);
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			return result;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0002855C File Offset: 0x0002675C
		public static void WriteToFile(string path, string data)
		{
			StreamWriter streamWriter = new StreamWriter(path);
			streamWriter.Write(data);
			streamWriter.Flush();
			streamWriter.Close();
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00028584 File Offset: 0x00026784
		public static float Abs(float value)
		{
			return (value >= 0f) ? value : (-value);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0002859C File Offset: 0x0002679C
		public static bool Approximately(float value1, float value2)
		{
			float num = value1 - value2;
			return num >= -1E-07f && num <= 1E-07f;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000285C8 File Offset: 0x000267C8
		public static bool IsNotZero(float value)
		{
			return value < -1E-07f || value > 1E-07f;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000285E0 File Offset: 0x000267E0
		public static bool IsZero(float value)
		{
			return value >= -1E-07f && value <= 1E-07f;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000285FC File Offset: 0x000267FC
		public static bool AbsoluteIsOverThreshold(float value, float threshold)
		{
			return value < -threshold || value > threshold;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00028610 File Offset: 0x00026810
		public static float NormalizeAngle(float angle)
		{
			while (angle < 0f)
			{
				angle += 360f;
			}
			while (angle > 360f)
			{
				angle -= 360f;
			}
			return angle;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00028648 File Offset: 0x00026848
		public static float VectorToAngle(Vector2 vector)
		{
			if (Utility.IsZero(vector.x) && Utility.IsZero(vector.y))
			{
				return 0f;
			}
			return Utility.NormalizeAngle(Mathf.Atan2(vector.x, vector.y) * 57.29578f);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0002869C File Offset: 0x0002689C
		public static float Min(float v0, float v1, float v2, float v3)
		{
			float num = (v0 < v1) ? v0 : v1;
			float num2 = (v2 < v3) ? v2 : v3;
			return (num < num2) ? num : num2;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000286D8 File Offset: 0x000268D8
		public static float Max(float v0, float v1, float v2, float v3)
		{
			float num = (v0 > v1) ? v0 : v1;
			float num2 = (v2 > v3) ? v2 : v3;
			return (num > num2) ? num : num2;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00028714 File Offset: 0x00026914
		internal static float ValueFromSides(float negativeSide, float positiveSide)
		{
			float num = Utility.Abs(negativeSide);
			float num2 = Utility.Abs(positiveSide);
			if (Utility.Approximately(num, num2))
			{
				return 0f;
			}
			return (num <= num2) ? num2 : (-num);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00028750 File Offset: 0x00026950
		internal static float ValueFromSides(float negativeSide, float positiveSide, bool invertSides)
		{
			if (invertSides)
			{
				return Utility.ValueFromSides(positiveSide, negativeSide);
			}
			return Utility.ValueFromSides(negativeSide, positiveSide);
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00028768 File Offset: 0x00026968
		internal static bool Is32Bit
		{
			get
			{
				return IntPtr.Size == 4;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00028774 File Offset: 0x00026974
		internal static bool Is64Bit
		{
			get
			{
				return IntPtr.Size == 8;
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00028780 File Offset: 0x00026980
		public static string HKLM_GetString(string path, string key)
		{
			string result;
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(path);
				if (registryKey == null)
				{
					result = string.Empty;
				}
				else
				{
					result = (string)registryKey.GetValue(key);
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000287EC File Offset: 0x000269EC
		public static string GetWindowsVersion()
		{
			string text = Utility.HKLM_GetString("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "ProductName");
			if (text != null)
			{
				string text2 = Utility.HKLM_GetString("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "CSDVersion");
				string str = (!Utility.Is32Bit) ? "64Bit" : "32Bit";
				return text + ((text2 == null) ? string.Empty : (" " + text2)) + " " + str;
			}
			return SystemInfo.operatingSystem;
		}

		// Token: 0x040003F6 RID: 1014
		public const float Epsilon = 1E-07f;

		// Token: 0x040003F7 RID: 1015
		private static Vector2[] circleVertexList = new Vector2[]
		{
			new Vector2(0f, 1f),
			new Vector2(0.2588f, 0.9659f),
			new Vector2(0.5f, 0.866f),
			new Vector2(0.7071f, 0.7071f),
			new Vector2(0.866f, 0.5f),
			new Vector2(0.9659f, 0.2588f),
			new Vector2(1f, 0f),
			new Vector2(0.9659f, -0.2588f),
			new Vector2(0.866f, -0.5f),
			new Vector2(0.7071f, -0.7071f),
			new Vector2(0.5f, -0.866f),
			new Vector2(0.2588f, -0.9659f),
			new Vector2(0f, -1f),
			new Vector2(-0.2588f, -0.9659f),
			new Vector2(-0.5f, -0.866f),
			new Vector2(-0.7071f, -0.7071f),
			new Vector2(-0.866f, -0.5f),
			new Vector2(-0.9659f, -0.2588f),
			new Vector2(-1f, --0f),
			new Vector2(-0.9659f, 0.2588f),
			new Vector2(-0.866f, 0.5f),
			new Vector2(-0.7071f, 0.7071f),
			new Vector2(-0.5f, 0.866f),
			new Vector2(-0.2588f, 0.9659f),
			new Vector2(0f, 1f)
		};
	}
}
