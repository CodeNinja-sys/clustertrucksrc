using System;
using UnityEngine;

// Token: 0x02000288 RID: 648
public class killTrigger : MonoBehaviour
{
	// Token: 0x06000F9C RID: 3996 RVA: 0x000658D4 File Offset: 0x00063AD4
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<player>().Die(this.deathType);
		}
		if (other.tag == "car" && this.killTrucks)
		{
			car component = other.transform.root.GetComponent<car>();
			if (component.mainRig)
			{
				component.mainRig.GetComponent<carCheckDamage>().Explode();
			}
			if (component.secondRig)
			{
				component.secondRig.GetComponent<carCheckDamage>().Explode();
			}
		}
	}

	// Token: 0x04000C7E RID: 3198
	public bool killTrucks;

	// Token: 0x04000C7F RID: 3199
	public int deathType;
}
