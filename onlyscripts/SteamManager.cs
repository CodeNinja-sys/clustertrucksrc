using System;
using System.Text;
using Steamworks;
using UnityEngine;

// Token: 0x02000258 RID: 600
[DisallowMultipleComponent]
internal class SteamManager : MonoBehaviour
{
	// Token: 0x1700029A RID: 666
	// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0005E620 File Offset: 0x0005C820
	private static SteamManager Instance
	{
		get
		{
			return SteamManager.s_instance ?? new GameObject("SteamManager").AddComponent<SteamManager>();
		}
	}

	// Token: 0x1700029B RID: 667
	// (get) Token: 0x06000E9A RID: 3738 RVA: 0x0005E640 File Offset: 0x0005C840
	public static bool Initialized
	{
		get
		{
			return SteamManager.Instance.m_bInitialized;
		}
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x0005E64C File Offset: 0x0005C84C
	private static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
		Debug.LogWarning(pchDebugText);
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x0005E654 File Offset: 0x0005C854
	private void Awake()
	{
		if (SteamManager.s_instance != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		SteamManager.s_instance = this;
		if (SteamManager.s_EverInialized)
		{
			throw new Exception("Tried to Initialize the SteamAPI twice in one session!");
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		if (!Packsize.Test())
		{
			Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", this);
		}
		if (!DllCheck.Test())
		{
			Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", this);
		}
		try
		{
			if (SteamAPI.RestartAppIfNecessary(AppId_t.Invalid))
			{
				Application.Quit();
				return;
			}
		}
		catch (DllNotFoundException arg)
		{
			Debug.LogError("[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n" + arg, this);
			Application.Quit();
			return;
		}
		this.m_bInitialized = SteamAPI.Init();
		if (!this.m_bInitialized)
		{
			Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);
			return;
		}
		SteamManager.s_EverInialized = true;
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x0005E750 File Offset: 0x0005C950
	private void OnEnable()
	{
		if (SteamManager.s_instance == null)
		{
			SteamManager.s_instance = this;
		}
		if (!this.m_bInitialized)
		{
			return;
		}
		if (this.m_SteamAPIWarningMessageHook == null)
		{
			this.m_SteamAPIWarningMessageHook = new SteamAPIWarningMessageHook_t(SteamManager.SteamAPIDebugTextHook);
			SteamClient.SetWarningMessageHook(this.m_SteamAPIWarningMessageHook);
		}
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x0005E7A8 File Offset: 0x0005C9A8
	private void OnDestroy()
	{
		if (SteamManager.s_instance != this)
		{
			return;
		}
		SteamManager.s_instance = null;
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.Shutdown();
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x0005E7E0 File Offset: 0x0005C9E0
	private void Update()
	{
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.RunCallbacks();
	}

	// Token: 0x04000B2A RID: 2858
	private static SteamManager s_instance;

	// Token: 0x04000B2B RID: 2859
	private static bool s_EverInialized;

	// Token: 0x04000B2C RID: 2860
	private bool m_bInitialized;

	// Token: 0x04000B2D RID: 2861
	private SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;
}
