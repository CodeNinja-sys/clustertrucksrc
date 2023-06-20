using System;
using UnityEngine;

// Token: 0x02000121 RID: 289
[RequireComponent(typeof(ExtraHandlerBase))]
public class RDTExtraRigid : RecordingDataTransmitterTransform
{
	// Token: 0x06000635 RID: 1589 RVA: 0x0002BA58 File Offset: 0x00029C58
	public void SetExtraInfoToSend(Vector3 OutGoingExtra)
	{
		this.mOutGoingExtraInfo = OutGoingExtra;
		this.mOutGoingExtraFlag = true;
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x0002BA68 File Offset: 0x00029C68
	protected override bool RegisterTransmitter()
	{
		this.mRecordingPosition = Singleton<DataRecorder>.Instance.RegisterTransmitter(base.GetComponent<RDTExtra>());
		return true;
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x0002BA84 File Offset: 0x00029C84
	protected override void Initialize()
	{
		base.Initialize();
		this.mExtraHandler = base.GetComponent<ExtraHandlerBase>();
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x0002BA98 File Offset: 0x00029C98
	private new void Start()
	{
		this.Initialize();
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x0002BAA0 File Offset: 0x00029CA0
	private void HandleExtra(Vector3 extraInfo)
	{
		this.mExtraHandler.HandleExtra(extraInfo);
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x0002BAB0 File Offset: 0x00029CB0
	public override void ApplyDataPackage(DataRecordingPackage lastDataPackage, DataRecordingPackage dataPackage, float interpolate)
	{
		base.ApplyDataPackage(lastDataPackage, dataPackage, interpolate);
		if (lastDataPackage.mAddon)
		{
			this.HandleExtra(lastDataPackage.mExtra);
		}
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x0002BAD8 File Offset: 0x00029CD8
	public override DataRecordingPackage GetDataRecordingPackage()
	{
		DataRecordingPackage dataRecordingPackage = base.GetDataRecordingPackage();
		if (!base.P_Dirty)
		{
			return dataRecordingPackage;
		}
		dataRecordingPackage.mAddon = this.mOutGoingExtraFlag;
		if (this.mOutGoingExtraFlag)
		{
			dataRecordingPackage.mExtra = this.mOutGoingExtraInfo;
			this.mOutGoingExtraFlag = false;
		}
		return dataRecordingPackage;
	}

	// Token: 0x04000481 RID: 1153
	private bool mOutGoingExtraFlag;

	// Token: 0x04000482 RID: 1154
	private Vector3 mOutGoingExtraInfo;

	// Token: 0x04000483 RID: 1155
	private ExtraHandlerBase mExtraHandler;

	// Token: 0x02000122 RID: 290
	private enum EXTRA_HANDLER
	{
		// Token: 0x04000485 RID: 1157
		ParticleSystem
	}
}
