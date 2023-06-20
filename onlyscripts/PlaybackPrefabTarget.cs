using System;
using UnityEngine;

// Token: 0x0200011B RID: 283
public class PlaybackPrefabTarget
{
	// Token: 0x06000615 RID: 1557 RVA: 0x0002B16C File Offset: 0x0002936C
	public PlaybackPrefabTarget(ObjectHistory objectHistory)
	{
		this.mObjectHistory = objectHistory;
	}

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x06000616 RID: 1558 RVA: 0x0002B17C File Offset: 0x0002937C
	// (set) Token: 0x06000617 RID: 1559 RVA: 0x0002B184 File Offset: 0x00029384
	private bool P_PlaybackDone
	{
		get
		{
			return this.mPlaybackDone;
		}
		set
		{
			if (!this.mPlaybackDone && value)
			{
				this.mPlaybackDone = value;
				this.mTarget.EndOfHistory();
			}
			else
			{
				this.mPlaybackDone = value;
			}
		}
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0002B1B8 File Offset: 0x000293B8
	public DataRecordingPackage GetCurrentState()
	{
		return this.mObjectHistory[this.mIterator];
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x0002B1CC File Offset: 0x000293CC
	public DataRecordingPackage GetLastState()
	{
		if (this.mIterator == 0)
		{
			return this.mObjectHistory[this.mIterator];
		}
		if (Singleton<RecordedDataPrefabPlayer>.Instance.P_PlaybackState != RecordedDataPrefabPlayer.PlaybackState.Reverse)
		{
			return this.mObjectHistory[this.mIterator - 1];
		}
		if (this.mIterator + 1 >= this.mObjectHistory.Length)
		{
			return this.mObjectHistory[this.mObjectHistory.Length - 1];
		}
		return this.mObjectHistory[this.mIterator + 1];
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x0002B260 File Offset: 0x00029460
	public void CheckHistory()
	{
		if (this.mObjectHistory == null)
		{
			Debug.Log("Shit is null");
		}
		for (int i = 0; i < this.mObjectHistory.Length; i++)
		{
			if (this.mObjectHistory[i] == null)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Null data found ",
					this.mTarget.name,
					" ",
					this.mTarget.gameObject.GetInstanceID()
				}));
			}
		}
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x0002B2F8 File Offset: 0x000294F8
	public void SendState()
	{
		this.CheckIterator();
		DataRecordingPackage currentState = this.GetCurrentState();
		DataRecordingPackage lastState = this.GetLastState();
		if (currentState == null)
		{
			Debug.Log(string.Concat(new object[]
			{
				"current state is null ",
				this.mTarget.gameObject.name,
				" ",
				this.mTarget.GetInstanceID()
			}));
		}
		if (lastState == null)
		{
			Debug.Log(string.Concat(new object[]
			{
				"current state is null ",
				this.mTarget.gameObject.name,
				" ",
				this.mTarget.GetInstanceID()
			}));
		}
		if (lastState == null || currentState == null)
		{
			return;
		}
		float interpolationProgress;
		if (Singleton<RecordedDataPrefabPlayer>.Instance.P_PlaybackState == RecordedDataPrefabPlayer.PlaybackState.Reverse)
		{
			interpolationProgress = Singleton<RecordedDataPrefabPlayer>.Instance.GetInterpolationProgress(currentState.mTime, lastState.mTime);
		}
		else
		{
			interpolationProgress = Singleton<RecordedDataPrefabPlayer>.Instance.GetInterpolationProgress(currentState.mTime, lastState.mTime);
		}
		this.SendPackages(lastState, currentState, interpolationProgress);
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x0002B40C File Offset: 0x0002960C
	private void SendPackages(DataRecordingPackage lastState, DataRecordingPackage currentState, float progress)
	{
		if (this.mTarget == null)
		{
			Debug.Log("mTarget is null. Name: " + lastState.mName);
		}
		else
		{
			this.mTarget.ApplyDataPackage(lastState, currentState, progress);
		}
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x0002B448 File Offset: 0x00029648
	private void SpawnPrefab(DataRecordingPackage package, Vector3 spawnPos)
	{
		if (this.mPlaybackDone)
		{
			return;
		}
		string mPrefabName = package.mPrefabName;
		if (mPrefabName != string.Empty)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Spawning Prefab ",
				mPrefabName,
				" : ",
				Time.frameCount
			}));
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(mPrefabName), spawnPos, Quaternion.identity);
			this.mTarget = gameObject.GetComponent<RecordingDataTransmitterBase>();
			this.mTarget.transform.parent = null;
			if (this.mTarget == null)
			{
				Debug.Log("mTarget is null", this.mTarget);
			}
			this.mTarget.StartPlayBack();
		}
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x0002B50C File Offset: 0x0002970C
	private void CheckIterator()
	{
		if (this.P_PlaybackDone)
		{
			return;
		}
		if (this.mObjectHistory == null)
		{
			Debug.Log("NoHistory", this.mTarget.gameObject);
		}
		if (this.mIterator >= this.mObjectHistory.Length - 1)
		{
			Debug.Log(this.mTarget.gameObject.name + " finisehd playback " + this.mTarget.gameObject.GetInstanceID());
			this.P_PlaybackDone = true;
			return;
		}
		DataRecordingPackage dataRecordingPackage = this.mObjectHistory[this.mIterator];
		if (dataRecordingPackage == null)
		{
			Debug.Log(string.Concat(new object[]
			{
				"History package is null for ",
				this.mTarget.gameObject.name,
				" ",
				this.mTarget.GetInstanceID()
			}));
			return;
		}
		float timeSincePlaybackStart = Singleton<RecordedDataPrefabPlayer>.Instance.GetTimeSincePlaybackStart();
		int num = 0;
		DataRecordingPackage dataRecordingPackage2 = new DataRecordingPackage();
		dataRecordingPackage2.mTime = 0f;
		if (Singleton<RecordedDataPrefabPlayer>.Instance.P_PlaybackState == RecordedDataPrefabPlayer.PlaybackState.Playing)
		{
			while (timeSincePlaybackStart > dataRecordingPackage.mTime)
			{
				if (this.mTarget == null)
				{
					this.SpawnPrefab(dataRecordingPackage, Vector3.zero);
				}
				if (this.mTarget == null)
				{
					Debug.Log("mTarget is null. Name: " + dataRecordingPackage.mName);
				}
				if (this.mIterator + 1 > this.mObjectHistory.Length - 1)
				{
					return;
				}
				this.MovePlaybackIterator(ref num, ref dataRecordingPackage, ref dataRecordingPackage2, 1);
			}
		}
		else if (Singleton<RecordedDataPrefabPlayer>.Instance.P_PlaybackState == RecordedDataPrefabPlayer.PlaybackState.Reverse)
		{
			while (timeSincePlaybackStart < dataRecordingPackage.mTime)
			{
				if (this.mIterator + 1 > this.mObjectHistory.Length - 1)
				{
					return;
				}
				if (this.mIterator == 0)
				{
					return;
				}
				this.MovePlaybackIterator(ref num, ref dataRecordingPackage, ref dataRecordingPackage2, -1);
			}
		}
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x0002B6FC File Offset: 0x000298FC
	private void MovePlaybackIterator(ref int iterations, ref DataRecordingPackage package, ref DataRecordingPackage lastPackage, int direction)
	{
		this.mIterator += direction;
		iterations++;
		if (iterations > 1)
		{
			this.SendPackages(lastPackage, package, 0.5f);
		}
		lastPackage = package;
		if (this.mIterator + 1 >= this.mObjectHistory.Length)
		{
			this.P_PlaybackDone = true;
			return;
		}
		package = this.mObjectHistory[this.mIterator + 1];
	}

	// Token: 0x0400046D RID: 1133
	public RecordingDataTransmitterBase mTarget;

	// Token: 0x0400046E RID: 1134
	private int mIterator;

	// Token: 0x0400046F RID: 1135
	private ObjectHistory mObjectHistory;

	// Token: 0x04000470 RID: 1136
	private bool mPlaybackDone;

	// Token: 0x04000471 RID: 1137
	private bool mBeenInitialized;
}
