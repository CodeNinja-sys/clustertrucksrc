using System;
using UnityEngine;

// Token: 0x02000124 RID: 292
public class RDTPlayer : RecordingDataTransmitterTransform
{
	// Token: 0x06000644 RID: 1604 RVA: 0x0002BBD4 File Offset: 0x00029DD4
	protected override void Initialize()
	{
		base.Initialize();
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0002BBDC File Offset: 0x00029DDC
	private new void Start()
	{
		this.Initialize();
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0002BBE4 File Offset: 0x00029DE4
	protected override bool RegisterTransmitter()
	{
		return base.RegisterTransmitter();
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x0002BBEC File Offset: 0x00029DEC
	public override DataRecordingPackage GetDataRecordingPackage()
	{
		DataRecordingPackage dataRecordingPackage = base.GetDataRecordingPackage();
		Debug.Log("Recording " + dataRecordingPackage.mPosition);
		return dataRecordingPackage;
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x0002BC1C File Offset: 0x00029E1C
	private void Update()
	{
	}
}
