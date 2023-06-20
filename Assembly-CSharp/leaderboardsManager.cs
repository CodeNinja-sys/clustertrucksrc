using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Steamworks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020002F3 RID: 755
public class leaderboardsManager : MonoBehaviour
{
	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x060011A6 RID: 4518 RVA: 0x00071F84 File Offset: 0x00070184
	public static bool SmallInitialization
	{
		get
		{
			return leaderboardsManager._smallInitialization;
		}
	}

	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x060011A7 RID: 4519 RVA: 0x00071F8C File Offset: 0x0007018C
	// (set) Token: 0x060011A8 RID: 4520 RVA: 0x00071FE4 File Offset: 0x000701E4
	public ELeaderboardDataRequest currentSortMethod
	{
		get
		{
			if (!(this.sortDropdown != null))
			{
				return this._currentSortMethod;
			}
			switch (this.sortDropdown.value)
			{
			case 0:
				return ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal;
			case 1:
				return ELeaderboardDataRequest.k_ELeaderboardDataRequestFriends;
			case 2:
				return ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser;
			default:
				throw new Exception("WRONG TYPE MOFO!");
			}
		}
		set
		{
			this._currentSortMethod = value;
		}
	}

	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x060011A9 RID: 4521 RVA: 0x00071FF0 File Offset: 0x000701F0
	// (set) Token: 0x060011AA RID: 4522 RVA: 0x00071FF8 File Offset: 0x000701F8
	public static bool ShowCampaignMap
	{
		get
		{
			return leaderboardsManager._showCampaignMap;
		}
		set
		{
			leaderboardsManager._showCampaignMap = value;
		}
	}

	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x060011AB RID: 4523 RVA: 0x00072000 File Offset: 0x00070200
	private int _mapCount
	{
		get
		{
			return 80;
		}
	}

	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x060011AC RID: 4524 RVA: 0x00072004 File Offset: 0x00070204
	// (set) Token: 0x060011AD RID: 4525 RVA: 0x0007200C File Offset: 0x0007020C
	public float DropDown_contentPanel_Yposition
	{
		get
		{
			return this.dropdown_contentPanel_Yposition;
		}
		set
		{
			this.dropdown_contentPanel_Yposition = value;
		}
	}

	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x060011AE RID: 4526 RVA: 0x00072018 File Offset: 0x00070218
	public static GhostRaceTime P_OthersGhost
	{
		get
		{
			return leaderboardsManager._OtherGhost;
		}
	}

	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x060011AF RID: 4527 RVA: 0x00072020 File Offset: 0x00070220
	public static GhostRaceTime P_MyGhost
	{
		get
		{
			return leaderboardsManager._MyGhost;
		}
	}

	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x060011B0 RID: 4528 RVA: 0x00072028 File Offset: 0x00070228
	public static bool P_HasGhost
	{
		get
		{
			return leaderboardsManager.P_MyGhost != null || leaderboardsManager.P_OthersGhost != null;
		}
	}

