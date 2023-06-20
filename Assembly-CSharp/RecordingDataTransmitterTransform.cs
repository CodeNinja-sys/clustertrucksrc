using System;
using UnityEngine;

// Token: 0x02000130 RID: 304
public class RecordingDataTransmitterTransform : RecordingDataTransmitterBase
{
	// Token: 0x06000698 RID: 1688 RVA: 0x0002C720 File Offset: 0x0002A920
	protected override void Initialize()
	{
		base.Initialize();
		Debug.Log("init");
		for (int i = 0; i < this.mLastPosnRots.Length; i++)
		{
			this.mLastPosnRots[i] = new RecordingDataTransmitterTransform.LastPosnRot();
		}
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x0002C764 File Offset: 0x0002A964
	public void Start()
	{
		this.Initialize();
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x0002C76C File Offset: 0x0002A96C
	protected bool Moved()
	{
		return true;
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x0002C77C File Offset: 0x0002A97C
	protected virtual void CheckDirty()
	{
		if (this.Moved())
		{
			this.mDirty = true;
		}
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x0002C790 File Offset: 0x0002A990
	protected override bool RegisterTransmitter()
	{
		this.mRecordingPosition = Singleton<DataRecorder>.Instance.RegisterTransmitter(base.GetComponent<RecordingDataTransmitterTransform>());
		return true;
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x0002C7AC File Offset: 0x0002A9AC
	private void SendFirstPackage()
	{
		Debug.Log("FirstPackage", base.gameObject);
		this.mDirty = true;
		this.mSentFirstPackage = true;
		DataRecordingPackage dataRecordingPackage = this.GetDataRecordingPackage();
		dataRecordingPackage.mFirstPackage = true;
		if (dataRecordingPackage == null)
		{
			return;
		}
		Singleton<DataRecorder>.Instance.P_RecordedData[this.mRecordingPosition].Add(dataRecordingPackage);
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x0002C808 File Offset: 0x0002AA08
	public override void RecordExtraPackage()
	{
		if (!this.mSentFirstPackage)
		{
			this.SendFirstPackage();
		}
		if (Singleton<DataRecorder>.Instance == null)
		{
			throw new Exception("DataRecorder is null");
		}
		if (Singleton<DataRecorder>.Instance.P_RecordingState != DataRecorder.RecordingState.Recording)
		{
			return;
		}
		DataRecordingPackage dataRecordingPackage = this.GetDataRecordingPackage();
		if (dataRecordingPackage == null)
		{
			return;
		}
		Singleton<DataRecorder>.Instance.P_RecordedData[this.mRecordingPosition].Add(dataRecordingPackage);
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x0002C87C File Offset: 0x0002AA7C
	public override DataRecordingPackage ForceGetDataRecordingPackage()
	{
		if (base.gameObject == null)
		{
			return null;
		}
		if (!this.mSentFirstPackage)
		{
			this.SendFirstPackage();
		}
		return new DataRecordingPackage
		{
			mInstanceID = base.gameObject.GetInstanceID(),
			mQuaternion = new Quaternion(base.transform.rotation.x, base.transform.rotation.y, base.transform.rotation.z, base.transform.rotation.w),
			mPosition = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z),
			mName = base.gameObject.name,
			mPrefabName = this.mPrefabToSpawn,
			mScene = info.currentLevel,
			mTime = Singleton<DataRecorder>.Instance.GetTimeSinceRecordingStart()
		};
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x0002C9A8 File Offset: 0x0002ABA8
	public override DataRecordingPackage GetDataRecordingPackage()
	{
		if (base.gameObject == null)
		{
			return null;
		}
		if (!this.mSentFirstPackage)
		{
			this.SendFirstPackage();
		}
		this.CheckDirty();
		if (!base.P_Dirty)
		{
			return null;
		}
		return new DataRecordingPackage
		{
			mInstanceID = base.gameObject.GetInstanceID(),
			mQuaternion = new Quaternion(base.transform.rotation.x, base.transform.rotation.y, base.transform.rotation.z, base.transform.rotation.w),
			mPosition = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z),
			mName = base.gameObject.name,
			mPrefabName = this.mPrefabToSpawn,
			mScene = info.currentLevel,
			mTime = Singleton<DataRecorder>.Instance.GetTimeSinceRecordingStart()
		};
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x0002CAE8 File Offset: 0x0002ACE8
	public override void ApplyDataPackage(DataRecordingPackage lastDataPackage, DataRecordingPackage dataPackage, float interpolate)
	{
		base.transform.position = Vector3.Lerp(lastDataPackage.mPosition, dataPackage.mPosition, interpolate);
		if (this.mPrintDebugData && lastDataPackage.mInstanceID != base.gameObject.GetInstanceID())
		{
			MonoBehaviour.print(string.Concat(new object[]
			{
				"Assingment fail ",
				lastDataPackage.mInstanceID,
				" vs ",
				base.gameObject.name
			}));
		}
		base.transform.rotation = Quaternion.Lerp(lastDataPackage.mQuaternion, dataPackage.mQuaternion, interpolate);
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x0002CBA0 File Offset: 0x0002ADA0
	private void FixedUpdate()
	{
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x0002CBA4 File Offset: 0x0002ADA4
	public override void EndOfHistory()
	{
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x0002CBA8 File Offset: 0x0002ADA8
	public override void StopPlayBack()
	{
		if (this.mScriptDisabledAtPlay == null)
		{
			MonoBehaviour.print(base.gameObject.name + " disable list is null");
			return;
		}
		for (int i = 0; i < this.mScriptDisabledAtPlay.Length; i++)
		{
			this.mScriptDisabledAtPlay[i].enabled = true;
		}
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x0002CC04 File Offset: 0x0002AE04
	public override void StartPlayBack()
	{
		if (this.mScriptDisabledAtPlay == null)
		{
			return;
		}
		for (int i = 0; i < this.mScriptDisabledAtPlay.Length; i++)
		{
			this.mScriptDisabledAtPlay[i].enabled = false;
		}
	}

	// Token: 0x040004AF RID: 1199
	[SerializeField]
	private MonoBehaviour[] mScriptDisabledAtPlay;

	// Token: 0x040004B0 RID: 1200
	[SerializeField]
	protected float mMinMovementForRecording;

	// Token: 0x040004B1 RID: 1201
	[SerializeField]
	protected float mMinRotationForRecording;

	// Token: 0x040004B2 RID: 1202
	[SerializeField]
	private bool mPrintDebugData;

	// Token: 0x040004B3 RID: 1203
	private int mLastPosIterator;

	// Token: 0x040004B4 RID: 1204
	private RecordingDataTransmitterTransform.LastPosnRot[] mLastPosnRots = new RecordingDataTransmitterTransform.LastPosnRot[3];

	// Token: 0x040004B5 RID: 1205
	private bool mSentFirstPackage;

	// Token: 0x02000131 RID: 305
	private class LastPosnRot
	{
		// Token: 0x040004B6 RID: 1206
		public Vector3 position;

		// Token: 0x040004B7 RID: 1207
		public Vector3 forward;
	}
}
