using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200008C RID: 140
	[Obsolete("Custom profiles are deprecated. Use the bindings API instead.", false)]
	public class CustomInputDeviceProfile : InputDeviceProfile
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x00013D70 File Offset: 0x00011F70
		public CustomInputDeviceProfile()
		{
			base.Name = "Custom Device Profile";
			base.Meta = "Custom Device Profile";
			base.SupportedPlatforms = new string[]
			{
				"Windows",
				"Mac",
				"Linux"
			};
			base.Sensitivity = 1f;
			base.LowerDeadZone = 0f;
			base.UpperDeadZone = 1f;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00013DE0 File Offset: 0x00011FE0
		public sealed override bool IsKnown
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00013DE4 File Offset: 0x00011FE4
		public sealed override bool IsJoystick
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00013DE8 File Offset: 0x00011FE8
		public sealed override bool HasJoystickName(string joystickName)
		{
			return false;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00013DEC File Offset: 0x00011FEC
		public sealed override bool HasLastResortRegex(string joystickName)
		{
			return false;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00013DF0 File Offset: 0x00011FF0
		public sealed override bool HasJoystickOrRegexName(string joystickName)
		{
			return false;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00013DF4 File Offset: 0x00011FF4
		protected static InputControlSource KeyCodeButton(params KeyCode[] keyCodeList)
		{
			return new UnityKeyCodeSource(keyCodeList);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00013DFC File Offset: 0x00011FFC
		protected static InputControlSource KeyCodeComboButton(params KeyCode[] keyCodeList)
		{
			return new UnityKeyCodeComboSource(keyCodeList);
		}
	}
}
