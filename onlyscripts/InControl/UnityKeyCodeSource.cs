using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000089 RID: 137
	public class UnityKeyCodeSource : InputControlSource
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x00013C70 File Offset: 0x00011E70
		public UnityKeyCodeSource()
		{
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00013C78 File Offset: 0x00011E78
		public UnityKeyCodeSource(params KeyCode[] keyCodeList)
		{
			this.KeyCodeList = keyCodeList;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00013C88 File Offset: 0x00011E88
		public float GetValue(InputDevice inputDevice)
		{
			return (!this.GetState(inputDevice)) ? 0f : 1f;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00013CA8 File Offset: 0x00011EA8
		public bool GetState(InputDevice inputDevice)
		{
			for (int i = 0; i < this.KeyCodeList.Length; i++)
			{
				if (Input.GetKey(this.KeyCodeList[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400039B RID: 923
		public KeyCode[] KeyCodeList;
	}
}
