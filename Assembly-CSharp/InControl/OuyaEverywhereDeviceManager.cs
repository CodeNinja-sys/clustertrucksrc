using System;

namespace InControl
{
	// Token: 0x02000045 RID: 69
	public class OuyaEverywhereDeviceManager : InputDeviceManager
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00009E0C File Offset: 0x0000800C
		public OuyaEverywhereDeviceManager()
		{
			for (int i = 0; i < 4; i++)
			{
				this.devices.Add(new OuyaEverywhereDevice(i));
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00009E50 File Offset: 0x00008050
		public override void Update(ulong updateTick, float deltaTime)
		{
			for (int i = 0; i < 4; i++)
			{
				OuyaEverywhereDevice ouyaEverywhereDevice = this.devices[i] as OuyaEverywhereDevice;
				if (ouyaEverywhereDevice.IsConnected != this.deviceConnected[i])
				{
					if (ouyaEverywhereDevice.IsConnected)
					{
						ouyaEverywhereDevice.BeforeAttach();
						InputManager.AttachDevice(ouyaEverywhereDevice);
					}
					else
					{
						InputManager.DetachDevice(ouyaEverywhereDevice);
					}
					this.deviceConnected[i] = ouyaEverywhereDevice.IsConnected;
				}
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00009EC4 File Offset: 0x000080C4
		public static void Enable()
		{
		}

		// Token: 0x0400010D RID: 269
		private bool[] deviceConnected = new bool[4];
	}
}
