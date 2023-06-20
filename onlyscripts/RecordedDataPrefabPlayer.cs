using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000129 RID: 297
public class RecordedDataPrefabPlayer : Singleton<RecordedDataPrefabPlayer>
{
	// Token: 0x17000134 RID: 308
	// (get) Token: 0x06000669 RID: 1641 RVA: 0x0002C0CC File Offset: 0x0002A2CC
	private Transform P_Player
	{
		get
		{
			if (this.mPlayer == null)
			{
				this.mPlayer = GameObject.FindGameObjectWithTag("Player").transform;
			}
			return this.mPlayer;
		}
	}

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x0600066A RID: 1642 RVA: 0x0002C108 File Offset: 0x0002A308
	public RecordedDataPrefabPlayer.PlaybackState P_PlaybackState
	{
		get
		{
			return this.mPlaybackState;
		}
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0002C110 File Offset: 0x0002A310
	public float GetTimeSincePlaybackStart()
	{
		return this.mPlaybackTime;
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x0002C118 File Offset: 0x0002A318
	public float GetInterpolationProgress(float currentStatesTime, float lastStatesTime)
	{
		float num = 1f - (currentStatesTime - this.GetTimeSincePlaybackStart()) / (currentStatesTime - lastStatesTime);
		return (!float.IsNaN(num)) ? num : 0f;
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x0002C150 File Offset: 0x0002A350
	private void CreatePlaybackTargets()
	{
		this.mPlaybackTargets.Clear();
		for (int i = 0; i < this.mRecordedData.Length; i++)
		{
			this.mPlaybackTargets.Add(new PlaybackPrefabTarget(this.mRecordedData[i]));
		}
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x0002C1A0 File Offset: 0x0002A3A0
	public void StartPlayback(HistoryCollection historyCollection)
	{
		Debug.Log(string.Concat(new object[]
		{
			"Calling Startplayback: numOfBytes: ",
			historyCollection.mObjectHistories[0].Length,
			" Frame: ",
			Time.frameCount
		}));
		this.mPlaybackState = RecordedDataPrefabPlayer.PlaybackState.Playing;
		this.mRecordedData = historyCollection;
		if (this.mRecordedData == null)
		{
			this.mPlaybackState = RecordedDataPrefabPlayer.PlaybackState.Stopped;
			Debug.Log("File could not be loaded. Stopping playback");
			return;
		}
		this.CreatePlaybackTargets();
		if (this.mRecordedData.Length == 0)
		{
			throw new UnityException("RecordedDataPlayer found no data to perform playback from");
		}
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x0002C23C File Offset: 0x0002A43C
	public void StopPlayback()
	{
		this.mPlaybackState = RecordedDataPrefabPlayer.PlaybackState.Stopped;
		this.mPlaybackTime = 0f;
		foreach (PlaybackPrefabTarget playbackPrefabTarget in this.mPlaybackTargets)
		{
			playbackPrefabTarget.mTarget.StopPlayBack();
		}
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0002C2B8 File Offset: 0x0002A4B8
	private void SendRecodingStates()
	{
		foreach (PlaybackPrefabTarget playbackPrefabTarget in this.mPlaybackTargets)
		{
			playbackPrefabTarget.SendState();
		}
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x0002C320 File Offset: 0x0002A520
	private void StartPause()
	{
		this.mPlaybackState = RecordedDataPrefabPlayer.PlaybackState.Paused;
		this.mLastPauseStamp = Time.timeSinceLevelLoad;
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x0002C334 File Offset: 0x0002A534
	private void Update()
	{
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x0002C338 File Offset: 0x0002A538
	private void RefreshPlaybackTime()
	{
		this.mPlaybackTime += Time.fixedDeltaTime * this.mPlaybackSpeed;
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x0002C354 File Offset: 0x0002A554
	private void FixedUpdate()
	{
		if (this.mPlaybackState == RecordedDataPrefabPlayer.PlaybackState.Playing || this.mPlaybackState == RecordedDataPrefabPlayer.PlaybackState.Reverse)
		{
			if (this.mPlaybackSpeed < 0f)
			{
				this.mPlaybackState = RecordedDataPrefabPlayer.PlaybackState.Reverse;
			}
			else
			{
				this.mPlaybackState = RecordedDataPrefabPlayer.PlaybackState.Playing;
			}
		}
		switch (this.mPlaybackState)
		{
		case RecordedDataPrefabPlayer.PlaybackState.Playing:
			this.RefreshPlaybackTime();
			this.SendRecodingStates();
			break;
		case RecordedDataPrefabPlayer.PlaybackState.Paused:
			this.SendRecodingStates();
			break;
		case RecordedDataPrefabPlayer.PlaybackState.Reverse:
			this.RefreshPlaybackTime();
			this.SendRecodingStates();
			break;
		}
	}

	// Token: 0x04000496 RID: 1174
	private const int PLAYBACK_STATE_SIZE = 4;

	// Token: 0x04000497 RID: 1175
	[Range(-2f, 2f)]
	public float mPlaybackSpeed = 1f;

	// Token: 0x04000498 RID: 1176
	private RecordedDataPrefabPlayer.PlaybackState mPlaybackState;

	// Token: 0x04000499 RID: 1177
	private float mLastPauseStamp;

	// Token: 0x0400049A RID: 1178
	private float mPlaybackTime;

	// Token: 0x0400049B RID: 1179
	private HistoryCollection mRecordedData;

	// Token: 0x0400049C RID: 1180
	private Transform mPlayer;

	// Token: 0x0400049D RID: 1181
	private Transform mRecordingCam;

	// Token: 0x0400049E RID: 1182
	private int[] mIterators;

	// Token: 0x0400049F RID: 1183
	private List<PlaybackPrefabTarget> mPlaybackTargets = new List<PlaybackPrefabTarget>();

	// Token: 0x0200012A RID: 298
	public enum PlaybackState
	{
		// Token: 0x040004A1 RID: 1185
		Stopped,
		// Token: 0x040004A2 RID: 1186
		Playing,
		// Token: 0x040004A3 RID: 1187
		Paused,
		// Token: 0x040004A4 RID: 1188
		Reverse
	}
}
