using System;
using System.Collections.Generic;
using System.IO;
using Steamworks;
using UnityEngine;

// Token: 0x020002BC RID: 700
public class steam_WorkshopHandler : MonoBehaviour
{
	// Token: 0x0600109E RID: 4254 RVA: 0x0006BF54 File Offset: 0x0006A154
	public static steam_WorkshopHandler Instance()
	{
		if (!steam_WorkshopHandler.workshophandler)
		{
			steam_WorkshopHandler.workshophandler = (UnityEngine.Object.FindObjectOfType(typeof(steam_WorkshopHandler)) as steam_WorkshopHandler);
			if (!steam_WorkshopHandler.workshophandler)
			{
				Debug.LogError("There needs to be one active Workshophandler script on a GameObject in your scene.");
			}
		}
		return steam_WorkshopHandler.workshophandler;
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x0006BFA8 File Offset: 0x0006A1A8
	private void Awake()
	{
		this.m_CreateItemResult = CallResult<CreateItemResult_t>.Create(new CallResult<CreateItemResult_t>.APIDispatchDelegate(this.onCreateItemResult));
		this.m_SubmitItemResult = CallResult<SubmitItemUpdateResult_t>.Create(new CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate(this.onUGCUpdateItemSubmit));
		this.m_UGCQUeruCompleted = CallResult<SteamUGCQueryCompleted_t>.Create(new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.onUGCQueryCompleted));
		this.m_RemoteStorageSubscribeResult = CallResult<RemoteStorageSubscribePublishedFileResult_t>.Create(new CallResult<RemoteStorageSubscribePublishedFileResult_t>.APIDispatchDelegate(this.onRemoteStorageSubscribeResult));
		this.m_LeaderboardFindResultDownload = CallResult<LeaderboardFindResult_t>.Create(new CallResult<LeaderboardFindResult_t>.APIDispatchDelegate(this.onLeaderboardFindResultDownload));
		this.m_LeaderboardFindResultUpload = CallResult<LeaderboardFindResult_t>.Create(new CallResult<LeaderboardFindResult_t>.APIDispatchDelegate(this.onLeaderboardFindResultUpload));
		this.m_LeaderboardFindResultV2 = CallResult<LeaderboardFindResult_t>.Create(new CallResult<LeaderboardFindResult_t>.APIDispatchDelegate(this.OnLeaderborardFound));
		this.m_LeaderboardFindForSelfGhost = CallResult<LeaderboardFindResult_t>.Create(new CallResult<LeaderboardFindResult_t>.APIDispatchDelegate(this.OnLeaderboardFindForSelfGhost));
		this.m_LeaderboardFindForBestOtherGhost = CallResult<LeaderboardFindResult_t>.Create(new CallResult<LeaderboardFindResult_t>.APIDispatchDelegate(this.OnLeaderboardFindForBestOtherGhost));
		this.m_LeaderboardFindForBestFriendGhost = CallResult<LeaderboardFindResult_t>.Create(new CallResult<LeaderboardFindResult_t>.APIDispatchDelegate(this.OnLeaderboardFindForBestFriendGhost));
		this.m_LeaderboardFindForOtherGhost = CallResult<LeaderboardFindResult_t>.Create(new CallResult<LeaderboardFindResult_t>.APIDispatchDelegate(this.OnLeaderboardFindForOtherGhost));
		this.m_LeaderBoardScoreUpload = CallResult<LeaderboardScoreUploaded_t>.Create(new CallResult<LeaderboardScoreUploaded_t>.APIDispatchDelegate(this.onLeaderBoardScoreUpload));
		this.m_LeaderBoardScoreUploadKeepBest = CallResult<LeaderboardScoreUploaded_t>.Create(new CallResult<LeaderboardScoreUploaded_t>.APIDispatchDelegate(this.onLeaderBoardScoreUploadKeepBest));
		this.m_LeaderBoardScoresDownloadedSelfGhost = CallResult<LeaderboardScoresDownloaded_t>.Create(new CallResult<LeaderboardScoresDownloaded_t>.APIDispatchDelegate(this.LeaderBoardScoresDownloadedSelfGhost));
		this.m_LeaderBoardScoresDownloadedBestOtherGhost = CallResult<LeaderboardScoresDownloaded_t>.Create(new CallResult<LeaderboardScoresDownloaded_t>.APIDispatchDelegate(this.LeaderBoardScoresDownloadedSelfGhost));
		this.m_LeaderBoardScoresDownloadedBestFriendGhost = CallResult<LeaderboardScoresDownloaded_t>.Create(new CallResult<LeaderboardScoresDownloaded_t>.APIDispatchDelegate(this.LeaderBoardScoresDownloadedSelfGhost));
		this.m_LeaderBoardScoresDownloadedOtherGhost = CallResult<LeaderboardScoresDownloaded_t>.Create(new CallResult<LeaderboardScoresDownloaded_t>.APIDispatchDelegate(this.LeaderBoardScoresDownloadedSelfGhost));
		this.m_LeaderBoardScoresDownloaded = CallResult<LeaderboardScoresDownloaded_t>.Create(new CallResult<LeaderboardScoresDownloaded_t>.APIDispatchDelegate(this.onLeaderboardScoresDownloaded));
		this.m_LeaderBoardScoresDownloadedUploadScoresLater = CallResult<LeaderboardScoresDownloaded_t>.Create(new CallResult<LeaderboardScoresDownloaded_t>.APIDispatchDelegate(this.onLeaderboardScoresDownloadedUploadScoresLater));
		this.m_LeaderBoardScoresDownloadedV2 = CallResult<LeaderboardScoresDownloaded_t>.Create(new CallResult<LeaderboardScoresDownloaded_t>.APIDispatchDelegate(this.OnLeaderBoardScoreDownloaded));
		this.m_LeaderBoardScoresDownloadedSelfV3 = CallResult<LeaderboardScoresDownloaded_t>.Create(new CallResult<LeaderboardScoresDownloaded_t>.APIDispatchDelegate(this.OnLeaderBoardScoreDownloadedSelf));
		this.m_DownloadItemResult = Callback<DownloadItemResult_t>.Create(new Callback<DownloadItemResult_t>.DispatchDelegate(this.onItemDownloadedResult));
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x0006C1B0 File Offset: 0x0006A3B0
	private void Start()
	{
		Debug.Log(SteamFriends.GetPersonaName());
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x0006C1BC File Offset: 0x0006A3BC
	private void Update()
	{
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x0006C1C0 File Offset: 0x0006A3C0
	public void uploadMapToSTeamWorkshop(steamWorkshopMapInfo _info)
	{
		this._currentMapInfo = _info;
		SteamAPICall_t hAPICall = SteamUGC.CreateItem(new AppId_t(397950U), EWorkshopFileType.k_EWorkshopFileTypeFirst);
		this.m_CreateItemResult.Set(hAPICall, null);
		Debug.Log("Called onCreateItemResult()");
		this.m_RealMapName = _info.getTitle();
		this.m_MapFile = _info.getMapFile();
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x0006C214 File Offset: 0x0006A414
	public void updateMapToSteamWorkshop(string p_id, steamWorkshopMapInfo _info)
	{
		this._currentMapInfo = _info;
		this.m_MapFile = _info.getMapFile();
		this.p_id = new PublishedFileId_t(ulong.Parse(p_id));
		this._Call = SteamUGC.StartItemUpdate(new AppId_t(397950U), this.p_id);
		this.setMapInfo(true);
		Debug.Log("Called updateMapToSteamWorkshop()");
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x0006C274 File Offset: 0x0006A474
	public void setMapInfo(bool updating)
	{
		SteamUGC.SetItemDescription(this._Call, this._currentMapInfo.getDescription());
		string fullName = this.m_MapFile.FullName;
		Debug.Log("CORRECT PATH: " + fullName);
		SteamUGC.SetItemContent(this._Call, fullName);
		Debug.Log(fullName);
		SteamUGC.SetItemVisibility(this._Call, ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic);
		SteamUGC.SetItemTags(this._Call, this._currentMapInfo.getTags());
		SteamAPICall_t hAPICall = SteamUGC.SubmitItemUpdate(this._Call, this._currentMapInfo.getChangeNotes());
		this.m_SubmitItemResult.Set(hAPICall, null);
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x0006C310 File Offset: 0x0006A510
	public void setMapInfo()
	{
		Debug.Log(1);
		SteamUGC.SetItemTitle(this._Call, this._currentMapInfo.getTitle());
		SteamUGC.SetItemDescription(this._Call, this._currentMapInfo.getDescription());
		SteamUGC.SetItemUpdateLanguage(this._Call, "English");
		string text = levelEditorManager.DefaultFilepath + "/previews/" + this.m_MapFile.Name.Split(new char[]
		{
			'.'
		})[0] + "/1.jpg";
		if (File.Exists(text))
		{
			SteamUGC.SetItemPreview(this._Call, text);
		}
		Debug.Log(1);
		string fullName = this.m_MapFile.FullName;
		if (!this.m_MapFile.Name.Split(new char[]
		{
			'.'
		})[0].Equals(this._currentMapInfo.getTitle()))
		{
			string text2 = levelEditorManager.DefaultFilepath + "/temp/";
			if (!Directory.Exists(text2))
			{
				Directory.CreateDirectory(text2);
			}
			FileInfo fileInfo = this.m_MapFile.CopyTo(text2 + this._currentMapInfo.getTitle() + levelEditorManager.fileEnding);
			fullName = fileInfo.FullName;
			Debug.Log("NEW FILE, MUST MAKE A COPY, DOES NOT EXIST WITH NAME " + fileInfo.FullName);
		}
		Debug.Log(1);
		Debug.Log("CORRECT PATH: " + fullName);
		SteamUGC.SetItemContent(this._Call, fullName);
		Debug.Log(1);
		Debug.Log(fullName);
		SteamUGC.SetItemVisibility(this._Call, ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic);
		SteamUGC.SetItemTags(this._Call, this._currentMapInfo.getTags());
		SteamAPICall_t hAPICall = SteamUGC.SubmitItemUpdate(this._Call, this._currentMapInfo.getChangeNotes());
		this.m_SubmitItemResult.Set(hAPICall, null);
		Debug.Log(1);
	}

	// Token: 0x060010A6 RID: 4262 RVA: 0x0006C4E8 File Offset: 0x0006A6E8
	public void getworkshopMaps()
	{
		DisplayManager.Instance().DisplayMessage("Getting maps from workshop...   " + downloadManager.currentSortingMethod.getSortMethod().ToString());
		Debug.LogError("Searching WIth: " + downloadManager.currentSortingMethod.getSortMethod().ToString());
		UGCQueryHandle_t handle = SteamUGC.CreateQueryAllUGCRequest(downloadManager.currentSortingMethod.getSortMethod(), EUGCMatchingUGCType.k_EUGCMatchingUGCType_Items, new AppId_t(397950U), new AppId_t(397950U), (uint)downloadManager.currentPage);
		SteamUGC.SetMatchAnyTag(handle, true);
		SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(handle);
		this.m_UGCQUeruCompleted.Set(hAPICall, null);
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x0006C588 File Offset: 0x0006A788
	private void onCreateItemResult(CreateItemResult_t _Callback, bool bIOFailure)
	{
		if (_Callback.m_eResult == EResult.k_EResultInsufficientPrivilege)
		{
			Debug.Log("You are currently banned CHEATER!");
			return;
		}
		if (_Callback.m_eResult == EResult.k_EResultTimeout)
		{
			Debug.Log("Time out Error, Please try again!");
			return;
		}
		if (_Callback.m_eResult == EResult.k_EResultNotLoggedOn)
		{
			Debug.Log("Not Logged on Steam!");
			return;
		}
		if (_Callback.m_bUserNeedsToAcceptWorkshopLegalAgreement)
		{
			Debug.Log("You Have to accept the terms of agrement!");
			SteamFriends.ActivateGameOverlayToWebPage("steam://url/CommunityFilePage/397950");
		}
		Debug.Log("Succesfully Created a workshop item!" + _Callback.m_eResult);
		this.p_id = _Callback.m_nPublishedFileId;
		Debug.Log(this.p_id);
		this._Call = SteamUGC.StartItemUpdate(new AppId_t(397950U), this.p_id);
		this.setMapInfo();
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x0006C65C File Offset: 0x0006A85C
	private void onUGCUpdateItemSubmit(SubmitItemUpdateResult_t _Callback, bool bioFail)
	{
		Debug.Log(bioFail);
		Debug.Log(_Callback.m_eResult);
		Debug.Log("TEMP FILE FULLNAME " + this.m_MapFile.FullName);
		if (!this.m_RealMapName.Equals(this.m_MapFile.Name.Split(new char[]
		{
			'.'
		})[0]))
		{
			Debug.Log(string.Concat(new string[]
			{
				"DELETING FILE FOR IT WAS A TEMPORARY: ",
				Application.dataPath,
				"/maps/temp/",
				this.m_RealMapName,
				levelEditorManager.fileEnding
			}));
			File.Delete(Application.dataPath + "/maps/temp/" + this.m_RealMapName + levelEditorManager.fileEnding);
		}
		SteamUGC.DownloadItem(this.p_id, true);
		Application.OpenURL("steam://url/CommunityFilePage/" + this.p_id.ToString());
		DisplayManager.Instance().DisplayMessage("Map Uploaded!" + _Callback.m_eResult);
		ModalPanel.Instance().ClosePanel();
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x0006C778 File Offset: 0x0006A978
	private void onUGCQueryCompleted(SteamUGCQueryCompleted_t _Callback, bool bioFail)
	{
		DisplayManager.Instance().DisplayMessage(string.Concat(new object[]
		{
			"Query Returned! With: ",
			Mathf.Floor(_Callback.m_unTotalMatchingResults / 50f),
			" Pages ",
			_Callback.m_unTotalMatchingResults,
			" Results"
		}));
		bool nextPage = false;
		if (1f + Mathf.Floor(_Callback.m_unTotalMatchingResults / 50f) > (float)downloadManager.currentPage)
		{
			nextPage = true;
		}
		List<steamWorkshopMapInfo> list = new List<steamWorkshopMapInfo>();
		for (uint num = 0U; num < _Callback.m_unNumResultsReturned; num += 1U)
		{
			SteamUGCDetails_t det;
			SteamUGC.GetQueryUGCResult(_Callback.m_handle, num, out det);
			if (det.m_ulSteamIDOwner != SteamUser.GetSteamID().m_SteamID)
			{
				Debug.Log(det.m_rgchDescription);
				uint subs;
				SteamUGC.GetQueryUGCStatistic(_Callback.m_handle, num, EItemStatistic.k_EItemStatistic_NumSubscriptions, out subs);
				list.Add(new steamWorkshopMapInfo(det, subs));
			}
		}
		UnityEngine.Object.FindObjectOfType<downloadManager>().showWorkShopMaps(list, nextPage);
		SteamUGC.ReleaseQueryUGCRequest(_Callback.m_handle);
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x0006C894 File Offset: 0x0006AA94
	private void onRemoteStorageSubscribeResult(RemoteStorageSubscribePublishedFileResult_t _Callback, bool bioFail)
	{
		Debug.Log(string.Concat(new object[]
		{
			"Subscribtion for item: ",
			_Callback.m_nPublishedFileId,
			"     ",
			_Callback.m_eResult
		}));
		if (_Callback.m_eResult == EResult.k_EResultOK)
		{
			DisplayManager.Instance().DisplayMessage("Subscription sucessful!");
		}
		else
		{
			DisplayManager.Instance().DisplayMessage("Subscription Unsucessful! Try again later!");
		}
	}

	// Token: 0x060010AB RID: 4267 RVA: 0x0006C910 File Offset: 0x0006AB10
	public void onLeaderboardFindResultDownload(LeaderboardFindResult_t _CallResult, bool bioFail)
	{
		if (bioFail)
		{
			Debug.Log("onLeaderboardFindResultDownload BIOFAIL Error Returning");
			DisplayManager.Instance().DisplayMessage("LeaderBoard Error!");
			return;
		}
		DisplayManager.Instance().DisplayMessage("Found LeaderBoard!  onLeaderboardFindResultDownload " + _CallResult.m_hSteamLeaderboard.m_SteamLeaderboard);
		Debug.Log(string.Concat(new object[]
		{
			"Found LeaderBoard!",
			_CallResult.m_hSteamLeaderboard.m_SteamLeaderboard,
			"   SCORE: ",
			(int)this.m_time,
			"  Deaths: ",
			this.m_score
		}));
		int nRangeStart = 1;
		int nRangeEnd = 10;
		ELeaderboardDataRequest currentSortMethod = UnityEngine.Object.FindObjectOfType<leaderboardsManager>().currentSortMethod;
		switch (currentSortMethod)
		{
		case ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal:
			if (leaderboardsManager.SmallInitialization)
			{
				Debug.LogError("Currrent Activr Leaderboard ", UnityEngine.Object.FindObjectOfType<leaderboardsManager>());
				throw new Exception("Wronf Datarequest for small enable");
			}
			nRangeStart = 1;
			nRangeEnd = 20;
			break;
		case ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser:
			if (leaderboardsManager.SmallInitialization)
			{
				nRangeStart = -2;
				nRangeEnd = 2;
			}
			else
			{
				nRangeStart = -4;
				nRangeEnd = 5;
			}
			break;
		case ELeaderboardDataRequest.k_ELeaderboardDataRequestFriends:
			if (leaderboardsManager.SmallInitialization)
			{
				nRangeStart = 1;
				nRangeEnd = 10;
			}
			else
			{
				nRangeStart = 1;
				nRangeEnd = 10;
			}
			break;
		}
		SteamAPICall_t hAPICall = SteamUserStats.DownloadLeaderboardEntries(_CallResult.m_hSteamLeaderboard, currentSortMethod, nRangeStart, nRangeEnd);
		this.m_LeaderBoardScoresDownloaded.Set(hAPICall, null);
	}

	// Token: 0x060010AC RID: 4268 RVA: 0x0006CA74 File Offset: 0x0006AC74
	public void onLeaderboardFindResultUpload(LeaderboardFindResult_t _CallResult, bool bioFail)
	{
		if (bioFail)
		{
			Debug.Log("onLeaderboardFindResultUpload BIOFAIL Error Returning");
			DisplayManager.Instance().DisplayMessage("LeaderBoard Error!");
			return;
		}
		DisplayManager.Instance().DisplayMessage("Found LeaderBoard!  onLeaderboardFindResultUpload " + _CallResult.m_hSteamLeaderboard.m_SteamLeaderboard);
		Debug.Log(string.Concat(new object[]
		{
			"Found LeaderBoard!",
			SteamUserStats.GetLeaderboardName(_CallResult.m_hSteamLeaderboard),
			"   SCORE: ",
			(int)this.m_time,
			"  Score: ",
			this.m_score
		}));
		SteamAPICall_t hAPICall = SteamUserStats.DownloadLeaderboardEntries(_CallResult.m_hSteamLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser, 0, 0);
		this.m_LeaderBoardScoresDownloadedUploadScoresLater.Set(hAPICall, null);
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x0006CB38 File Offset: 0x0006AD38
	public void onLeaderBoardScoreUpload(LeaderboardScoreUploaded_t _CallResult, bool biofail)
	{
		if (biofail)
		{
			Debug.Log("BIOFAIL, returning");
			return;
		}
		Debug.Log((_CallResult.m_bSuccess >= 1) ? string.Concat(new object[]
		{
			"Sucessfully Uploaded Score! ",
			_CallResult.m_nScore,
			" : ",
			Time.frameCount
		}) : ("Failed To Upload Score for: " + _CallResult.m_hSteamLeaderboard));
		DisplayManager.Instance().DisplayMessage("Uploaded Score!" + _CallResult.m_bSuccess);
	}

	// Token: 0x060010AE RID: 4270 RVA: 0x0006CBDC File Offset: 0x0006ADDC
	private void onLeaderBoardScoreUploadKeepBest(LeaderboardScoreUploaded_t param, bool bIOFailure)
	{
		if (bIOFailure)
		{
			Debug.Log("BIOFAIL, returning");
			return;
		}
		if (param.m_bSuccess == 1)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Score Changed: ",
				param.m_bScoreChanged,
				" : ",
				Time.frameCount
			}));
			Debug.Log(string.Concat(new object[]
			{
				"Score uploaded: ",
				param.m_nScore,
				" : ",
				Time.frameCount
			}));
		}
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x0006CC80 File Offset: 0x0006AE80
	public void onLeaderboardScoresDownloadedUploadScoresLater(LeaderboardScoresDownloaded_t _Callresult, bool biofail)
	{
		if (biofail)
		{
			Debug.Log("Biofail!");
			return;
		}
		Debug.Log("Downloaded Scores, Uploading! onLeaderboardScoresDownloadedUploadScoresLater : " + Time.frameCount);
		int[] pDetails = new int[1];
		LeaderboardEntry_t leaderboardEntry_t;
		SteamUserStats.GetDownloadedLeaderboardEntry(_Callresult.m_hSteamLeaderboardEntries, 0, out leaderboardEntry_t, pDetails, 1);
		if ((int)(this.m_time * 1000f) < leaderboardEntry_t.m_nScore || _Callresult.m_cEntryCount < 1)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Better Score! Previous SCORE: ",
				leaderboardEntry_t.m_nScore,
				"  NEW SCORE: ",
				this.m_time,
				" : ",
				_Callresult.m_hSteamLeaderboard,
				" : ",
				Time.frameCount
			}));
			int[] pScoreDetails = new int[]
			{
				this.m_score
			};
			SteamAPICall_t hAPICall = SteamUserStats.UploadLeaderboardScore(_Callresult.m_hSteamLeaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate, (int)(this.m_time * 1000f), pScoreDetails, 1);
			this.m_LeaderBoardScoreUpload.Set(hAPICall, null);
			byte[] fileData = HistoryCollection.GetFileData(info.currentLevel);
			Singleton<SteamCloudStorageHandler>.Instance.UploadGhostFileToLeaderBoard(fileData, _Callresult.m_hSteamLeaderboard);
		}
		else
		{
			Singleton<DataRecorder>.Instance.StopRecording();
			Debug.Log("No new Highscore!  : " + Time.frameCount);
		}
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x0006CDE0 File Offset: 0x0006AFE0
	public void onLeaderboardScoresDownloaded(LeaderboardScoresDownloaded_t _Callresult, bool biofail)
	{
		Debug.Log("Scores Downloaded With entry count: " + _Callresult.m_cEntryCount);
		List<leaderboardScore> list = new List<leaderboardScore>();
		for (int i = 0; i < _Callresult.m_cEntryCount; i++)
		{
			int[] array = new int[1];
			LeaderboardEntry_t l;
			SteamUserStats.GetDownloadedLeaderboardEntry(_Callresult.m_hSteamLeaderboardEntries, i, out l, array, 1);
			list.Add(new leaderboardScore(l, array));
		}
		if (list.Count <= 0)
		{
			Debug.Log("No Scores have been made for this map yet!");
			UnityEngine.Object.FindObjectOfType<leaderboardsManager>().clearLeaderboard();
			UnityEngine.Object.FindObjectOfType<leaderboardsManager>().anim.SetBool("nothing", true);
			UnityEngine.Object.FindObjectOfType<leaderboardsManager>().ChangeLoadingState(false);
			return;
		}
		UnityEngine.Object.FindObjectOfType<leaderboardsManager>().recieveLeaderBoardEntries(list, this.m_time);
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x0006CEA0 File Offset: 0x0006B0A0
	public void subscibeToWorkshopItem(PublishedFileId_t p_id)
	{
		SteamAPICall_t hAPICall = SteamUGC.SubscribeItem(p_id);
		this.m_RemoteStorageSubscribeResult.Set(hAPICall, null);
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x0006CEC4 File Offset: 0x0006B0C4
	public void UploadScoreToLeaderBoard(string _id, float time, int score)
	{
		this.m_time = time;
		Debug.Log(string.Concat(new object[]
		{
			"Time: ",
			this.m_time,
			" Map: ",
			info.currentLevel,
			" Frame: ",
			Time.frameCount,
			" : ",
			Time.time
		}));
		this.m_score = score;
		SteamAPICall_t hAPICall = SteamUserStats.FindOrCreateLeaderboard("LEVEL " + _id, ELeaderboardSortMethod.k_ELeaderboardSortMethodAscending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeTimeMilliSeconds);
		this.m_LeaderboardFindResultUpload.Set(hAPICall, null);
		DisplayManager.Instance().DisplayMessage("Uploading To Steam!");
		Debug.Log("Uploading To Steam! To Leaderboard: " + _id);
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x0006CF84 File Offset: 0x0006B184
	public void DownloadLeaderboardEntries(string _id, bool campaign = false)
	{
		leaderboardsManager.ShowCampaignMap = campaign;
		string str = (!campaign) ? _id : _id.Split(new char[]
		{
			' '
		})[1];
		SteamAPICall_t hAPICall = SteamUserStats.FindOrCreateLeaderboard("LEVEL " + str, ELeaderboardSortMethod.k_ELeaderboardSortMethodAscending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeTimeMilliSeconds);
		this.m_LeaderboardFindResultDownload.Set(hAPICall, null);
		Debug.Log("Downloading Scores! From leaderboard: " + str);
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x0006CFE8 File Offset: 0x0006B1E8
	public void beginDownload(PublishedFileId_t p_id)
	{
		SteamUGC.DownloadItem(p_id, true);
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x0006CFF4 File Offset: 0x0006B1F4
	public void onItemDownloadedResult(DownloadItemResult_t _callback)
	{
		DisplayManager.Instance().DisplayMessage("Download: " + _callback.m_eResult);
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x0006D024 File Offset: 0x0006B224
	public void retrieveLeaderboardStatsfor(PublishedFileId_t _pid, steam_WorkshopHandler.LeaderBoardDelegate functionToSend)
	{
		this._currentDelegateFunction = functionToSend;
		SteamAPICall_t hAPICall = SteamUserStats.FindOrCreateLeaderboard("LEVEL " + _pid.m_PublishedFileId.ToString(), ELeaderboardSortMethod.k_ELeaderboardSortMethodAscending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeTimeMilliSeconds);
		this.m_LeaderboardFindResultV2.Set(hAPICall, null);
	}

	// Token: 0x060010B7 RID: 4279 RVA: 0x0006D064 File Offset: 0x0006B264
	public void OnLeaderborardFound(LeaderboardFindResult_t _callback, bool biofail)
	{
		if (biofail)
		{
			throw new Exception("Biofail");
		}
		Debug.LogError(string.Concat(new object[]
		{
			"LB VALUE: ",
			_callback.m_bLeaderboardFound,
			" :: ",
			_callback.m_hSteamLeaderboard.m_SteamLeaderboard
		}));
		if (_callback.m_hSteamLeaderboard.m_SteamLeaderboard == 0UL)
		{
			workShopmap_info.Instance().clearGrid();
			throw new Exception("LeaderBoard Not found Exception!");
		}
		SteamAPICall_t hAPICall = SteamUserStats.DownloadLeaderboardEntries(_callback.m_hSteamLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 1, 3);
		this.m_LeaderBoardScoresDownloadedV2.Set(hAPICall, null);
		SteamAPICall_t hAPICall2 = SteamUserStats.DownloadLeaderboardEntries(_callback.m_hSteamLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser, 0, 0);
		this.m_LeaderBoardScoresDownloadedSelfV3.Set(hAPICall2, null);
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x0006D128 File Offset: 0x0006B328
	public void OnLeaderBoardScoreDownloaded(LeaderboardScoresDownloaded_t _callback, bool biofail)
	{
		if (biofail)
		{
			throw new Exception("Biofail");
		}
		List<leaderboardScore> list = new List<leaderboardScore>();
		for (int i = 0; i < _callback.m_cEntryCount; i++)
		{
			int[] array = new int[1];
			LeaderboardEntry_t l;
			SteamUserStats.GetDownloadedLeaderboardEntry(_callback.m_hSteamLeaderboardEntries, i, out l, array, 1);
			list.Add(new leaderboardScore(l, array));
		}
		if (list.Count <= 0)
		{
			Debug.Log("No Scores have been made for this map yet!");
		}
		this._currentDelegateFunction(list.ToArray(), false);
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x0006D1B4 File Offset: 0x0006B3B4
	public void OnLeaderBoardScoreDownloadedSelf(LeaderboardScoresDownloaded_t _callback, bool biofail)
	{
		if (biofail)
		{
			throw new Exception("Biofail");
		}
		List<leaderboardScore> list = new List<leaderboardScore>();
		for (int i = 0; i < _callback.m_cEntryCount; i++)
		{
			int[] array = new int[1];
			LeaderboardEntry_t l;
			SteamUserStats.GetDownloadedLeaderboardEntry(_callback.m_hSteamLeaderboardEntries, i, out l, array, 1);
			list.Add(new leaderboardScore(l, array));
		}
		if (list.Count <= 0)
		{
			Debug.Log("No Scores have been made for this map yet!");
		}
		this._currentDelegateFunction(list.ToArray(), true);
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x0006D240 File Offset: 0x0006B440
	public void FetchMyLeaderboardEntry(string mapID, LeaderboardGhostHandler.GhostMethod ghostMethod)
	{
		Debug.Log(string.Concat(new object[]
		{
			"Called: FetchMyLeaderboardEntry: ",
			Time.frameCount,
			" With ghostmethod: ",
			ghostMethod.ToString()
		}));
		SteamAPICall_t hAPICall = SteamUserStats.FindOrCreateLeaderboard("LEVEL " + mapID, ELeaderboardSortMethod.k_ELeaderboardSortMethodAscending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeTimeMilliSeconds);
		switch (ghostMethod)
		{
		case LeaderboardGhostHandler.GhostMethod.Self:
			if (leaderboardsManager.P_MyGhost == null)
			{
				steam_WorkshopHandler.LoadSelfGhost = true;
				this.m_LeaderboardFindForSelfGhost.Set(hAPICall, null);
			}
			break;
		case LeaderboardGhostHandler.GhostMethod.BestOther:
			if (leaderboardsManager.P_OthersGhost == null)
			{
				steam_WorkshopHandler.LoadSelfGhost = false;
				this.m_LeaderboardFindForBestOtherGhost.Set(hAPICall, null);
			}
			break;
		case LeaderboardGhostHandler.GhostMethod.BestFriend:
			if (leaderboardsManager.P_OthersGhost == null)
			{
				steam_WorkshopHandler.LoadSelfGhost = false;
				this.m_LeaderboardFindForBestFriendGhost.Set(hAPICall, null);
			}
			break;
		case LeaderboardGhostHandler.GhostMethod.Other:
			if (leaderboardsManager.P_OthersGhost == null)
			{
				steam_WorkshopHandler.LoadSelfGhost = false;
				this.m_LeaderboardFindForOtherGhost.Set(hAPICall, null);
			}
			break;
		default:
			throw new Exception("Invalid: " + ghostMethod.ToString() + " Is not setup!");
		}
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x0006D364 File Offset: 0x0006B564
	private void OnLeaderboardFindForSelfGhost(LeaderboardFindResult_t param, bool bIOFailure)
	{
		if (bIOFailure)
		{
			Debug.Log("Biofail: " + bIOFailure);
			return;
		}
		Debug.Log("LeaderboardFound: Code " + param.m_bLeaderboardFound);
		if (param.m_bLeaderboardFound == 1)
		{
			Debug.Log("LeaderboardFound: Downloading Entries :" + Time.frameCount);
			SteamAPICall_t hAPICall = SteamUserStats.DownloadLeaderboardEntries(param.m_hSteamLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser, 0, 0);
			this.m_LeaderBoardScoresDownloadedSelfGhost.Set(hAPICall, null);
		}
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x0006D3EC File Offset: 0x0006B5EC
	private void OnLeaderboardFindForBestFriendGhost(LeaderboardFindResult_t param, bool bIOFailure)
	{
		if (bIOFailure)
		{
			Debug.Log("Biofail: " + bIOFailure);
			return;
		}
		Debug.Log("LeaderboardFound: Code " + param.m_bLeaderboardFound);
		if (param.m_bLeaderboardFound == 1)
		{
			Debug.Log("OnLeaderboardFindForBestFriendGhost: Downloading Entries :" + Time.frameCount);
			SteamAPICall_t hAPICall = SteamUserStats.DownloadLeaderboardEntries(param.m_hSteamLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestFriends, 1, 2);
			this.m_LeaderBoardScoresDownloadedSelfGhost.Set(hAPICall, null);
		}
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x0006D474 File Offset: 0x0006B674
	private void OnLeaderboardFindForBestOtherGhost(LeaderboardFindResult_t param, bool bIOFailure)
	{
		if (bIOFailure)
		{
			Debug.Log("Biofail: " + bIOFailure);
			return;
		}
		Debug.Log("LeaderboardFound: Code " + param.m_bLeaderboardFound);
		if (param.m_bLeaderboardFound == 1)
		{
			Debug.Log("LeaderboardFound: Downloading Entries :" + Time.frameCount);
			SteamAPICall_t hAPICall = SteamUserStats.DownloadLeaderboardEntries(param.m_hSteamLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 1, 1);
			this.m_LeaderBoardScoresDownloadedSelfGhost.Set(hAPICall, null);
		}
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x0006D4FC File Offset: 0x0006B6FC
	private void OnLeaderboardFindForOtherGhost(LeaderboardFindResult_t param, bool bIOFailure)
	{
		if (bIOFailure)
		{
			Debug.Log("Biofail: " + bIOFailure);
			return;
		}
		Debug.Log("LeaderboardFound: Code " + param.m_bLeaderboardFound);
		if (param.m_bLeaderboardFound == 1)
		{
			if (LeaderboardGhostHandler.CurrentOtherGhost == (CSteamID)0UL)
			{
				throw new Exception("SteamId is null: Current Other Ghost!");
			}
			SteamAPICall_t hAPICall = SteamUserStats.DownloadLeaderboardEntriesForUsers(param.m_hSteamLeaderboard, new CSteamID[]
			{
				LeaderboardGhostHandler.CurrentOtherGhost
			}, 1);
			this.m_LeaderBoardScoresDownloadedSelfGhost.Set(hAPICall, null);
			Debug.Log(string.Concat(new object[]
			{
				"OnLeaderboardFindForOtherGhost: Downloading Entries For: ",
				SteamFriends.GetFriendPersonaName(LeaderboardGhostHandler.CurrentOtherGhost),
				" Frame: ",
				Time.frameCount
			}));
		}
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x0006D5DC File Offset: 0x0006B7DC
	private void LeaderBoardScoresDownloadedSelfGhost(LeaderboardScoresDownloaded_t param, bool bIOFailure)
	{
		if (bIOFailure)
		{
			Debug.Log("Biofail: " + bIOFailure);
			return;
		}
		LeaderboardEntry_t leaderboardEntry_t;
		SteamUserStats.GetDownloadedLeaderboardEntry(param.m_hSteamLeaderboardEntries, 0, out leaderboardEntry_t, null, 0);
		if (param.m_cEntryCount == 0)
		{
			LeaderboardGhostHandler.SetGhostMethod = 0;
			leaderboardsManager.FlushAllGhosts();
			return;
		}
		if (LeaderboardGhostHandler.CurrentGhostMethod == LeaderboardGhostHandler.GhostMethod.BestFriend)
		{
			if (param.m_cEntryCount > 1)
			{
				LeaderboardEntry_t leaderboardEntry_t2;
				SteamUserStats.GetDownloadedLeaderboardEntry(param.m_hSteamLeaderboardEntries, 1, out leaderboardEntry_t2, null, 0);
				if (leaderboardEntry_t.m_steamIDUser == SteamUser.GetSteamID())
				{
					leaderboardEntry_t = leaderboardEntry_t2;
				}
			}
			else if (leaderboardEntry_t.m_steamIDUser == SteamUser.GetSteamID())
			{
				return;
			}
		}
		Debug.Log("Ghost Method: " + LeaderboardGhostHandler.CurrentGhostMethod.ToString());
		Debug.Log(string.Concat(new object[]
		{
			SteamUserStats.GetLeaderboardName(param.m_hSteamLeaderboard),
			" :Entries Downloaded: With: ",
			param.m_cEntryCount,
			" Entry Count",
			Time.frameCount,
			" SelfGhost: ",
			steam_WorkshopHandler.LoadSelfGhost,
			" For User: ",
			SteamFriends.GetFriendPersonaName(leaderboardEntry_t.m_steamIDUser),
			" Rank: ",
			leaderboardEntry_t.m_nGlobalRank
		}));
		leaderboardsManager componentInChildren = Manager.Instance().GetComponentInChildren<leaderboardsManager>(true);
		if (steam_WorkshopHandler.LoadSelfGhost)
		{
			componentInChildren.SetMyGhostData(leaderboardEntry_t.m_hUGC);
		}
		else
		{
			componentInChildren.ChangeGhostData(leaderboardEntry_t.m_hUGC);
		}
	}

	// Token: 0x04000DB5 RID: 3509
	private const int NUM_UPLOAD_RETRIES = 5;

	// Token: 0x04000DB6 RID: 3510
	private steam_WorkshopHandler.LeaderBoardDelegate _currentDelegateFunction;

	// Token: 0x04000DB7 RID: 3511
	private CallResult<CreateItemResult_t> m_CreateItemResult;

	// Token: 0x04000DB8 RID: 3512
	private CallResult<SubmitItemUpdateResult_t> m_SubmitItemResult;

	// Token: 0x04000DB9 RID: 3513
	private CallResult<SteamUGCQueryCompleted_t> m_UGCQUeruCompleted;

	// Token: 0x04000DBA RID: 3514
	private CallResult<RemoteStorageSubscribePublishedFileResult_t> m_RemoteStorageSubscribeResult;

	// Token: 0x04000DBB RID: 3515
	private CallResult<LeaderboardFindResult_t> m_LeaderboardFindResultDownload;

	// Token: 0x04000DBC RID: 3516
	private CallResult<LeaderboardFindResult_t> m_LeaderboardFindResultUpload;

	// Token: 0x04000DBD RID: 3517
	private CallResult<LeaderboardFindResult_t> m_LeaderboardFindResultV2;

	// Token: 0x04000DBE RID: 3518
	private CallResult<LeaderboardFindResult_t> m_LeaderboardFindForSelfGhost;

	// Token: 0x04000DBF RID: 3519
	private CallResult<LeaderboardFindResult_t> m_LeaderboardFindForBestOtherGhost;

	// Token: 0x04000DC0 RID: 3520
	private CallResult<LeaderboardFindResult_t> m_LeaderboardFindForBestFriendGhost;

	// Token: 0x04000DC1 RID: 3521
	private CallResult<LeaderboardFindResult_t> m_LeaderboardFindForOtherGhost;

	// Token: 0x04000DC2 RID: 3522
	private CallResult<LeaderboardScoreUploaded_t> m_LeaderBoardScoreUpload;

	// Token: 0x04000DC3 RID: 3523
	private CallResult<LeaderboardScoreUploaded_t> m_LeaderBoardScoreUploadKeepBest;

	// Token: 0x04000DC4 RID: 3524
	private CallResult<LeaderboardScoresDownloaded_t> m_LeaderBoardScoresDownloaded;

	// Token: 0x04000DC5 RID: 3525
	private CallResult<LeaderboardScoresDownloaded_t> m_LeaderBoardScoresDownloadedUploadScoresLater;

	// Token: 0x04000DC6 RID: 3526
	private CallResult<LeaderboardScoresDownloaded_t> m_LeaderBoardScoresDownloadedV2;

	// Token: 0x04000DC7 RID: 3527
	private CallResult<LeaderboardScoresDownloaded_t> m_LeaderBoardScoresDownloadedSelfV3;

	// Token: 0x04000DC8 RID: 3528
	private CallResult<LeaderboardScoresDownloaded_t> m_LeaderBoardScoresDownloadedSelfGhost;

	// Token: 0x04000DC9 RID: 3529
	private CallResult<LeaderboardScoresDownloaded_t> m_LeaderBoardScoresDownloadedBestOtherGhost;

	// Token: 0x04000DCA RID: 3530
	private CallResult<LeaderboardScoresDownloaded_t> m_LeaderBoardScoresDownloadedBestFriendGhost;

	// Token: 0x04000DCB RID: 3531
	private CallResult<LeaderboardScoresDownloaded_t> m_LeaderBoardScoresDownloadedOtherGhost;

	// Token: 0x04000DCC RID: 3532
	protected Callback<DownloadItemResult_t> m_DownloadItemResult;

	// Token: 0x04000DCD RID: 3533
	private PublishedFileId_t p_id;

	// Token: 0x04000DCE RID: 3534
	private UGCUpdateHandle_t _Call;

	// Token: 0x04000DCF RID: 3535
	private steamWorkshopMapInfo _currentMapInfo;

	// Token: 0x04000DD0 RID: 3536
	private FileInfo m_MapFile;

	// Token: 0x04000DD1 RID: 3537
	private string m_RealMapName = string.Empty;

	// Token: 0x04000DD2 RID: 3538
	private float m_time = -1f;

	// Token: 0x04000DD3 RID: 3539
	private int m_score;

	// Token: 0x04000DD4 RID: 3540
	private int _currentRetryCount;

	// Token: 0x04000DD5 RID: 3541
	private static bool LoadSelfGhost;

	// Token: 0x04000DD6 RID: 3542
	private static steam_WorkshopHandler workshophandler;

	// Token: 0x02000319 RID: 793
	// (Invoke) Token: 0x06001272 RID: 4722
	public delegate void LeaderBoardDelegate(leaderboardScore[] data, bool self);
}
