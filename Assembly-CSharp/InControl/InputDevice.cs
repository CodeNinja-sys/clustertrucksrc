using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200006E RID: 110
	public class InputDevice
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0000E8B4 File Offset: 0x0000CAB4
		public InputDevice(string name)
		{
			this.Name = name;
			this.Meta = string.Empty;
			this.LastChangeTick = 0UL;
			this.Controls = new InputControl[83];
			this.LeftStickX = new OneAxisInputControl();
			this.LeftStickY = new OneAxisInputControl();
			this.LeftStick = new TwoAxisInputControl();
			this.RightStickX = new OneAxisInputControl();
			this.RightStickY = new OneAxisInputControl();
			this.RightStick = new TwoAxisInputControl();
			this.DPadX = new OneAxisInputControl();
			this.DPadY = new OneAxisInputControl();
			this.DPad = new TwoAxisInputControl();
			this.Command = this.AddControl(InputControlType.Command, "Command");
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000E984 File Offset: 0x0000CB84
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x0000E98C File Offset: 0x0000CB8C
		public string Name { get; protected set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000E998 File Offset: 0x0000CB98
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x0000E9A0 File Offset: 0x0000CBA0
		public string Meta { get; protected set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
		public ulong LastChangeTick { get; protected set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000E9C8 File Offset: 0x0000CBC8
		public InputControl[] Controls { get; protected set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000E9DC File Offset: 0x0000CBDC
		public OneAxisInputControl LeftStickX { get; protected set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000E9E8 File Offset: 0x0000CBE8
		// (set) Token: 0x060002DF RID: 735 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		public OneAxisInputControl LeftStickY { get; protected set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x0000EA04 File Offset: 0x0000CC04
		public TwoAxisInputControl LeftStick { get; protected set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000EA10 File Offset: 0x0000CC10
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000EA18 File Offset: 0x0000CC18
		public OneAxisInputControl RightStickX { get; protected set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000EA24 File Offset: 0x0000CC24
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000EA2C File Offset: 0x0000CC2C
		public OneAxisInputControl RightStickY { get; protected set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000EA38 File Offset: 0x0000CC38
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000EA40 File Offset: 0x0000CC40
		public TwoAxisInputControl RightStick { get; protected set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000EA4C File Offset: 0x0000CC4C
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000EA54 File Offset: 0x0000CC54
		public OneAxisInputControl DPadX { get; protected set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000EA60 File Offset: 0x0000CC60
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000EA68 File Offset: 0x0000CC68
		public OneAxisInputControl DPadY { get; protected set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000EA74 File Offset: 0x0000CC74
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000EA7C File Offset: 0x0000CC7C
		public TwoAxisInputControl DPad { get; protected set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000EA88 File Offset: 0x0000CC88
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000EA90 File Offset: 0x0000CC90
		public InputControl Command { get; protected set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000EA9C File Offset: 0x0000CC9C
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
		public bool IsAttached { get; internal set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000EAB0 File Offset: 0x0000CCB0
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000EAB8 File Offset: 0x0000CCB8
		internal bool RawSticks { get; set; }

		// Token: 0x060002F4 RID: 756 RVA: 0x0000EAC4 File Offset: 0x0000CCC4
		public bool HasControl(InputControlType inputControlType)
		{
			return this.Controls[(int)inputControlType] != null;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000EAD4 File Offset: 0x0000CCD4
		public InputControl GetControl(InputControlType inputControlType)
		{
			InputControl inputControl = this.Controls[(int)inputControlType];
			return inputControl ?? InputControl.Null;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
		public static InputControlType GetInputControlTypeByName(string inputControlName)
		{
			return (InputControlType)((int)Enum.Parse(typeof(InputControlType), inputControlName));
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000EB10 File Offset: 0x0000CD10
		public InputControl GetControlByName(string inputControlName)
		{
			InputControlType inputControlTypeByName = InputDevice.GetInputControlTypeByName(inputControlName);
			return this.GetControl(inputControlTypeByName);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		public InputControl AddControl(InputControlType inputControlType, string handle)
		{
			InputControl inputControl = new InputControl(handle, inputControlType);
			this.Controls[(int)inputControlType] = inputControl;
			return inputControl;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000EB4C File Offset: 0x0000CD4C
		public InputControl AddControl(InputControlType inputControlType, string handle, float lowerDeadZone, float upperDeadZone)
		{
			InputControl inputControl = this.AddControl(inputControlType, handle);
			inputControl.LowerDeadZone = lowerDeadZone;
			inputControl.UpperDeadZone = upperDeadZone;
			return inputControl;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000EB74 File Offset: 0x0000CD74
		public void ClearInputState()
		{
			this.LeftStickX.ClearInputState();
			this.LeftStickY.ClearInputState();
			this.LeftStick.ClearInputState();
			this.RightStickX.ClearInputState();
			this.RightStickY.ClearInputState();
			this.RightStick.ClearInputState();
			this.DPadX.ClearInputState();
			this.DPadY.ClearInputState();
			this.DPad.ClearInputState();
			int num = this.Controls.Length;
			for (int i = 0; i < num; i++)
			{
				InputControl inputControl = this.Controls[i];
				if (inputControl != null)
				{
					inputControl.ClearInputState();
				}
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000EC14 File Offset: 0x0000CE14
		internal void UpdateWithState(InputControlType inputControlType, bool state, ulong updateTick, float deltaTime)
		{
			this.GetControl(inputControlType).UpdateWithState(state, updateTick, deltaTime);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000EC28 File Offset: 0x0000CE28
		internal void UpdateWithValue(InputControlType inputControlType, float value, ulong updateTick, float deltaTime)
		{
			this.GetControl(inputControlType).UpdateWithValue(value, updateTick, deltaTime);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000EC3C File Offset: 0x0000CE3C
		internal void UpdateLeftStickWithValue(Vector2 value, ulong updateTick, float deltaTime)
		{
			this.LeftStickLeft.UpdateWithValue(Mathf.Max(0f, -value.x), updateTick, deltaTime);
			this.LeftStickRight.UpdateWithValue(Mathf.Max(0f, value.x), updateTick, deltaTime);
			if (InputManager.InvertYAxis)
			{
				this.LeftStickUp.UpdateWithValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
				this.LeftStickDown.UpdateWithValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
			}
			else
			{
				this.LeftStickUp.UpdateWithValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
				this.LeftStickDown.UpdateWithValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000ED18 File Offset: 0x0000CF18
		internal void UpdateLeftStickWithRawValue(Vector2 value, ulong updateTick, float deltaTime)
		{
			this.LeftStickLeft.UpdateWithRawValue(Mathf.Max(0f, -value.x), updateTick, deltaTime);
			this.LeftStickRight.UpdateWithRawValue(Mathf.Max(0f, value.x), updateTick, deltaTime);
			if (InputManager.InvertYAxis)
			{
				this.LeftStickUp.UpdateWithRawValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
				this.LeftStickDown.UpdateWithRawValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
			}
			else
			{
				this.LeftStickUp.UpdateWithRawValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
				this.LeftStickDown.UpdateWithRawValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000EDF4 File Offset: 0x0000CFF4
		internal void CommitLeftStick()
		{
			this.LeftStickUp.Commit();
			this.LeftStickDown.Commit();
			this.LeftStickLeft.Commit();
			this.LeftStickRight.Commit();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000EE30 File Offset: 0x0000D030
		internal void UpdateRightStickWithValue(Vector2 value, ulong updateTick, float deltaTime)
		{
			this.RightStickLeft.UpdateWithValue(Mathf.Max(0f, -value.x), updateTick, deltaTime);
			this.RightStickRight.UpdateWithValue(Mathf.Max(0f, value.x), updateTick, deltaTime);
			if (InputManager.InvertYAxis)
			{
				this.RightStickUp.UpdateWithValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
				this.RightStickDown.UpdateWithValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
			}
			else
			{
				this.RightStickUp.UpdateWithValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
				this.RightStickDown.UpdateWithValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000EF0C File Offset: 0x0000D10C
		internal void UpdateRightStickWithRawValue(Vector2 value, ulong updateTick, float deltaTime)
		{
			this.RightStickLeft.UpdateWithRawValue(Mathf.Max(0f, -value.x), updateTick, deltaTime);
			this.RightStickRight.UpdateWithRawValue(Mathf.Max(0f, value.x), updateTick, deltaTime);
			if (InputManager.InvertYAxis)
			{
				this.RightStickUp.UpdateWithRawValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
				this.RightStickDown.UpdateWithRawValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
			}
			else
			{
				this.RightStickUp.UpdateWithRawValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
				this.RightStickDown.UpdateWithRawValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		internal void CommitRightStick()
		{
			this.RightStickUp.Commit();
			this.RightStickDown.Commit();
			this.RightStickLeft.Commit();
			this.RightStickRight.Commit();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000F024 File Offset: 0x0000D224
		public virtual void Update(ulong updateTick, float deltaTime)
		{
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000F028 File Offset: 0x0000D228
		private bool AnyCommandControlIsPressed()
		{
			for (int i = 24; i <= 34; i++)
			{
				InputControl inputControl = this.Controls[i];
				if (inputControl != null && inputControl.IsPressed)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000F068 File Offset: 0x0000D268
		internal void ProcessLeftStick(ulong updateTick, float deltaTime)
		{
			float x = Utility.ValueFromSides(this.LeftStickLeft.NextRawValue, this.LeftStickRight.NextRawValue);
			float y = Utility.ValueFromSides(this.LeftStickDown.NextRawValue, this.LeftStickUp.NextRawValue, InputManager.InvertYAxis);
			Vector2 vector;
			if (this.RawSticks)
			{
				vector = new Vector2(x, y);
			}
			else
			{
				float lowerDeadZone = Utility.Max(this.LeftStickLeft.LowerDeadZone, this.LeftStickRight.LowerDeadZone, this.LeftStickUp.LowerDeadZone, this.LeftStickDown.LowerDeadZone);
				float upperDeadZone = Utility.Min(this.LeftStickLeft.UpperDeadZone, this.LeftStickRight.UpperDeadZone, this.LeftStickUp.UpperDeadZone, this.LeftStickDown.UpperDeadZone);
				vector = Utility.ApplyCircularDeadZone(x, y, lowerDeadZone, upperDeadZone);
			}
			this.LeftStick.Raw = true;
			this.LeftStick.UpdateWithAxes(vector.x, vector.y, updateTick, deltaTime);
			this.LeftStickX.Raw = true;
			this.LeftStickX.CommitWithValue(vector.x, updateTick, deltaTime);
			this.LeftStickY.Raw = true;
			this.LeftStickY.CommitWithValue(vector.y, updateTick, deltaTime);
			this.LeftStickLeft.SetValue(this.LeftStick.Left.Value, updateTick);
			this.LeftStickRight.SetValue(this.LeftStick.Right.Value, updateTick);
			this.LeftStickUp.SetValue(this.LeftStick.Up.Value, updateTick);
			this.LeftStickDown.SetValue(this.LeftStick.Down.Value, updateTick);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000F214 File Offset: 0x0000D414
		internal void ProcessRightStick(ulong updateTick, float deltaTime)
		{
			float x = Utility.ValueFromSides(this.RightStickLeft.NextRawValue, this.RightStickRight.NextRawValue);
			float y = Utility.ValueFromSides(this.RightStickDown.NextRawValue, this.RightStickUp.NextRawValue, InputManager.InvertYAxis);
			Vector2 vector;
			if (this.RawSticks)
			{
				vector = new Vector2(x, y);
			}
			else
			{
				float lowerDeadZone = Utility.Max(this.RightStickLeft.LowerDeadZone, this.RightStickRight.LowerDeadZone, this.RightStickUp.LowerDeadZone, this.RightStickDown.LowerDeadZone);
				float upperDeadZone = Utility.Min(this.RightStickLeft.UpperDeadZone, this.RightStickRight.UpperDeadZone, this.RightStickUp.UpperDeadZone, this.RightStickDown.UpperDeadZone);
				vector = Utility.ApplyCircularDeadZone(x, y, lowerDeadZone, upperDeadZone);
			}
			this.RightStick.Raw = true;
			this.RightStick.UpdateWithAxes(vector.x, vector.y, updateTick, deltaTime);
			this.RightStickX.Raw = true;
			this.RightStickX.CommitWithValue(vector.x, updateTick, deltaTime);
			this.RightStickY.Raw = true;
			this.RightStickY.CommitWithValue(vector.y, updateTick, deltaTime);
			this.RightStickLeft.SetValue(this.RightStick.Left.Value, updateTick);
			this.RightStickRight.SetValue(this.RightStick.Right.Value, updateTick);
			this.RightStickUp.SetValue(this.RightStick.Up.Value, updateTick);
			this.RightStickDown.SetValue(this.RightStick.Down.Value, updateTick);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000F3C0 File Offset: 0x0000D5C0
		internal void ProcessDPad(ulong updateTick, float deltaTime)
		{
			float lowerDeadZone = Utility.Max(this.DPadLeft.LowerDeadZone, this.DPadRight.LowerDeadZone, this.DPadUp.LowerDeadZone, this.DPadDown.LowerDeadZone);
			float upperDeadZone = Utility.Min(this.DPadLeft.UpperDeadZone, this.DPadRight.UpperDeadZone, this.DPadUp.UpperDeadZone, this.DPadDown.UpperDeadZone);
			float x = Utility.ValueFromSides(this.DPadLeft.NextRawValue, this.DPadRight.NextRawValue);
			float y = Utility.ValueFromSides(this.DPadDown.NextRawValue, this.DPadUp.NextRawValue, InputManager.InvertYAxis);
			Vector2 vector = Utility.ApplyCircularDeadZone(x, y, lowerDeadZone, upperDeadZone);
			this.DPad.Raw = true;
			this.DPad.UpdateWithAxes(vector.x, vector.y, updateTick, deltaTime);
			this.DPadX.Raw = true;
			this.DPadX.CommitWithValue(vector.x, updateTick, deltaTime);
			this.DPadY.Raw = true;
			this.DPadY.CommitWithValue(vector.y, updateTick, deltaTime);
			this.DPadLeft.SetValue(this.DPad.Left.Value, updateTick);
			this.DPadRight.SetValue(this.DPad.Right.Value, updateTick);
			this.DPadUp.SetValue(this.DPad.Up.Value, updateTick);
			this.DPadDown.SetValue(this.DPad.Down.Value, updateTick);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000F550 File Offset: 0x0000D750
		public void Commit(ulong updateTick, float deltaTime)
		{
			this.ProcessLeftStick(updateTick, deltaTime);
			this.ProcessRightStick(updateTick, deltaTime);
			this.ProcessDPad(updateTick, deltaTime);
			int num = this.Controls.Length;
			for (int i = 0; i < num; i++)
			{
				InputControl inputControl = this.Controls[i];
				if (inputControl != null)
				{
					inputControl.Commit();
					if (inputControl.HasChanged)
					{
						this.LastChangeTick = updateTick;
					}
				}
			}
			if (this.IsKnown)
			{
				this.Command.CommitWithState(this.AnyCommandControlIsPressed(), updateTick, deltaTime);
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000F5D8 File Offset: 0x0000D7D8
		public bool LastChangedAfter(InputDevice otherDevice)
		{
			return this.LastChangeTick > otherDevice.LastChangeTick;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000F5E8 File Offset: 0x0000D7E8
		internal void RequestActivation()
		{
			this.LastChangeTick = InputManager.CurrentTick;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000F5F8 File Offset: 0x0000D7F8
		public virtual void Vibrate(float leftMotor, float rightMotor)
		{
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000F5FC File Offset: 0x0000D7FC
		public void Vibrate(float intensity)
		{
			this.Vibrate(intensity, intensity);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000F608 File Offset: 0x0000D808
		public void StopVibration()
		{
			this.Vibrate(0f);
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000F618 File Offset: 0x0000D818
		public virtual bool IsSupportedOnThisPlatform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000F61C File Offset: 0x0000D81C
		public virtual bool IsKnown
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000F620 File Offset: 0x0000D820
		public bool IsUnknown
		{
			get
			{
				return !this.IsKnown;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000F62C File Offset: 0x0000D82C
		public bool MenuWasPressed
		{
			get
			{
				return this.GetControl(InputControlType.Command).WasPressed;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000F63C File Offset: 0x0000D83C
		public InputControl AnyButton
		{
			get
			{
				int length = this.Controls.GetLength(0);
				for (int i = 0; i < length; i++)
				{
					InputControl inputControl = this.Controls[i];
					if (inputControl != null && inputControl.IsButton && inputControl.IsPressed)
					{
						return inputControl;
					}
				}
				return InputControl.Null;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000F694 File Offset: 0x0000D894
		public InputControl LeftStickUp
		{
			get
			{
				return this.GetControl(InputControlType.LeftStickUp);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000F6A0 File Offset: 0x0000D8A0
		public InputControl LeftStickDown
		{
			get
			{
				return this.GetControl(InputControlType.LeftStickDown);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000F6AC File Offset: 0x0000D8AC
		public InputControl LeftStickLeft
		{
			get
			{
				return this.GetControl(InputControlType.LeftStickLeft);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000F6B8 File Offset: 0x0000D8B8
		public InputControl LeftStickRight
		{
			get
			{
				return this.GetControl(InputControlType.LeftStickRight);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000F6C4 File Offset: 0x0000D8C4
		public InputControl RightStickUp
		{
			get
			{
				return this.GetControl(InputControlType.RightStickUp);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
		public InputControl RightStickDown
		{
			get
			{
				return this.GetControl(InputControlType.RightStickDown);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000F6DC File Offset: 0x0000D8DC
		public InputControl RightStickLeft
		{
			get
			{
				return this.GetControl(InputControlType.RightStickLeft);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000F6E8 File Offset: 0x0000D8E8
		public InputControl RightStickRight
		{
			get
			{
				return this.GetControl(InputControlType.RightStickRight);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
		public InputControl DPadUp
		{
			get
			{
				return this.GetControl(InputControlType.DPadUp);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000F700 File Offset: 0x0000D900
		public InputControl DPadDown
		{
			get
			{
				return this.GetControl(InputControlType.DPadDown);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000F70C File Offset: 0x0000D90C
		public InputControl DPadLeft
		{
			get
			{
				return this.GetControl(InputControlType.DPadLeft);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000F718 File Offset: 0x0000D918
		public InputControl DPadRight
		{
			get
			{
				return this.GetControl(InputControlType.DPadRight);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000F724 File Offset: 0x0000D924
		public InputControl Action1
		{
			get
			{
				return this.GetControl(InputControlType.Action1);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000F730 File Offset: 0x0000D930
		public InputControl Action2
		{
			get
			{
				return this.GetControl(InputControlType.Action2);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000F73C File Offset: 0x0000D93C
		public InputControl Action3
		{
			get
			{
				return this.GetControl(InputControlType.Action3);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000F748 File Offset: 0x0000D948
		public InputControl Action4
		{
			get
			{
				return this.GetControl(InputControlType.Action4);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000F754 File Offset: 0x0000D954
		public InputControl LeftTrigger
		{
			get
			{
				return this.GetControl(InputControlType.LeftTrigger);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000F760 File Offset: 0x0000D960
		public InputControl RightTrigger
		{
			get
			{
				return this.GetControl(InputControlType.RightTrigger);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000F76C File Offset: 0x0000D96C
		public InputControl LeftBumper
		{
			get
			{
				return this.GetControl(InputControlType.LeftBumper);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000F778 File Offset: 0x0000D978
		public InputControl RightBumper
		{
			get
			{
				return this.GetControl(InputControlType.RightBumper);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000F784 File Offset: 0x0000D984
		public InputControl LeftStickButton
		{
			get
			{
				return this.GetControl(InputControlType.LeftStickButton);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000F790 File Offset: 0x0000D990
		public InputControl RightStickButton
		{
			get
			{
				return this.GetControl(InputControlType.RightStickButton);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000F79C File Offset: 0x0000D99C
		public TwoAxisInputControl Direction
		{
			get
			{
				return (this.DPad.UpdateTick <= this.LeftStick.UpdateTick) ? this.LeftStick : this.DPad;
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000F7D8 File Offset: 0x0000D9D8
		public static implicit operator bool(InputDevice device)
		{
			return device != null;
		}

		// Token: 0x04000290 RID: 656
		public static readonly InputDevice Null = new InputDevice("None");

		// Token: 0x04000291 RID: 657
		internal int SortOrder = int.MaxValue;
	}
}
