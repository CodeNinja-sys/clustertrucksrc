using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x02000183 RID: 387
	public static class EditorCameraZoom
	{
		// Token: 0x0600089D RID: 2205 RVA: 0x0003819C File Offset: 0x0003639C
		public static void ZoomCamera(Camera camera, float zoomAmount)
		{
			float num = 1f;
			if (camera.orthographic)
			{
				float num2 = camera.orthographicSize - zoomAmount;
				if (num2 < 1E-06f)
				{
					float num3 = 1E-06f - num2;
					float num4 = num3 / zoomAmount;
					num2 = 1E-06f;
					num = 1f - num4;
				}
				camera.orthographicSize = Mathf.Max(1E-06f, num2);
				zoomAmount *= num;
			}
			Transform transform = camera.transform;
			transform.position += transform.forward * zoomAmount;
		}
	}
}
