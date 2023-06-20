using System;
using System.Collections.Generic;
using System.IO;
using Steamworks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200023D RID: 573
public class setUploadInfo : MonoBehaviour
{
	// Token: 0x06000DCF RID: 3535 RVA: 0x00059CAC File Offset: 0x00057EAC
	private void Awake()
	{
		this.m_SteamUGCQueryCompleted = CallResult<SteamUGCQueryCompleted_t>.Create(new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.onSteamUGCQueryResult));
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x00059CC8 File Offset: 0x00057EC8
	public void onClickDone()
	{
		string text = string.Empty;
		string text2 = string.Empty;
		if (string.IsNullOrEmpty(this._title.text) && this._newMapToggle.isOn)
		{
			DisplayManager.Instance().DisplayMessage("Mapname cannot be empty!");
			return;
		}
		if (string.IsNullOrEmpty(this._desc.text))
		{
			DisplayManager.Instance().DisplayMessage("Item Description cannot be empty!");
			return;
		}
		if (this._currentTags.Count < 1)
		{
			DisplayManager.Instance().DisplayMessage("Tags cannot be none!");
			return;
		}
		if (string.IsNullOrEmpty(this._changeNotes.text))
		{
			DisplayManager.Instance().DisplayMessage("Change Notes cannot be empty!");
			return;
		}
		string[] tags = this._currentTags.ToArray();
		bool isOn = this._existingMapToggle.isOn;
		FileInfo currentMapFileToUpload = this.m_CurrentMapFileToUpload;
		if (isOn)
		{
			Debug.Log("UPDATING!");
			text = this.workshopMaps[this._mapDropdown.value].getDetails().m_rgchTitle;
			text2 = this.workshopMaps[this._mapDropdown.value].getDetails().m_nPublishedFileId.ToString();
			Debug.Log("PUBLISH ID: " + text2);
		}
		else
		{
			Debug.Log("NEW MAP!!");
			text = this._title.text;
		}
		if (string.IsNullOrEmpty(text))
		{
			DisplayManager.Instance().DisplayMessage("Mapname cannot be empty!");
			return;
		}
		steamWorkshopMapInfo info = new steamWorkshopMapInfo(text, this._desc.text, tags, this._changeNotes.text, text2, currentMapFileToUpload);
		this.uploadInfoSet(info, isOn);
		this.clean();
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x00059E7C File Offset: 0x0005807C
	private void showLocalFiles()
	{
		foreach (Button button in this.fileToUploadGrid.GetComponentsInChildren<Button>())
		{
			UnityEngine.Object.Destroy(button.gameObject);
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(levelEditorManager.DefaultFilepath);
		FileInfo[] files = directoryInfo.GetFiles();
		if (files.Length < 1)
		{
			DisplayManager.Instance().DisplayMessage("No Saved Maps!");
			return;
		}
		foreach (FileInfo fileInfo in files)
		{
			if (!fileInfo.Name.EndsWith(".meta") && fileInfo.Name.EndsWith(levelEditorManager.fileEnding))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uploadCellPrefab);
				gameObject.SetActive(true);
				gameObject.GetComponent<getFile>().setFile(fileInfo);
				gameObject.GetComponent<assignLeaderboardButtonInfo>().assignButtonValues(fileInfo.Name.Split(new char[]
				{
					'.'
				})[0], string.Empty, fileInfo.Name.Split(new char[]
				{
					'.'
				})[0], string.Empty, null);
				gameObject.transform.SetParent(this.fileToUploadGrid.transform, false);
			}
		}
		Debug.Log("FOUND: " + this.workshopMaps.Count + "  WORKSHOPMaps!");
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x00059FE0 File Offset: 0x000581E0
	private void OnDisable()
	{
		this._currentTags.Clear();
		this._UIBlocker.DeActivate();
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x00059FF8 File Offset: 0x000581F8
	private void OnEnable()
	{
		this._UIBlocker.Activate();
		this._title.text = string.Empty;
		this._desc.text = string.Empty;
		this._changeNotes.text = string.Empty;
		this._mapDropdown.ClearOptions();
		this.showLocalFiles();
		FileInfo currentMapFile = levelEditorManager.Instance().getCurrentMapFile();
		this.dropdownValue = null;
		if (currentMapFile != null)
		{
			Debug.Log(currentMapFile.FullName);
			if (currentMapFile.FullName.Contains("Steam\\"))
			{
				Debug.Log(2);
				this._newMapToggle.isOn = false;
				this._existingMapToggle.isOn = true;
				this.dropdownValue = currentMapFile.Name.Split(new char[]
				{
					'.'
				})[0];
				Debug.Log(this.dropdownValue);
			}
			else
			{
				Debug.Log(3);
				this._newMapToggle.isOn = true;
				this._existingMapToggle.isOn = false;
			}
		}
		else
		{
			Debug.Log(4);
			this._newMapToggle.isOn = true;
			this._existingMapToggle.isOn = false;
		}
		this.disable();
		UGCQueryHandle_t handle = SteamUGC.CreateQueryUserUGCRequest(SteamUser.GetSteamID().GetAccountID(), EUserUGCList.k_EUserUGCList_Published, EUGCMatchingUGCType.k_EUGCMatchingUGCType_Items, EUserUGCListSortOrder.k_EUserUGCListSortOrder_VoteScoreDesc, SteamUtils.GetAppID(), SteamUtils.GetAppID(), 1U);
		SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(handle);
		this.m_SteamUGCQueryCompleted.Set(hAPICall, null);
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x0005A160 File Offset: 0x00058360
	public void onClickCancel()
	{
		this.clean();
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x0005A168 File Offset: 0x00058368
	public void NEWtoggleValueChanged()
	{
		if (this._existingMapToggle.isOn)
		{
			this._existingMapToggle.isOn = !this._newMapToggle.isOn;
			this.disable();
		}
		else
		{
			this._newMapToggle.isOn = true;
		}
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x0005A1B8 File Offset: 0x000583B8
	public void EXISTINGtoggleValueChanged()
	{
		if (this._newMapToggle.isOn)
		{
			this._newMapToggle.isOn = !this._existingMapToggle.isOn;
			this.disable();
		}
		else
		{
			this._existingMapToggle.isOn = true;
		}
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x0005A208 File Offset: 0x00058408
	private void disable()
	{
		if (this._newMapToggle.isOn)
		{
			this._mapDropdown.interactable = false;
			this._title.interactable = true;
			return;
		}
		if (this._existingMapToggle.isOn)
		{
			this._title.interactable = false;
			this._mapDropdown.interactable = true;
		}
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x0005A268 File Offset: 0x00058468
	private void clean()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x0005A278 File Offset: 0x00058478
	public void changedCurrentWorkShopMap(int value)
	{
		Debug.Log("Changed WorkShopmap to: " + this.workshopMaps[value].getDetails().m_rgchTitle.ToString());
		this._desc.text = this.workshopMaps[value].getDetails().m_rgchDescription;
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x0005A2D8 File Offset: 0x000584D8
	private void upload(steamWorkshopMapInfo _info)
	{
		DisplayManager.Instance().DisplayMessage("Uploading...");
		string path = levelEditorManager.DefaultFilepath + "\\" + _info.getTitle() + levelEditorManager.fileEnding;
		if (!File.Exists(path))
		{
			Debug.Log("TEMP MAP!");
		}
		else
		{
			Debug.Log("SAVED MAP!");
		}
		steam_WorkshopHandler.Instance().uploadMapToSTeamWorkshop(_info);
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x0005A340 File Offset: 0x00058540
	private void upload(string p_id, steamWorkshopMapInfo _info)
	{
		DisplayManager.Instance().DisplayMessage("Updating...");
		steam_WorkshopHandler.Instance().updateMapToSteamWorkshop(p_id, _info);
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x0005A360 File Offset: 0x00058560
	public void uploadInfoSet(steamWorkshopMapInfo _info, bool updating)
	{
		Debug.Log("Updating: " + updating);
		if (updating)
		{
			this.upload(_info.getPublishID(), _info);
		}
		else
		{
			this.upload(_info);
		}
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x0005A3A4 File Offset: 0x000585A4
	private void setCurrentMap(FileInfo _file)
	{
		this.m_CurrentMapFileToUpload = _file;
		this._title.text = this.m_CurrentMapFileToUpload.Name.Split(new char[]
		{
			'.'
		})[0];
		Debug.Log("New Current File: " + _file.FullName);
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x0005A3F8 File Offset: 0x000585F8
	public void OnSubmit(BaseEventData p)
	{
		PointerEventData pointerEventData = p as PointerEventData;
		if (p.selectedObject.tag == "loadMap" && p.selectedObject.GetComponent<Button>().image != this.m_currentMapButtonImage)
		{
			if (this.m_currentMapButtonImage != null)
			{
				this.m_currentMapButtonImage.color = Color.white;
			}
			this.m_currentMapButtonImage = p.selectedObject.GetComponent<Button>().image;
			this.m_currentMapButtonImage.color = Color.red;
			this.setCurrentMap(p.selectedObject.GetComponent<getFile>().getFileInfo());
		}
		if (p.selectedObject.tag == "tag")
		{
			string text = p.selectedObject.GetComponentInChildren<Text>().text;
			if (this._currentTags.Contains(text))
			{
				p.selectedObject.GetComponent<Button>().image.color = Color.white;
				this._currentTags.Remove(text);
				return;
			}
			this._currentTags.Add(text);
			p.selectedObject.GetComponent<Button>().image.color = Color.red;
		}
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x0005A530 File Offset: 0x00058730
	public void onSteamUGCQueryResult(SteamUGCQueryCompleted_t _callResult, bool biofail)
	{
		if (biofail)
		{
			MonoBehaviour.print("biofail");
			return;
		}
		DisplayManager.Instance().DisplayMessage("Query Returned! With: " + _callResult.m_unTotalMatchingResults);
		this.workshopMaps.Clear();
		for (uint num = 0U; num < _callResult.m_unNumResultsReturned; num += 1U)
		{
			SteamUGCDetails_t det;
			SteamUGC.GetQueryUGCResult(_callResult.m_handle, num, out det);
			if (det.m_ulSteamIDOwner == SteamUser.GetSteamID().m_SteamID)
			{
				uint subs;
				SteamUGC.GetQueryUGCStatistic(_callResult.m_handle, num, EItemStatistic.k_EItemStatistic_NumSubscriptions, out subs);
				this.workshopMaps.Add(new steamWorkshopMapInfo(det, subs));
				this._mapDropdown.options.Add(new Dropdown.OptionData(det.m_rgchTitle));
				if (det.m_rgchTitle.Equals(this.dropdownValue))
				{
					this._mapDropdown.value = this._mapDropdown.options.Count - 1;
					this._mapDropdown.RefreshShownValue();
				}
			}
		}
		SteamUGC.ReleaseQueryUGCRequest(_callResult.m_handle);
	}

	// Token: 0x04000A63 RID: 2659
	public GameObject fileToUploadGrid;

	// Token: 0x04000A64 RID: 2660
	public GameObject uploadCellPrefab;

	// Token: 0x04000A65 RID: 2661
	public InputField _desc;

	// Token: 0x04000A66 RID: 2662
	public InputField _changeNotes;

	// Token: 0x04000A67 RID: 2663
	public InputField _title;

	// Token: 0x04000A68 RID: 2664
	public Dropdown _mapDropdown;

	// Token: 0x04000A69 RID: 2665
	public Toggle _newMapToggle;

	// Token: 0x04000A6A RID: 2666
	public Toggle _existingMapToggle;

	// Token: 0x04000A6B RID: 2667
	public List<steamWorkshopMapInfo> workshopMaps = new List<steamWorkshopMapInfo>();

	// Token: 0x04000A6C RID: 2668
	public UIblocker _UIBlocker;

	// Token: 0x04000A6D RID: 2669
	private Image m_currentMapButtonImage;

	// Token: 0x04000A6E RID: 2670
	private List<FileInfo> localMaps = new List<FileInfo>();

	// Token: 0x04000A6F RID: 2671
	private FileInfo m_CurrentMapFileToUpload;

	// Token: 0x04000A70 RID: 2672
	private List<string> _currentTags = new List<string>();

	// Token: 0x04000A71 RID: 2673
	private CallResult<SteamUGCQueryCompleted_t> m_SteamUGCQueryCompleted;

	// Token: 0x04000A72 RID: 2674
	private string dropdownValue;
}
