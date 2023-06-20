using System;

namespace XInputDotNetPure
{
	// Token: 0x02000100 RID: 256
	public struct GamePadButtons
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x00029478 File Offset: 0x00027678
		internal GamePadButtons(ButtonState start, ButtonState back, ButtonState leftStick, ButtonState rightStick, ButtonState leftShoulder, ButtonState rightShoulder, ButtonState a, ButtonState b, ButtonState x, ButtonState y)
		{
			this.start = start;
			this.back = back;
			this.leftStick = leftStick;
			this.rightStick = rightStick;
			this.leftShoulder = leftShoulder;
			this.rightShoulder = rightShoulder;
			this.a = a;
			this.b = b;
			this.x = x;
			this.y = y;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x000294D4 File Offset: 0x000276D4
		public ButtonState Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x000294DC File Offset: 0x000276DC
		public ButtonState Back
		{
			get
			{
				return this.back;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x000294E4 File Offset: 0x000276E4
		public ButtonState LeftStick
		{
			get
			{
				return this.leftStick;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x000294EC File Offset: 0x000276EC
		public ButtonState RightStick
		{
			get
			{
				return this.rightStick;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x000294F4 File Offset: 0x000276F4
		public ButtonState LeftShoulder
		{
			get
			{
				return this.leftShoulder;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x000294FC File Offset: 0x000276FC
		public ButtonState RightShoulder
		{
			get
			{
				return this.rightShoulder;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00029504 File Offset: 0x00027704
		public ButtonState A
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0002950C File Offset: 0x0002770C
		public ButtonState B
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00029514 File Offset: 0x00027714
		public ButtonState X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0002951C File Offset: 0x0002771C
		public ButtonState Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x0400040A RID: 1034
		private ButtonState start;

		// Token: 0x0400040B RID: 1035
		private ButtonState back;

		// Token: 0x0400040C RID: 1036
		private ButtonState leftStick;

		// Token: 0x0400040D RID: 1037
		private ButtonState rightStick;

		// Token: 0x0400040E RID: 1038
		private ButtonState leftShoulder;

		// Token: 0x0400040F RID: 1039
		private ButtonState rightShoulder;

		// Token: 0x04000410 RID: 1040
		private ButtonState a;

		// Token: 0x04000411 RID: 1041
		private ButtonState b;

		// Token: 0x04000412 RID: 1042
		private ButtonState x;

		// Token: 0x04000413 RID: 1043
		private ButtonState y;
	}
}
