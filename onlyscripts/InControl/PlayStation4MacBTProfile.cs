﻿using System;

namespace InControl
{
	// Token: 0x020000D3 RID: 211
	[AutoDiscover]
	public class PlayStation4MacBTProfile : UnityInputDeviceProfile
	{
		// Token: 0x060004CE RID: 1230 RVA: 0x0002083C File Offset: 0x0001EA3C
		public PlayStation4MacBTProfile()
		{
			base.Name = "PlayStation 4 Controller";
			base.Meta = "PlayStation 4 Controller on Mac";
			base.SupportedPlatforms = new string[]
			{
				"OS X"
			};
			this.JoystickNames = new string[]
			{
				"Unknown Wireless Controller"
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
					Handle = "Share",
					Target = InputControlType.Share,
					Source = UnityInputDeviceProfile.Button8
				},
				new InputControlMapping
				{
					Handle = "Options",
					Target = InputControlType.Options,
					Source = UnityInputDeviceProfile.Button9
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
					Handle = "System",
					Target = InputControlType.System,
					Source = UnityInputDeviceProfile.Button12
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
				UnityInputDeviceProfile.RightStickUpMapping(UnityInputDeviceProfile.Analog3),
				UnityInputDeviceProfile.RightStickDownMapping(UnityInputDeviceProfile.Analog3),
				UnityInputDeviceProfile.LeftTriggerMapping(UnityInputDeviceProfile.Analog4),
				UnityInputDeviceProfile.RightTriggerMapping(UnityInputDeviceProfile.Analog5),
				UnityInputDeviceProfile.DPadLeftMapping(UnityInputDeviceProfile.Analog10),
				UnityInputDeviceProfile.DPadRightMapping(UnityInputDeviceProfile.Analog10),
				UnityInputDeviceProfile.DPadUpMapping(UnityInputDeviceProfile.Analog11),
				UnityInputDeviceProfile.DPadDownMapping(UnityInputDeviceProfile.Analog11),
				UnityInputDeviceProfile.DPadLeftMapping(UnityInputDeviceProfile.Analog6),
				UnityInputDeviceProfile.DPadRightMapping(UnityInputDeviceProfile.Analog6),
				UnityInputDeviceProfile.DPadUpMapping(UnityInputDeviceProfile.Analog7),
				UnityInputDeviceProfile.DPadDownMapping(UnityInputDeviceProfile.Analog7)
			};
		}
	}
}
