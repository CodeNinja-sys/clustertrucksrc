using System;
using UnityEngine;

// Token: 0x02000116 RID: 278
public abstract class ExtraHandlerBase : MonoBehaviour
{
	// Token: 0x06000601 RID: 1537 RVA: 0x0002AED4 File Offset: 0x000290D4
	protected void Init()
	{
		this.mRdtExtra = base.GetComponent<RDTExtra>();
	}

	// Token: 0x06000602 RID: 1538
	public abstract void HandleExtra(Vector3 info);

	// Token: 0x06000603 RID: 1539
	protected abstract void SendExtra(Vector3 info);

	// Token: 0x04000468 RID: 1128
	protected RDTExtra mRdtExtra;
}