	// Token: 0x060011B1 RID: 4529 RVA: 0x00072044 File Offset: 0x00070244
	private void Awake()
	{
		this.ws_Handler = UnityEngine.Object.FindObjectOfType<steam_WorkshopHandler>();
		if (!this.ws_Handler)
		{
			throw new Exception("There needs to be a steam_workshophandler!");
		}
		this.m_OnRemoteStorageDownloadUGCResult = CallResult<RemoteStorageDownloadUGCResult_t>.Create(new CallResult<RemoteStorageDownloadUGCResult_t>.APIDispatchDelegate(this.OnRemoteStorageDownloadUGCResult));
		this.m_OnRemoteStorageDownloadMyUGCResult = CallResult<RemoteStorageDownloadUGCResult_t>.Create(new CallResult<RemoteStorageDownloadUGCResult_t>.APIDispatchDelegate(this.OnRemoteStorageDownloadMyUGCResult));
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x000720A8 File Offset: 0x000702A8
	private void Start()
	{
		this.m_PersonsaStateChanged = Callback<PersonaStateChange_t>.Create(new Callback<PersonaStateChange_t>.DispatchDelegate(this.OnPersonaStateChanged));
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x000720C4 File Offset: 0x000702C4
	public void SmallEnable(int map)
	{
		leaderboardsManager._smallInitialization = true;
		this.Initializing = true;
		this.Awake();
		this.clearLeaderboard();
		this.currentSortMethod = ((!this.ShowFriendsToggle.isOn) ? ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser : ELeaderboardDataRequest.k_ELeaderboardDataRequestFriends);
		MonoBehaviour.print(string.Concat(new object[]
		{
			"Small init was enabled with sorting: ",
			this.currentSortMethod.ToString(),
			" : ",
			Time.frameCount
		}));
		leaderboardsManager.currentMaptype = 1;
		leaderboardsManager.currentLeaderboardMap = "LEVEL " + map;
		base.StartCoroutine(this.SmallInitCourutine());
	}

	// Token: 0x060011B4 RID: 4532 RVA: 0x00072170 File Offset: 0x00070370
	public void EnableMenu()
	{
		leaderboardsManager._smallInitialization = false;
		this.Initializing = true;
		this.Awake();
		this.clearLeaderboard();
		if (this.mapDropDown != null)
		{
			this.mapDropDown.options.Clear();
		}
		if (this.sortDropdown != null)
		{
			this.sortDropdown.value = 0;
			this.sortDropdown.captionText.text = this.sortDropdown.options[0].text;
		}
		MonoBehaviour.print("script was enabled");
		leaderboardsManager.currentMaptype = 1;
		leaderboardsManager.currentLeaderboardMap = "LEVEL " + info.lastPlayedLevel.ToString();
		base.StartCoroutine(this.InitCourutine());
	}

	// Token: 0x060011B5 RID: 4533 RVA: 0x00072230 File Offset: 0x00070430
	public static void FlushMyGhost()
	{
		leaderboardsManager._MyGhost = null;
	}

	// Token: 0x060011B6 RID: 4534 RVA: 0x00072238 File Offset: 0x00070438
	public static void FlushOtherGhost()
	{
		leaderboardsManager._OtherGhost = null;
	}

	// Token: 0x060011B7 RID: 4535 RVA: 0x00072240 File Offset: 0x00070440
	public static void FlushAllGhosts()
	{
		Debug.Log("Flushing all ghosts!");
		leaderboardsManager.FlushMyGhost();
		leaderboardsManager.FlushOtherGhost();
	}

	// Token: 0x060011B8 RID: 4536 RVA: 0x00072258 File Offset: 0x00070458
	public void DisableMenu()
	{
		this.DropDown_contentPanel_Yposition = 0f;
		this.closeDropdown();
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x0007226C File Offset: 0x0007046C
	private IEnumerator SmallInitCourutine()
	{
		this.ChangeLoadingState(true);
		yield return new WaitForSeconds(0.5f);
		this.ws_Handler.DownloadLeaderboardEntries(leaderboardsManager.currentLeaderboardMap, true);
		this.Initializing = false;
		yield break;
	}

	// Token: 0x060011BA RID: 4538 RVA: 0x00072288 File Offset: 0x00070488
	private IEnumerator InitCourutine()
	{
		this.ChangeLoadingState(true);
		this.showCampaignMaps(this._mapCount, info.lastPlayedLevel);
		yield return new WaitForSeconds(0.5f);
		this.ws_Handler.DownloadLeaderboardEntries(leaderboardsManager.currentLeaderboardMap, true);
		this.Initializing = false;
		yield break;
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x000722A4 File Offset: 0x000704A4
	public void ChangeLoadingState(bool isLoading)
	{
		if (isLoading)
		{
			this.anim.SetBool("nothing", false);
			this.anim.Play("animationLoop");
			this.anim.SetBool("loading", true);
		}
		else
		{
			this.anim.SetBool("loading", false);
		}
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x00072300 File Offset: 0x00070500
	private void Update()
	{
	}

	// Token: 0x060011BD RID: 4541 RVA: 0x00072304 File Offset: 0x00070504
	public void OnSubscribedItemsGet(bool fail, RemoteStorageSubscribePublishedFileResult_t _callback)
	{
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x00072308 File Offset: 0x00070508
	public void getLeaderBoardMaps()
	{
		if (this.mapDropDown != null)
		{
			this.mapDropDown.options.Clear();
		}
		int num = leaderboardsManager.currentMaptype;
		int num2 = num;
		if (num2 != 1)
		{
			if (num2 == 2)
			{
				UGCQueryHandle_t handle = SteamUGC.CreateQueryUserUGCRequest(SteamUser.GetSteamID().GetAccountID(), EUserUGCList.k_EUserUGCList_Published, EUGCMatchingUGCType.k_EUGCMatchingUGCType_Items, EUserUGCListSortOrder.k_EUserUGCListSortOrder_LastUpdatedDesc, SteamUtils.GetAppID(), SteamUtils.GetAppID(), 1U);
				SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(handle);
				this.m_SteamUGCQueryCompleted.Set(hAPICall, null);
			}
		}
		else
		{
			this.showCampaignMaps(this._mapCount, 0);
		}
	}

	// Token: 0x060011BF RID: 4543 RVA: 0x000723A0 File Offset: 0x000705A0
	public void OnSubmit(BaseEventData b)
	{
		if (b.selectedObject.tag == "leaderboardMap")
		{
			Debug.Log("LeaderboardMap");
			FileInfo fileInfo = b.selectedObject.GetComponent<getFile>().getFileInfo();
			if (fileInfo != null)
			{
				if (fileInfo.FullName.Contains("steam"))
				{
					Debug.Log("STEAMWORKS MAP!");
					leaderboardsManager.currentLeaderboardMap = fileInfo.Directory.Name;
					this.ws_Handler.DownloadLeaderboardEntries(fileInfo.Directory.Name, false);
				}
			}
			else
			{
				Debug.Log("Campaign MAP!");
				leaderboardsManager.currentLeaderboardMap = b.selectedObject.GetComponentInChildren<Text>().text;
				this.ws_Handler.DownloadLeaderboardEntries(b.selectedObject.GetComponentInChildren<Text>().text, true);
			}
			this.ShowTab(leaderboardsManager.currentLeaderboardTAB);
		}
	}

	// Token: 0x060011C0 RID: 4544 RVA: 0x0007247C File Offset: 0x0007067C
	private void refreshMapDropDownList()
	{
		if (this.mapDropDown != null)
		{
			this.mapDropDown.value = 0;
		}
		if (this.mapDropDown != null)
		{
			this.mapDropDown.RefreshShownValue();
		}
		this.switchedLeaderboardMap();
	}

	// Token: 0x060011C1 RID: 4545 RVA: 0x000724C8 File Offset: 0x000706C8
	public void showCampaignMaps(int _mapCount, int setLevel = 0)
	{
		this.currentSteamworksFiles = null;
		for (int i = 1; i <= _mapCount; i++)
		{
			Dropdown.OptionData item = new Dropdown.OptionData("LEVEL " + i.ToString());
			if (this.mapDropDown != null)
			{
				this.mapDropDown.options.Add(item);
			}
		}
		if (setLevel != 0)
		{
			if (this.mapDropDown != null)
			{
				this.mapDropDown.value = setLevel - 1;
			}
			if (this.mapDropDown != null)
			{
				this.mapDropDown.RefreshShownValue();
			}
		}
		if (this.mapDropDown != null)
		{
			this.mapDropDown.GetComponentInChildren<Text>().text = this.mapDropDown.options[this.mapDropDown.value].text;
		}
	}

	// Token: 0x060011C2 RID: 4546 RVA: 0x000725AC File Offset: 0x000707AC
	public void recieveLeaderBoardEntries(List<leaderboardScore> entryList, float mapTime = -1f)
	{
		base.StartCoroutine(this.ShowLeaderBoardsEntriesWithTimer(entryList, 0.05f, mapTime));
	}

	// Token: 0x060011C3 RID: 4547 RVA: 0x000725C4 File Offset: 0x000707C4
	private IEnumerator ShowLeaderBoardsEntriesWithTimer(List<leaderboardScore> entryList, float timeToWait, float mapTime)
	{
		this.ChangeLoadingState(false);
		yield return new WaitForSeconds(0.5f);
		string _name = string.Empty;
		this.clearLeaderboard();
		foreach (leaderboardScore item in entryList)
		{
			if (!SteamFriends.RequestUserInformation(item.getEntry().m_steamIDUser, true))
			{
				_name = SteamFriends.GetFriendPersonaName(item.getEntry().m_steamIDUser);
			}
			else
			{
				while (SteamFriends.GetFriendPersonaName(item.getEntry().m_steamIDUser).Contains("unknown"))
				{
					Debug.Log("Waiting For love");
				}
			}
			int milliseconds = item.getEntry().m_nScore;
			int seconds = milliseconds / 1000;
			int minutes = (int)Mathf.Floor((float)(seconds / 60));
			string parts = (milliseconds - seconds * 1000).ToString();
			while (parts.Length < 3)
			{
				parts = "0" + parts;
			}
			string finalTime = string.Concat(new object[]
			{
				minutes.ToString(),
				":",
				seconds,
				":",
				parts
			});
			bool newHighscore = item.getEntry().m_steamIDUser == SteamUser.GetSteamID() && milliseconds == (int)(mapTime * 1000f);
			GameObject cell = UnityEngine.Object.Instantiate<GameObject>(this.LeaderBoardCellPrefab);
			cell.SetActive(true);
			cell.GetComponent<assignLeaderboardButtonInfo>().assignLeaderBoardButtons(item.getEntry().m_nGlobalRank.ToString(), _name, finalTime, item.getDetails()[0].ToString(), item.getEntry().m_hUGC, this, item.getEntry().m_steamIDUser, newHighscore, string.Empty);
			yield return new WaitForSeconds(timeToWait);
			cell.transform.SetParent(this.LeaderBoardGrid.transform, false);
		}
		yield break;
	}

	// Token: 0x060011C4 RID: 4548 RVA: 0x0007260C File Offset: 0x0007080C
	public void clearLeaderboard()
	{
		foreach (Button button in this.LeaderBoardGrid.GetComponentsInChildren<Button>())
		{
			UnityEngine.Object.Destroy(button.gameObject);
		}
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x00072648 File Offset: 0x00070848
	public void ShowTab(int tab)
	{
		bool active = true;
		if (leaderboardsManager.currentLeaderboardTAB == tab)
		{
			active = !this.tabs[leaderboardsManager.currentLeaderboardTAB - 1].activeInHierarchy;
		}
		leaderboardsManager.currentLeaderboardTAB = tab;
		for (int i = 0; i < this.tabs.Length; i++)
		{
			if (i + 1 == leaderboardsManager.currentLeaderboardTAB)
			{
				this.tabs[i].SetActive(active);
			}
			else
			{
				this.tabs[i].SetActive(false);
			}
		}
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x000726C8 File Offset: 0x000708C8
	public void switchSortMethod()
	{
		if (this.Initializing)
		{
			return;
		}
		switch (this.sortDropdown.value)
		{
		case 0:
			this.currentSortMethod = ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal;
			break;
		case 1:
			this.currentSortMethod = ELeaderboardDataRequest.k_ELeaderboardDataRequestFriends;
			break;
		case 2:
			this.currentSortMethod = ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser;
			break;
		default:
			throw new Exception("SortMethod invalid!");
		}
		this.clearLeaderboard();
		this.ChangeLoadingState(true);
		this.ws_Handler.DownloadLeaderboardEntries(leaderboardsManager.currentLeaderboardMap, leaderboardsManager.ShowCampaignMap);
		Debug.Log("Current SOrting MEthod: " + this.currentSortMethod.ToString());
	}

	// Token: 0x060011C7 RID: 4551 RVA: 0x00072778 File Offset: 0x00070978
	public void setCurrentMapType(int _type)
	{
		leaderboardsManager.currentMaptype = _type;
		this.getLeaderBoardMaps();
		Debug.Log("New Maptype: " + leaderboardsManager.currentMaptype);
	}

	// Token: 0x060011C8 RID: 4552 RVA: 0x000727A0 File Offset: 0x000709A0
	public void SaveNewFropDownPosition(BaseEventData b)
	{
		Vector3 position = b.selectedObject.transform.parent.position;
		Debug.Log(position, b.selectedObject);
	}

	// Token: 0x060011C9 RID: 4553 RVA: 0x000727D4 File Offset: 0x000709D4
	public void switchedLeaderboardMap()
	{
		if (this.Initializing)
		{
			return;
		}
		this.DropDown_contentPanel_Yposition = UnityEngine.Object.FindObjectOfType<DropDownContentHolder>().GetComponent<RectTransform>().localPosition.y;
		this.clearLeaderboard();
		this.ChangeLoadingState(true);
		if (this.mapDropDown != null)
		{
			leaderboardsManager.currentLeaderboardMap = this.mapDropDown.options[this.mapDropDown.value].text;
		}
		Debug.Log(leaderboardsManager.currentLeaderboardMap);
		if (leaderboardsManager.currentMaptype == 2)
		{
			FileInfo fileInfo = this.currentSteamworksFiles[this.mapDropDown.value];
			Debug.Log("ST&EAMWORKS MAP!");
			leaderboardsManager.currentLeaderboardMap = fileInfo.Directory.Name;
			this.ws_Handler.DownloadLeaderboardEntries(fileInfo.Directory.Name, false);
		}
		else
		{
			if (leaderboardsManager.currentMaptype != 1)
			{
				throw new Exception("Wrong maptype! " + leaderboardsManager.currentMaptype);
			}
			Debug.Log("Campaign MAP!");
			this.ws_Handler.DownloadLeaderboardEntries(leaderboardsManager.currentLeaderboardMap, true);
		}
	}

	// Token: 0x060011CA RID: 4554 RVA: 0x000728F0 File Offset: 0x00070AF0
	public void OnPersonaStateChanged(PersonaStateChange_t _callBack)
	{
		Debug.Log("RETURNED: " + SteamFriends.GetFriendPersonaName(new CSteamID(_callBack.m_ulSteamID)));
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x00072920 File Offset: 0x00070B20
	public void closeDropdown()
	{
		if (this.mapDropDown != null)
		{
			this.mapDropDown.Hide();
		}
		if (this.sortDropdown != null)
		{
			this.sortDropdown.Hide();
		}
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x00072968 File Offset: 0x00070B68
	public void OnFriendsToggleChanged(bool showFriends)
	{
		if (showFriends)
		{
			this.currentSortMethod = ELeaderboardDataRequest.k_ELeaderboardDataRequestFriends;
		}
		else
		{
			this.currentSortMethod = ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser;
		}
		this.clearLeaderboard();
		this.ChangeLoadingState(true);
		this.ws_Handler.DownloadLeaderboardEntries(leaderboardsManager.currentLeaderboardMap, true);
		Debug.Log("Current SOrting MEthod: " + this.currentSortMethod.ToString());
	}

	// Token: 0x060011CD RID: 4557 RVA: 0x000729CC File Offset: 0x00070BCC
	public void ChangeGhostData(UGCHandle_t ghostDataHandle)
	{
		Debug.Log("Setting GhostData " + ghostDataHandle.m_UGCHandle);
		SteamAPICall_t hAPICall = SteamRemoteStorage.UGCDownload(ghostDataHandle, 0U);
		if (this.m_OnRemoteStorageDownloadUGCResult == null)
		{
			Debug.Log("m_OnRemoteStorageDownloadUGCResult null: Calling Awake() " + Time.frameCount);
			this.Awake();
		}
		this.m_OnRemoteStorageDownloadUGCResult.Set(hAPICall, null);
	}

	// Token: 0x060011CE RID: 4558 RVA: 0x00072A34 File Offset: 0x00070C34
	public void SetMyGhostData(UGCHandle_t ghostDataHandle)
	{
		if (this.m_OnRemoteStorageDownloadMyUGCResult == null)
		{
			Debug.Log("m_OnRemoteStorageDownloadMyUGCResult null: Calling Awake() " + Time.frameCount);
			this.Awake();
		}
		Debug.Log(string.Concat(new object[]
		{
			"Setting MyGhostDdata ",
			ghostDataHandle.m_UGCHandle,
			"  : ",
			Time.frameCount
		}));
		SteamAPICall_t hAPICall = SteamRemoteStorage.UGCDownload(ghostDataHandle, 0U);
		this.m_OnRemoteStorageDownloadMyUGCResult.Set(hAPICall, null);
	}

	// Token: 0x060011CF RID: 4559 RVA: 0x00072ABC File Offset: 0x00070CBC
	private IEnumerator downloadProgress(UGCHandle_t ghostDataHandle)
	{
		Debug.Log("Download Progress Started for Ghost");
		float time = Time.time;
		float timeToWait = 10f;
		int framecount = Time.frameCount;
		while (time + timeToWait > Time.time)
		{
			if (framecount != Time.frameCount)
			{
				int bytesExpected;
				int bytesLoaded;
				SteamRemoteStorage.GetUGCDownloadProgress(ghostDataHandle, out bytesExpected, out bytesLoaded);
				Debug.Log(bytesLoaded + " / " + bytesExpected);
				framecount = Time.frameCount;
			}
			yield return 0;
		}
		yield break;
	}

	// Token: 0x060011D0 RID: 4560 RVA: 0x00072AE0 File Offset: 0x00070CE0
	private void GeneralOnRemoteStorageDownloadUGCResult(RemoteStorageDownloadUGCResult_t param, bool bIOFailure, ref GhostRaceTime ghostTarget)
	{
		if (bIOFailure)
		{
			Debug.Log("Biofail: " + bIOFailure);
			return;
		}
		if (param.m_eResult == EResult.k_EResultOK)
		{
			byte[] array = new byte[param.m_nSizeInBytes];
			SteamRemoteStorage.UGCRead(param.m_hFile, array, param.m_nSizeInBytes, 0U, EUGCReadAction.k_EUGCRead_ContinueReadingUntilFinished);
			ghostTarget = new GhostRaceTime(array, SteamFriends.GetFriendPersonaName(new CSteamID(param.m_ulSteamIDOwner)));
			Debug.Log("Read GhostData: " + Time.frameCount);
			if (UnityEngine.Object.FindObjectOfType<RaceGhostButtonTag>())
			{
				UnityEngine.Object.FindObjectOfType<RaceGhostButtonTag>().GetComponent<Button>().interactable = true;
			}
		}
		else
		{
			ghostTarget = null;
			Debug.Log(string.Concat(new object[]
			{
				"OnRemoteStorageDownloadUGCResult Result: ",
				param.m_eResult,
				" Filename: ",
				param.m_pchFileName
			}));
		}
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x00072BCC File Offset: 0x00070DCC
	public void OnRemoteStorageDownloadMyUGCResult(RemoteStorageDownloadUGCResult_t param, bool bIOFailure)
	{
		this.GeneralOnRemoteStorageDownloadUGCResult(param, bIOFailure, ref leaderboardsManager._MyGhost);
		Debug.Log("Downloaded MyGhostDdata: " + Time.frameCount);
	}

	// Token: 0x060011D2 RID: 4562 RVA: 0x00072C00 File Offset: 0x00070E00
	public void OnRemoteStorageDownloadUGCResult(RemoteStorageDownloadUGCResult_t param, bool bIOFailure)
	{
		this.GeneralOnRemoteStorageDownloadUGCResult(param, bIOFailure, ref leaderboardsManager._OtherGhost);
		Debug.Log("Downloaded GhostData:  " + Time.frameCount);
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x00072C34 File Offset: 0x00070E34
	public void PlayGhost()
	{
		info.PlayingGhostFromLeaderBoard = true;
		info.currentLevel = this.mapDropDown.value + 1;
		info.onLastLevel = false;
		Manager.Instance().Play();
	}

	// Token: 0x04000ED4 RID: 3796
	private const float DeadTime = 0.5f;

	// Token: 0x04000ED5 RID: 3797
	private const float Dropdown_DEFAULT_Yposition = 0f;

	// Token: 0x04000ED6 RID: 3798
	private const float timeToBlup = 0.05f;

	// Token: 0x04000ED7 RID: 3799
	private static GhostRaceTime _OtherGhost;

	// Token: 0x04000ED8 RID: 3800
	private static GhostRaceTime _MyGhost;

	// Token: 0x04000ED9 RID: 3801
	private bool Initializing;

	// Token: 0x04000EDA RID: 3802
	private static bool _smallInitialization;

	// Token: 0x04000EDB RID: 3803
	private ELeaderboardDataRequest _currentSortMethod;

	// Token: 0x04000EDC RID: 3804
	public static int currentLeaderboardTAB;

	// Token: 0x04000EDD RID: 3805
	public static int currentMaptype = 1;

	// Token: 0x04000EDE RID: 3806
	private static bool _showCampaignMap = true;

	// Token: 0x04000EDF RID: 3807
	public GameObject LBMapPickerGrid;

	// Token: 0x04000EE0 RID: 3808
	public GameObject LeaderBoardGrid;

	// Token: 0x04000EE1 RID: 3809
	public GameObject LBMapCellPrefab;

	// Token: 0x04000EE2 RID: 3810
	public GameObject LeaderBoardCellPrefab;

	// Token: 0x04000EE3 RID: 3811
	public GameObject[] tabs;

	// Token: 0x04000EE4 RID: 3812
	public Dropdown sortDropdown;

	// Token: 0x04000EE5 RID: 3813
	public Dropdown mapDropDown;

	// Token: 0x04000EE6 RID: 3814
	protected Callback<PersonaStateChange_t> m_PersonsaStateChanged;

	// Token: 0x04000EE7 RID: 3815
	private CallResult<RemoteStorageEnumerateUserSubscribedFilesResult_t> m_RemoteStorageEnumerateUserSubscribedFilesResult;

	// Token: 0x04000EE8 RID: 3816
	private CallResult<SteamUGCQueryCompleted_t> m_SteamUGCQueryCompleted;

	// Token: 0x04000EE9 RID: 3817
	private CallResult<LeaderboardFindResult_t> m_LeaderboardFindResult;

	// Token: 0x04000EEA RID: 3818
	private CallResult<LeaderboardScoresDownloaded_t> m_LeaderBoardScoresDownloaded;

	// Token: 0x04000EEB RID: 3819
	private CallResult<RemoteStorageDownloadUGCResult_t> m_OnRemoteStorageDownloadUGCResult;

	// Token: 0x04000EEC RID: 3820
	private CallResult<RemoteStorageDownloadUGCResult_t> m_OnRemoteStorageDownloadMyUGCResult;

	// Token: 0x04000EED RID: 3821
	private float dropdown_contentPanel_Yposition;

	// Token: 0x04000EEE RID: 3822
	private FileInfo[] currentSteamworksFiles;

	// Token: 0x04000EEF RID: 3823
	private static string currentLeaderboardMap = string.Empty;

	// Token: 0x04000EF0 RID: 3824
	private steam_WorkshopHandler ws_Handler;

	// Token: 0x04000EF1 RID: 3825
	public Animator anim;

	// Token: 0x04000EF2 RID: 3826
	[SerializeField]
	private Toggle ShowFriendsToggle;
}
