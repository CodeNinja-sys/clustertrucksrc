using System;
using System.IO;

namespace InControl
{
	// Token: 0x02000048 RID: 72
	public abstract class BindingSource : InputControlSource, IEquatable<BindingSource>
	{
		// Token: 0x0600018D RID: 397
		public abstract float GetValue(InputDevice inputDevice);

		// Token: 0x0600018E RID: 398
		public abstract bool GetState(InputDevice inputDevice);

		// Token: 0x0600018F RID: 399
		public abstract bool Equals(BindingSource other);

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000190 RID: 400
		public abstract string Name { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000191 RID: 401
		public abstract string DeviceName { get; }

		// Token: 0x06000192 RID: 402 RVA: 0x00009EF8 File Offset: 0x000080F8
		public override bool Equals(object other)
		{
			return this.Equals((BindingSource)other);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009F08 File Offset: 0x00008108
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000194 RID: 404
		internal abstract BindingSourceType BindingSourceType { get; }

		// Token: 0x06000195 RID: 405
		internal abstract void Save(BinaryWriter writer);

		// Token: 0x06000196 RID: 406
		internal abstract void Load(BinaryReader reader);

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00009F10 File Offset: 0x00008110
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00009F18 File Offset: 0x00008118
		internal PlayerAction BoundTo { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00009F24 File Offset: 0x00008124
		internal virtual bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00009F28 File Offset: 0x00008128
		public static bool operator ==(BindingSource a, BindingSource b)
		{
			return object.ReferenceEquals(a, b) || (a != null && b != null && a.BindingSourceType == b.BindingSourceType && a.Equals(b));
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00009F6C File Offset: 0x0000816C
		public static bool operator !=(BindingSource a, BindingSource b)
		{
			return !(a == b);
		}
	}
}
