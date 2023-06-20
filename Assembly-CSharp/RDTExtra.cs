using System;
using UnityEngine;

// Token: 0x0200011F RID: 287
[RequireComponent(typeof(ExtraHandlerBase))]
public class RDTExtra : RecordingDataTransmitterTransform
{
	// Token: 0x0600062D RID: 1581 RVA: 0x0002B97C File Offset: 0x00029B7C
	public void SetExtraInfoToSend(Vector3 OutGoingExtra)
	{
		this.mOutGoingExtraInfo = OutGoingExtra;
		this.mOutGoingExtraFlag = true;
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0002B98C File Offset: 0x00029B8C
	protected override bool RegisterTransmitter()
	{
		this.mRecordingPosition = Singleton<DataRecorder>.Instance.RegisterTransmitter(base.GetComponent<RDTExtra>());
		return true;
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0002B9A8 File Offset: 0x00029BA8
	protected override void Initialize()
	{
		base.Initialize();
		this.mExtraHandler = base.GetComponent<ExtraHandlerBase>();
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x0002B9BC File Offset: 0x00029BBC
	private new void Start()
	{
		this.Initialize();
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x0002B9C4 File Offset: 0x00029BC4
	private void HandleExtra(Vector3 extraInfo)
	{
		this.mExtraHandler.HandleExtra(extraInfo);
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x0002B9D4 File Offset: 0x00029BD4
	public override void ApplyDataPackage(DataRecordingPackage lastDataPackage, DataRecordingPackage dataPackage, float interpolate)
	{
		base.ApplyDataPackage(lastDataPackage, dataPackage, interpolate);
		if (lastDataPackage.mAddon)
		{
			this.HandleExtra(lastDataPackage.mExtra);
		}
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x0002B9FC File Offset: 0x00029BFC
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

	// Token: 0x0400047C RID: 1148
	private bool mOutGoingExtraFlag;

	// Token: 0x0400047D RID: 1149
	private Vector3 mOutGoingExtraInfo;

	// Token: 0x0400047E RID: 1150
	private ExtraHandlerBase mExtraHandler;

	// Token: 0x02000120 RID: 288
	private enum EXTRA_HANDLER
	{
		// Token: 0x04000480 RID: 1152
		ParticleSystem
	}
}
