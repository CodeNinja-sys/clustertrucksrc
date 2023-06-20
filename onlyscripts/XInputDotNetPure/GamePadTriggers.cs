using System;

namespace XInputDotNetPure
{
	// Token: 0x02000104 RID: 260
	public struct GamePadTriggers
	{
		// Token: 0x060005B5 RID: 1461 RVA: 0x000295BC File Offset: 0x000277BC
		internal GamePadTriggers(float left, float right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x000295CC File Offset: 0x000277CC
		public float Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x000295D4 File Offset: 0x000277D4
		public float Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x0400041B RID: 1051
		private float left;

		// Token: 0x0400041C RID: 1052
		private float right;
	}
}
