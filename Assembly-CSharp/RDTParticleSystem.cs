using System;
using UnityEngine;

// Token: 0x02000123 RID: 291
public class RDTParticleSystem : RecordingDataTransmitterTransform
{
	// Token: 0x0600063D RID: 1597 RVA: 0x0002BB34 File Offset: 0x00029D34
	protected override void Initialize()
	{
		base.Initialize();
		this.mParticleSystem = base.GetComponent<ParticleSystem>();
		if (this.mParticleSystem == null)
		{
			Debug.Log("didn't find particle system", base.gameObject);
		}
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0002BB74 File Offset: 0x00029D74
	protected override bool RegisterTransmitter()
	{
		this.mRecordingPosition = Singleton<DataRecorder>.Instance.RegisterTransmitter(base.GetComponent<RDTParticleSystem>());
		return true;
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x0002BB90 File Offset: 0x00029D90
	public override void ApplyDataPackage(DataRecordingPackage lastDataPackage, DataRecordingPackage dataPackage, float interpolate)
	{
		base.ApplyDataPackage(lastDataPackage, dataPackage, interpolate);
		if (lastDataPackage.mAddon)
		{
		}
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0002BBA8 File Offset: 0x00029DA8
	public override DataRecordingPackage GetDataRecordingPackage()
	{
		return base.GetDataRecordingPackage();
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x0002BBC0 File Offset: 0x00029DC0
	private new void Start()
	{
		this.Initialize();
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0002BBC8 File Offset: 0x00029DC8
	private void Update()
	{
	}

	// Token: 0x04000486 RID: 1158
	private ParticleSystem mParticleSystem;
}
