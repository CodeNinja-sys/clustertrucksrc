using System;

namespace InControl
{
	// Token: 0x020000D2 RID: 210
	[AutoDiscover]
	public class PlayStation4LinuxProfile : UnityInputDeviceProfile
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x00020550 File Offset: 0x0001E750
		public PlayStation4LinuxProfile()
		{
			base.Name = "PlayStation 4 Controller";
			base.Meta = "PlayStation 4 Controller on Linux";
			base.SupportedPlatforms = new string[]
			{
				"Linux"
			};
			this.JoystickNames = new string[]
			{
				"Sony Computer Entertainment Wireless Controller"
			};
			base.ButtonMappings = new InputControlMapping[]
			{
				new InputControlMapping
				{
					Handle = "Cross",
					Target = InputControlType.Action1,
					Source = UnityInputDeviceProfile.Button1
				},
				new InputControlMapping
				{
					Handle = "Circle",
					Target = InputControlType.Action2,
					Source = UnityInputDeviceProfile.Button2
				},
				new InputControlMapping
				{
					Handle = "Square",
					Target = InputControlType.Action3,
					Source = UnityInputDeviceProfile.Button0
				},
				new InputControlMapping
				{
					Handle = "Triangle",
					Target = InputControlType.Action4,
					Source = UnityInputDeviceProfile.Button3
				},
				new InputControlMapping
				{
					Handle = "Left Bumper",
					Target = InputControlType.LeftBumper,
					Source = UnityInputDeviceProfile.Button4
				},
				new InputControlMapping
				{
					Handle = "Right Bumper",
					Target = InputControlType.RightBumper,
					Source = UnityInputDeviceProfile.Button5
				},
				new InputControlMapping
				{
					Handle = "Options",
					Target = InputControlType.Options,
					Source = UnityInputDeviceProfile.Button9
				},
				new InputControlMapping
				{
					Handle = "Left Trigger",
					Target = InputControlType.LeftTrigger,
					Source = UnityInputDeviceProfile.Button6
				},
				new InputControlMapping
				{
					Handle = "Right Trigger",
					Target = InputControlType.RightTrigger,
					Source = UnityInputDeviceProfile.Button7
				},
				new InputControlMapping
				{
					Handle = "Left Stick Button",
					Target = InputControlType.LeftStickButton,
					Source = UnityInputDeviceProfile.Button10
				},
				new InputControlMapping
				{
					Handle = "Right Stick Button",
					Target = InputControlType.RightStickButton,
					Source = UnityInputDeviceProfile.Button11
				},
				new InputControlMapping
				{
					Handle = "TouchPad Button",
					Target = InputControlType.TouchPadTap,
					Source = UnityInputDeviceProfile.Button13
				}
			};
			base.AnalogMappings = new InputControlMapping[]
			{
				UnityInputDeviceProfile.LeftStickLeftMapping(UnityInputDeviceProfile.Analog0),
				UnityInputDeviceProfile.LeftStickRightMapping(UnityInputDeviceProfile.Analog0),
				UnityInputDeviceProfile.LeftStickUpMapping(UnityInputDeviceProfile.Analog1),
				UnityInputDeviceProfile.LeftStickDownMapping(UnityInputDeviceProfile.Analog1),
				UnityInputDeviceProfile.RightStickLeftMapping(UnityInputDeviceProfile.Analog2),
				UnityInputDeviceProfile.RightStickRightMapping(UnityInputDeviceProfile.Analog2),
				UnityInputDeviceProfile.RightStickUpMapping(UnityInputDeviceProfile.Analog5),
				UnityInputDeviceProfile.RightStickDownMapping(UnityInputDeviceProfile.Analog5),
				UnityInputDeviceProfile.DPadLeftMapping(UnityInputDeviceProfile.Analog6),
				UnityInputDeviceProfile.DPadRightMapping(UnityInputDeviceProfile.Analog6),
				UnityInputDeviceProfile.DPadUpMapping(UnityInputDeviceProfile.Analog7),
				UnityInputDeviceProfile.DPadDownMapping(UnityInputDeviceProfile.Analog7)
			};
		}
	}
}
