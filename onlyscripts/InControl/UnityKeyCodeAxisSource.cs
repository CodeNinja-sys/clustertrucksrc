using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000087 RID: 135
	public class UnityKeyCodeAxisSource : InputControlSource
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x00013B90 File Offset: 0x00011D90
		public UnityKeyCodeAxisSource()
		{
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00013B98 File Offset: 0x00011D98
		public UnityKeyCodeAxisSource(KeyCode negativeKeyCode, KeyCode positiveKeyCode)
		{
			this.NegativeKeyCode = negativeKeyCode;
			this.PositiveKeyCode = positiveKeyCode;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00013BB0 File Offset: 0x00011DB0
		public float GetValue(InputDevice inputDevice)
		{
			int num = 0;
			if (Input.GetKey(this.NegativeKeyCode))
			{
				num--;
			}
			if (Input.GetKey(this.PositiveKeyCode))
			{
				num++;
			}
			return (float)num;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00013BEC File Offset: 0x00011DEC
		public bool GetState(InputDevice inputDevice)
		{
			return Utility.IsNotZero(this.GetValue(inputDevice));
		}

		// Token: 0x04000398 RID: 920
		public KeyCode NegativeKeyCode;

		// Token: 0x04000399 RID: 921
		public KeyCode PositiveKeyCode;
	}
}
