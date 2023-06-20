using System;
using UnityEngine;

// Token: 0x02000125 RID: 293
public class RDTPrefab : RecordingDataTransmitterTransform
{
	// Token: 0x0600064A RID: 1610 RVA: 0x0002BC28 File Offset: 0x00029E28
	protected override bool RegisterTransmitter()
	{
		return true;
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0002BC2C File Offset: 0x00029E2C
	public override void EndOfHistory()
	{
		ParticleSystem[] componentsInChildren = base.gameObject.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem particleSystem in componentsInChildren)
		{
			particleSystem.emission.enabled = false;
		}
		base.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
		UnityEngine.Object.Instantiate(Resources.Load("PlayerGhostExplosion"), base.transform.position, Quaternion.identity);
	}
}
