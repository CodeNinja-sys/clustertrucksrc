using System;

namespace XInputDotNetPure
{
	// Token: 0x02000101 RID: 257
	public struct GamePadDPad
	{
		// Token: 0x060005A9 RID: 1449 RVA: 0x00029524 File Offset: 0x00027724
		internal GamePadDPad(ButtonState up, ButtonState down, ButtonState left, ButtonState right)
		{
			this.up = up;
			this.down = down;
			this.left = left;
			this.right = right;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00029544 File Offset: 0x00027744
		public ButtonState Up
		{
			get
			{
				return this.up;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0002954C File Offset: 0x0002774C
		public ButtonState Down
		{
			get
			{
				return this.down;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00029554 File Offset: 0x00027754
		public ButtonState Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0002955C File Offset: 0x0002775C
		public ButtonState Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x04000414 RID: 1044
		private ButtonState up;

		// Token: 0x04000415 RID: 1045
		private ButtonState down;

		// Token: 0x04000416 RID: 1046
		private ButtonState left;

		// Token: 0x04000417 RID: 1047
		private ButtonState right;
	}
}
