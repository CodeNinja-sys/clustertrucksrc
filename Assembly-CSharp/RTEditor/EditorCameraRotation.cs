using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x02000182 RID: 386
	public static class EditorCameraRotation
	{
		// Token: 0x0600089C RID: 2204 RVA: 0x0003816C File Offset: 0x0003636C
		public static void RotateCamera(Camera camera, float rotationCameraRight, float rotationGlobalUp)
		{
			Transform transform = camera.transform;
			transform.Rotate(transform.right, rotationCameraRight, Space.World);
			transform.Rotate(Vector3.up, rotationGlobalUp, Space.World);
		}
	}
}
