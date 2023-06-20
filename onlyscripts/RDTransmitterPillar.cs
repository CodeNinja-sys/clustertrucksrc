using System;
using UnityEngine;

// Token: 0x02000126 RID: 294
public sealed class RDTransmitterPillar : RecordingDataTransmitterRigid
{
	// Token: 0x0600064D RID: 1613 RVA: 0x0002BCA8 File Offset: 0x00029EA8
	public void RegisterPillarExplosion(Vector3 explosionPosition)
	{
		this.mExplodedPos = explosionPosition;
		this.mExploded = true;
		this.mDirty = true;
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0002BCC0 File Offset: 0x00029EC0
	protected override void Initialize()
	{
		base.Initialize();
		this.mDestructive = base.GetComponent<destructive>();
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0002BCD4 File Offset: 0x00029ED4
	private new void Start()
	{
		this.Initialize();
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0002BCDC File Offset: 0x00029EDC
	protected override bool RegisterTransmitter()
	{
		this.mRecordingPosition = Singleton<DataRecorder>.Instance.RegisterTransmitter(base.GetComponent<RDTransmitterPillar>());
		return true;
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x0002BCF8 File Offset: 0x00029EF8
	private void PillarUnExpload()
	{
		if (this.mHaveUnExplodedOnce)
		{
			return;
		}
		this.mHaveExplodedOnce = false;
		this.mHaveUnExplodedOnce = true;
		if (this.mDestructive == null)
		{
			this.mDestructive = base.GetComponent<destructive>();
		}
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x0002BD34 File Offset: 0x00029F34
	private void PillarExploded(Vector3 pos)
	{
		if (this.mHaveExplodedOnce)
		{
			return;
		}
		this.mHaveExplodedOnce = true;
		this.mHaveUnExplodedOnce = false;
		if (this.mDestructive == null)
		{
			this.mDestructive = base.GetComponent<destructive>();
		}
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x0002BD70 File Offset: 0x00029F70
	private void HandleExtra(Vector3 pos)
	{
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x0002BD74 File Offset: 0x00029F74
	public override void ApplyDataPackage(DataRecordingPackage lastDataPackage, DataRecordingPackage dataPackage, float interpolate)
	{
		base.ApplyDataPackage(lastDataPackage, dataPackage, interpolate);
		if (lastDataPackage.mAddon)
		{
			this.HandleExtra(lastDataPackage.mExtra);
		}
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x0002BD9C File Offset: 0x00029F9C
	public override DataRecordingPackage GetDataRecordingPackage()
	{
		DataRecordingPackage dataRecordingPackage = base.GetDataRecordingPackage();
		if (!base.P_Dirty)
		{
			return dataRecordingPackage;
		}
		dataRecordingPackage.mAddon = this.mExploded;
		if (this.mExploded && !this.mSentExplosion)
		{
			dataRecordingPackage.mExtra = this.mExplodedPos;
			this.mSentExplosion = true;
			MonoBehaviour.print(string.Concat(new object[]
			{
				base.gameObject.name,
				" ",
				base.gameObject.GetInstanceID(),
				" sent explosion package"
			}));
		}
		this.mExploded = false;
		return dataRecordingPackage;
	}

	// Token: 0x04000487 RID: 1159
	private bool mExploded;

	// Token: 0x04000488 RID: 1160
	private bool mSentExplosion;

	// Token: 0x04000489 RID: 1161
	private Vector3 mExplodedPos;

	// Token: 0x0400048A RID: 1162
	private bool mHaveExplodedOnce;

	// Token: 0x0400048B RID: 1163
	private bool mHaveUnExplodedOnce;

	// Token: 0x0400048C RID: 1164
	private destructive mDestructive;
}
