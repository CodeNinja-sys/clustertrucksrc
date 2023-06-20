using System;
using Steamworks;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public class SteamCloudStorageHandler : Singleton<SteamCloudStorageHandler>
{
	// Token: 0x06001131 RID: 4401 RVA: 0x00070090 File Offset: 0x0006E290
	private void Awake()
	{
		this.mOnRemoteStorageFileWriteAsyncResultCallback = CallResult<RemoteStorageFileShareResult_t>.Create(new CallResult<RemoteStorageFileShareResult_t>.APIDispatchDelegate(this.OnRemoteFileShareCallbackFunction));
		this.mOnFileWriteAsyncComplete = CallResult<RemoteStorageFileWriteAsyncComplete_t>.Create(new CallResult<RemoteStorageFileWriteAsyncComplete_t>.APIDispatchDelegate(this.OnFileWriteAsyncComplete));
		this.mOnLeaderboradUGCset = CallResult<LeaderboardUGCSet_t>.Create(new CallResult<LeaderboardUGCSet_t>.APIDispatchDelegate(this.OnLeaderboradUGCset));
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x000700E4 File Offset: 0x0006E2E4
	public void UploadGhostFileToLeaderBoard(byte[] ghostData, SteamLeaderboard_t pLeaderboard)
	{
		if (SteamCloudStorageHandler.mBusy)
		{
			return;
		}
		SteamCloudStorageHandler.mBusy = true;
		this.mleaderBoard = pLeaderboard;
		string text = "ghost" + info.currentLevel.ToString() + ".dat";
		Debug.Log("Writing Ghost: " + Time.frameCount);
		if (SteamRemoteStorage.FileWrite(text, ghostData, ghostData.Length))
		{
			Debug.Log("Writing done. Uploading: " + Time.frameCount);
			SteamAPICall_t hAPICall = SteamRemoteStorage.FileShare(text);
			this.mOnRemoteStorageFileWriteAsyncResultCallback.Set(hAPICall, null);
			return;
		}
		throw new Exception("File not found!" + text);
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x00070194 File Offset: 0x0006E394
	public void OnRemoteFileShareCallbackFunction(RemoteStorageFileShareResult_t callBack, bool biofail)
	{
		if (biofail)
		{
			Debug.Log("Biofail: " + biofail + " Exiting!");
			return;
		}
		Debug.Log(string.Concat(new object[]
		{
			"Returned GhostWrite: ",
			callBack.m_eResult.ToString(),
			" : ",
			Time.frameCount
		}));
		SteamAPICall_t hAPICall = SteamUserStats.AttachLeaderboardUGC(this.mleaderBoard, callBack.m_hFile);
		this.mOnLeaderboradUGCset.Set(hAPICall, null);
		SteamCloudStorageHandler.mBusy = false;
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x0007022C File Offset: 0x0006E42C
	private void OnFileWriteAsyncComplete(RemoteStorageFileWriteAsyncComplete_t param, bool bIOFailure)
	{
		if (bIOFailure)
		{
			Debug.Log("Biofail: " + bIOFailure);
			return;
		}
		Debug.Log("OnFileWriteAsyncComplete: Result: " + param.m_eResult.ToString());
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x00070278 File Offset: 0x0006E478
	private void OnLeaderboradUGCset(LeaderboardUGCSet_t param, bool bIOFailure)
	{
		if (bIOFailure)
		{
			Debug.Log("Biofail: " + bIOFailure);
			return;
		}
		Debug.Log(string.Concat(new object[]
		{
			"OnLeaderboradUGCset: Result: ",
			param.m_eResult.ToString(),
			" Frame: ",
			Time.frameCount,
			" : ",
			Time.time
		}));
	}

	// Token: 0x04000E5F RID: 3679
	private CallResult<RemoteStorageFileShareResult_t> mOnRemoteStorageFileWriteAsyncResultCallback;

	// Token: 0x04000E60 RID: 3680
	private CallResult<RemoteStorageFileWriteAsyncComplete_t> mOnFileWriteAsyncComplete;

	// Token: 0x04000E61 RID: 3681
	private CallResult<LeaderboardUGCSet_t> mOnLeaderboradUGCset;

	// Token: 0x04000E62 RID: 3682
	private SteamLeaderboard_t mleaderBoard;

	// Token: 0x04000E63 RID: 3683
	private static bool mBusy;
}
