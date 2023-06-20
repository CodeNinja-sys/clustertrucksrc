using System;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
	// Token: 0x020000F1 RID: 241
	public class UnityInputDeviceManager : InputDeviceManager
	{
		// Token: 0x06000518 RID: 1304 RVA: 0x00026578 File Offset: 0x00024778
		public UnityInputDeviceManager()
		{
			this.AddSystemDeviceProfiles();
			this.QueryJoystickInfo();
			this.AttachDevices();
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000265B4 File Offset: 0x000247B4
		public override void Update(ulong updateTick, float deltaTime)
		{
			this.deviceRefreshTimer += deltaTime;
			if (this.deviceRefreshTimer >= 1f)
			{
				this.deviceRefreshTimer = 0f;
				this.QueryJoystickInfo();
				if (this.JoystickInfoHasChanged)
				{
					Logger.LogInfo("Change in attached Unity joysticks detected; refreshing device list.");
					this.DetachDevices();
					this.AttachDevices();
				}
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00026614 File Offset: 0x00024814
		private void QueryJoystickInfo()
		{
			this.joystickNames = Input.GetJoystickNames();
			this.joystickCount = this.joystickNames.Length;
			this.joystickHash = 527 + this.joystickCount;
			for (int i = 0; i < this.joystickCount; i++)
			{
				this.joystickHash = this.joystickHash * 31 + this.joystickNames[i].GetHashCode();
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00026680 File Offset: 0x00024880
		private bool JoystickInfoHasChanged
		{
			get
			{
				return this.joystickHash != this.lastJoystickHash || this.joystickCount != this.lastJoystickCount;
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000266A8 File Offset: 0x000248A8
		private void AttachDevices()
		{
			this.AttachKeyboardDevices();
			this.AttachJoystickDevices();
			this.lastJoystickCount = this.joystickCount;
			this.lastJoystickHash = this.joystickHash;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000266DC File Offset: 0x000248DC
		private void DetachDevices()
		{
			int count = this.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.DetachDevice(this.devices[i]);
			}
			this.devices.Clear();
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00026724 File Offset: 0x00024924
		public void ReloadDevices()
		{
			this.QueryJoystickInfo();
			this.DetachDevices();
			this.AttachDevices();
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00026738 File Offset: 0x00024938
		private void AttachDevice(UnityInputDevice device)
		{
			this.devices.Add(device);
			InputManager.AttachDevice(device);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0002674C File Offset: 0x0002494C
		private void AttachKeyboardDevices()
		{
			int count = this.systemDeviceProfiles.Count;
			for (int i = 0; i < count; i++)
			{
				InputDeviceProfile inputDeviceProfile = this.systemDeviceProfiles[i];
				if (inputDeviceProfile.IsNotJoystick && inputDeviceProfile.IsSupportedOnThisPlatform)
				{
					this.AttachDevice(new UnityInputDevice(inputDeviceProfile));
				}
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x000267A8 File Offset: 0x000249A8
		private void AttachJoystickDevices()
		{
			try
			{
				for (int i = 0; i < this.joystickCount; i++)
				{
					this.DetectJoystickDevice(i + 1, this.joystickNames[i]);
				}
			}
			catch (Exception ex)
			{
				Logger.LogError(ex.Message);
				Logger.LogError(ex.StackTrace);
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0002681C File Offset: 0x00024A1C
		private bool HasAttachedDeviceWithJoystickId(int unityJoystickId)
		{
			int count = this.devices.Count;
			for (int i = 0; i < count; i++)
			{
				UnityInputDevice unityInputDevice = this.devices[i] as UnityInputDevice;
				if (unityInputDevice != null && unityInputDevice.JoystickId == unityJoystickId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00026870 File Offset: 0x00024A70
		private void DetectJoystickDevice(int unityJoystickId, string unityJoystickName)
		{
			if (this.HasAttachedDeviceWithJoystickId(unityJoystickId))
			{
				return;
			}
			if (unityJoystickName.IndexOf("webcam", StringComparison.OrdinalIgnoreCase) != -1)
			{
				return;
			}
			if (InputManager.UnityVersion < new VersionInfo(4, 5, 0, 0) && (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXWebPlayer) && unityJoystickName == "Unknown Wireless Controller")
			{
				return;
			}
			if (InputManager.UnityVersion >= new VersionInfo(4, 6, 3, 0) && (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer) && string.IsNullOrEmpty(unityJoystickName))
			{
				return;
			}
			InputDeviceProfile inputDeviceProfile = null;
			if (inputDeviceProfile == null)
			{
				inputDeviceProfile = this.customDeviceProfiles.Find((InputDeviceProfile config) => config.HasJoystickName(unityJoystickName));
			}
			if (inputDeviceProfile == null)
			{
				inputDeviceProfile = this.systemDeviceProfiles.Find((InputDeviceProfile config) => config.HasJoystickName(unityJoystickName));
			}
			if (inputDeviceProfile == null)
			{
				inputDeviceProfile = this.customDeviceProfiles.Find((InputDeviceProfile config) => config.HasLastResortRegex(unityJoystickName));
			}
			if (inputDeviceProfile == null)
			{
				inputDeviceProfile = this.systemDeviceProfiles.Find((InputDeviceProfile config) => config.HasLastResortRegex(unityJoystickName));
			}
			if (inputDeviceProfile == null)
			{
				Logger.LogWarning(string.Concat(new object[]
				{
					"Device ",
					unityJoystickId,
					" with name \"",
					unityJoystickName,
					"\" does not match any supported profiles and will be considered an unknown controller."
				}));
				UnknownUnityDeviceProfile profile = new UnknownUnityDeviceProfile(unityJoystickName);
				UnknownUnityInputDevice device = new UnknownUnityInputDevice(profile, unityJoystickId);
				this.AttachDevice(device);
				return;
			}
			if (!inputDeviceProfile.IsHidden)
			{
				UnityInputDevice device2 = new UnityInputDevice(inputDeviceProfile, unityJoystickId);
				this.AttachDevice(device2);
				Logger.LogInfo(string.Concat(new object[]
				{
					"Device ",
					unityJoystickId,
					" matched profile ",
					inputDeviceProfile.GetType().Name,
					" (",
					inputDeviceProfile.Name,
					")"
				}));
			}
			else
			{
				Logger.LogInfo(string.Concat(new object[]
				{
					"Device ",
					unityJoystickId,
					" matching profile ",
					inputDeviceProfile.GetType().Name,
					" (",
					inputDeviceProfile.Name,
					") is hidden and will not be attached."
				}));
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00026AE8 File Offset: 0x00024CE8
		private void AddSystemDeviceProfile(UnityInputDeviceProfile deviceProfile)
		{
			if (deviceProfile.IsSupportedOnThisPlatform)
			{
				this.systemDeviceProfiles.Add(deviceProfile);
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00026B04 File Offset: 0x00024D04
		private void AddSystemDeviceProfiles()
		{
			foreach (string typeName in UnityInputDeviceProfileList.Profiles)
			{
				UnityInputDeviceProfile deviceProfile = (UnityInputDeviceProfile)Activator.CreateInstance(Type.GetType(typeName));
				this.AddSystemDeviceProfile(deviceProfile);
			}
		}

		// Token: 0x040003B5 RID: 949
		private const float deviceRefreshInterval = 1f;

		// Token: 0x040003B6 RID: 950
		private float deviceRefreshTimer;

		// Token: 0x040003B7 RID: 951
		private List<InputDeviceProfile> systemDeviceProfiles = new List<InputDeviceProfile>();

		// Token: 0x040003B8 RID: 952
		private List<InputDeviceProfile> customDeviceProfiles = new List<InputDeviceProfile>();

		// Token: 0x040003B9 RID: 953
		private string[] joystickNames;

		// Token: 0x040003BA RID: 954
		private int lastJoystickCount;

		// Token: 0x040003BB RID: 955
		private int lastJoystickHash;

		// Token: 0x040003BC RID: 956
		private int joystickCount;

		// Token: 0x040003BD RID: 957
		private int joystickHash;
	}
}
