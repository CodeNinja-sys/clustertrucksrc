using System;

namespace InControl
{
	// Token: 0x02000064 RID: 100
	public struct InputControlState
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0000DF94 File Offset: 0x0000C194
		public void Reset()
		{
			this.State = false;
			this.Value = 0f;
			this.RawValue = 0f;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000DFB4 File Offset: 0x0000C1B4
		public void Set(float value)
		{
			this.Value = value;
			this.State = Utility.IsNotZero(value);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000DFCC File Offset: 0x0000C1CC
		public void Set(float value, float threshold)
		{
			this.Value = value;
			this.State = Utility.AbsoluteIsOverThreshold(value, threshold);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000DFE4 File Offset: 0x0000C1E4
		public void Set(bool state)
		{
			this.State = state;
			this.Value = ((!state) ? 0f : 1f);
			this.RawValue = this.Value;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000E020 File Offset: 0x0000C220
		public static implicit operator bool(InputControlState state)
		{
			return state.State;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000E02C File Offset: 0x0000C22C
		public static implicit operator float(InputControlState state)
		{
			return state.Value;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000E038 File Offset: 0x0000C238
		public static bool operator ==(InputControlState a, InputControlState b)
		{
			return a.State == b.State && Utility.Approximately(a.Value, b.Value);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000E070 File Offset: 0x0000C270
		public static bool operator !=(InputControlState a, InputControlState b)
		{
			return a.State != b.State || !Utility.Approximately(a.Value, b.Value);
		}

		// Token: 0x0400020E RID: 526
		public bool State;

		// Token: 0x0400020F RID: 527
		public float Value;

		// Token: 0x04000210 RID: 528
		public float RawValue;
	}
}
