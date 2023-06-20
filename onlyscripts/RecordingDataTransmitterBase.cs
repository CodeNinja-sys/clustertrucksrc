using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200012E RID: 302
public abstract class RecordingDataTransmitterBase : MonoBehaviour
{
	// Token: 0x0600067F RID: 1663
	public abstract DataRecordingPackage GetDataRecordingPackage();

	// Token: 0x06000680 RID: 1664
	public abstract DataRecordingPackage ForceGetDataRecordingPackage();

	// Token: 0x06000681 RID: 1665
	public abstract void ApplyDataPackage(DataRecordingPackage lastDataPackage, DataRecordingPackage dataPackage, float interpolate);

	// Token: 0x06000682 RID: 1666
	public abstract void RecordExtraPackage();

	// Token: 0x06000683 RID: 1667
	public abstract void StopPlayBack();

	// Token: 0x06000684 RID: 1668
	public abstract void StartPlayBack();

	// Token: 0x06000685 RID: 1669
	public abstract void EndOfHistory();

	// Token: 0x06000686 RID: 1670
	protected abstract bool RegisterTransmitter();

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x06000687 RID: 1671 RVA: 0x0002C49C File Offset: 0x0002A69C
	// (set) Token: 0x06000688 RID: 1672 RVA: 0x0002C4A4 File Offset: 0x0002A6A4
	public string P_PrefabName
	{
		get
		{
			return this.mPrefabToSpawn;
		}
		set
		{
			this.mPrefabToSpawn = value;
		}
	}

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x06000689 RID: 1673 RVA: 0x0002C4B0 File Offset: 0x0002A6B0
	public bool P_Dirty
	{
		get
		{
			return this.mDirty;
		}
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0002C4B8 File Offset: 0x0002A6B8
	protected virtual void Initialize()
	{
		if (!this.mRegistred)
		{
			this.mRegistred = this.RegisterTransmitter();
		}
		base.StartCoroutine(this.ResetDirty());
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0002C4EC File Offset: 0x0002A6EC
	protected virtual void EndOfFrame()
	{
		this.mDirty = false;
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0002C4F8 File Offset: 0x0002A6F8
	private IEnumerator ResetDirty()
	{
		yield return new WaitForEndOfFrame();
		this.EndOfFrame();
		base.StartCoroutine(this.ResetDirty());
		yield break;
	}

	// Token: 0x040004A8 RID: 1192
	protected bool mRegistred;

	// Token: 0x040004A9 RID: 1193
	protected int mRecordingPosition;

	// Token: 0x040004AA RID: 1194
	protected bool mDirty;

	// Token: 0x040004AB RID: 1195
	[SerializeField]
	protected string mPrefabToSpawn = string.Empty;
}
