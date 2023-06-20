using System;
using UnityEngine;

// Token: 0x020001E4 RID: 484
public class CamDodge : MonoBehaviour
{
	// Token: 0x06000B68 RID: 2920 RVA: 0x00046F4C File Offset: 0x0004514C
	private void Start()
	{
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x00046F50 File Offset: 0x00045150
	private void Update()
	{
		base.transform.localPosition = Vector3.zero;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, base.transform.forward, out raycastHit, 1f))
		{
			base.transform.localPosition += -base.transform.forward * (1f - Vector3.Distance(base.transform.position, raycastHit.point));
		}
	}
}
