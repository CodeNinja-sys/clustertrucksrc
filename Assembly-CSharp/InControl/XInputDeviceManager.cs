using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using XInputDotNetPure;

namespace InControl
{
	// Token: 0x020000FD RID: 253
	public class XInputDeviceManager : InputDeviceManager
	{
		// Token: 0x0600058E RID: 1422 RVA: 0x000290A4 File Offset: 0x000272A4
		public XInputDeviceManager()
		{
			if (InputManager.XInputUpdateRate == 0U)
			{
				this.timeStep = Mathf.FloorToInt(Time.fixedDeltaTime * 1000f);
			}
			else
			{
				this.timeStep = Mathf.FloorToInt(1f / InputManager.XInputUpdateRate * 1000f);
			}
			this.bufferSize = (int)Math.Max(InputManager.XInputBufferSize, 1U);
			for (int i = 0; i < 4; i++)
			{
				this.gamePadState[i] = new RingBuffer<GamePadState>(this.bufferSize);
			}
			this.StartWorker();
			for (int j = 0; j < 4; j++)
			{
				this.devices.Add(new XInputDevice(j, this));
			}
			this.Update(0UL, 0f);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00029180 File Offset: 0x00027380
		private void StartWorker()
		{
			if (this.thread == null)
			{
				this.thread = new Thread(new ThreadStart(this.Worker));
				this.thread.IsBackground = true;
				this.thread.Start();
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000291BC File Offset: 0x000273BC
		private void StopWorker()
		{
			if (this.thread != null)
			{
				this.thread.Abort();
				this.thread.Join();
				this.thread = null;
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000291F4 File Offset: 0x000273F4
		private void Worker()
		{
			for (;;)
			{
				for (int i = 0; i < 4; i++)
				{
					this.gamePadState[i].Enqueue(GamePad.GetState((PlayerIndex)i));
				}
				Thread.Sleep(this.timeStep);
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00029238 File Offset: 0x00027438
		internal GamePadState GetState(int deviceIndex)
		{
			return this.gamePadState[deviceIndex].Dequeue();
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00029248 File Offset: 0x00027448
		public override void Update(ulong updateTick, float deltaTime)
		{
			for (int i = 0; i < 4; i++)
			{
				XInputDevice xinputDevice = this.devices[i] as XInputDevice;
				if (!xinputDevice.IsConnected)
				{
					xinputDevice.GetState();
				}
				if (xinputDevice.IsConnected != this.deviceConnected[i])
				{
					if (xinputDevice.IsConnected)
					{
						InputManager.AttachDevice(xinputDevice);
					}
					else
					{
						InputManager.DetachDevice(xinputDevice);
					}
					this.deviceConnected[i] = xinputDevice.IsConnected;
				}
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000292C8 File Offset: 0x000274C8
		public override void Destroy()
		{
			this.StopWorker();
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x000292D0 File Offset: 0x000274D0
		public static bool CheckPlatformSupport(ICollection<string> errors)
		{
			if (Application.platform != RuntimePlatform.WindowsPlayer && Application.platform != RuntimePlatform.WindowsEditor)
			{
				return false;
			}
			try
			{
				GamePad.GetState(PlayerIndex.One);
			}
			catch (DllNotFoundException ex)
			{
				if (errors != null)
				{
					errors.Add(ex.Message + ".dll could not be found or is missing a dependency.");
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0002934C File Offset: 0x0002754C
		internal static void Enable()
		{
			List<string> list = new List<string>();
			if (XInputDeviceManager.CheckPlatformSupport(list))
			{
				InputManager.HideDevicesWithProfile(typeof(Xbox360WinProfile));
				InputManager.HideDevicesWithProfile(typeof(XboxOneWinProfile));
				InputManager.HideDevicesWithProfile(typeof(XboxOneWin10Profile));
				InputManager.HideDevicesWithProfile(typeof(LogitechF310ModeXWinProfile));
				InputManager.HideDevicesWithProfile(typeof(LogitechF510ModeXWinProfile));
				InputManager.HideDevicesWithProfile(typeof(LogitechF710ModeXWinProfile));
				InputManager.AddDeviceManager<XInputDeviceManager>();
			}
			else
			{
				foreach (string text in list)
				{
					Logger.LogError(text);
				}
			}
		}

		// Token: 0x04000401 RID: 1025
		private const int maxDevices = 4;

		// Token: 0x04000402 RID: 1026
		private bool[] deviceConnected = new bool[4];

		// Token: 0x04000403 RID: 1027
		private RingBuffer<GamePadState>[] gamePadState = new RingBuffer<GamePadState>[4];

		// Token: 0x04000404 RID: 1028
		private Thread thread;

		// Token: 0x04000405 RID: 1029
		private int timeStep;

		// Token: 0x04000406 RID: 1030
		private int bufferSize;
	}
}
