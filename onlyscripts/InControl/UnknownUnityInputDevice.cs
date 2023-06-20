using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x020000F5 RID: 245
	public class UnknownUnityInputDevice : UnityInputDevice
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x00027688 File Offset: 0x00025888
		internal UnknownUnityInputDevice(InputDeviceProfile profile, int joystickId) : base(profile, joystickId)
		{
			this.AnalogSnapshot = new float[20];
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x000276A0 File Offset: 0x000258A0
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x000276A8 File Offset: 0x000258A8
		internal float[] AnalogSnapshot { get; private set; }

		// Token: 0x06000545 RID: 1349 RVA: 0x000276B4 File Offset: 0x000258B4
		internal void TakeSnapshot()
		{
			for (int i = 0; i < 20; i++)
			{
				InputControlType inputControlType = InputControlType.Analog0 + i;
				float num = Utility.ApplySnapping(base.GetControl(inputControlType).RawValue, 0.5f);
				this.AnalogSnapshot[i] = num;
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000276FC File Offset: 0x000258FC
		internal UnknownDeviceControl GetFirstPressedAnalog()
		{
			for (int i = 0; i < 20; i++)
			{
				InputControlType inputControlType = InputControlType.Analog0 + i;
				float num = Utility.ApplySnapping(base.GetControl(inputControlType).RawValue, 0.5f);
				float num2 = num - this.AnalogSnapshot[i];
				Debug.Log(num);
				Debug.Log(this.AnalogSnapshot[i]);
				Debug.Log(num2);
				if (num2 > 1.9f)
				{
					return new UnknownDeviceControl(inputControlType, InputRangeType.MinusOneToOne);
				}
				if (num2 < -0.9f)
				{
					return new UnknownDeviceControl(inputControlType, InputRangeType.ZeroToMinusOne);
				}
				if (num2 > 0.9f)
				{
					return new UnknownDeviceControl(inputControlType, InputRangeType.ZeroToOne);
				}
			}
			return UnknownDeviceControl.None;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000277AC File Offset: 0x000259AC
		internal UnknownDeviceControl GetFirstPressedButton()
		{
			for (int i = 0; i < 20; i++)
			{
				InputControlType inputControlType = InputControlType.Button0 + i;
				if (base.GetControl(inputControlType).IsPressed)
				{
					return new UnknownDeviceControl(inputControlType, InputRangeType.ZeroToOne);
				}
			}
			return UnknownDeviceControl.None;
		}
	}
}
