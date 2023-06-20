using System;

namespace InControl
{
	// Token: 0x0200005B RID: 91
	public class UnknownDeviceBindingSourceListener : BindingSourceListener
	{
		// Token: 0x06000242 RID: 578 RVA: 0x0000D31C File Offset: 0x0000B51C
		public void Reset()
		{
			this.detectFound = UnknownDeviceControl.None;
			this.detectPhase = UnknownDeviceBindingSourceListener.DetectPhase.WaitForInitialRelease;
			this.TakeSnapshotOnUnknownDevices();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000D338 File Offset: 0x0000B538
		private void TakeSnapshotOnUnknownDevices()
		{
			int count = InputManager.Devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputDevice inputDevice = InputManager.Devices[i];
				if (inputDevice.IsUnknown)
				{
					UnknownUnityInputDevice unknownUnityInputDevice = inputDevice as UnknownUnityInputDevice;
					if (unknownUnityInputDevice != null)
					{
						unknownUnityInputDevice.TakeSnapshot();
					}
				}
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000D38C File Offset: 0x0000B58C
		public BindingSource Listen(BindingListenOptions listenOptions, InputDevice device)
		{
			if (!listenOptions.IncludeUnknownControllers || device.IsKnown)
			{
				return null;
			}
			if (this.detectPhase == UnknownDeviceBindingSourceListener.DetectPhase.WaitForControlRelease && this.detectFound && !this.IsPressed(this.detectFound, device))
			{
				UnknownDeviceBindingSource result = new UnknownDeviceBindingSource(this.detectFound);
				this.Reset();
				return result;
			}
			UnknownDeviceControl control = this.ListenForControl(listenOptions, device);
			if (control)
			{
				if (this.detectPhase == UnknownDeviceBindingSourceListener.DetectPhase.WaitForControlPress)
				{
					this.detectFound = control;
					this.detectPhase = UnknownDeviceBindingSourceListener.DetectPhase.WaitForControlRelease;
				}
			}
			else if (this.detectPhase == UnknownDeviceBindingSourceListener.DetectPhase.WaitForInitialRelease)
			{
				this.detectPhase = UnknownDeviceBindingSourceListener.DetectPhase.WaitForControlPress;
			}
			return null;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000D43C File Offset: 0x0000B63C
		private bool IsPressed(UnknownDeviceControl control, InputDevice device)
		{
			float value = control.GetValue(device);
			return Utility.AbsoluteIsOverThreshold(value, 0.5f);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000D460 File Offset: 0x0000B660
		private UnknownDeviceControl ListenForControl(BindingListenOptions listenOptions, InputDevice device)
		{
			if (device.IsUnknown)
			{
				UnknownUnityInputDevice unknownUnityInputDevice = device as UnknownUnityInputDevice;
				if (unknownUnityInputDevice != null)
				{
					UnknownDeviceControl firstPressedButton = unknownUnityInputDevice.GetFirstPressedButton();
					if (firstPressedButton)
					{
						return firstPressedButton;
					}
					UnknownDeviceControl firstPressedAnalog = unknownUnityInputDevice.GetFirstPressedAnalog();
					if (firstPressedAnalog)
					{
						return firstPressedAnalog;
					}
				}
			}
			return UnknownDeviceControl.None;
		}

		// Token: 0x040001D8 RID: 472
		private UnknownDeviceControl detectFound;

		// Token: 0x040001D9 RID: 473
		private UnknownDeviceBindingSourceListener.DetectPhase detectPhase;

		// Token: 0x0200005C RID: 92
		private enum DetectPhase
		{
			// Token: 0x040001DB RID: 475
			WaitForInitialRelease,
			// Token: 0x040001DC RID: 476
			WaitForControlPress,
			// Token: 0x040001DD RID: 477
			WaitForControlRelease
		}
	}
}
