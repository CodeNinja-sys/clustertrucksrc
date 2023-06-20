using System;
using UnityEngine;

// Token: 0x02000270 RID: 624
public class changeGravityTrigger : MonoBehaviour
{
	// Token: 0x06000F15 RID: 3861 RVA: 0x0006232C File Offset: 0x0006052C
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "car")
		{
			other.transform.parent.GetComponent<Gravity>().SetDirection(base.transform.forward);
		}
	}
}
