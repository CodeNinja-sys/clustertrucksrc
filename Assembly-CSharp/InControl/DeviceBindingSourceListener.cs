using System;

namespace InControl
{
	// Token: 0x0200004D RID: 77
	public class DeviceBindingSourceListener : BindingSourceListener
	{
		// Token: 0x060001AE RID: 430 RVA: 0x0000A198 File Offset: 0x00008398
		public void Reset()
		{
			this.detectFound = InputControlType.None;
			this.detectPhase = 0;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000A1A8 File Offset: 0x000083A8
		public BindingSource Listen(BindingListenOptions listenOptions, InputDevice device)
		{
			if (!listenOptions.IncludeControllers || device.IsUnknown)
			{
				return null;
			}
			if (this.detectFound != InputControlType.None && !this.IsPressed(this.detectFound, device) && this.detectPhase == 2)
			{
				DeviceBindingSource result = new DeviceBindingSource(this.detectFound);
				this.Reset();
				return result;
			}
			InputControlType inputControlType = this.ListenForControl(listenOptions, device);
			if (inputControlType != InputControlType.None)
			{
				if (this.detectPhase == 1)
				{
					this.detectFound = inputControlType;
					this.detectPhase = 2;
				}
			}
			else if (this.detectPhase == 0)
			{
				this.detectPhase = 1;
			}
			return null;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000A24C File Offset: 0x0000844C
		private bool IsPressed(InputControl control)
		{
			return Utility.AbsoluteIsOverThreshold(control.Value, 0.5f);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000A260 File Offset: 0x00008460
		private bool IsPressed(InputControlType control, InputDevice device)
		{
			return this.IsPressed(device.GetControl(control));
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000A270 File Offset: 0x00008470
		private InputControlType ListenForControl(BindingListenOptions listenOptions, InputDevice device)
		{
			if (device.IsKnown)
			{
				int num = device.Controls.Length;
				for (int i = 0; i < num; i++)
				{
					InputControl inputControl = device.Controls[i];
					if (inputControl != null && this.IsPressed(inputControl) && (listenOptions.IncludeNonStandardControls || inputControl.IsStandard))
					{
						InputControlType target = inputControl.Target;
						if (target != InputControlType.Command || !listenOptions.IncludeNonStandardControls)
						{
							return target;
						}
					}
				}
			}
			return InputControlType.None;
		}

		// Token: 0x04000128 RID: 296
		private InputControlType detectFound;

		// Token: 0x04000129 RID: 297
		private int detectPhase;
	}
}
