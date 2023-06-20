using System;
using System.IO;

namespace InControl
{
	// Token: 0x0200005D RID: 93
	public struct UnknownDeviceControl : IEquatable<UnknownDeviceControl>
	{
		// Token: 0x06000247 RID: 583 RVA: 0x0000D4B4 File Offset: 0x0000B6B4
		public UnknownDeviceControl(InputControlType control, InputRangeType sourceRange)
		{
			this.Control = control;
			this.SourceRange = sourceRange;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000D4D4 File Offset: 0x0000B6D4
		internal float GetValue(InputDevice device)
		{
			if (device == null)
			{
				return 0f;
			}
			float value = device.GetControl(this.Control).Value;
			return InputRange.Remap(value, this.SourceRange, InputRangeType.ZeroToOne);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000D50C File Offset: 0x0000B70C
		public bool Equals(UnknownDeviceControl other)
		{
			return this.Control == other.Control && this.SourceRange == other.SourceRange;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000D540 File Offset: 0x0000B740
		public override bool Equals(object other)
		{
			return this.Equals((UnknownDeviceControl)other);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000D550 File Offset: 0x0000B750
		public override int GetHashCode()
		{
			return this.Control.GetHashCode() ^ this.SourceRange.GetHashCode();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000D574 File Offset: 0x0000B774
		internal void Save(BinaryWriter writer)
		{
			writer.Write((int)this.Control);
			writer.Write((int)this.SourceRange);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000D590 File Offset: 0x0000B790
		internal void Load(BinaryReader reader)
		{
			this.Control = (InputControlType)reader.ReadInt32();
			this.SourceRange = (InputRangeType)reader.ReadInt32();
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000D5AC File Offset: 0x0000B7AC
		public static bool operator ==(UnknownDeviceControl a, UnknownDeviceControl b)
		{
			if (object.ReferenceEquals(null, a))
			{
				return object.ReferenceEquals(null, b);
			}
			return a.Equals(b);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
		public static bool operator !=(UnknownDeviceControl a, UnknownDeviceControl b)
		{
			return !(a == b);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000D5EC File Offset: 0x0000B7EC
		public static implicit operator bool(UnknownDeviceControl control)
		{
			return control.Control != InputControlType.None;
		}

		// Token: 0x040001DE RID: 478
		public static readonly UnknownDeviceControl None = new UnknownDeviceControl(InputControlType.None, InputRangeType.None);

		// Token: 0x040001DF RID: 479
		public InputControlType Control;

		// Token: 0x040001E0 RID: 480
		public InputRangeType SourceRange;
	}
}
