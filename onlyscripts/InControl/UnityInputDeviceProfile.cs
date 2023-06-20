using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace InControl
{
	// Token: 0x020000F3 RID: 243
	public class UnityInputDeviceProfile : InputDeviceProfile
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x00026B50 File Offset: 0x00024D50
		public UnityInputDeviceProfile()
		{
			base.Sensitivity = 1f;
			base.LowerDeadZone = 0.2f;
			base.UpperDeadZone = 0.9f;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00026D8C File Offset: 0x00024F8C
		public override bool IsKnown
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00026D90 File Offset: 0x00024F90
		public override bool IsJoystick
		{
			get
			{
				return this.LastResortRegex != null || (this.JoystickNames != null && this.JoystickNames.Length > 0) || (this.JoystickRegex != null && this.JoystickRegex.Length > 0);
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00026DE0 File Offset: 0x00024FE0
		public override bool HasJoystickName(string joystickName)
		{
			if (base.IsNotJoystick)
			{
				return false;
			}
			if (this.JoystickNames != null && this.JoystickNames.Contains(joystickName, StringComparer.OrdinalIgnoreCase))
			{
				return true;
			}
			if (this.JoystickRegex != null)
			{
				for (int i = 0; i < this.JoystickRegex.Length; i++)
				{
					if (Regex.IsMatch(joystickName, this.JoystickRegex[i], RegexOptions.IgnoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00026E58 File Offset: 0x00025058
		public override bool HasLastResortRegex(string joystickName)
		{
			return !base.IsNotJoystick && this.LastResortRegex != null && Regex.IsMatch(joystickName, this.LastResortRegex, RegexOptions.IgnoreCase);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00026E84 File Offset: 0x00025084
		public override bool HasJoystickOrRegexName(string joystickName)
		{
			return this.HasJoystickName(joystickName) || this.HasLastResortRegex(joystickName);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00026E9C File Offset: 0x0002509C
		protected static InputControlSource Button(int index)
		{
			return new UnityButtonSource(index);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00026EA4 File Offset: 0x000250A4
		protected static InputControlSource Analog(int index)
		{
			return new UnityAnalogSource(index);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00026EAC File Offset: 0x000250AC
		protected static InputControlMapping LeftStickLeftMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Left Stick Left",
				Target = InputControlType.LeftStickLeft,
				Source = analog,
				SourceRange = InputRange.ZeroToMinusOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00026EF0 File Offset: 0x000250F0
		protected static InputControlMapping LeftStickRightMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Left Stick Right",
				Target = InputControlType.LeftStickRight,
				Source = analog,
				SourceRange = InputRange.ZeroToOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00026F34 File Offset: 0x00025134
		protected static InputControlMapping LeftStickUpMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Left Stick Up",
				Target = InputControlType.LeftStickUp,
				Source = analog,
				SourceRange = InputRange.ZeroToMinusOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00026F78 File Offset: 0x00025178
		protected static InputControlMapping LeftStickDownMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Left Stick Down",
				Target = InputControlType.LeftStickDown,
				Source = analog,
				SourceRange = InputRange.ZeroToOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00026FBC File Offset: 0x000251BC
		protected static InputControlMapping RightStickLeftMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Right Stick Left",
				Target = InputControlType.RightStickLeft,
				Source = analog,
				SourceRange = InputRange.ZeroToMinusOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00027000 File Offset: 0x00025200
		protected static InputControlMapping RightStickRightMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Right Stick Right",
				Target = InputControlType.RightStickRight,
				Source = analog,
				SourceRange = InputRange.ZeroToOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00027044 File Offset: 0x00025244
		protected static InputControlMapping RightStickUpMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Right Stick Up",
				Target = InputControlType.RightStickUp,
				Source = analog,
				SourceRange = InputRange.ZeroToMinusOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00027088 File Offset: 0x00025288
		protected static InputControlMapping RightStickDownMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Right Stick Down",
				Target = InputControlType.RightStickDown,
				Source = analog,
				SourceRange = InputRange.ZeroToOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000270CC File Offset: 0x000252CC
		protected static InputControlMapping LeftTriggerMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Left Trigger",
				Target = InputControlType.LeftTrigger,
				Source = analog,
				SourceRange = InputRange.MinusOneToOne,
				TargetRange = InputRange.ZeroToOne,
				IgnoreInitialZeroValue = true
			};
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00027118 File Offset: 0x00025318
		protected static InputControlMapping RightTriggerMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Right Trigger",
				Target = InputControlType.RightTrigger,
				Source = analog,
				SourceRange = InputRange.MinusOneToOne,
				TargetRange = InputRange.ZeroToOne,
				IgnoreInitialZeroValue = true
			};
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00027164 File Offset: 0x00025364
		protected static InputControlMapping DPadLeftMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "DPad Left",
				Target = InputControlType.DPadLeft,
				Source = analog,
				SourceRange = InputRange.ZeroToMinusOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000271A8 File Offset: 0x000253A8
		protected static InputControlMapping DPadRightMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "DPad Right",
				Target = InputControlType.DPadRight,
				Source = analog,
				SourceRange = InputRange.ZeroToOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000271EC File Offset: 0x000253EC
		protected static InputControlMapping DPadUpMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "DPad Up",
				Target = InputControlType.DPadUp,
				Source = analog,
				SourceRange = InputRange.ZeroToMinusOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00027230 File Offset: 0x00025430
		protected static InputControlMapping DPadDownMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "DPad Down",
				Target = InputControlType.DPadDown,
				Source = analog,
				SourceRange = InputRange.ZeroToOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00027274 File Offset: 0x00025474
		protected static InputControlMapping DPadUpMapping2(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "DPad Up",
				Target = InputControlType.DPadUp,
				Source = analog,
				SourceRange = InputRange.ZeroToOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000272B8 File Offset: 0x000254B8
		protected static InputControlMapping DPadDownMapping2(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "DPad Down",
				Target = InputControlType.DPadDown,
				Source = analog,
				SourceRange = InputRange.ZeroToMinusOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x040003BE RID: 958
		[SerializeField]
		protected string[] JoystickNames;

		// Token: 0x040003BF RID: 959
		[SerializeField]
		protected string[] JoystickRegex;

		// Token: 0x040003C0 RID: 960
		[SerializeField]
		protected string LastResortRegex;

		// Token: 0x040003C1 RID: 961
		protected static InputControlSource Button0 = UnityInputDeviceProfile.Button(0);

		// Token: 0x040003C2 RID: 962
		protected static InputControlSource Button1 = UnityInputDeviceProfile.Button(1);

		// Token: 0x040003C3 RID: 963
		protected static InputControlSource Button2 = UnityInputDeviceProfile.Button(2);

		// Token: 0x040003C4 RID: 964
		protected static InputControlSource Button3 = UnityInputDeviceProfile.Button(3);

		// Token: 0x040003C5 RID: 965
		protected static InputControlSource Button4 = UnityInputDeviceProfile.Button(4);

		// Token: 0x040003C6 RID: 966
		protected static InputControlSource Button5 = UnityInputDeviceProfile.Button(5);

		// Token: 0x040003C7 RID: 967
		protected static InputControlSource Button6 = UnityInputDeviceProfile.Button(6);

		// Token: 0x040003C8 RID: 968
		protected static InputControlSource Button7 = UnityInputDeviceProfile.Button(7);

		// Token: 0x040003C9 RID: 969
		protected static InputControlSource Button8 = UnityInputDeviceProfile.Button(8);

		// Token: 0x040003CA RID: 970
		protected static InputControlSource Button9 = UnityInputDeviceProfile.Button(9);

		// Token: 0x040003CB RID: 971
		protected static InputControlSource Button10 = UnityInputDeviceProfile.Button(10);

		// Token: 0x040003CC RID: 972
		protected static InputControlSource Button11 = UnityInputDeviceProfile.Button(11);

		// Token: 0x040003CD RID: 973
		protected static InputControlSource Button12 = UnityInputDeviceProfile.Button(12);

		// Token: 0x040003CE RID: 974
		protected static InputControlSource Button13 = UnityInputDeviceProfile.Button(13);

		// Token: 0x040003CF RID: 975
		protected static InputControlSource Button14 = UnityInputDeviceProfile.Button(14);

		// Token: 0x040003D0 RID: 976
		protected static InputControlSource Button15 = UnityInputDeviceProfile.Button(15);

		// Token: 0x040003D1 RID: 977
		protected static InputControlSource Button16 = UnityInputDeviceProfile.Button(16);

		// Token: 0x040003D2 RID: 978
		protected static InputControlSource Button17 = UnityInputDeviceProfile.Button(17);

		// Token: 0x040003D3 RID: 979
		protected static InputControlSource Button18 = UnityInputDeviceProfile.Button(18);

		// Token: 0x040003D4 RID: 980
		protected static InputControlSource Button19 = UnityInputDeviceProfile.Button(19);

		// Token: 0x040003D5 RID: 981
		protected static InputControlSource Analog0 = UnityInputDeviceProfile.Analog(0);

		// Token: 0x040003D6 RID: 982
		protected static InputControlSource Analog1 = UnityInputDeviceProfile.Analog(1);

		// Token: 0x040003D7 RID: 983
		protected static InputControlSource Analog2 = UnityInputDeviceProfile.Analog(2);

		// Token: 0x040003D8 RID: 984
		protected static InputControlSource Analog3 = UnityInputDeviceProfile.Analog(3);

		// Token: 0x040003D9 RID: 985
		protected static InputControlSource Analog4 = UnityInputDeviceProfile.Analog(4);

		// Token: 0x040003DA RID: 986
		protected static InputControlSource Analog5 = UnityInputDeviceProfile.Analog(5);

		// Token: 0x040003DB RID: 987
		protected static InputControlSource Analog6 = UnityInputDeviceProfile.Analog(6);

		// Token: 0x040003DC RID: 988
		protected static InputControlSource Analog7 = UnityInputDeviceProfile.Analog(7);

		// Token: 0x040003DD RID: 989
		protected static InputControlSource Analog8 = UnityInputDeviceProfile.Analog(8);

		// Token: 0x040003DE RID: 990
		protected static InputControlSource Analog9 = UnityInputDeviceProfile.Analog(9);

		// Token: 0x040003DF RID: 991
		protected static InputControlSource Analog10 = UnityInputDeviceProfile.Analog(10);

		// Token: 0x040003E0 RID: 992
		protected static InputControlSource Analog11 = UnityInputDeviceProfile.Analog(11);

		// Token: 0x040003E1 RID: 993
		protected static InputControlSource Analog12 = UnityInputDeviceProfile.Analog(12);

		// Token: 0x040003E2 RID: 994
		protected static InputControlSource Analog13 = UnityInputDeviceProfile.Analog(13);

		// Token: 0x040003E3 RID: 995
		protected static InputControlSource Analog14 = UnityInputDeviceProfile.Analog(14);

		// Token: 0x040003E4 RID: 996
		protected static InputControlSource Analog15 = UnityInputDeviceProfile.Analog(15);

		// Token: 0x040003E5 RID: 997
		protected static InputControlSource Analog16 = UnityInputDeviceProfile.Analog(16);

		// Token: 0x040003E6 RID: 998
		protected static InputControlSource Analog17 = UnityInputDeviceProfile.Analog(17);

		// Token: 0x040003E7 RID: 999
		protected static InputControlSource Analog18 = UnityInputDeviceProfile.Analog(18);

		// Token: 0x040003E8 RID: 1000
		protected static InputControlSource Analog19 = UnityInputDeviceProfile.Analog(19);

		// Token: 0x040003E9 RID: 1001
		protected static InputControlSource MenuKey = new UnityKeyCodeSource(new KeyCode[]
		{
			KeyCode.Menu
		});

		// Token: 0x040003EA RID: 1002
		protected static InputControlSource EscapeKey = new UnityKeyCodeSource(new KeyCode[]
		{
			KeyCode.Escape
		});
	}
}
