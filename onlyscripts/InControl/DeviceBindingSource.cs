using System;
using System.IO;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200004C RID: 76
	public class DeviceBindingSource : BindingSource
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00009F78 File Offset: 0x00008178
		internal DeviceBindingSource()
		{
			this.Control = InputControlType.None;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00009F88 File Offset: 0x00008188
		public DeviceBindingSource(InputControlType control)
		{
			this.Control = control;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00009F98 File Offset: 0x00008198
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00009FA0 File Offset: 0x000081A0
		public InputControlType Control { get; protected set; }

		// Token: 0x060001A2 RID: 418 RVA: 0x00009FAC File Offset: 0x000081AC
		public override float GetValue(InputDevice inputDevice)
		{
			if (inputDevice == null)
			{
				return 0f;
			}
			return inputDevice.GetControl(this.Control).Value;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009FCC File Offset: 0x000081CC
		public override bool GetState(InputDevice inputDevice)
		{
			return inputDevice != null && inputDevice.GetControl(this.Control).State;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00009FE8 File Offset: 0x000081E8
		public override string Name
		{
			get
			{
				if (base.BoundTo == null)
				{
					return string.Empty;
				}
				InputDevice device = base.BoundTo.Device;
				InputControl control = device.GetControl(this.Control);
				if (control == InputControl.Null)
				{
					return this.Control.ToString();
				}
				return device.GetControl(this.Control).Handle;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000A04C File Offset: 0x0000824C
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
					return "Controller";
				}
				return device.Name;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000A090 File Offset: 0x00008290
		public override bool Equals(BindingSource other)
		{
			if (other == null)
			{
				return false;
			}
			DeviceBindingSource deviceBindingSource = other as DeviceBindingSource;
			return deviceBindingSource != null && this.Control == deviceBindingSource.Control;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000A0D0 File Offset: 0x000082D0
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			DeviceBindingSource deviceBindingSource = other as DeviceBindingSource;
			return deviceBindingSource != null && this.Control == deviceBindingSource.Control;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000A108 File Offset: 0x00008308
		public override int GetHashCode()
		{
			return this.Control.GetHashCode();
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000A11C File Offset: 0x0000831C
		internal override BindingSourceType BindingSourceType
		{
			get
			{
				return BindingSourceType.DeviceBindingSource;
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000A120 File Offset: 0x00008320
		internal override void Save(BinaryWriter writer)
		{
			writer.Write((int)this.Control);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000A130 File Offset: 0x00008330
		internal override void Load(BinaryReader reader)
		{
			this.Control = (InputControlType)reader.ReadInt32();
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000A140 File Offset: 0x00008340
		internal override bool IsValid
		{
			get
			{
				if (base.BoundTo == null)
				{
					Debug.LogError("Cannot query property 'IsValid' for unbound BindingSource.");
					return false;
				}
				return base.BoundTo.Device.HasControl(this.Control) || Utility.TargetIsStandard(this.Control);
			}
		}
	}
}
