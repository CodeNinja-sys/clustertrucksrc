using System;
using System.IO;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200005A RID: 90
	public class UnknownDeviceBindingSource : BindingSource
	{
		// Token: 0x06000232 RID: 562 RVA: 0x0000D04C File Offset: 0x0000B24C
		internal UnknownDeviceBindingSource()
		{
			this.Control = UnknownDeviceControl.None;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000D060 File Offset: 0x0000B260
		public UnknownDeviceBindingSource(UnknownDeviceControl control)
		{
			this.Control = control;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000D070 File Offset: 0x0000B270
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000D078 File Offset: 0x0000B278
		public UnknownDeviceControl Control { get; protected set; }

		// Token: 0x06000236 RID: 566 RVA: 0x0000D084 File Offset: 0x0000B284
		public override float GetValue(InputDevice device)
		{
			return this.Control.GetValue(device);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
		public override bool GetState(InputDevice device)
		{
			return device != null && Utility.IsNotZero(this.GetValue(device));
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
		public override string Name
		{
			get
			{
				if (base.BoundTo == null)
				{
					return string.Empty;
				}
				string str = string.Empty;
				if (this.Control.SourceRange == InputRangeType.ZeroToMinusOne)
				{
					str = "Negative ";
				}
				else if (this.Control.SourceRange == InputRangeType.ZeroToOne)
				{
					str = "Positive ";
				}
				InputDevice device = base.BoundTo.Device;
				if (device == InputDevice.Null)
				{
					return str + this.Control.Control.ToString();
				}
				InputControl control = device.GetControl(this.Control.Control);
				if (control == InputControl.Null)
				{
					return str + this.Control.Control.ToString();
				}
				return str + control.Handle;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000D19C File Offset: 0x0000B39C
		public override string DeviceName
		{
			get
			{
				if (base.BoundTo == null)
				{
					return string.Empty;
				}
				InputDevice device = base.BoundTo.Device;
				if (device == InputDevice.Null)
				{
					return "Unknown Controller";
				}
				return device.Name;
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000D1E0 File Offset: 0x0000B3E0
		public override bool Equals(BindingSource other)
		{
			if (other == null)
			{
				return false;
			}
			UnknownDeviceBindingSource unknownDeviceBindingSource = other as UnknownDeviceBindingSource;
			return unknownDeviceBindingSource != null && this.Control == unknownDeviceBindingSource.Control;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000D224 File Offset: 0x0000B424
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			UnknownDeviceBindingSource unknownDeviceBindingSource = other as UnknownDeviceBindingSource;
			return unknownDeviceBindingSource != null && this.Control == unknownDeviceBindingSource.Control;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000D260 File Offset: 0x0000B460
		public override int GetHashCode()
		{
			return this.Control.GetHashCode();
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000D27C File Offset: 0x0000B47C
		internal override BindingSourceType BindingSourceType
		{
			get
			{
				return BindingSourceType.UnknownDeviceBindingSource;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000D280 File Offset: 0x0000B480
		internal override bool IsValid
		{
			get
			{
				if (base.BoundTo == null)
				{
					Debug.LogError("Cannot query property 'IsValid' for unbound BindingSource.");
					return false;
				}
				InputDevice device = base.BoundTo.Device;
				return device == InputDevice.Null || device.HasControl(this.Control.Control);
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000D2D4 File Offset: 0x0000B4D4
		internal override void Load(BinaryReader reader)
		{
			UnknownDeviceControl control = default(UnknownDeviceControl);
			control.Load(reader);
			this.Control = control;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
		internal override void Save(BinaryWriter writer)
		{
			this.Control.Save(writer);
		}
	}
}
