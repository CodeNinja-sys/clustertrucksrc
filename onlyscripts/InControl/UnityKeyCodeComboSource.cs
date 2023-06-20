using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000088 RID: 136
	public class UnityKeyCodeComboSource : InputControlSource
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x00013BFC File Offset: 0x00011DFC
		public UnityKeyCodeComboSource()
		{
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00013C04 File Offset: 0x00011E04
		public UnityKeyCodeComboSource(params KeyCode[] keyCodeList)
		{
			this.KeyCodeList = keyCodeList;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00013C14 File Offset: 0x00011E14
		public float GetValue(InputDevice inputDevice)
		{
			return (!this.GetState(inputDevice)) ? 0f : 1f;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00013C34 File Offset: 0x00011E34
		public bool GetState(InputDevice inputDevice)
		{
			for (int i = 0; i < this.KeyCodeList.Length; i++)
			{
				if (!Input.GetKey(this.KeyCodeList[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400039A RID: 922
		public KeyCode[] KeyCodeList;
	}
}
