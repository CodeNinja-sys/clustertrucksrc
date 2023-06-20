using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020002EE RID: 750
public class downloadManager : MonoBehaviour
{
	// Token: 0x06001185 RID: 4485 RVA: 0x000713B0 File Offset: 0x0006F5B0
	public static downloadManager Instance()
	{
		if (!downloadManager._downloadManager)
		{
			downloadManager._downloadManager = (UnityEngine.Object.FindObjectOfType(typeof(downloadManager)) as downloadManager);
			if (!downloadManager._downloadManager)
			{
				Debug.LogError("There needs to be one active downloadManager script on a GameObject in your scene.");
			}
		}
		return downloadManager._downloadManager;
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x00071404 File Offset: 0x0006F604
	private void Start()
	{
		this.m_SteamUGCQueryCompleted = CallResult<SteamUGCQueryCompleted_t>.Create(new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.onSteamUGCQueryResult));
		this.m_PersonsaStateChanged = Callback<PersonaStateChange_t>.Create(new Callback<PersonaStateChange_t>.DispatchDelegate(this.OnPersonaStateChanged));
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x00071440 File Offset: 0x0006F640
	private void OnEnable()
	{
		this._previousPageButton.interactable = false;
		this._nextPageButton.interactable = false;
		downloadManager.currentPage = 1;
		downloadManager.currentSortingMethod = new downloadManager.sortMethodExtend(EUGCQuery.k_EUGCQuery_RankedByVotesUp);
		this.InitializeDownloadgrid();
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x00071480 File Offset: 0x0006F680
	private void Update()
	{
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x00071484 File Offset: 0x0006F684
	public void incrementPage()
	{
		downloadManager.currentPage++;
		this.onAuthorSortMethodChanged();
	}

	// Token: 0x0600118A RID: 4490 RVA: 0x00071498 File Offset: 0x0006F698
	public void decrementPage()
	{
		downloadManager.currentPage--;
		if (downloadManager.currentPage < 1)
		{
			throw new InvalidOperationException("Page Cannot be Zero or Negative");
		}
		this.onAuthorSortMethodChanged();
	}

	// Token: 0x0600118B RID: 4491 RVA: 0x000714D0 File Offset: 0x0006F6D0
	public void showWorkShopMaps(List<steamWorkshopMapInfo> _list, bool _nextPage)
	{
		Debug.Log(string.Concat(new object[]
		{
			"LIST SIZE: ",
			_list.Count,
			"  _NextPage: ",
			_nextPage,
			" CurrentPage: ",
			downloadManager.currentPage
		}));
		this._nextPageButton.interactable = _nextPage;
		if (downloadManager.currentPage > 1)
		{
			this._previousPageButton.interactable = true;
		}
		else
		{
			this._previousPageButton.interactable = false;
		}
		DisplayManager.Instance().DisplayMessage("Loading Maps from workshop...");
		foreach (steamWorkshopMapInfo steamWorkshopMapInfo in _list)
		{
			string name = string.Empty;
			if (!SteamFriends.RequestUserInformation(new CSteamID(steamWorkshopMapInfo.getDetails().m_ulSteamIDOwner), true))
			{
				name = SteamFriends.GetFriendPersonaName(new CSteamID(steamWorkshopMapInfo.getDetails().m_ulSteamIDOwner));
			}
			else
			{
				while (SteamFriends.GetFriendPersonaName(new CSteamID(steamWorkshopMapInfo.getDetails().m_ulSteamIDOwner)).Contains("unknown"))
				{
					Debug.Log("Waiting for Love");
				}
				name = SteamFriends.GetFriendPersonaName(new CSteamID(steamWorkshopMapInfo.getDetails().m_ulSteamIDOwner));
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.downloadCellPrefab);
			gameObject.SetActive(true);
			gameObject.GetComponent<Button>().interactable = true;
			gameObject.transform.SetParent(this.downloadGrid.transform, false);
			string death = (steamWorkshopMapInfo.getDetails().m_unVotesUp > 0U) ? steamWorkshopMapInfo.getDetails().m_unVotesUp.ToString() : "UNRATED";
			gameObject.GetComponent<assignLeaderboardButtonInfo>().assignButtonValues(steamWorkshopMapInfo.getDetails().m_rgchTitle, name, steamWorkshopMapInfo.getSubs().ToString(), death, downloadManager.UnixTimeStampToDateTime(steamWorkshopMapInfo.getDetails().m_rtimeCreated).ToString(), steamWorkshopMapInfo.getDetails().m_nPublishedFileId, steamWorkshopMapInfo.getDetails());
			gameObject.name = steamWorkshopMapInfo.getDetails().m_rgchURL;
		}
		DisplayManager.Instance().DisplayMessage("Done!");
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x00071740 File Offset: 0x0006F940
	public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
	{
		DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		result = result.AddSeconds(unixTimeStamp).ToLocalTime();
		return result;
	}

	// Token: 0x0600118D RID: 4493 RVA: 0x00071774 File Offset: 0x0006F974
	public void OnSubmit(BaseEventData p)
	{
		PointerEventData pointerEventData = p as PointerEventData;
		if (p.selectedObject.tag == "workshopMap")
		{
			workShopmap_info.Instance().assignCurrentMapInfo(p.selectedObject.GetComponent<assignLeaderboardButtonInfo>().getDetails());
		}
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x000717C0 File Offset: 0x0006F9C0
	public void switchSortingMethod(int method)
	{
		if (downloadManager.currentSortingMethod.getSelf())
		{
			EUserUGCListSortOrder eSortOrder = EUserUGCListSortOrder.k_EUserUGCListSortOrder_VoteScoreDesc;
			if (method != 0)
			{
				if (method != 1)
				{
					if (method == 12)
					{
						eSortOrder = EUserUGCListSortOrder.k_EUserUGCListSortOrder_SubscriptionDateDesc;
					}
				}
				else
				{
					eSortOrder = EUserUGCListSortOrder.k_EUserUGCListSortOrder_CreationOrderDesc;
				}
			}
			else
			{
				eSortOrder = EUserUGCListSortOrder.k_EUserUGCListSortOrder_VoteScoreDesc;
			}
			this.clearMapGrid();
			UGCQueryHandle_t handle = SteamUGC.CreateQueryUserUGCRequest(SteamUser.GetSteamID().GetAccountID(), EUserUGCList.k_EUserUGCList_Published, EUGCMatchingUGCType.k_EUGCMatchingUGCType_Items, eSortOrder, SteamUtils.GetAppID(), SteamUtils.GetAppID(), (uint)downloadManager.currentPage);
			SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(handle);
			this.m_SteamUGCQueryCompleted.Set(hAPICall, null);
			return;
		}
		downloadManager.currentSortingMethod = new downloadManager.sortMethodExtend((EUGCQuery)method);
		Debug.Log("Sorting Method: " + downloadManager.currentSortingMethod.getSelf());
		this.InitializeDownloadgrid();
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x00071880 File Offset: 0x0006FA80
	public void InitializeDownloadgrid()
	{
		this.clearMapGrid();
		if (this.getAuthorDropdown().value == 2)
		{
			this.getAuthorDropdown().value = 0;
		}
		steam_WorkshopHandler.Instance().getworkshopMaps();
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x000718BC File Offset: 0x0006FABC
	public Dropdown getAuthorDropdown()
	{
		return this._authorDropDown;
	}

	// Token: 0x06001191 RID: 4497 RVA: 0x000718C4 File Offset: 0x0006FAC4
	public void switchDisplayMethod(int method)
	{
		if (method < 0)
		{
			Debug.Log("Collecting OWN maps!");
			downloadManager.currentSortingMethod = new downloadManager.sortMethodExtend();
			this.switchSortingMethod(0);
			return;
		}
		downloadManager.currentSortingMethod = new downloadManager.sortMethodExtend((EUGCQuery)method);
		Debug.Log("Sorting Method: " + downloadManager.currentSortingMethod.getSortMethod().ToString());
		this.InitializeDownloadgrid();
	}

	// Token: 0x06001192 RID: 4498 RVA: 0x00071928 File Offset: 0x0006FB28
	public void OnPersonaStateChanged(PersonaStateChange_t _callBack)
	{
		Debug.Log("RETURNED: " + SteamFriends.GetFriendPersonaName(new CSteamID(_callBack.m_ulSteamID)));
	}

	// Token: 0x06001193 RID: 4499 RVA: 0x00071958 File Offset: 0x0006FB58
	public void onAuthorSortMethodChanged()
	{
		this.clearMapGrid();
		int value = this._authorDropDown.value;
		if (value == 0)
		{
			if (downloadManager.currentSortingMethod.getSortMethod() != (EUGCQuery)value || downloadManager.currentSortingMethod.getSelf())
			{
				this.ResetPage();
			}
			this.switchDisplayMethod(0);
		}
		if (value == 1)
		{
			if (downloadManager.currentSortingMethod.getSortMethod() != (EUGCQuery)value || downloadManager.currentSortingMethod.getSelf())
			{
				this.ResetPage();
			}
			this.switchDisplayMethod(5);
		}
		if (value == 2)
		{
			if (!downloadManager.currentSortingMethod.getSelf())
			{
				this.ResetPage();
			}
			this.switchDisplayMethod(-1);
		}
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x00071A00 File Offset: 0x0006FC00
	public void onSteamUGCQueryResult(SteamUGCQueryCompleted_t _callResult, bool biofail)
	{
		if (biofail)
		{
			MonoBehaviour.print("biofail");
			return;
		}
		DisplayManager.Instance().DisplayMessage("Query Returned! With: " + _callResult.m_unTotalMatchingResults);
		bool nextPage = false;
		if (1f + Mathf.Floor(_callResult.m_unTotalMatchingResults / 50f) > (float)downloadManager.currentPage)
		{
			nextPage = true;
		}
		List<steamWorkshopMapInfo> list = new List<steamWorkshopMapInfo>();
		for (uint num = 0U; num < _callResult.m_unNumResultsReturned; num += 1U)
		{
			SteamUGCDetails_t det;
			SteamUGC.GetQueryUGCResult(_callResult.m_handle, num, out det);
			if (det.m_ulSteamIDOwner == SteamUser.GetSteamID().m_SteamID)
			{
				uint subs;
				SteamUGC.GetQueryUGCStatistic(_callResult.m_handle, num, EItemStatistic.k_EItemStatistic_NumSubscriptions, out subs);
				list.Add(new steamWorkshopMapInfo(det, subs));
			}
		}
		SteamUGC.ReleaseQueryUGCRequest(_callResult.m_handle);
		this.showWorkShopMaps(list, nextPage);
	}

	// Token: 0x06001195 RID: 4501 RVA: 0x00071AE4 File Offset: 0x0006FCE4
	public void clearMapGrid()
	{
		Debug.Log("Clear Grid!");
		foreach (Button button in this.downloadGrid.GetComponentsInChildren<Button>(true))
		{
			UnityEngine.Object.Destroy(button.gameObject);
		}
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x00071B2C File Offset: 0x0006FD2C
	private void ResetPage()
	{
		this._previousPageButton.interactable = false;
		this._nextPageButton.interactable = false;
		downloadManager.currentPage = 1;
	}

	// Token: 0x04000EAF RID: 3759
	private CallResult<SteamUGCQueryCompleted_t> m_SteamUGCQueryCompleted;

	// Token: 0x04000EB0 RID: 3760
	public string get;

	// Token: 0x04000EB1 RID: 3761
	public GameObject downloadGrid;

	// Token: 0x04000EB2 RID: 3762
	public GameObject downloadCellPrefab;

	// Token: 0x04000EB3 RID: 3763
	public Button _nextPageButton;

	// Token: 0x04000EB4 RID: 3764
	public Button _previousPageButton;

	// Token: 0x04000EB5 RID: 3765
	public static downloadManager.sortMethodExtend currentSortingMethod = new downloadManager.sortMethodExtend(EUGCQuery.k_EUGCQuery_RankedByVote);

	// Token: 0x04000EB6 RID: 3766
	protected Callback<PersonaStateChange_t> m_PersonsaStateChanged;

	// Token: 0x04000EB7 RID: 3767
	public Dropdown _authorDropDown;

	// Token: 0x04000EB8 RID: 3768
	public static int currentPage = 1;

	// Token: 0x04000EB9 RID: 3769
	public static int resultsPerPage = 10;

	// Token: 0x04000EBA RID: 3770
	private static downloadManager _downloadManager;

	// Token: 0x020002EF RID: 751
	public class sortMethodExtend
	{
		// Token: 0x06001197 RID: 4503 RVA: 0x00071B4C File Offset: 0x0006FD4C
		public sortMethodExtend(EUGCQuery _set)
		{
			this.m_sortMethod = _set;
			this.m_self = false;
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00071B64 File Offset: 0x0006FD64
		public sortMethodExtend()
		{
			this.m_self = true;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00071B74 File Offset: 0x0006FD74
		public EUGCQuery getSortMethod()
		{
			return this.m_sortMethod;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00071B7C File Offset: 0x0006FD7C
		public bool getSelf()
		{
			return this.m_self;
		}

		// Token: 0x04000EBB RID: 3771
		private EUGCQuery m_sortMethod;

		// Token: 0x04000EBC RID: 3772
		private bool m_self;
	}
}
