using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000068 RID: 104
	public class OneAxisInputControl : InputControlBase
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x0000E2EC File Offset: 0x0000C4EC
		internal void CommitWithSides(InputControl negativeSide, InputControl positiveSide, ulong updateTick, float deltaTime)
		{
			base.LowerDeadZone = Mathf.Max(negativeSide.LowerDeadZone, positiveSide.LowerDeadZone);
			base.UpperDeadZone = Mathf.Min(negativeSide.UpperDeadZone, positiveSide.UpperDeadZone);
			this.Raw = (negativeSide.Raw || positiveSide.Raw);
			float value = Utility.ValueFromSides(negativeSide.RawValue, positiveSide.RawValue);
			base.CommitWithValue(value, updateTick, deltaTime);
		}
	}
}
