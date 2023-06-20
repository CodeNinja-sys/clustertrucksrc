using System;
using UnityEngine;

// Token: 0x02000128 RID: 296
[RequireComponent(typeof(carCheckDamage))]
public sealed class RDTransmitterTruck : RecordingDataTransmitterRigid
{
	// Token: 0x06000661 RID: 1633 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
	public void TruckExploded()
	{
		this.mTruckExploded = true;
		this.mDirty = true;
		this.RecordExtraPackage();
		this.mTruckExploded = false;
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x0002BFE8 File Offset: 0x0002A1E8
	private new void Start()
	{
		this.Initialize();
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x0002BFF0 File Offset: 0x0002A1F0
	protected override void Initialize()
	{
		base.Initialize();
		this.mCarDamage = base.GetComponent<carCheckDamage>();
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x0002C004 File Offset: 0x0002A204
	protected override bool RegisterTransmitter()
	{
		this.mRecordingPosition = Singleton<DataRecorder>.Instance.RegisterTransmitter(base.GetComponent<RDTransmitterTruck>());
		return true;
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x0002C020 File Offset: 0x0002A220
	private void ExplodeTruck()
	{
		if (this.mExplodeOnce)
		{
			return;
		}
		this.mExplodeOnce = true;
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x0002C038 File Offset: 0x0002A238
	public override DataRecordingPackage GetDataRecordingPackage()
	{
		DataRecordingPackage dataRecordingPackage = base.GetDataRecordingPackage();
		if (!base.P_Dirty)
		{
			return dataRecordingPackage;
		}
		dataRecordingPackage.mAddon = this.mTruckExploded;
		return dataRecordingPackage;
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x0002C068 File Offset: 0x0002A268
	public override void ApplyDataPackage(DataRecordingPackage lastDataPackage, DataRecordingPackage dataPackage, float interpolate)
	{
		base.ApplyDataPackage(lastDataPackage, dataPackage, interpolate);
		if (lastDataPackage.mAddon)
		{
			this.ExplodeTruck();
		}
		if (interpolate > 0.2f && dataPackage.mAddon)
		{
			this.ExplodeTruck();
		}
	}

	// Token: 0x04000493 RID: 1171
	private carCheckDamage mCarDamage;

	// Token: 0x04000494 RID: 1172
	private bool mTruckExploded;

	// Token: 0x04000495 RID: 1173
	private bool mExplodeOnce;
}
