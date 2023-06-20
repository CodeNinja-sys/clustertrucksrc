using System;
using UnityEngine;

namespace XInputDotNetPure
{
	// Token: 0x02000102 RID: 258
	public struct GamePadThumbSticks
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x00029564 File Offset: 0x00027764
		internal GamePadThumbSticks(GamePadThumbSticks.StickValue left, GamePadThumbSticks.StickValue right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x00029574 File Offset: 0x00027774
		public GamePadThumbSticks.StickValue Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0002957C File Offset: 0x0002777C
		public GamePadThumbSticks.StickValue Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x04000418 RID: 1048
		private GamePadThumbSticks.StickValue left;

		// Token: 0x04000419 RID: 1049
		private GamePadThumbSticks.StickValue right;

		// Token: 0x02000103 RID: 259
		public struct StickValue
		{
			// Token: 0x060005B1 RID: 1457 RVA: 0x00029584 File Offset: 0x00027784
			internal StickValue(float x, float y)
			{
				this.vector = new Vector2(x, y);
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00029594 File Offset: 0x00027794
			public float X
			{
				get
				{
					return this.vector.x;
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x060005B3 RID: 1459 RVA: 0x000295A4 File Offset: 0x000277A4
			public float Y
			{
				get
				{
					return this.vector.y;
				}
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x060005B4 RID: 1460 RVA: 0x000295B4 File Offset: 0x000277B4
			public Vector2 Vector
			{
				get
				{
					return this.vector;
				}
			}

			// Token: 0x0400041A RID: 1050
			private Vector2 vector;
		}
	}
}
