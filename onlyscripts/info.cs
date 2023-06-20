using System;
using UnityEngine;

// Token: 0x02000286 RID: 646
public class info : MonoBehaviour
{
	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x06000F84 RID: 3972 RVA: 0x000656A4 File Offset: 0x000638A4
	// (set) Token: 0x06000F85 RID: 3973 RVA: 0x000656AC File Offset: 0x000638AC
	public static GhostRaceInfo P_GhostRaceInfo
	{
		get
		{
			return info.mGhostRaceInfo;
		}
		set
		{
			info.mGhostRaceInfo = value;
		}
	}

	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x06000F86 RID: 3974 RVA: 0x000656B4 File Offset: 0x000638B4
	public static string ABILITY_MOVEMENT_KEY
	{
		get
		{
			return "AbilityMove";
		}
	}

	// Token: 0x170002A4 RID: 676
	// (get) Token: 0x06000F87 RID: 3975 RVA: 0x000656BC File Offset: 0x000638BC
	public static string ABILITY_UTILITY_KEY
	{
		get
		{
			return "AbilityUtility";
		}
	}

	// Token: 0x170002A5 RID: 677
	// (get) Token: 0x06000F88 RID: 3976 RVA: 0x000656C4 File Offset: 0x000638C4
	public static string CurrentUtilityAbility
	{
		get
		{
			return PlayerPrefs.GetString(info.ABILITY_UTILITY_KEY, null);
		}
	}

	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x06000F89 RID: 3977 RVA: 0x000656D4 File Offset: 0x000638D4
	public static string CurrentMovementAbility
	{
		get
		{
			return PlayerPrefs.GetString(info.ABILITY_MOVEMENT_KEY, null);
		}
	}

	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x06000F8A RID: 3978 RVA: 0x000656E4 File Offset: 0x000638E4
	public static int PauseFrames
	{
		get
		{
			return info._pauseFrames;
		}
	}

	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x06000F8B RID: 3979 RVA: 0x000656EC File Offset: 0x000638EC
	// (set) Token: 0x06000F8C RID: 3980 RVA: 0x000656F4 File Offset: 0x000638F4
	public static bool paused
	{
		get
		{
			return info._paused;
		}
		set
		{
			info._paused = value;
			if (!info._paused)
			{
				info._pauseFrames = 10;
			}
		}
	}

	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x06000F8D RID: 3981 RVA: 0x00065710 File Offset: 0x00063910
	public static int currentProgressWorld
	{
		get
		{
			return (int)Mathf.Floor((float)(info.completedLevels / 10)) + 1;
		}
	}

	// Token: 0x170002AA RID: 682
	// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00065724 File Offset: 0x00063924
	// (set) Token: 0x06000F8F RID: 3983 RVA: 0x0006572C File Offset: 0x0006392C
	public static int currentLevel
	{
		get
		{
			return info._currentLevel;
		}
		set
		{
			PlayerPrefs.SetInt("currentLevel", value);
			Debug.Log("CURRENTLEVEL CHANGHED: " + value);
			info._currentLevel = value;
		}
	}

	// Token: 0x170002AB RID: 683
	// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00065760 File Offset: 0x00063960
	public static int currentlyPlayedWorld
	{
		get
		{
			return (int)Mathf.Floor((float)(((!Manager.usingMapcycle) ? (info.currentLevel - 1) : (Manager.MapcycleArray[info.currentLevel - 1] - 1)) / 10)) + 1;
		}
	}

	// Token: 0x170002AC RID: 684
	// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00065794 File Offset: 0x00063994
	// (set) Token: 0x06000F92 RID: 3986 RVA: 0x0006579C File Offset: 0x0006399C
	public static bool PlayingGhostFromLeaderBoard
	{
		get
		{
			return info._playingGhostFromLeaderboard;
		}
		set
		{
			info._playingGhostFromLeaderboard = value;
			Debug.Log("Playing Ghost From Leaderboard: " + info._playingGhostFromLeaderboard);
		}
	}

