using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Image Effects/Amplify Color Volume")]
public class AmplifyColorVolume : AmplifyColorVolumeBase
{
	// Token: 0x06000030 RID: 48 RVA: 0x00003A90 File Offset: 0x00001C90
	private void OnTriggerEnter(Collider other)
	{
		AmplifyColorTriggerProxy component = other.GetComponent<AmplifyColorTriggerProxy>();
		if (component != null && component.OwnerEffect.UseVolumes && (component.OwnerEffect.VolumeCollisionMask & 1 << base.gameObject.layer) != 0)
		{
			component.OwnerEffect.EnterVolume(this);
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00003AF4 File Offset: 0x00001CF4
	private void OnTriggerExit(Collider other)
	{
		AmplifyColorTriggerProxy component = other.GetComponent<AmplifyColorTriggerProxy>();
		if (component != null && component.OwnerEffect.UseVolumes && (component.OwnerEffect.VolumeCollisionMask & 1 << base.gameObject.layer) != 0)
		{
			component.OwnerEffect.ExitVolume(this);
		}
	}
}
