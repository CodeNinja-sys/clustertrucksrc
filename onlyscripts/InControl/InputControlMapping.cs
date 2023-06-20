using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000062 RID: 98
	public class InputControlMapping
	{
		// Token: 0x06000291 RID: 657 RVA: 0x0000DED4 File Offset: 0x0000C0D4
		public float MapValue(float value)
		{
			if (this.Raw)
			{
				value *= this.Scale;
				value = ((!this.SourceRange.Excludes(value)) ? value : 0f);
			}
			else
			{
				value = Mathf.Clamp(value * this.Scale, -1f, 1f);
				value = InputRange.Remap(value, this.SourceRange, this.TargetRange);
			}
			if (this.Invert)
			{
				value = -value;
			}
			return value;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000DF58 File Offset: 0x0000C158
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000DF88 File Offset: 0x0000C188
		public string Handle
		{
			get
			{
				return (!string.IsNullOrEmpty(this.handle)) ? this.handle : this.Target.ToString();
			}
			set
			{
				this.handle = value;
			}
		}

		// Token: 0x04000202 RID: 514
		public InputControlSource Source;

		// Token: 0x04000203 RID: 515
		public InputControlType Target;

		// Token: 0x04000204 RID: 516
		public bool Invert;

		// Token: 0x04000205 RID: 517
		public float Scale = 1f;

		// Token: 0x04000206 RID: 518
		public bool Raw;

		// Token: 0x04000207 RID: 519
		public bool IgnoreInitialZeroValue;

		// Token: 0x04000208 RID: 520
		public float Sensitivity = 1f;

		// Token: 0x04000209 RID: 521
		public float LowerDeadZone;

		// Token: 0x0400020A RID: 522
		public float UpperDeadZone = 1f;

		// Token: 0x0400020B RID: 523
		public InputRange SourceRange = InputRange.MinusOneToOne;

		// Token: 0x0400020C RID: 524
		public InputRange TargetRange = InputRange.MinusOneToOne;

		// Token: 0x0400020D RID: 525
		private string handle;
	}
}
