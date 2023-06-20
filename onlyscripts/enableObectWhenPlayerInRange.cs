using System;
using UnityEngine;

// Token: 0x020002F1 RID: 753
public class enableObectWhenPlayerInRange : MonoBehaviour
{
	// Token: 0x0600119F RID: 4511 RVA: 0x00071C10 File Offset: 0x0006FE10
	private void Start()
	{
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x00071C14 File Offset: 0x0006FE14
	private void Update()
	{
		if (this.p)
		{
			if (Vector3.Distance(base.transform.position, this.p.position) < this.range)
			{
				this.activate.SetActive(true);
				base.enabled = false;
			}
		}
		else
		{
			this.p = UnityEngine.Object.FindObjectOfType<player>().transform;
		}
	}

	// Token: 0x04000EBF RID: 3775
	public float range = 300f;

	// Token: 0x04000EC0 RID: 3776
	private Transform p;

	// Token: 0x04000EC1 RID: 3777
	public GameObject activate;
}
