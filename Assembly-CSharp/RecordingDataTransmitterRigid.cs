using System;
using UnityEngine;

// Token: 0x0200012F RID: 303
public class RecordingDataTransmitterRigid : RecordingDataTransmitterTransform
{
	// Token: 0x17000139 RID: 313
	// (get) Token: 0x0600068E RID: 1678 RVA: 0x0002C51C File Offset: 0x0002A71C
	private Rigidbody P_Rigidbody
	{
		get
		{
			if (this.mRigidbody == null)
			{
				this.mRigidbody = base.GetComponent<Rigidbody>();
			}
			return this.mRigidbody;
		}
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0002C544 File Offset: 0x0002A744
	protected override bool RegisterTransmitter()
	{
		this.mRecordingPosition = Singleton<DataRecorder>.Instance.RegisterTransmitter(base.GetComponent<RecordingDataTransmitterRigid>());
		return true;
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x0002C560 File Offset: 0x0002A760
	protected override void Initialize()
	{
		base.Initialize();
		this.mRigidbody = base.GetComponent<Rigidbody>();
		this.mDataRecorder = Singleton<DataRecorder>.Instance;
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x0002C580 File Offset: 0x0002A780
	private new void Start()
	{
		if (base.GetComponent<destructive>())
		{
			base.gameObject.AddComponent<RDTransmitterPillar>();
			MonoBehaviour.print("mutate " + base.gameObject.name);
			UnityEngine.Object.Destroy(base.GetComponent<RecordingDataTransmitterRigid>());
			return;
		}
		this.Initialize();
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x0002C5D8 File Offset: 0x0002A7D8
	private void OnCollisionEnter(Collision collision)
	{
		this.RecordExtraPackage();
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x0002C5E0 File Offset: 0x0002A7E0
	public override DataRecordingPackage GetDataRecordingPackage()
	{
		DataRecordingPackage dataRecordingPackage = base.GetDataRecordingPackage();
		if (!base.P_Dirty)
		{
			return dataRecordingPackage;
		}
		if (this.mRigidbody == null)
		{
			this.mRigidbody = base.GetComponent<Rigidbody>();
			if (this.mRigidbody == null)
			{
				MonoBehaviour.print("Can't find rigidbody");
			}
		}
		dataRecordingPackage.mVelocity = new Vector3(this.mRigidbody.velocity.x, this.mRigidbody.velocity.y, this.mRigidbody.velocity.z);
		dataRecordingPackage.mAngularVelocity = new Vector3(this.mRigidbody.angularVelocity.x, this.mRigidbody.angularVelocity.y, this.mRigidbody.angularVelocity.z);
		return dataRecordingPackage;
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x0002C6D0 File Offset: 0x0002A8D0
	public override void ApplyDataPackage(DataRecordingPackage lastDataPackage, DataRecordingPackage dataPackage, float interpolate)
	{
		base.ApplyDataPackage(lastDataPackage, dataPackage, interpolate);
		if (lastDataPackage == null)
		{
			lastDataPackage = dataPackage;
		}
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x0002C6E4 File Offset: 0x0002A8E4
	public override void StopPlayBack()
	{
		base.StopPlayBack();
		this.P_Rigidbody.isKinematic = false;
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x0002C6F8 File Offset: 0x0002A8F8
	public override void StartPlayBack()
	{
		base.StartPlayBack();
		this.P_Rigidbody.isKinematic = true;
	}

	// Token: 0x040004AC RID: 1196
	private Rigidbody mRigidbody;

	// Token: 0x040004AD RID: 1197
	private DataRecorder mDataRecorder;

	// Token: 0x040004AE RID: 1198
	private bool mPackageSentSinceSleeping;
}
