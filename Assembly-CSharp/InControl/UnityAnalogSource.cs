using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000083 RID: 131
	public class UnityAnalogSource : InputControlSource
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x00013890 File Offset: 0x00011A90
		public UnityAnalogSource()
		{
			UnityAnalogSource.SetupAnalogQueries();
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000138A0 File Offset: 0x00011AA0
		public UnityAnalogSource(int analogId)
		{
			this.AnalogId = analogId;
			UnityAnalogSource.SetupAnalogQueries();
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000138B4 File Offset: 0x00011AB4
		public float GetValue(InputDevice inputDevice)
		{
			int joystickId = (inputDevice as UnityInputDevice).JoystickId;
			string analogKey = UnityAnalogSource.GetAnalogKey(joystickId, this.AnalogId);
			return Input.GetAxisRaw(analogKey);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000138E0 File Offset: 0x00011AE0
		public bool GetState(InputDevice inputDevice)
		{
			return Utility.IsNotZero(this.GetValue(inputDevice));
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000138F0 File Offset: 0x00011AF0
		private static void SetupAnalogQueries()
		{
			if (UnityAnalogSource.analogQueries == null)
			{
				UnityAnalogSource.analogQueries = new string[10, 20];
				for (int i = 1; i <= 10; i++)
				{
					for (int j = 0; j < 20; j++)
					{
						UnityAnalogSource.analogQueries[i - 1, j] = string.Concat(new object[]
						{
							"joystick ",
							i,
							" analog ",
							j
						});
					}
				}
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00013978 File Offset: 0x00011B78
		private static string GetAnalogKey(int joystickId, int analogId)
		{
			return UnityAnalogSource.analogQueries[joystickId - 1, analogId];
		}

		// Token: 0x0400038F RID: 911
		private static string[,] analogQueries;

		// Token: 0x04000390 RID: 912
		public int AnalogId;
	}
}
