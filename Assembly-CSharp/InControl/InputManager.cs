using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000072 RID: 114
	public class InputManager
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600034F RID: 847 RVA: 0x0000FF00 File Offset: 0x0000E100
		// (remove) Token: 0x06000350 RID: 848 RVA: 0x0000FF18 File Offset: 0x0000E118
		public static event Action OnSetup;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000351 RID: 849 RVA: 0x0000FF30 File Offset: 0x0000E130
		// (remove) Token: 0x06000352 RID: 850 RVA: 0x0000FF48 File Offset: 0x0000E148
		public static event Action<ulong, float> OnUpdate;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000353 RID: 851 RVA: 0x0000FF60 File Offset: 0x0000E160
		// (remove) Token: 0x06000354 RID: 852 RVA: 0x0000FF78 File Offset: 0x0000E178
		public static event Action OnReset;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000355 RID: 853 RVA: 0x0000FF90 File Offset: 0x0000E190
		// (remove) Token: 0x06000356 RID: 854 RVA: 0x0000FFA8 File Offset: 0x0000E1A8
		public static event Action<InputDevice> OnDeviceAttached;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000357 RID: 855 RVA: 0x0000FFC0 File Offset: 0x0000E1C0
		// (remove) Token: 0x06000358 RID: 856 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
		public static event Action<InputDevice> OnDeviceDetached;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000359 RID: 857 RVA: 0x0000FFF0 File Offset: 0x0000E1F0
		// (remove) Token: 0x0600035A RID: 858 RVA: 0x00010008 File Offset: 0x0000E208
		public static event Action<InputDevice> OnActiveDeviceChanged;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600035B RID: 859 RVA: 0x00010020 File Offset: 0x0000E220
		// (remove) Token: 0x0600035C RID: 860 RVA: 0x00010038 File Offset: 0x0000E238
		internal static event Action<ulong, float> OnUpdateDevices;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600035D RID: 861 RVA: 0x00010050 File Offset: 0x0000E250
		// (remove) Token: 0x0600035E RID: 862 RVA: 0x00010068 File Offset: 0x0000E268
		internal static event Action<ulong, float> OnCommitDevices;

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00010080 File Offset: 0x0000E280
		// (set) Token: 0x06000360 RID: 864 RVA: 0x00010088 File Offset: 0x0000E288
		public static bool MenuWasPressed { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00010090 File Offset: 0x0000E290
		// (set) Token: 0x06000362 RID: 866 RVA: 0x00010098 File Offset: 0x0000E298
		public static bool InvertYAxis { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000363 RID: 867 RVA: 0x000100A0 File Offset: 0x0000E2A0
		// (set) Token: 0x06000364 RID: 868 RVA: 0x000100A8 File Offset: 0x0000E2A8
		public static bool IsSetup { get; private set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000100B0 File Offset: 0x0000E2B0
		// (set) Token: 0x06000366 RID: 870 RVA: 0x000100B8 File Offset: 0x0000E2B8
		internal static string Platform { get; private set; }

		// Token: 0x06000367 RID: 871 RVA: 0x000100C0 File Offset: 0x0000E2C0
		[Obsolete("Calling InputManager.Setup() directly is no longer supported. Use the InControlManager component to manage the lifecycle of the input manager instead.", true)]
		public static void Setup()
		{
			InputManager.SetupInternal();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000100C8 File Offset: 0x0000E2C8
		internal static bool SetupInternal()
		{
			if (InputManager.IsSetup)
			{
				return false;
			}
			InputManager.Platform = (Utility.GetWindowsVersion() + " " + SystemInfo.deviceModel).ToUpper();
			InputManager.initialTime = 0f;
			InputManager.currentTime = 0f;
			InputManager.lastUpdateTime = 0f;
			InputManager.currentTick = 0UL;
			InputManager.deviceManagers.Clear();
			InputManager.deviceManagerTable.Clear();
			InputManager.devices.Clear();
			InputManager.Devices = new ReadOnlyCollection<InputDevice>(InputManager.devices);
			InputManager.activeDevice = InputDevice.Null;
			InputManager.playerActionSets.Clear();
			InputManager.IsSetup = true;
			if (InputManager.EnableXInput)
			{
				XInputDeviceManager.Enable();
			}
			if (InputManager.OnSetup != null)
			{
				InputManager.OnSetup();
				InputManager.OnSetup = null;
			}
			bool flag = true;
			if (flag)
			{
				InputManager.AddDeviceManager<UnityInputDeviceManager>();
			}
			return true;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000101A4 File Offset: 0x0000E3A4
		[Obsolete("Calling InputManager.Reset() method directly is no longer supported. Use the InControlManager component to manage the lifecycle of the input manager instead.", true)]
		public static void Reset()
		{
			InputManager.ResetInternal();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000101AC File Offset: 0x0000E3AC
		internal static void ResetInternal()
		{
			if (InputManager.OnReset != null)
			{
				InputManager.OnReset();
			}
			InputManager.OnSetup = null;
			InputManager.OnUpdate = null;
			InputManager.OnReset = null;
			InputManager.OnActiveDeviceChanged = null;
			InputManager.OnDeviceAttached = null;
			InputManager.OnDeviceDetached = null;
			InputManager.OnUpdateDevices = null;
			InputManager.OnCommitDevices = null;
			InputManager.DestroyDeviceManagers();
			InputManager.DestroyDevices();
			InputManager.playerActionSets.Clear();
			InputManager.IsSetup = false;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00010218 File Offset: 0x0000E418
		[Obsolete("Calling InputManager.Update() directly is no longer supported. Use the InControlManager component to manage the lifecycle of the input manager instead.", true)]
		public static void Update()
		{
			InputManager.UpdateInternal();
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00010220 File Offset: 0x0000E420
		internal static void UpdateInternal()
		{
			InputManager.AssertIsSetup();
			if (InputManager.OnSetup != null)
			{
				InputManager.OnSetup();
				InputManager.OnSetup = null;
			}
			InputManager.currentTick += 1UL;
			InputManager.UpdateCurrentTime();
			float num = InputManager.currentTime - InputManager.lastUpdateTime;
			InputManager.UpdateDeviceManagers(num);
			InputManager.MenuWasPressed = false;
			InputManager.UpdateDevices(num);
			InputManager.CommitDevices(num);
			InputManager.UpdatePlayerActionSets(num);
			InputManager.UpdateActiveDevice();
			if (InputManager.OnUpdate != null)
			{
				InputManager.OnUpdate(InputManager.currentTick, num);
			}
			InputManager.lastUpdateTime = InputManager.currentTime;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000102B4 File Offset: 0x0000E4B4
		public static void Reload()
		{
			InputManager.ResetInternal();
			InputManager.SetupInternal();
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000102C4 File Offset: 0x0000E4C4
		private static void AssertIsSetup()
		{
			if (!InputManager.IsSetup)
			{
				throw new Exception("InputManager is not initialized. Call InputManager.Setup() first.");
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000102DC File Offset: 0x0000E4DC
		private static void SetZeroTickOnAllControls()
		{
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputControl[] controls = InputManager.devices[i].Controls;
				int num = controls.Length;
				for (int j = 0; j < num; j++)
				{
					InputControl inputControl = controls[j];
					if (inputControl != null)
					{
						inputControl.SetZeroTick();
					}
				}
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00010348 File Offset: 0x0000E548
		public static void ClearInputState()
		{
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.devices[i].ClearInputState();
			}
			int count2 = InputManager.playerActionSets.Count;
			for (int j = 0; j < count2; j++)
			{
				InputManager.playerActionSets[j].ClearInputState();
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000103B0 File Offset: 0x0000E5B0
		internal static void OnApplicationFocus(bool focusState)
		{
			if (!focusState)
			{
				InputManager.SetZeroTickOnAllControls();
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000103C0 File Offset: 0x0000E5C0
		internal static void OnApplicationPause(bool pauseState)
		{
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000103C4 File Offset: 0x0000E5C4
		internal static void OnApplicationQuit()
		{
			InputManager.ResetInternal();
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000103CC File Offset: 0x0000E5CC
		internal static void OnLevelWasLoaded()
		{
			InputManager.SetZeroTickOnAllControls();
			InputManager.UpdateInternal();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000103D8 File Offset: 0x0000E5D8
		public static void AddDeviceManager(InputDeviceManager deviceManager)
		{
			InputManager.AssertIsSetup();
			Type type = deviceManager.GetType();
			if (InputManager.deviceManagerTable.ContainsKey(type))
			{
				Logger.LogError("A device manager of type '" + type.Name + "' already exists; cannot add another.");
				return;
			}
			InputManager.deviceManagers.Add(deviceManager);
			InputManager.deviceManagerTable.Add(type, deviceManager);
			deviceManager.Update(InputManager.currentTick, InputManager.currentTime - InputManager.lastUpdateTime);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001044C File Offset: 0x0000E64C
		public static void AddDeviceManager<T>() where T : InputDeviceManager, new()
		{
			InputManager.AddDeviceManager(Activator.CreateInstance<T>());
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00010460 File Offset: 0x0000E660
		public static T GetDeviceManager<T>() where T : InputDeviceManager
		{
			InputDeviceManager inputDeviceManager;
			if (InputManager.deviceManagerTable.TryGetValue(typeof(!!0), out inputDeviceManager))
			{
				return inputDeviceManager as !!0;
			}
			return (!!0)((object)null);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001049C File Offset: 0x0000E69C
		public static bool HasDeviceManager<T>() where T : InputDeviceManager
		{
			return InputManager.deviceManagerTable.ContainsKey(typeof(!!0));
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000104B4 File Offset: 0x0000E6B4
		private static void UpdateCurrentTime()
		{
			if (InputManager.initialTime < 1E-45f)
			{
				InputManager.initialTime = Time.realtimeSinceStartup;
			}
			InputManager.currentTime = Mathf.Max(0f, Time.realtimeSinceStartup - InputManager.initialTime);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000104EC File Offset: 0x0000E6EC
		private static void UpdateDeviceManagers(float deltaTime)
		{
			int count = InputManager.deviceManagers.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.deviceManagers[i].Update(InputManager.currentTick, deltaTime);
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001052C File Offset: 0x0000E72C
		private static void DestroyDeviceManagers()
		{
			int count = InputManager.deviceManagers.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.deviceManagers[i].Destroy();
			}
			InputManager.deviceManagers.Clear();
			InputManager.deviceManagerTable.Clear();
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001057C File Offset: 0x0000E77C
		private static void DestroyDevices()
		{
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputDevice inputDevice = InputManager.devices[i];
				inputDevice.StopVibration();
				inputDevice.IsAttached = false;
			}
			InputManager.devices.Clear();
			InputManager.activeDevice = InputDevice.Null;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000105D4 File Offset: 0x0000E7D4
		private static void UpdateDevices(float deltaTime)
		{
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputDevice inputDevice = InputManager.devices[i];
				inputDevice.Update(InputManager.currentTick, deltaTime);
			}
			if (InputManager.OnUpdateDevices != null)
			{
				InputManager.OnUpdateDevices(InputManager.currentTick, deltaTime);
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00010630 File Offset: 0x0000E830
		private static void CommitDevices(float deltaTime)
		{
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputDevice inputDevice = InputManager.devices[i];
				inputDevice.Commit(InputManager.currentTick, deltaTime);
				if (inputDevice.MenuWasPressed)
				{
					InputManager.MenuWasPressed = true;
				}
			}
			if (InputManager.OnCommitDevices != null)
			{
				InputManager.OnCommitDevices(InputManager.currentTick, deltaTime);
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000106A0 File Offset: 0x0000E8A0
		private static void UpdateActiveDevice()
		{
			InputDevice inputDevice = InputManager.ActiveDevice;
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputDevice inputDevice2 = InputManager.devices[i];
				if (inputDevice2.LastChangedAfter(InputManager.ActiveDevice))
				{
					InputManager.ActiveDevice = inputDevice2;
				}
			}
			if (inputDevice != InputManager.ActiveDevice && InputManager.OnActiveDeviceChanged != null)
			{
				InputManager.OnActiveDeviceChanged(InputManager.ActiveDevice);
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00010718 File Offset: 0x0000E918
		public static void AttachDevice(InputDevice inputDevice)
		{
			InputManager.AssertIsSetup();
			if (!inputDevice.IsSupportedOnThisPlatform)
			{
				return;
			}
			if (InputManager.devices.Contains(inputDevice))
			{
				inputDevice.IsAttached = true;
				return;
			}
			InputManager.devices.Add(inputDevice);
			InputManager.devices.Sort((InputDevice d1, InputDevice d2) => d1.SortOrder.CompareTo(d2.SortOrder));
			inputDevice.IsAttached = true;
			if (InputManager.OnDeviceAttached != null)
			{
				InputManager.OnDeviceAttached(inputDevice);
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001079C File Offset: 0x0000E99C
		public static void DetachDevice(InputDevice inputDevice)
		{
			if (!inputDevice.IsAttached)
			{
				return;
			}
			if (!InputManager.IsSetup)
			{
				inputDevice.IsAttached = false;
				return;
			}
			if (!InputManager.devices.Contains(inputDevice))
			{
				inputDevice.IsAttached = false;
				return;
			}
			InputManager.devices.Remove(inputDevice);
			inputDevice.IsAttached = false;
			if (InputManager.ActiveDevice == inputDevice)
			{
				InputManager.ActiveDevice = InputDevice.Null;
			}
			if (InputManager.OnDeviceDetached != null)
			{
				InputManager.OnDeviceDetached(inputDevice);
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001081C File Offset: 0x0000EA1C
		public static void HideDevicesWithProfile(Type type)
		{
			if (type.IsSubclassOf(typeof(UnityInputDeviceProfile)))
			{
				InputDeviceProfile.Hide(type);
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001083C File Offset: 0x0000EA3C
		internal static void AttachPlayerActionSet(PlayerActionSet playerActionSet)
		{
			if (InputManager.playerActionSets.Contains(playerActionSet))
			{
				Logger.LogWarning("Player action set is already attached.");
			}
			else
			{
				InputManager.playerActionSets.Add(playerActionSet);
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00010874 File Offset: 0x0000EA74
		internal static void DetachPlayerActionSet(PlayerActionSet playerActionSet)
		{
			InputManager.playerActionSets.Remove(playerActionSet);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00010884 File Offset: 0x0000EA84
		internal static void UpdatePlayerActionSets(float deltaTime)
		{
			int count = InputManager.playerActionSets.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.playerActionSets[i].Update(InputManager.currentTick, deltaTime);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000386 RID: 902 RVA: 0x000108C4 File Offset: 0x0000EAC4
		public static bool AnyKeyIsPressed
		{
			get
			{
				return KeyCombo.Detect(true).Count > 0;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000387 RID: 903 RVA: 0x000108E4 File Offset: 0x0000EAE4
		// (set) Token: 0x06000388 RID: 904 RVA: 0x00010900 File Offset: 0x0000EB00
		public static InputDevice ActiveDevice
		{
			get
			{
				return (InputManager.activeDevice != null) ? InputManager.activeDevice : InputDevice.Null;
			}
			private set
			{
				InputManager.activeDevice = ((value != null) ? value : InputDevice.Null);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00010918 File Offset: 0x0000EB18
		// (set) Token: 0x0600038A RID: 906 RVA: 0x00010920 File Offset: 0x0000EB20
		public static bool EnableXInput { get; internal set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00010928 File Offset: 0x0000EB28
		// (set) Token: 0x0600038C RID: 908 RVA: 0x00010930 File Offset: 0x0000EB30
		public static uint XInputUpdateRate { get; internal set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00010938 File Offset: 0x0000EB38
		// (set) Token: 0x0600038E RID: 910 RVA: 0x00010940 File Offset: 0x0000EB40
		public static uint XInputBufferSize { get; internal set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00010948 File Offset: 0x0000EB48
		// (set) Token: 0x06000390 RID: 912 RVA: 0x00010950 File Offset: 0x0000EB50
		public static bool EnableICade { get; internal set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00010958 File Offset: 0x0000EB58
		internal static VersionInfo UnityVersion
		{
			get
			{
				if (InputManager.unityVersion == null)
				{
					InputManager.unityVersion = new VersionInfo?(VersionInfo.UnityVersion());
				}
				return InputManager.unityVersion.Value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00010990 File Offset: 0x0000EB90
		internal static ulong CurrentTick
		{
			get
			{
				return InputManager.currentTick;
			}
		}

		// Token: 0x040002BF RID: 703
		public static readonly VersionInfo Version = VersionInfo.InControlVersion();

		// Token: 0x040002C0 RID: 704
		private static List<InputDeviceManager> deviceManagers = new List<InputDeviceManager>();

		// Token: 0x040002C1 RID: 705
		private static Dictionary<Type, InputDeviceManager> deviceManagerTable = new Dictionary<Type, InputDeviceManager>();

		// Token: 0x040002C2 RID: 706
		private static InputDevice activeDevice = InputDevice.Null;

		// Token: 0x040002C3 RID: 707
		private static List<InputDevice> devices = new List<InputDevice>();

		// Token: 0x040002C4 RID: 708
		private static List<PlayerActionSet> playerActionSets = new List<PlayerActionSet>();

		// Token: 0x040002C5 RID: 709
		public static ReadOnlyCollection<InputDevice> Devices;

		// Token: 0x040002C6 RID: 710
		private static float initialTime;

		// Token: 0x040002C7 RID: 711
		private static float currentTime;

		// Token: 0x040002C8 RID: 712
		private static float lastUpdateTime;

		// Token: 0x040002C9 RID: 713
		private static ulong currentTick;

		// Token: 0x040002CA RID: 714
		private static VersionInfo? unityVersion;
	}
}
