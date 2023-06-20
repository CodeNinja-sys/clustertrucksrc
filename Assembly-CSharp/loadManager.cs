using System;
using System.Collections.Generic;
using System.IO;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200028E RID: 654
public class loadManager : MonoBehaviour
{
	// Token: 0x06000FAF RID: 4015 RVA: 0x00065B58 File Offset: 0x00063D58
	public static loadManager Instance()
	{
		if (!loadManager._loadManager)
		{
			loadManager._loadManager = (UnityEngine.Object.FindObjectOfType(typeof(loadManager)) as loadManager);
			if (!loadManager._loadManager)
			{
				Debug.LogError("There needs to be one active loadManager script on a GameObject in your scene.");
			}
		}
		return loadManager._loadManager;
	}

	// Token: 0x06000FB0 RID: 4016 RVA: 0x00065BAC File Offset: 0x00063DAC
	private void Awake()
	{
		this.m_SteamUGCDetailsResult = CallResult<SteamUGCQueryCompleted_t>.Create(new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnSteamUGCDetailsResult));
		this.m_SteamUGCDetailsResultONLYSELF = CallResult<SteamUGCQueryCompleted_t>.Create(new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnSteamUGCDetailsResultONLYSELF));
	}

	// Token: 0x06000FB1 RID: 4017 RVA: 0x00065BE8 File Offset: 0x00063DE8
	private void Update()
	{
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x00065BEC File Offset: 0x00063DEC
	private void OnDisable()
	{
		this._UIBlocker.DeActivate();
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x00065BFC File Offset: 0x00063DFC
	private void OnEnable()
	{
		this._UIBlocker.Activate();
		this.clearGrid();
		this.showCustomMaps();
		this.showDownloadedWorkShopMaps();
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x00065C1C File Offset: 0x00063E1C
	public void showDownloadedWorkShopMaps()
	{
		string path = "C:/Program Files (x86)/Steam/steamapps/workshop/content/397950";
		if (!Directory.Exists(path))
		{
			return;
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(path);
		DirectoryInfo[] directories = directoryInfo.GetDirectories();
		List<PublishedFileId_t> list = new List<PublishedFileId_t>(directories.Length);
		foreach (DirectoryInfo directoryInfo2 in directories)
		{
			list.Add(new PublishedFileId_t(ulong.Parse(directoryInfo2.Name)));
		}
		UGCQueryHandle_t handle = SteamUGC.CreateQueryUGCDetailsRequest(list.ToArray(), (uint)list.Count);
		SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(handle);
		if (!this.OnlySelf)
		{
			this.m_SteamUGCDetailsResult.Set(hAPICall, null);
			return;
		}
		this.m_SteamUGCDetailsResultONLYSELF.Set(hAPICall, null);
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x00065CD0 File Offset: 0x00063ED0
	public bool showDownloadedWorkShopMaps(List<FileInfo> workshopMaps)
	{
		Debug.Log("FOUND: " + workshopMaps.Count + "  WORKSHOPMaps!");
		foreach (FileInfo fileInfo in workshopMaps)
		{
			if (!fileInfo.Name.EndsWith(".meta") && fileInfo.Name.EndsWith(levelEditorManager.fileEnding))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.loadCellPrefab);
				gameObject.SetActive(true);
				gameObject.GetComponent<Button>().interactable = true;
				gameObject.transform.SetParent(this.loadGrid.transform);
				gameObject.GetComponent<getFile>().setFile(fileInfo);
				gameObject.GetComponent<assignLeaderboardButtonInfo>().assignButtonValues(fileInfo.Name.Split(new char[]
				{
					'.'
				})[0], string.Empty, fileInfo.Name.Split(new char[]
				{
					'.'
				})[0], fileInfo.CreationTimeUtc.ToString(), null);
				gameObject.name = fileInfo.FullName;
			}
		}
		return true;
	}

	// Token: 0x06000FB6 RID: 4022 RVA: 0x00065E10 File Offset: 0x00064010
	public void showCustomMaps()
	{
		List<FileInfo> list = new List<FileInfo>();
		foreach (getFile getFile in this.loadGrid.GetComponentsInChildren<getFile>())
		{
			if (getFile.getFileInfo().FullName.Contains("Steam/"))
			{
				list.Add(getFile.getFileInfo());
				Debug.Log(getFile.getFileInfo().Name);
			}
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(levelEditorManager.CurrentFilePath + "/");
		if (!Directory.Exists(directoryInfo.FullName))
		{
			Directory.CreateDirectory(directoryInfo.FullName);
		}
		FileInfo[] files = directoryInfo.GetFiles();
		Debug.Log("FOUND: " + files.Length + "  Maps!");
		foreach (FileInfo fileInfo in files)
		{
			bool flag = false;
			foreach (FileInfo fileInfo2 in list)
			{
				if (fileInfo2.Name == fileInfo.Name && fileInfo.Length == fileInfo2.Length)
				{
					Debug.Log(fileInfo.Name + " Dublett!");
					flag = true;
					break;
				}
			}
			if (!flag && !fileInfo.Name.EndsWith(".meta") && fileInfo.Name.EndsWith(levelEditorManager.fileEnding))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.loadCellPrefab);
				gameObject.SetActive(true);
				gameObject.GetComponent<Button>().interactable = true;
				gameObject.transform.SetParent(this.loadGrid.transform);
				gameObject.GetComponent<getFile>().setFile(fileInfo);
				gameObject.GetComponent<assignLeaderboardButtonInfo>().assignButtonValues(fileInfo.Name.Split(new char[]
				{
					'.'
				})[0], string.Empty, fileInfo.Name.Split(new char[]
				{
					'.'
				})[0], "000", null);
			}
		}
	}

	// Token: 0x06000FB7 RID: 4023 RVA: 0x0006605C File Offset: 0x0006425C
	public void clearGrid()
	{
		foreach (Button button in this.loadGrid.GetComponentsInChildren<Button>())
		{
			UnityEngine.Object.Destroy(button.gameObject);
		}
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x00066098 File Offset: 0x00064298
	private void OnSteamUGCDetailsResult(SteamUGCQueryCompleted_t _callback, bool bioFail)
	{
		if (bioFail)
		{
			MonoBehaviour.print("BioFail");
			return;
		}
		MonoBehaviour.print("RETURNED!" + _callback.m_eResult);
		List<string> list = new List<string>();
		for (uint num = 0U; num < _callback.m_unNumResultsReturned; num += 1U)
		{
			SteamUGCDetails_t steamUGCDetails_t;
			SteamUGC.GetQueryUGCResult(_callback.m_handle, num, out steamUGCDetails_t);
			if (steamUGCDetails_t.m_ulSteamIDOwner != SteamUser.GetSteamID().m_SteamID)
			{
				list.Add(steamUGCDetails_t.m_nPublishedFileId.ToString());
			}
		}
		List<FileInfo> list2 = new List<FileInfo>();
		DirectoryInfo directoryInfo = new DirectoryInfo("C:/Program Files (x86)/Steam/steamapps/workshop/content/397950");
		DirectoryInfo[] directories = directoryInfo.GetDirectories();
		foreach (DirectoryInfo directoryInfo2 in directories)
		{
			if (list.Contains(directoryInfo2.Name))
			{
				foreach (FileInfo fileInfo in directoryInfo2.GetFiles())
				{
					if (!fileInfo.Name.EndsWith(".meta") && fileInfo.Name.EndsWith(levelEditorManager.fileEnding))
					{
						list2.Add(fileInfo);
					}
				}
			}
		}
		this.showDownloadedWorkShopMaps(list2);
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x000661E4 File Offset: 0x000643E4
	private void OnSteamUGCDetailsResultONLYSELF(SteamUGCQueryCompleted_t _callback, bool bioFail)
	{
		if (bioFail)
		{
			MonoBehaviour.print("BioFail");
			return;
		}
		MonoBehaviour.print("RETURNED!" + _callback.m_eResult);
		List<string> list = new List<string>();
		for (uint num = 0U; num < _callback.m_unNumResultsReturned; num += 1U)
		{
			SteamUGCDetails_t steamUGCDetails_t;
			SteamUGC.GetQueryUGCResult(_callback.m_handle, num, out steamUGCDetails_t);
			if (steamUGCDetails_t.m_ulSteamIDOwner == SteamUser.GetSteamID().m_SteamID)
			{
				list.Add(steamUGCDetails_t.m_nPublishedFileId.ToString());
			}
		}
		List<FileInfo> list2 = new List<FileInfo>();
		DirectoryInfo directoryInfo = new DirectoryInfo("C:/Program Files (x86)/Steam/steamapps/workshop/content/397950");
		DirectoryInfo[] directories = directoryInfo.GetDirectories();
		foreach (DirectoryInfo directoryInfo2 in directories)
		{
			if (list.Contains(directoryInfo2.Name))
			{
				foreach (FileInfo fileInfo in directoryInfo2.GetFiles())
				{
					if (!fileInfo.Name.EndsWith(".meta") && fileInfo.Name.EndsWith(levelEditorManager.fileEnding))
					{
						list2.Add(fileInfo);
					}
				}
			}
		}
		this.showDownloadedWorkShopMaps(list2);
	}

	// Token: 0x04000C8A RID: 3210
	public UIblocker _UIBlocker;

	// Token: 0x04000C8B RID: 3211
	private CallResult<SteamUGCQueryCompleted_t> m_SteamUGCDetailsResult;

	// Token: 0x04000C8C RID: 3212
	private CallResult<SteamUGCQueryCompleted_t> m_SteamUGCDetailsResultONLYSELF;

	// Token: 0x04000C8D RID: 3213
	public bool OnlySelf;

	// Token: 0x04000C8E RID: 3214
	public GameObject loadGrid;

	// Token: 0x04000C8F RID: 3215
	public GameObject loadCellPrefab;

	// Token: 0x04000C90 RID: 3216
	private static loadManager _loadManager;
}
