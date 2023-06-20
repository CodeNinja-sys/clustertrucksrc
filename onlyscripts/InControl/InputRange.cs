using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000066 RID: 102
	public struct InputRange
	{
		// Token: 0x0600029E RID: 670 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
		private InputRange(float value0, float value1, InputRangeType type)
		{
			this.Value0 = value0;
			this.Value1 = value1;
			this.Type = type;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000E0B8 File Offset: 0x0000C2B8
		public InputRange(InputRangeType type)
		{
			this.Value0 = InputRange.TypeToRange[(int)type].Value0;
			this.Value1 = InputRange.TypeToRange[(int)type].Value1;
			this.Type = type;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000E214 File Offset: 0x0000C414
		public bool Includes(float value)
		{
			return !this.Excludes(value);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000E220 File Offset: 0x0000C420
		public bool Excludes(float value)
		{
			return this.Type == InputRangeType.None || value < Mathf.Min(this.Value0, this.Value1) || value > Mathf.Max(this.Value0, this.Value1);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000E260 File Offset: 0x0000C460
		public static float Remap(float value, InputRange sourceRange, InputRange targetRange)
		{
			if (sourceRange.Excludes(value))
			{
				return 0f;
			}
			float t = Mathf.InverseLerp(sourceRange.Value0, sourceRange.Value1, value);
			return Mathf.Lerp(targetRange.Value0, targetRange.Value1, t);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		internal static float Remap(float value, InputRangeType sourceRangeType, InputRangeType targetRangeType)
		{
			InputRange sourceRange = InputRange.TypeToRange[(int)sourceRangeType];
			InputRange targetRange = InputRange.TypeToRange[(int)targetRangeType];
			return InputRange.Remap(value, sourceRange, targetRange);
		}

		// Token: 0x04000265 RID: 613
		public static readonly InputRange None = new InputRange(0f, 0f, InputRangeType.None);

		// Token: 0x04000266 RID: 614
		public static readonly InputRange MinusOneToOne = new InputRange(-1f, 1f, InputRangeType.MinusOneToOne);

		// Token: 0x04000267 RID: 615
		public static readonly InputRange ZeroToOne = new InputRange(0f, 1f, InputRangeType.ZeroToOne);

		// Token: 0x04000268 RID: 616
		public static readonly InputRange ZeroToMinusOne = new InputRange(0f, -1f, InputRangeType.ZeroToMinusOne);

		// Token: 0x04000269 RID: 617
		public static readonly InputRange ZeroToNegativeInfinity = new InputRange(0f, float.NegativeInfinity, InputRangeType.ZeroToNegativeInfinity);

		// Token: 0x0400026A RID: 618
		public static readonly InputRange ZeroToPositiveInfinity = new InputRange(0f, float.PositiveInfinity, InputRangeType.ZeroToPositiveInfinity);

		// Token: 0x0400026B RID: 619
		public static readonly InputRange Everything = new InputRange(float.NegativeInfinity, float.PositiveInfinity, InputRangeType.Everything);

		// Token: 0x0400026C RID: 620
		private static readonly InputRange[] TypeToRange = new InputRange[]
		{
			InputRange.None,
			InputRange.MinusOneToOne,
			InputRange.ZeroToOne,
			InputRange.ZeroToMinusOne,
			InputRange.ZeroToNegativeInfinity,
			InputRange.ZeroToPositiveInfinity,
			InputRange.Everything
		};

		// Token: 0x0400026D RID: 621
		public readonly float Value0;

		// Token: 0x0400026E RID: 622
		public readonly float Value1;

		// Token: 0x0400026F RID: 623
		public readonly InputRangeType Type;
	}
}
