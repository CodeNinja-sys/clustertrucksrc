using System;
using UnityEngine;

// Token: 0x02000311 RID: 785
public class triggerDissable : MonoBehaviour
{
	// Token: 0x06001255 RID: 4693 RVA: 0x00075518 File Offset: 0x00073718
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Player")
		{
			this.dissable.SetActive(false);
		}
	}

	// Token: 0x04000FAB RID: 4011
	public GameObject dissable;
}