	// Token: 0x170002AD RID: 685
	// (get) Token: 0x06000F93 RID: 3987 RVA: 0x000657C0 File Offset: 0x000639C0
	// (set) Token: 0x06000F94 RID: 3988 RVA: 0x000657C8 File Offset: 0x000639C8
	public static bool ShowClock
	{
		get
		{
			return info._showClock;
		}
		set
		{
			info._showClock = value;
		}
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x000657D0 File Offset: 0x000639D0
	private void Awake()
	{
		Application.targetFrameRate = 90;
	}

	// Token: 0x06000F96 RID: 3990 RVA: 0x000657DC File Offset: 0x000639DC
	private void Update()
	{
		Time.timeScale = info.playerControlledTime * info.gameTime * info.superTime;
		Time.fixedDeltaTime = Mathf.Clamp(Time.timeScale * 0.0166f, 0.001f, 0.2f);
		if (Input.GetKey(KeyCode.Z) && Input.GetKeyDown(KeyCode.P))
		{
			PlayerPrefs.DeleteAll();
		}
		if (Input.GetKey(KeyCode.Z) && Input.GetKeyDown(KeyCode.C) && Application.isEditor)
		{
			pointsHandler.AddPoints(10000f);
		}
		if (!info.playing)
		{
			info.playerControlledTime = 1f;
			info.superTime = 1f;
		}
		if (info.PauseFrames > 0)
		{
			info._pauseFrames--;
		}
	}

	// Token: 0x06000F97 RID: 3991 RVA: 0x0006589C File Offset: 0x00063A9C
	public static void ResetTime()
	{
		info.playerControlledTime = 1f;
		info.gameTime = 1f;
		info.superTime = 1f;
	}

	// Token: 0x04000C63 RID: 3171
	private static GhostRaceInfo mGhostRaceInfo;

	// Token: 0x04000C64 RID: 3172
	public static bool darkWorld;

	// Token: 0x04000C65 RID: 3173
	public static float extraAirTime;

	// Token: 0x04000C66 RID: 3174
	public static float truckWidth = 1f;

	// Token: 0x04000C67 RID: 3175
	public static float drag = 0.995f;

	// Token: 0x04000C68 RID: 3176
	public static float speedMultiplier = 0.7f;

	// Token: 0x04000C69 RID: 3177
	public static float scoreMultiplier = 1f;

	// Token: 0x04000C6A RID: 3178
	public static string abilityName = string.Empty;

	// Token: 0x04000C6B RID: 3179
	public static string utilityName = string.Empty;

	// Token: 0x04000C6C RID: 3180
	public static float levelLenght;

	// Token: 0x04000C6D RID: 3181
	public static int lastPlayedLevel = 1;

	// Token: 0x04000C6E RID: 3182
	public static bool onLastLevel;

	// Token: 0x04000C6F RID: 3183
	public static bool playing;

	// Token: 0x04000C70 RID: 3184
	private static int _pauseFrames;

	// Token: 0x04000C71 RID: 3185
	private static bool _paused;

	// Token: 0x04000C72 RID: 3186
	public static int completedLevels;

	// Token: 0x04000C73 RID: 3187
	private static int _currentLevel = 1;

	// Token: 0x04000C74 RID: 3188
	public static int currentWorld;

	// Token: 0x04000C75 RID: 3189
	private static bool _playingGhostFromLeaderboard;

	// Token: 0x04000C76 RID: 3190
	private static bool _showClock;

	// Token: 0x04000C77 RID: 3191
	public static bool SkippingCurrentLevel;

	// Token: 0x04000C78 RID: 3192
	public static float playerControlledTime = 1f;

	// Token: 0x04000C79 RID: 3193
	public static float superTime = 1f;

	// Token: 0x04000C7A RID: 3194
	public static float gameTime = 1f;

	// Token: 0x04000C7B RID: 3195
	public static bool isShowBuild;

	// Token: 0x04000C7C RID: 3196
	public static int lastLevel = 105;

	// Token: 0x04000C7D RID: 3197
	public static float explosive = 1f;
}
