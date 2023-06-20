using System;
using UnityEngine;

// Token: 0x02000127 RID: 295
public class RDTransmitterPillarPiece : RecordingDataTransmitterRigid
{
	// Token: 0x06000657 RID: 1623 RVA: 0x0002BE48 File Offset: 0x0002A048
	public void RegisterPeiceExplosion()
	{
		this.mExploded = true;
		this.mDirty = true;
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x0002BE58 File Offset: 0x0002A058
	protected override void Initialize()
	{
		base.Initialize();
		this.mDestruPiece = base.GetComponent<destructivePiece>();
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0002BE6C File Offset: 0x0002A06C
	private new void Start()
	{
		this.Initialize();
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x0002BE74 File Offset: 0x0002A074
	protected override bool RegisterTransmitter()
	{
		this.mRecordingPosition = Singleton<DataRecorder>.Instance.RegisterTransmitter(base.GetComponent<RDTransmitterPillarPiece>());
		return true;
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x0002BE90 File Offset: 0x0002A090
	private void UnExplodePiece()
	{
		if (this.mHaveUnExploded)
		{
			return;
		}
		this.mHaveUnExploded = true;
		this.mHaveExploded = false;
		if (this.mDestruPiece == null)
		{
			this.mDestruPiece = base.GetComponent<destructivePiece>();
		}
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x0002BECC File Offset: 0x0002A0CC
	private void PieceExploded()
	{
		if (this.mHaveExploded)
		{
			return;
		}
		this.mHaveExploded = true;
		if (this.mDestruPiece == null)
		{
			this.mDestruPiece = base.GetComponent<destructivePiece>();
		}
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x0002BF0C File Offset: 0x0002A10C
	private void HandleExtra()
	{
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x0002BF10 File Offset: 0x0002A110
	public override void ApplyDataPackage(DataRecordingPackage lastDataPackage, DataRecordingPackage dataPackage, float interpolate)
	{
		base.ApplyDataPackage(lastDataPackage, dataPackage, interpolate);
		if (lastDataPackage.mAddon)
		{
			this.HandleExtra();
		}
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0002BF2C File Offset: 0x0002A12C
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
			this.mSentExplosion = true;
			MonoBehaviour.print(string.Concat(new object[]
			{
				base.gameObject.name,
				" ",
				base.gameObject.GetInstanceID(),
				" sent explosion PEICE package"
			}));
		}
		this.mExploded = false;
		return dataRecordingPackage;
	}

	// Token: 0x0400048D RID: 1165
	private bool mSentExplosion;

	// Token: 0x0400048E RID: 1166
	private bool mExploded;

	// Token: 0x0400048F RID: 1167
	private Vector3 mExplodedPos;

	// Token: 0x04000490 RID: 1168
	private bool mHaveExploded;

	// Token: 0x04000491 RID: 1169
	private bool mHaveUnExploded;

	// Token: 0x04000492 RID: 1170
	private destructivePiece mDestruPiece;
}
