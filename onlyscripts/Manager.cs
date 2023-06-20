using System;
using System.Collections;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000245 RID: 581
public class Manager : MonoBehaviour
{
	// Token: 0x1700028A RID: 650
	// (get) Token: 0x06000E23 RID: 3619 RVA: 0x0005C6E8 File Offset: 0x0005A8E8
	public bool IsMenuActive
	{
		get
		{
			return this.MainCanvas.activeInHierarchy || this.levelSelect.activeInHierarchy;
		}
	}

	// Token: 0x06000E24 RID: 3620 RVA: 0x0005C708 File Offset: 0x0005A908
	public static Manager Instance()
	{
		if (!Manager._Manager)
		{
			Manager._Manager = UnityEngine.Object.FindObjectOfType<Manager>();
			if (!Manager._Manager)
			{
				Debug.LogError("There needs to be one active Manager script on a GameObject in your scene.");
			}
		}
		return Manager._Manager;
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x0005C744 File Offset: 0x0005A944
	private void Awake()
	{
		if (PlayerPrefs.GetInt("BurnThemAll") != 5)
		{
			PlayerPrefs.DeleteAll();
			PlayerPrefs.SetInt("BurnThemAll", 5);
		}
		info.completedLevels = PlayerPrefs.GetInt("completed");
		info.currentLevel = PlayerPrefs.GetInt("currentLevel");
		if (info.currentLevel == 0)
		{
			info.currentLevel = 1;
		}
		info.abilityName = info.CurrentMovementAbility;
		info.utilityName = info.CurrentUtilityAbility;
		Manager.usingMapcycle = this.usingMapcyclePublic;
		Manager.MapcycleArray = this.MapcycleArrayPublic;
		if (PlayerPrefs.GetInt("level") == 0)
		{
			PlayerPrefs.SetInt("level", 1);
		}
		Manager.Controller = this.ControllerSet;
		info.isShowBuild = this.ShowbuildSet;
		if (info.isShowBuild)
		{
			Manager.usingMapcycle = true;
		}
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x0005C80C File Offset: 0x0005AA0C
	private void Start()
	{
		if (PlayerPrefs.GetFloat("playThroughTime") > 0f)
		{
			this.currentPlayThroughTime = PlayerPrefs.GetFloat("playThroughTime");
		}
		if (PlayerPrefs.GetFloat("bestTime") > 0f)
		{
			this.playThroughText.text = string.Concat(new string[]
			{
				"BEST TIME: ",
				(PlayerPrefs.GetFloat("bestTime") / 60f).ToString("F0"),
				"M ",
				Mathf.Floor(PlayerPrefs.GetFloat("bestTime") % 60f).ToString(),
				"S"
			});
		}
		if (info.isShowBuild)
		{
			info.currentLevel = Manager.MapcycleArray[0];
			this.myRestart = false;
			this.NewGame();
		}
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x0005C8E4 File Offset: 0x0005AAE4
	private void Update()
	{
		if (info.playing)
		{
			this.currentPlayThroughTime += Time.unscaledDeltaTime;
		}
		if (Input.GetKeyDown(KeyCode.K) && Application.isEditor)
		{
			info.completedLevels = 89;
		}
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x0005C930 File Offset: 0x0005AB30
	public void NewGame()
	{
		helpText.reset();
		info.currentLevel = 1;
		PlayerPrefs.SetInt("currentLvl", info.currentLevel);
		this.Play();
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x0005C960 File Offset: 0x0005AB60
	public void Play()
	{
		this.CloseMenu();
		this.ClearLevel();
		int i = Manager.usingMapcycle ? Manager.MapcycleArray[info.currentLevel - 1] : info.currentLevel;
		LeaderboardGhostHandler.GhostMethod currentGhostMethod = LeaderboardGhostHandler.CurrentGhostMethod;
		this.StartLevel(i, true, currentGhostMethod);
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x0005C9AC File Offset: 0x0005ABAC
	public void RestartLevel()
	{
		Singleton<DataRecorder>.Instance.StopRecording();
		Singleton<RecordedDataPrefabPlayer>.Instance.StopPlayback();
		LeaderboardGhostHandler.GhostMethod currentGhostMethod = LeaderboardGhostHandler.CurrentGhostMethod;
		this.ClearLevel();
		int i = Manager.usingMapcycle ? Manager.MapcycleArray[info.currentLevel - 1] : info.currentLevel;
		this.StartLevel(i, false, currentGhostMethod);
	}

	// Token: 0x06000E2B RID: 3627 RVA: 0x0005CA04 File Offset: 0x0005AC04
	private void restartGame()
	{
	}

	// Token: 0x06000E2C RID: 3628 RVA: 0x0005CA08 File Offset: 0x0005AC08
	private void ShowThanksScreen()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		this.OpenMenuFromGame();
		this.endGameText.text = (this.currentPlayThroughTime / 60f).ToString("F0") + "M " + Mathf.Floor(this.currentPlayThroughTime % 60f).ToString() + "S";
		this.myMenuBar.ChangeMenu(this.thanksScreen);
		base.StartCoroutine(this.clearMap());
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x0005CA94 File Offset: 0x0005AC94
	private IEnumerator clearMap()
	{
		yield return new WaitForSeconds(1f);
		this.ClearLevel();
		info.playing = false;
		info.currentLevel = 1;
		PlayerPrefs.SetInt("level", 1);
		this.playThroughText.text = string.Concat(new string[]
		{
			"BEST TIME: ",
			(PlayerPrefs.GetFloat("bestTime") / 60f).ToString("F0"),
			"M ",
			Mathf.Floor(PlayerPrefs.GetFloat("bestTime") % 60f).ToString(),
			"S"
		});
		yield break;
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x0005CAB0 File Offset: 0x0005ACB0
	public void NextLevel()
	{
		if (!info.PlayingGhostFromLeaderBoard)
		{
			leaderboardsManager.FlushAllGhosts();
		}
		Manager.canSkipCurrentMap = false;
		if (!Manager.usingMapcycle || info.currentLevel <= Manager.MapcycleArray.Length - 1)
		{
			if (info.currentLevel > info.lastLevel)
			{
				this.OpenMenuFromGame();
			}
			else
			{
				this.ClearLevel();
				if (info.completedLevels == info.currentLevel && info.currentLevel % 10 == 0)
				{
					this.levelSelect.GetComponent<LevelSeletHandler>().beatWorld = true;
					this.OpenLevelSelectFromGram();
					return;
				}
				info.currentLevel++;
				int i = Manager.usingMapcycle ? Manager.MapcycleArray[info.currentLevel - 1] : info.currentLevel;
				LeaderboardGhostHandler.GhostMethod currentGhostMethod = LeaderboardGhostHandler.CurrentGhostMethod;
				this.StartLevel(i, true, currentGhostMethod);
			}
			PlayerPrefs.SetInt("currentLvl", info.currentLevel);
			return;
		}
		if (info.isShowBuild)
		{
			this.NewGame();
			return;
		}
		this.ShowThanksScreen();
		this.SetBestTime();
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x0005CBB0 File Offset: 0x0005ADB0
	private void ResetPlayTime()
	{
		PlayerPrefs.SetFloat("bestTime", 0f);
		PlayerPrefs.SetFloat("playThroughTime", 0f);
		this.currentPlayThroughTime = 0f;
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x0005CBDC File Offset: 0x0005ADDC
	private void SavePlayTime()
	{
		PlayerPrefs.SetFloat("playThroughTime", this.currentPlayThroughTime);
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x0005CBF0 File Offset: 0x0005ADF0
	private void SetBestTime()
	{
		if (this.currentPlayThroughTime < PlayerPrefs.GetFloat("bestTime") || PlayerPrefs.GetFloat("bestTime") == 0f)
		{
			PlayerPrefs.SetFloat("bestTime", this.currentPlayThroughTime);
		}
		this.currentPlayThroughTime = 0f;
		PlayerPrefs.SetFloat("playThroughTime", this.currentPlayThroughTime);
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x0005CC54 File Offset: 0x0005AE54
	private IEnumerator SetupGhost(LeaderboardGhostHandler.GhostMethod ghostMethod)
	{
		Debug.Log("Calling GhostSetup: " + Time.frameCount);
		float accaptableWaitTime = 1.5f;
		int lastFrame = Time.frameCount;
		float startTime = Time.time;
		this.mWaitingForGhostLoad = true;
		bool loadMyGhost = ghostMethod == LeaderboardGhostHandler.GhostMethod.Self && !info.PlayingGhostFromLeaderBoard;
		GhostRaceTime ghostToPlay = (!loadMyGhost) ? leaderboardsManager.P_OthersGhost : leaderboardsManager.P_MyGhost;
		while (ghostToPlay == null)
		{
			if (lastFrame != Time.frameCount)
			{
				Debug.Log("Loading Ghost: " + Time.frameCount);
				lastFrame = Time.frameCount;
				ghostToPlay = ((!loadMyGhost) ? leaderboardsManager.P_OthersGhost : leaderboardsManager.P_MyGhost);
				SteamAPI.RunCallbacks();
			}
			if (startTime + accaptableWaitTime < Time.time)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Gave up on loading ghost: ",
					Time.frameCount,
					"StartTime: ",
					startTime,
					" Waittime: ",
					accaptableWaitTime,
					" CurrentTime: ",
					Time.time
				}));
				LeaderboardGhostHandler.SetGhostMethod = 0;
				leaderboardsManager.FlushAllGhosts();
				break;
			}
			yield return new WaitForEndOfFrame();
		}
		this.mWaitingForGhostLoad = false;
		ghostToPlay = ((!loadMyGhost) ? leaderboardsManager.P_OthersGhost : leaderboardsManager.P_MyGhost);
		if (ghostToPlay == null)
		{
			info.P_GhostRaceInfo = null;
			Debug.Log("Both ghosts are wrong... are you connected to the supernet? or do you just like rabbits= and not have ghostsrs to play");
			yield break;
		}
		Debug.Log("found Other Ghost: " + Time.frameCount);
		byte[] ghostFile = ghostToPlay.GhostData;
		HistoryCollection historyCollection = new HistoryCollection(ghostFile);
		info.P_GhostRaceInfo = new GhostRaceInfo(ghostToPlay.GhostName, historyCollection.Time);
		if (ghostFile == null)
		{
			yield break;
		}
		GameManager.mapTime = 0f;
		Singleton<RecordedDataPrefabPlayer>.Instance.StartPlayback(historyCollection);
		yield break;
	}

	// Token: 0x06000E33 RID: 3635 RVA: 0x0005CC80 File Offset: 0x0005AE80
	private IEnumerator InstantiateMap(int mapNumber)
	{
		while (this.mWaitingForGhostLoad)
		{
			Debug.Log("Map loading waiting for ghost: " + Time.frameCount);
			yield return 0;
		}
		if (info.onLastLevel)
		{
			UnityEngine.Object.Instantiate(Resources.Load("mapLast"), base.transform.position, base.transform.rotation);
		}
		else
		{
			UnityEngine.Object.Instantiate(Resources.Load("map" + mapNumber.ToString()), base.transform.position, base.transform.rotation);
			UnityEngine.Object.Instantiate(Resources.Load("world" + info.currentlyPlayedWorld.ToString()), base.transform.position, base.transform.rotation);
		}
		yield break;
	}

	// Token: 0x06000E34 RID: 3636 RVA: 0x0005CCAC File Offset: 0x0005AEAC
	public void StartLevel(int i, bool newLevel, LeaderboardGhostHandler.GhostMethod ghostMethod = LeaderboardGhostHandler.GhostMethod.None)
	{
		if (ghostMethod != LeaderboardGhostHandler.GhostMethod.None || info.PlayingGhostFromLeaderBoard)
		{
			if (!info.PlayingGhostFromLeaderBoard)
			{
				steam_WorkshopHandler.Instance().FetchMyLeaderboardEntry(info.currentLevel.ToString(), ghostMethod);
			}
			base.StartCoroutine(this.SetupGhost(ghostMethod));
		}
		else
		{
			this.mWaitingForGhostLoad = false;
		}
		if (i != 90)
		{
			this.musicMan.StartSong(info.currentlyPlayedWorld);
		}
		else
		{
			this.musicMan.StopSong();
		}
		car.numberOfTrucks = 0;
		this.SavePlayTime();
		Time.timeScale = 1f;
		if (!Manager.usingMapcycle)
		{
			info.currentLevel = i;
		}
		try
		{
			base.StartCoroutine(this.InstantiateMap(i));
			if (!this._playerReference)
			{
				base.StartCoroutine(this.spawnPlayer(Vector3.up * 2f, newLevel, i));
			}
			else
			{
				base.StartCoroutine(this.EnableAfterFrame(this._playerReference.gameObject, newLevel, i));
			}
		}
		catch
		{
			this.NextLevel();
		}
		Debug.Log("Loading Level: " + i);
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x0005CDF0 File Offset: 0x0005AFF0
	private IEnumerator EnableAfterFrame(GameObject go, bool newLevel, int i)
	{
		while (this.mWaitingForGhostLoad)
		{
			Debug.Log("Map loading waiting for ghost");
			yield return 0;
		}
		yield return new WaitForEndOfFrame();
		go.GetComponentInChildren<AssignGhostInfo>(true).Init();
		Singleton<DataRecorder>.Instance.StartRecording();
		go.SetActive(true);
		this._playerReference.GetComponent<GameManager>().ResetMe();
		if (newLevel)
		{
			int levelToLoad = Manager.usingMapcycle ? Manager.MapcycleArray.IndexOfPhilip(i) : i;
			this._playerReference.GetComponentInChildren<helpText>(true).getBigTextComponent().changeTextTemporary("LEVEL " + levelToLoad, true, 3f);
		}
		else
		{
			this._playerReference.GetComponentInChildren<helpText>(true).getBigTextComponent().changeText(string.Empty);
		}
		if (GameManager.mapDeaths >= 100)
		{
			Debug.Log("Skip Current Map");
		}
		if (newLevel)
		{
			this.RestartLevel();
		}
		yield break;
	}

	// Token: 0x1700028B RID: 651
	// (get) Token: 0x06000E36 RID: 3638 RVA: 0x0005CE38 File Offset: 0x0005B038
	private RecordingDataTransmitterBase P_PlayerTransmitter
	{
		get
		{
			return this._playerReference.GetComponentInChildren<RecordingDataTransmitterBase>(true);
		}
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x0005CE48 File Offset: 0x0005B048
	private IEnumerator spawnPlayer(Vector3 pos, bool newLevel, int i)
	{
		while (this.mWaitingForGhostLoad)
		{
			Debug.Log("spawnPlayer Waitig for ghost");
			yield return 0;
		}
		if (this._playerReference)
		{
			yield break;
		}
		yield return new WaitForEndOfFrame();
		GameObject player = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("player"), base.transform.position, base.transform.rotation);
		player.GetComponentInChildren<AssignGhostInfo>(true).Init();
		Singleton<DataRecorder>.Instance.StartRecording();
		this.UI_Slide_Small = player.GetComponentInChildren<UI_Slide_General>();
		player.GetComponent<GameManager>().ResetMe();
		player.transform.FindChild("hitbox").position = pos;
		player.name = player.name.Replace("(Clone)", string.Empty);
		info.playing = true;
		if (newLevel)
		{
			player.GetComponentInChildren<helpText>().getBigTextComponent().changeTextTemporary("LEVEL " + i, true, 3f);
		}
		if (GameManager.mapDeaths >= 20)
		{
			Debug.Log("Skip Current Map");
			Manager.canSkipCurrentMap = true;
			this.UI_Slide_Small.changeTextTemporary((!Manager.Controller) ? this.SkipMap_text : this.SkipMap_textController, 2f);
		}
		this._playerReference = player;
		if (newLevel)
		{
			this.RestartLevel();
		}
		yield break;
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x0005CE90 File Offset: 0x0005B090
	public void WinLevel()
	{
		if (!info.PlayingGhostFromLeaderBoard)
		{
			leaderboardsManager.FlushAllGhosts();
		}
		if (info.currentLevel > info.completedLevels)
		{
			info.completedLevels++;
		}
		PlayerPrefs.SetInt("completed", info.completedLevels);
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x0005CECC File Offset: 0x0005B0CC
	public void ClearLevel()
	{
		info.gameTime = 1f;
		if (this._playerReference)
		{
			if (!(this.P_PlayerTransmitter == null))
			{
				this.mPrefabName = this.P_PlayerTransmitter.P_PrefabName;
				UnityEngine.Object.Destroy(this.P_PlayerTransmitter);
			}
			this._playerReference.SetActive(false);
		}
		foreach (RemoveOnMapChange removeOnMapChange in UnityEngine.Object.FindObjectsOfType(typeof(RemoveOnMapChange)))
		{
			UnityEngine.Object.Destroy(removeOnMapChange.gameObject);
		}
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x0005CF6C File Offset: 0x0005B16C
	public void CloseMenu()
	{
		this.MainCanvas.SetActive(false);
		this.MenuCam.SetActive(false);
		this.uiCam.SetActive(false);
		this.levelSelect.SetActive(false);
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x0005CFAC File Offset: 0x0005B1AC
	public void GoToLevelSelect()
	{
		info.PlayingGhostFromLeaderBoard = false;
		leaderboardsManager.FlushAllGhosts();
		this.levelSelect.SetActive(true);
		this.MainCanvas.SetActive(false);
		this.MenuCam.SetActive(false);
		this.uiCam.SetActive(false);
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x0005CFF4 File Offset: 0x0005B1F4
	public void OpenMenuFromGame()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		this.MenuCam.SetActive(true);
		this.uiCam.SetActive(true);
		this.MainCanvas.SetActive(true);
		Manager.Instance().ClearLevel();
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x0005D03C File Offset: 0x0005B23C
	public void OpenLevelSelectFromGram()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		this.levelSelect.SetActive(true);
		Manager.Instance().ClearLevel();
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x0005D06C File Offset: 0x0005B26C
	public void FadeMainMenu()
	{
		this.menuBlack.SetBlack();
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x0005D07C File Offset: 0x0005B27C
	public void OpenMenu()
	{
		this.MenuCam.SetActive(true);
		this.uiCam.SetActive(true);
		this.MainCanvas.SetActive(true);
		this.levelSelect.SetActive(false);
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x0005D0BC File Offset: 0x0005B2BC
	public void GoToLevelEditor()
	{
		SceneManager.LoadScene("Editor", LoadSceneMode.Single);
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x0005D0CC File Offset: 0x0005B2CC
	public void Quit()
	{
		this.SavePlayTime();
		Application.Quit();
	}

	// Token: 0x04000AC2 RID: 2754
	public static bool Controller;

	// Token: 0x04000AC3 RID: 2755
	public bool ControllerSet;

	// Token: 0x04000AC4 RID: 2756
	public bool ShowbuildSet;

	// Token: 0x04000AC5 RID: 2757
	public string SkipMap_textController = "PRESS Y TO SKIP LEVEL";

	// Token: 0x04000AC6 RID: 2758
	public string SkipMap_text = "PRESS Y TO SKIP LEVEL";

	// Token: 0x04000AC7 RID: 2759
	public static int[] MapcycleArray;

	// Token: 0x04000AC8 RID: 2760
	public int[] MapcycleArrayPublic;

	// Token: 0x04000AC9 RID: 2761
	public static bool usingMapcycle;

	// Token: 0x04000ACA RID: 2762
	public bool usingMapcyclePublic;

	// Token: 0x04000ACB RID: 2763
	public static bool canSkipCurrentMap;

	// Token: 0x04000ACC RID: 2764
	public UI_Slide_General UI_Slide_Small;

	// Token: 0x04000ACD RID: 2765
	public UI_Slide_General UI_Slide_Big;

	// Token: 0x04000ACE RID: 2766
	public GameObject MainCanvas;

	// Token: 0x04000ACF RID: 2767
	public GameObject MenuCam;

	// Token: 0x04000AD0 RID: 2768
	public GameObject uiCam;

	// Token: 0x04000AD1 RID: 2769
	public GameObject levelSelect;

	// Token: 0x04000AD2 RID: 2770
	public menuBlack menuBlack;

	// Token: 0x04000AD3 RID: 2771
	private bool myRestart;

	// Token: 0x04000AD4 RID: 2772
	[HideInInspector]
	private int state;

	// Token: 0x04000AD5 RID: 2773
	public AudioSource windAU;

	// Token: 0x04000AD6 RID: 2774
	public SENaturalBloomAndDirtyLens menuBloom;

	// Token: 0x04000AD7 RID: 2775
	public menuBar myMenuBar;

	// Token: 0x04000AD8 RID: 2776
	private static Manager _Manager;

	// Token: 0x04000AD9 RID: 2777
	private GameObject _playerReference;

	// Token: 0x04000ADA RID: 2778
	public GameObject emptyObject;

	// Token: 0x04000ADB RID: 2779
	public GameObject thanksScreen;

	// Token: 0x04000ADC RID: 2780
	private float currentPlayThroughTime;

	// Token: 0x04000ADD RID: 2781
	public Text playThroughText;

	// Token: 0x04000ADE RID: 2782
	public Text endGameText;

	// Token: 0x04000ADF RID: 2783
	public musicManager musicMan;

	// Token: 0x04000AE0 RID: 2784
	private string mPrefabName;

	// Token: 0x04000AE1 RID: 2785
	private bool mWaitingForGhostLoad = true;
}
