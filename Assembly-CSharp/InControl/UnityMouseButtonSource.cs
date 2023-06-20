using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200008B RID: 139
	public class UnityMouseButtonSource : InputControlSource
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x00013D28 File Offset: 0x00011F28
		public UnityMouseButtonSource()
		{
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00013D30 File Offset: 0x00011F30
		public UnityMouseButtonSource(int buttonId)
		{
			this.ButtonId = buttonId;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00013D40 File Offset: 0x00011F40
		public float GetValue(InputDevice inputDevice)
		{
			return (!this.GetState(inputDevice)) ? 0f : 1f;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00013D60 File Offset: 0x00011F60
		public bool GetState(InputDevice inputDevice)
		{
			return Input.GetMouseButton(this.ButtonId);
		}

		// Token: 0x0400039D RID: 925
		public int ButtonId;
	}
}
