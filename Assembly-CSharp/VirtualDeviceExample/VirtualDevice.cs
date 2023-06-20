using System;
using InControl;
using UnityEngine;

namespace VirtualDeviceExample
{
	// Token: 0x02000042 RID: 66
	public class VirtualDevice : InputDevice
	{
		// Token: 0x06000178 RID: 376 RVA: 0x00009A40 File Offset: 0x00007C40
		public VirtualDevice() : base("Virtual Controller")
		{
			base.AddControl(InputControlType.LeftStickLeft, "Left Stick Left");
			base.AddControl(InputControlType.LeftStickRight, "Left Stick Right");
			base.AddControl(InputControlType.LeftStickUp, "Left Stick Up");
			base.AddControl(InputControlType.LeftStickDown, "Left Stick Down");
			base.AddControl(InputControlType.Action1, "A");
			base.AddControl(InputControlType.Action2, "B");
			base.AddControl(InputControlType.Action3, "X");
			base.AddControl(InputControlType.Action4, "Y");
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00009AC4 File Offset: 0x00007CC4
		public override void Update(ulong updateTick, float deltaTime)
		{
			Vector2 value = this.GenerateRotatingVector();
			base.UpdateLeftStickWithValue(value, updateTick, deltaTime);
			int num = this.GenerateSequentialButtonPresses();
			base.UpdateWithState(InputControlType.Action1, num == 0, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Action2, num == 1, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Action3, num == 2, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Action4, num == 3, updateTick, deltaTime);
			base.Commit(updateTick, deltaTime);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00009B28 File Offset: 0x00007D28
		private Vector2 GenerateRotatingVector()
		{
			float time = Time.time;
			Vector2 vector = new Vector2(Mathf.Cos(time), -Mathf.Sin(time));
			return vector.normalized;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00009B58 File Offset: 0x00007D58
		private int GenerateSequentialButtonPresses()
		{
			float num = Time.time * 0.5f;
			float num2 = num - Mathf.Floor(num);
			return Mathf.FloorToInt(num2 * 4f);
		}
	}
}
