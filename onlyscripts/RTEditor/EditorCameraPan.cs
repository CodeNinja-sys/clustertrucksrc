using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x02000181 RID: 385
	public static class EditorCameraPan
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x00038128 File Offset: 0x00036328
		public static void PanCamera(Camera camera, float panAmountRight, float panAmountUp)
		{
			Transform transform = camera.transform;
			transform.position += transform.right * panAmountRight + transform.up * panAmountUp;
		}
	}
}
