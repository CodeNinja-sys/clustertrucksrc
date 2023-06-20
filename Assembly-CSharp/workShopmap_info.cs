using System;
using System.IO;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000241 RID: 577
public class workShopmap_info : MonoBehaviour
{
	// Token: 0x17000289 RID: 649
	// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0005B464 File Offset: 0x00059664
	public steam_WorkshopHandler.LeaderBoardDelegate getDelegateFunction
	{
		get
		{
			return this._delegateFunction;
		}
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x0005B46C File Offset: 0x0005966C
	public static workShopmap_info Instance()
	{
		if (!workShopmap_info.workshophandler)
		{
			workShopmap_info.workshophandler = (UnityEngine.Object.FindObjectOfType(typeof(workShopmap_info)) as workShopmap_info);
			if (!workShopmap_info.workshophandler)
			{
				Debug.LogError("There needs to be one active workShopmap_info script on a GameObject in your scene.");
			}
		}
		return workShopmap_info.workshophandler;
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x0005B4C0 File Offset: 0x000596C0
	private void Awake()
	{
		this.m_DownloadResult = CallResult<RemoteStorageDownloadUGCResult_t>.Create(new CallResult<RemoteStorageDownloadUGCResult_t>.APIDispatchDelegate(this.OnDownloadImageComplete));
		this._delegateFunction = new steam_WorkshopHandler.LeaderBoardDelegate(this.assignScores);
		this._workshopPageButtonText = this._workshopPageButton.GetComponentInChildren<Text>();
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x0005B508 File Offset: 0x00059708
	private void Update()
	{
		this._workshopPageButton.interactable = (this._currentMap.m_nConsumerAppID.m_AppId != 0U);
	}

	// Token: 0x06000E05 RID: 3589 RVA: 0x0005B52C File Offset: 0x0005972C
	public void assignCurrentMapInfo(SteamUGCDetails_t info)
	{
		if (info.m_nPublishedFileId == this._currentMap.m_nPublishedFileId)
		{
			return;
		}
		this._workshopPageButton.gameObject.SetActive(true);
		this._workshopPageButtonText.text = "WORKSHOP PAGE";
		this._prevImg.sprite = this._loadingImage;
		Debug.Log(info.m_rgchTitle);
		this._currentMap = info;
		this._titleText.text = this._currentMap.m_rgchTitle;
		this._descText.text = this._currentMap.m_rgchDescription;
		SteamAPICall_t hAPICall = SteamRemoteStorage.UGCDownload(this._currentMap.m_hPreviewFile, 0U);
		this.m_DownloadResult.Set(hAPICall, null);
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x0005B5E8 File Offset: 0x000597E8
	public void assignCurrentMapInfo(string title)
	{
		this.clearGrid();
		this._workshopPageButton.gameObject.SetActive(false);
		string text = Application.dataPath + "/maps/previews/" + title + "/1.jpg";
		Debug.Log(text);
		this._prevImg.sprite = workShopmap_info.LoadImage(text);
		this._titleText.text = title;
		this._descText.text = "This Map Has Not Been Uploaded Yet!";
	}

	// Token: 0x06000E07 RID: 3591 RVA: 0x0005B658 File Offset: 0x00059858
	public void clearGrid()
	{
		foreach (Button button in this.grid.GetComponentsInChildren<Button>())
		{
			UnityEngine.Object.Destroy(button.gameObject);
		}
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x0005B694 File Offset: 0x00059894
	public void assignScores(leaderboardScore[] _scores, bool self)
	{
		if (self)
		{
			string text = "---_---_---_---";
			Debug.Log(text);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cellPrefab);
			gameObject.SetActive(true);
			gameObject.GetComponent<assignLeaderboardButtonInfo>().assignButtonValues(text, null, null, null, null);
			gameObject.transform.SetParent(this.grid, false);
			gameObject.SetActive(true);
			string text2 = string.Empty;
			if (!SteamFriends.RequestUserInformation(_scores[0].getEntry().m_steamIDUser, true))
			{
				text2 = SteamFriends.GetFriendPersonaName(_scores[0].getEntry().m_steamIDUser);
			}
			else
			{
				while (SteamFriends.GetFriendPersonaName(_scores[0].getEntry().m_steamIDUser).Contains("unknown"))
				{
					Debug.Log("Waiting For love");
				}
			}
			string message = string.Concat(new object[]
			{
				_scores[0].getEntry().m_nGlobalRank,
				"  ",
				text2,
				"  ",
				_scores[0].getEntry().m_nScore,
				"  ",
				_scores[0].getDetails()[0]
			});
			Debug.Log(message);
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.cellPrefab);
			gameObject2.SetActive(true);
			float num = (float)_scores[0].getEntry().m_nScore / 1000f;
			gameObject2.GetComponent<assignLeaderboardButtonInfo>().assignButtonValues(_scores[0].getEntry().m_nGlobalRank.ToString(), text2, num.ToString(), _scores[0].getDetails()[0].ToString(), null);
			gameObject2.transform.SetParent(this.grid, false);
			gameObject2.SetActive(true);
			return;
		}
		this.clearGrid();
		foreach (leaderboardScore leaderboardScore in _scores)
		{
			string text3 = string.Empty;
			if (!SteamFriends.RequestUserInformation(leaderboardScore.getEntry().m_steamIDUser, true))
			{
				text3 = SteamFriends.GetFriendPersonaName(leaderboardScore.getEntry().m_steamIDUser);
			}
			else
			{
				while (SteamFriends.GetFriendPersonaName(leaderboardScore.getEntry().m_steamIDUser).Contains("unknown"))
				{
					Debug.Log("Waiting For love");
				}
			}
			string message2 = string.Concat(new object[]
			{
				leaderboardScore.getEntry().m_nGlobalRank,
				"  ",
				text3,
				"  ",
				leaderboardScore.getEntry().m_nScore,
				"  ",
				leaderboardScore.getDetails()[0]
			});
			Debug.Log(message2);
			GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.cellPrefab);
			gameObject3.SetActive(true);
			float num2 = (float)leaderboardScore.getEntry().m_nScore / 1000f;
			gameObject3.GetComponent<assignLeaderboardButtonInfo>().assignButtonValues(leaderboardScore.getEntry().m_nGlobalRank.ToString(), text3, num2.ToString(), leaderboardScore.getDetails()[0].ToString(), null);
			gameObject3.transform.SetParent(this.grid, false);
			gameObject3.SetActive(true);
		}
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x0005B9F0 File Offset: 0x00059BF0
	public void downloadMap()
	{
		PublishedFileId_t nPublishedFileId = this._currentMap.m_nPublishedFileId;
		steam_WorkshopHandler.Instance().subscibeToWorkshopItem(nPublishedFileId);
		DisplayManager.Instance().DisplayMessage("Downloading map: " + this._currentMap.m_rgchTitle);
		steam_WorkshopHandler.Instance().beginDownload(nPublishedFileId);
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x0005BA40 File Offset: 0x00059C40
	public void openItemURL()
	{
		Debug.Log("Opening Url: steam://url/CommunityFilePage/" + this._currentMap.m_nPublishedFileId.ToString());
		SteamFriends.ActivateGameOverlayToWebPage("http://steamcommunity.com/app/397950");
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x0005BA6C File Offset: 0x00059C6C
	public void OnDownloadImageComplete(RemoteStorageDownloadUGCResult_t _callresult, bool biofail)
	{
		if (biofail)
		{
			throw new Exception("WTF");
		}
		DisplayManager.Instance().DisplayMessage(string.Concat(new object[]
		{
			"OnDownloadImageComplete, ",
			_callresult.m_eResult,
			" File: ",
			_callresult.m_pchFileName
		}));
		byte[] array = new byte[_callresult.m_nSizeInBytes];
		SteamRemoteStorage.UGCRead(_callresult.m_hFile, array, _callresult.m_nSizeInBytes, 0U, EUGCReadAction.k_EUGCRead_Close);
		Debug.Log(array.Length);
		Texture2D texture2D = new Texture2D(1, 1, TextureFormat.ARGB32, false);
		texture2D.LoadImage(array);
		Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
		this._prevImg.sprite = sprite;
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x0005BB4C File Offset: 0x00059D4C
	public static Sprite LoadImage(string filePath)
	{
		Texture2D texture2D = null;
		if (File.Exists(filePath))
		{
			byte[] data = File.ReadAllBytes(filePath);
			texture2D = new Texture2D(2, 2);
			texture2D.LoadImage(data);
		}
		return Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
	}

	// Token: 0x04000A9B RID: 2715
	private steam_WorkshopHandler.LeaderBoardDelegate _delegateFunction;

	// Token: 0x04000A9C RID: 2716
	private CallResult<RemoteStorageDownloadUGCResult_t> m_DownloadResult;

	// Token: 0x04000A9D RID: 2717
	private SteamUGCDetails_t _currentMap;

	// Token: 0x04000A9E RID: 2718
	public Transform grid;

	// Token: 0x04000A9F RID: 2719
	public GameObject cellPrefab;

	// Token: 0x04000AA0 RID: 2720
	public Button _downloadButton;

	// Token: 0x04000AA1 RID: 2721
	public Button _workshopPageButton;

	// Token: 0x04000AA2 RID: 2722
	public Sprite _loadingImage;

	// Token: 0x04000AA3 RID: 2723
	public Text _infoText;

	// Token: 0x04000AA4 RID: 2724
	public Text _titleText;

	// Token: 0x04000AA5 RID: 2725
	public Text _descText;

	// Token: 0x04000AA6 RID: 2726
	public Text _workshopPageButtonText;

	// Token: 0x04000AA7 RID: 2727
	public Image _prevImg;

	// Token: 0x04000AA8 RID: 2728
	private static workShopmap_info workshophandler;
}
