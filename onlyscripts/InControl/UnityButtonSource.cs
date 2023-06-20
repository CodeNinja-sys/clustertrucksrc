using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000084 RID: 132
	public class UnityButtonSource : InputControlSource
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x00013988 File Offset: 0x00011B88
		public UnityButtonSource()
		{
			UnityButtonSource.SetupButtonQueries();
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00013998 File Offset: 0x00011B98
		public UnityButtonSource(int buttonId)
		{
			this.ButtonId = buttonId;
			UnityButtonSource.SetupButtonQueries();
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000139AC File Offset: 0x00011BAC
		public float GetValue(InputDevice inputDevice)
		{
			return (!this.GetState(inputDevice)) ? 0f : 1f;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000139CC File Offset: 0x00011BCC
		public bool GetState(InputDevice inputDevice)
		{
			int joystickId = (inputDevice as UnityInputDevice).JoystickId;
			string buttonKey = UnityButtonSource.GetButtonKey(joystickId, this.ButtonId);
			return Input.GetKey(buttonKey);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000139F8 File Offset: 0x00011BF8
		private static void SetupButtonQueries()
		{
			if (UnityButtonSource.buttonQueries == null)
			{
				UnityButtonSource.buttonQueries = new string[10, 20];
				for (int i = 1; i <= 10; i++)
				{
					for (int j = 0; j < 20; j++)
					{
						UnityButtonSource.buttonQueries[i - 1, j] = string.Concat(new object[]
						{
							"joystick ",
							i,
							" button ",
							j
						});
					}
				}
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00013A80 File Offset: 0x00011C80
		private static string GetButtonKey(int joystickId, int buttonId)
		{
			return UnityButtonSource.buttonQueries[joystickId - 1, buttonId];
		}

		// Token: 0x04000391 RID: 913
		private static string[,] buttonQueries;

		// Token: 0x04000392 RID: 914
		public int ButtonId;
	}
}
