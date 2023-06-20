using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x020000F0 RID: 240
	public class UnityInputDevice : InputDevice
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x000262D8 File Offset: 0x000244D8
		public UnityInputDevice(InputDeviceProfile profile, int joystickId) : base(profile.Name)
		{
			this.Initialize(profile, joystickId);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000262F0 File Offset: 0x000244F0
		public UnityInputDevice(InputDeviceProfile profile) : base(profile.Name)
		{
			this.Initialize(profile, 0);
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00026308 File Offset: 0x00024508
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x00026310 File Offset: 0x00024510
		internal int JoystickId { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0002631C File Offset: 0x0002451C
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x00026324 File Offset: 0x00024524
		public InputDeviceProfile Profile { get; protected set; }

		// Token: 0x06000514 RID: 1300 RVA: 0x00026330 File Offset: 0x00024530
		private void Initialize(InputDeviceProfile profile, int joystickId)
		{
			this.Profile = profile;
			base.Meta = this.Profile.Meta;
			int analogCount = this.Profile.AnalogCount;
			for (int i = 0; i < analogCount; i++)
			{
				InputControlMapping inputControlMapping = this.Profile.AnalogMappings[i];
				InputControl inputControl = base.AddControl(inputControlMapping.Target, inputControlMapping.Handle);
				inputControl.Sensitivity = Mathf.Min(this.Profile.Sensitivity, inputControlMapping.Sensitivity);
				inputControl.LowerDeadZone = Mathf.Max(this.Profile.LowerDeadZone, inputControlMapping.LowerDeadZone);
				inputControl.UpperDeadZone = Mathf.Min(this.Profile.UpperDeadZone, inputControlMapping.UpperDeadZone);
				inputControl.Raw = inputControlMapping.Raw;
			}
			int buttonCount = this.Profile.ButtonCount;
			for (int j = 0; j < buttonCount; j++)
			{
				InputControlMapping inputControlMapping2 = this.Profile.ButtonMappings[j];
				base.AddControl(inputControlMapping2.Target, inputControlMapping2.Handle);
			}
			this.JoystickId = joystickId;
			if (joystickId != 0)
			{
				this.SortOrder = 100 + joystickId;
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00026454 File Offset: 0x00024654
		public override void Update(ulong updateTick, float deltaTime)
		{
			if (this.Profile == null)
			{
				return;
			}
			int analogCount = this.Profile.AnalogCount;
			for (int i = 0; i < analogCount; i++)
			{
				InputControlMapping inputControlMapping = this.Profile.AnalogMappings[i];
				float value = inputControlMapping.Source.GetValue(this);
				InputControl control = base.GetControl(inputControlMapping.Target);
				if (!inputControlMapping.IgnoreInitialZeroValue || !control.IsOnZeroTick || !Utility.IsZero(value))
				{
					float value2 = inputControlMapping.MapValue(value);
					control.UpdateWithValue(value2, updateTick, deltaTime);
				}
			}
			int buttonCount = this.Profile.ButtonCount;
			for (int j = 0; j < buttonCount; j++)
			{
				InputControlMapping inputControlMapping2 = this.Profile.ButtonMappings[j];
				bool state = inputControlMapping2.Source.GetState(this);
				base.UpdateWithState(inputControlMapping2.Target, state, updateTick, deltaTime);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00026540 File Offset: 0x00024740
		public override bool IsSupportedOnThisPlatform
		{
			get
			{
				return this.Profile != null && this.Profile.IsSupportedOnThisPlatform;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0002655C File Offset: 0x0002475C
		public override bool IsKnown
		{
			get
			{
				return this.Profile != null && this.Profile.IsKnown;
			}
		}

		// Token: 0x040003B0 RID: 944
		public const int MaxDevices = 10;

		// Token: 0x040003B1 RID: 945
		public const int MaxButtons = 20;

		// Token: 0x040003B2 RID: 946
		public const int MaxAnalogs = 20;
	}
}
