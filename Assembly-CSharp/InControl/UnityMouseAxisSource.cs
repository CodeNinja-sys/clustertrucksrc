using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200008A RID: 138
	public class UnityMouseAxisSource : InputControlSource
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x00013CE4 File Offset: 0x00011EE4
		public UnityMouseAxisSource()
		{
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00013CEC File Offset: 0x00011EEC
		public UnityMouseAxisSource(string axis)
		{
			this.MouseAxisQuery = "mouse " + axis;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00013D08 File Offset: 0x00011F08
		public float GetValue(InputDevice inputDevice)
		{
			return Input.GetAxisRaw(this.MouseAxisQuery);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00013D18 File Offset: 0x00011F18
		public bool GetState(InputDevice inputDevice)
		{
			return Utility.IsNotZero(this.GetValue(inputDevice));
		}

		// Token: 0x0400039C RID: 924
		public string MouseAxisQuery;
	}
}
