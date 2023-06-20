using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000113 RID: 275
public class DataRecorder : Singleton<DataRecorder>
{
	// Token: 0x17000129 RID: 297
	// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0002AA5C File Offset: 0x00028C5C
	public DataRecorder.RecordingState P_RecordingState
	{
		get
		{
			return this.mRecordingState;
		}
	}

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x060005EA RID: 1514 RVA: 0x0002AA64 File Offset: 0x00028C64
	public List<List<DataRecordingPackage>> P_RecordedData
	{
		get
		{
			return this.mRecordedData;
		}
	}

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x060005EB RID: 1515 RVA: 0x0002AA6C File Offset: 0x00028C6C
	public float P_RecordingRate
	{
		get
		{
			return (float)DataRecorder.mRecordingRate;
		}
	}

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x060005EC RID: 1516 RVA: 0x0002AA74 File Offset: 0x00028C74
	public List<RecordingDataTransmitterBase> P_DataTransmitters
	{
		get
		{
			return this.mDataTransmitters;
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x060005ED RID: 1517 RVA: 0x0002AA7C File Offset: 0x00028C7C
	// (set) Token: 0x060005EE RID: 1518 RVA: 0x0002AA84 File Offset: 0x00028C84
	private static float P_StartRecordingTime
	{
		get
		{
			return DataRecorder.mStartRecordingTime;
		}
		set
		{
			DataRecorder.mStartRecordingTime = value;
		}
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x0002AA8C File Offset: 0x00028C8C
	public int RegisterTransmitter(RecordingDataTransmitterBase transmitter)
	{
		this.mDataTransmitters.Add(transmitter);
		int result = this.mDataTransmitters.Count - 1;
		List<DataRecordingPackage> list = new List<DataRecordingPackage>();
		list.Add(new DataRecordingPackage
		{
			mFirstPackage = true
		});
		this.mRecordedData.Add(new List<DataRecordingPackage>());
		return result;
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x0002AAE0 File Offset: 0x00028CE0
	public float GetTimeSinceRecordingStart()
	{
		return GameManager.mapTime;
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x0002AAE8 File Offset: 0x00028CE8
	private void RecordDataFromTransmitters(bool force = false)
	{
		if (this.mDataTransmitters.Count == 0)
		{
			Debug.Log("No transmitters");
			return;
		}
		for (int i = 0; i < this.mDataTransmitters.Count; i++)
		{
			DataRecordingPackage dataRecordingPackage = (!force) ? this.mDataTransmitters[i].GetDataRecordingPackage() : this.mDataTransmitters[i].ForceGetDataRecordingPackage();
			if (dataRecordingPackage != null)
			{
				this.mRecordedData[i].Add(dataRecordingPackage);
			}
		}
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x0002AB78 File Offset: 0x00028D78
	public HistoryCollection CopyRecordedData()
	{
		if (this.mRecordedData == null || this.mRecordedData.Count == 0)
		{
			MonoBehaviour.print("Can't copy recorded data since none have been recorded");
			return null;
		}
		DataRecordingPackage[][] array = new DataRecordingPackage[this.mDataTransmitters.Count][];
		for (int i = 0; i < this.mRecordedData.Count; i++)
		{
			array[i] = this.mRecordedData[i].ToArray();
		}
		return new HistoryCollection(array);
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x0002ABF4 File Offset: 0x00028DF4
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.transform.gameObject);
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x0002AC08 File Offset: 0x00028E08
	private void Start()
	{
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x0002AC0C File Offset: 0x00028E0C
	private void CheckHistory(HistoryCollection levelHistory)
	{
		bool flag = false;
		for (int i = 0; i < levelHistory.Length; i++)
		{
			ObjectHistory objectHistory = levelHistory[i];
			for (int j = 0; j < objectHistory.Length; j++)
			{
				DataRecordingPackage dataRecordingPackage = objectHistory[j];
				if (dataRecordingPackage == null)
				{
					MonoBehaviour.print("Corrupt data found");
				}
				if (dataRecordingPackage.mFirstPackage)
				{
					if (flag)
					{
						Debug.Log("Duplicate first packages");
					}
					flag = true;
				}
			}
		}
		if (!flag)
		{
			Debug.Log("NoFirstPackageFound");
		}
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x0002AC9C File Offset: 0x00028E9C
	private void Flush()
	{
		this.mRecordedData = new List<List<DataRecordingPackage>>();
		this.mDataTransmitters = new List<RecordingDataTransmitterBase>();
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x0002ACB4 File Offset: 0x00028EB4
	public void StartRecording()
	{
		Debug.Log("Started Recording: " + Time.frameCount);
		if (this.mRecordingState == DataRecorder.RecordingState.Recording)
		{
			Debug.Log("Terminating attemt to start recording while recording");
		}
		DataRecorder.mStartRecordingTime = Time.timeSinceLevelLoad;
		this.mRecordingState = DataRecorder.RecordingState.Recording;
		this.RecordDataFromTransmitters(false);
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x0002AD08 File Offset: 0x00028F08
	public HistoryCollection SaveToFile()
	{
		HistoryCollection historyCollection = this.CopyRecordedData();
		MonoBehaviour.print("Checking recorded data : " + Time.frameCount);
		if (historyCollection == null)
		{
			MonoBehaviour.print("RECORDER: No data recorded. Terminating save");
			return null;
		}
		this.CheckHistory(historyCollection);
		historyCollection.SaveToFile(info.currentLevel);
		MonoBehaviour.print("Saving done : " + Time.frameCount);
		this.StopRecording();
		return historyCollection;
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x0002AD7C File Offset: 0x00028F7C
	public void StopRecording()
	{
		this.RecordDataFromTransmitters(true);
		this.mRecordingState = DataRecorder.RecordingState.Waiting;
		DataRecorder.mStartRecordingTime = 0f;
		this.Flush();
		Debug.Log("Stopped Recording : " + Time.frameCount);
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x0002ADB8 File Offset: 0x00028FB8
	private void Record()
	{
		if (Time.frameCount % DataRecorder.mRecordingRate == 0)
		{
			this.RecordDataFromTransmitters(false);
		}
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x0002ADD4 File Offset: 0x00028FD4
	private void UpdateWaiting()
	{
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x0002ADD8 File Offset: 0x00028FD8
	private void Update()
	{
		DataRecorder.RecordingState recordingState = this.mRecordingState;
		if (recordingState != DataRecorder.RecordingState.Waiting)
		{
			if (recordingState != DataRecorder.RecordingState.Recording)
			{
			}
		}
		else
		{
			this.UpdateWaiting();
		}
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x0002AE10 File Offset: 0x00029010
	private void FixedUpdate()
	{
		DataRecorder.RecordingState recordingState = this.mRecordingState;
		if (recordingState != DataRecorder.RecordingState.Waiting)
		{
			if (recordingState == DataRecorder.RecordingState.Recording)
			{
				this.Record();
			}
		}
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0002AE4C File Offset: 0x0002904C
	private void CycleRecordingState()
	{
		this.mRecordingState++;
		this.mRecordingState %= (DataRecorder.RecordingState)2;
		MonoBehaviour.print("RecordingState: " + this.mRecordingState);
		DataRecorder.RecordingState recordingState = this.mRecordingState;
		if (recordingState != DataRecorder.RecordingState.Waiting)
		{
			if (recordingState == DataRecorder.RecordingState.Recording)
			{
				this.StartRecording();
			}
		}
		else
		{
			this.StopRecording();
		}
	}

	// Token: 0x04000453 RID: 1107
	private const int RECORDING_STATE_SIZE = 2;

	// Token: 0x04000454 RID: 1108
	private List<RecordingDataTransmitterBase> mDataTransmitters = new List<RecordingDataTransmitterBase>();

	// Token: 0x04000455 RID: 1109
	private static int mRecordingRate = 6;

	// Token: 0x04000456 RID: 1110
	private static float mStartRecordingTime;

	// Token: 0x04000457 RID: 1111
	private DataRecorder.RecordingState mRecordingState;

	// Token: 0x04000458 RID: 1112
	private List<List<DataRecordingPackage>> mRecordedData = new List<List<DataRecordingPackage>>();

	// Token: 0x02000114 RID: 276
	public enum RecordingState
	{
		// Token: 0x0400045A RID: 1114
		Waiting,
		// Token: 0x0400045B RID: 1115
		Recording
	}
}
