using System;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200005E RID: 94
	public class InControlManager : SingletonMonoBehavior<InControlManager>
	{
		// Token: 0x06000253 RID: 595 RVA: 0x0000D610 File Offset: 0x0000B810
		private void OnEnable()
		{
			if (!base.SetupSingleton())
			{
				return;
			}
			InputManager.InvertYAxis = this.invertYAxis;
			InputManager.EnableXInput = this.enableXInput;
			InputManager.XInputUpdateRate = (uint)Mathf.Max(this.xInputUpdateRate, 0);
			InputManager.XInputBufferSize = (uint)Mathf.Max(this.xInputBufferSize, 0);
			InputManager.EnableICade = this.enableICade;
			if (InputManager.SetupInternal())
			{
				if (this.logDebugInfo)
				{
					Debug.Log("InControl (version " + InputManager.Version + ")");
					Logger.OnLogMessage += this.LogMessage;
				}
				foreach (string text in this.customProfiles)
				{
					Type type = Type.GetType(text);
					if (type == null)
					{
						Debug.LogError("Cannot find class for custom profile: " + text);
					}
					else
					{
						InputDeviceProfile inputDeviceProfile = Activator.CreateInstance(type) as InputDeviceProfile;
						if (inputDeviceProfile != null)
						{
							InputManager.AttachDevice(new UnityInputDevice(inputDeviceProfile));
						}
					}
				}
			}
			if (this.dontDestroyOnLoad)
			{
				UnityEngine.Object.DontDestroyOnLoad(this);
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000D754 File Offset: 0x0000B954
		private void OnDisable()
		{
			if (SingletonMonoBehavior<InControlManager>.Instance == this)
			{
				InputManager.ResetInternal();
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000D76C File Offset: 0x0000B96C
		private void Update()
		{
			if (!this.useFixedUpdate || Utility.IsZero(Time.timeScale))
			{
				InputManager.UpdateInternal();
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000D790 File Offset: 0x0000B990
		private void FixedUpdate()
		{
			if (this.useFixedUpdate)
			{
				InputManager.UpdateInternal();
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000D7A4 File Offset: 0x0000B9A4
		private void OnApplicationFocus(bool focusState)
		{
			InputManager.OnApplicationFocus(focusState);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		private void OnApplicationPause(bool pauseState)
		{
			InputManager.OnApplicationPause(pauseState);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000D7B4 File Offset: 0x0000B9B4
		private void OnApplicationQuit()
		{
			InputManager.OnApplicationQuit();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000D7BC File Offset: 0x0000B9BC
		private void OnLevelWasLoaded(int level)
		{
			InputManager.OnLevelWasLoaded();
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		private void LogMessage(LogMessage logMessage)
		{
			switch (logMessage.type)
			{
			case LogMessageType.Info:
				Debug.Log(logMessage.text);
				break;
			case LogMessageType.Warning:
				Debug.LogWarning(logMessage.text);
				break;
			case LogMessageType.Error:
				Debug.LogError(logMessage.text);
				break;
			}
		}

		// Token: 0x040001E1 RID: 481
		public bool logDebugInfo;

		// Token: 0x040001E2 RID: 482
		public bool invertYAxis;

		// Token: 0x040001E3 RID: 483
		public bool useFixedUpdate;

		// Token: 0x040001E4 RID: 484
		public bool dontDestroyOnLoad;

		// Token: 0x040001E5 RID: 485
		public bool enableXInput;

		// Token: 0x040001E6 RID: 486
		public int xInputUpdateRate;

		// Token: 0x040001E7 RID: 487
		public int xInputBufferSize;

		// Token: 0x040001E8 RID: 488
		public bool enableICade;

		// Token: 0x040001E9 RID: 489
		public List<string> customProfiles = new List<string>();
	}
}
