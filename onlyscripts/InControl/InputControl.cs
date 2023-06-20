using System;

namespace InControl
{
	// Token: 0x02000060 RID: 96
	public class InputControl : InputControlBase
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000D824 File Offset: 0x0000BA24
		private InputControl()
		{
			this.Handle = "None";
			this.Target = InputControlType.None;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000D840 File Offset: 0x0000BA40
		public InputControl(string handle, InputControlType target)
		{
			this.Handle = handle;
			this.Target = target;
			this.IsButton = Utility.TargetIsButton(target);
			this.IsAnalog = !this.IsButton;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000D888 File Offset: 0x0000BA88
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000D890 File Offset: 0x0000BA90
		public string Handle { get; protected set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000D89C File Offset: 0x0000BA9C
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000D8A4 File Offset: 0x0000BAA4
		public InputControlType Target { get; protected set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000D8B8 File Offset: 0x0000BAB8
		public bool IsButton { get; protected set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000D8C4 File Offset: 0x0000BAC4
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000D8CC File Offset: 0x0000BACC
		public bool IsAnalog { get; protected set; }

		// Token: 0x0600026C RID: 620 RVA: 0x0000D8D8 File Offset: 0x0000BAD8
		internal void SetZeroTick()
		{
			this.zeroTick = base.UpdateTick;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000D8E8 File Offset: 0x0000BAE8
		internal bool IsOnZeroTick
		{
			get
			{
				return base.UpdateTick == this.zeroTick;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
		public bool IsStandard
		{
			get
			{
				return Utility.TargetIsStandard(this.Target);
			}
		}

		// Token: 0x040001EA RID: 490
		public static readonly InputControl Null = new InputControl();

		// Token: 0x040001EB RID: 491
		private ulong zeroTick;
	}
}
