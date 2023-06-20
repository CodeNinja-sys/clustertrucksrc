using System;

namespace InControl
{
	// Token: 0x02000096 RID: 150
	[AutoDiscover]
	public class BuffaloClassicMacProfile : UnityInputDeviceProfile
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x00015360 File Offset: 0x00013560
		public BuffaloClassicMacProfile()
		{
			base.Name = "Buffalo Class Gamepad";
			base.Meta = "Buffalo Class Gamepad on Mac";
			base.SupportedPlatforms = new string[]
			{
				"OS X"
			};
			this.JoystickNames = new string[]
			{
				" USB,2-axis 8-button gamepad"
			};
			base.ButtonMappings = new InputControlMapping[]
			{
				new InputControlMapping
				{
					Handle = "A",
					Target = InputControlType.Action2,
					Source = UnityInputDeviceProfile.Button0
				},
				new InputControlMapping
				{
					Handle = "B",
					Target = InputControlType.Action1,
					Source = UnityInputDeviceProfile.Button1
				},
				new InputControlMapping
				{
					Handle = "X",
					Target = InputControlType.Action4,
					Source = UnityInputDeviceProfile.Button2
				},
				new InputControlMapping
				{
					Handle = "Y",
					Target = InputControlType.Action3,
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
					Handle = "Select",
					Target = InputControlType.Select,
					Source = UnityInputDeviceProfile.Button6
				},
				new InputControlMapping
				{
					Handle = "Start",
					Target = InputControlType.Start,
					Source = UnityInputDeviceProfile.Button7
				}
			};
			base.AnalogMappings = new InputControlMapping[]
			{
				UnityInputDeviceProfile.DPadLeftMapping(UnityInputDeviceProfile.Analog0),
				UnityInputDeviceProfile.DPadRightMapping(UnityInputDeviceProfile.Analog0),
				UnityInputDeviceProfile.DPadUpMapping(UnityInputDeviceProfile.Analog1),
				UnityInputDeviceProfile.DPadDownMapping(UnityInputDeviceProfile.Analog1)
			};
		}
	}
}
