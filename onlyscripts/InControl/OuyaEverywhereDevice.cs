using System;

namespace InControl
{
	// Token: 0x02000044 RID: 68
	public class OuyaEverywhereDevice : InputDevice
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00009C74 File Offset: 0x00007E74
		public OuyaEverywhereDevice(int deviceIndex) : base("OUYA Controller")
		{
			this.DeviceIndex = deviceIndex;
			this.SortOrder = deviceIndex;
			base.Meta = "OUYA Everywhere Device #" + deviceIndex;
			base.AddControl(InputControlType.LeftStickLeft, "Left Stick Left");
			base.AddControl(InputControlType.LeftStickRight, "Left Stick Right");
			base.AddControl(InputControlType.LeftStickUp, "Left Stick Up");
			base.AddControl(InputControlType.LeftStickDown, "Left Stick Down");
			base.AddControl(InputControlType.RightStickLeft, "Right Stick Left");
			base.AddControl(InputControlType.RightStickRight, "Right Stick Right");
			base.AddControl(InputControlType.RightStickUp, "Right Stick Up");
			base.AddControl(InputControlType.RightStickDown, "Right Stick Down");
			base.AddControl(InputControlType.LeftTrigger, "Left Trigger");
			base.AddControl(InputControlType.RightTrigger, "Right Trigger");
			base.AddControl(InputControlType.DPadUp, "DPad Up");
			base.AddControl(InputControlType.DPadDown, "DPad Down");
			base.AddControl(InputControlType.DPadLeft, "DPad Left");
			base.AddControl(InputControlType.DPadRight, "DPad Right");
			base.AddControl(InputControlType.Action1, "O");
			base.AddControl(InputControlType.Action2, "A");
			base.AddControl(InputControlType.Action3, "Y");
			base.AddControl(InputControlType.Action4, "U");
			base.AddControl(InputControlType.LeftBumper, "Left Bumper");
			base.AddControl(InputControlType.RightBumper, "Right Bumper");
			base.AddControl(InputControlType.LeftStickButton, "Left Stick Button");
			base.AddControl(InputControlType.RightStickButton, "Right Stick Button");
			base.AddControl(InputControlType.Menu, "Menu");
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00009DEC File Offset: 0x00007FEC
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00009DF4 File Offset: 0x00007FF4
		public int DeviceIndex { get; private set; }

		// Token: 0x06000184 RID: 388 RVA: 0x00009E00 File Offset: 0x00008000
		public void BeforeAttach()
		{
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00009E04 File Offset: 0x00008004
		public override void Update(ulong updateTick, float deltaTime)
		{
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00009E08 File Offset: 0x00008008
		public bool IsConnected
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400010A RID: 266
		private const float LowerDeadZone = 0.2f;

		// Token: 0x0400010B RID: 267
		private const float UpperDeadZone = 0.9f;
	}
}
